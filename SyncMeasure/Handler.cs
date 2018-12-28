﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using RDotNet;
using SyncMeasure.Properties;


namespace SyncMeasure
{
    public class Handler
    {
        private readonly REngine _engine;
        private readonly Dictionary<string, double> _weights;   // weight of sync properties.
        private readonly Dictionary<string, string> _colNames;  // csv file column names. (to be read with R).
        private List<Frame> _frames;                            // The parsed data.
        private EFormat _format;                                // format being used.
        private ECvv _cvvMethod;
        private string _graphic;
        private int _timeLag;                                   // Time lag [ms] for person 1 with respect to person 0. 

        public enum EGraphics
        {
            POINTS,
            BOTH,
            LINES
        }

        public enum EFormat
        {
            OLD,
            NEW
        };

        public enum ECvv
        {
            CVV,
            ABS_CVV,
            SQUARE_CVV
        };

        public Handler(out ResultStatus resultStatus)
        {
            /* [R] initialization */
            try
            {
                REngine.SetEnvironmentVariables();
                _engine = REngine.GetInstance();
                resultStatus = LoadRPackages();         
            }
            catch (Exception)
            {
                resultStatus = new ResultStatus(false, @"RENGINE");
            }

            /* .NET initialization */
            _weights = new Dictionary<string, double>
            {
                {Resources.ARM, 0.3}, // Arm's CVV weight.
                {Resources.ELBOW, 0.3}, // Elbow's CVV weight.
                {Resources.HAND, 0.3}, // Hand's CVV weight.
                {Resources.GRAB, 0.05}, // Grab weight.
                {Resources.PINCH, 0.05} // Pinch weight.
                //{Resources.GESTURE, 0} // Gesture's weight.
            };
            _colNames = new Dictionary<string, string>
            {
                {Resources.TIMESTAMP, ""},
                {Resources.FRAME_ID, ""},
                {Resources.HAND_ID, ""},
                {Resources.HANDS_IN_FRAME, ""},
                {Resources.HAND_TYPE, ""},
                {Resources.HAND_POS_X, ""},
                {Resources.HAND_POS_Y, ""},
                {Resources.HAND_POS_Z, ""},
                {Resources.HAND_VEL_X, ""},
                {Resources.HAND_VEL_Y, ""},
                {Resources.HAND_VEL_Z, ""},
                {Resources.PITCH, ""},
                {Resources.ROLL, ""},
                {Resources.YAW, ""},
                {Resources.GRAB_STRENGTH, ""},
                {Resources.PINCH_STRENGTH, ""},
                {Resources.ARM_POS_X, ""},
                {Resources.ARM_POS_Y, ""},
                {Resources.ARM_POS_Z, ""},
                {Resources.ELBOW_POS_X, ""},
                {Resources.ELBOW_POS_Y, ""},
                {Resources.ELBOW_POS_Z, ""},
                {Resources.GRAB_ANGLE, ""}
            };
            _cvvMethod = ECvv.CVV;
            _graphic = "b";
            _timeLag = 0;
            SetWeight(0.3, 0.3, 0.3, 0.05, 0.05, out _);      // Default weight initialization 
            SetNewFormatDefaults();
            LoadUserSettings();
        }

        ~Handler()
        {
            /* Try delete saved graphs */
            try { File.Delete(Resources.ARM_CVV_GRAPH); } catch (Exception) {/*ignored*/}
            try { File.Delete(Resources.ELBOW_CVV_GRAPH); } catch (Exception) {/*ignored*/}
            try { File.Delete(Resources.HAND_CVV_GRAPH); } catch (Exception) {/*ignored*/}
            try { File.Delete(Resources.GRAB_GRAPH); } catch (Exception) {/*ignored*/}
            try { File.Delete(Resources.PINCH_GRAPH); } catch (Exception) {/*ignored*/}
            try { File.Delete(Resources.ALL_GRAPH); } catch (Exception) {/*ignored*/}

            _engine.Dispose();
        }

        private ResultStatus LoadRPackages()
        {
            /* Load packages */
            if (_engine.Evaluate("require(cvv)").AsInteger()[0] == 0)
            {
                return new ResultStatus(false,
                    @"cvv package couldn't be found. Please install by running the following commands in R:" +
                    Environment.NewLine + @"install.packages('devtools')" +
                    Environment.NewLine +
                    @"library(devtools)" + Environment.NewLine +
                    @"install_github('reissphil/cvv')");
            }

            if (_engine.Evaluate("require(data.table)").AsInteger()[0] == 0)
            {
                return new ResultStatus(false,
                    @"data.table package couldn't be found. Please install by running the following commands in R:" +
                    Environment.NewLine + @"install.packages('data.table')");
            }

            /*  ToDO: Enable parallel calculations?
            if (_engine.Evaluate("require(future)").AsInteger()[0] == 0)
            {
                return new ResultStatus(false,
                    @"future package couldn't be found. Please install by running the following commands in R:" +
                    Environment.NewLine + @"install.packages('future')");
            }
            */
            return new ResultStatus(true, "");
        }

        public void RemoveFromR(string toRemove)
        {
            try
            {
                _engine.Evaluate("remove(" + toRemove + ")");
            }
            catch (Exception)
            {
                // Don't care
            }
        }

        public void RemoveFromR(params string[] toRemove)
        {
            foreach (var r in toRemove)
            {
                RemoveFromR(r);
            }
        }

        private void RemovePosFromR(string posName)
        {
            RemoveFromR(posName + ".pos.x");
            RemoveFromR(posName + ".pos.y");
            RemoveFromR(posName + ".pos.z");
        }

        private void RemovePosFromR(params string[] posNames)
        {
            foreach (var posName in posNames)
            {
                RemovePosFromR(posName);
            }
        }

        public DynamicVector GetRSymbolAsVector(string symbol)
        {
            return _engine.GetSymbol(symbol).AsVector();
        }

        /// <summary>
        /// Report progress and tells if needs to cancel work.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="bgWorker"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool ReportProgress(int progress, BackgroundWorker bgWorker, DoWorkEventArgs args)
        {
            if (bgWorker.CancellationPending)
            {
                args.Cancel = true;
                return true;
            }
            bgWorker.ReportProgress(progress);
            return false;
        }

        /// <summary>
        /// positions data.
        /// index 0 = first hand.
        /// index 1 = second hand.
        /// each cell contains list of coordinates.
        /// </summary>
        /// <returns></returns>
        private List<double>[][] GetSyncArray()
        {
            return new[]
            {
                new[] {new List<double>(), new List<double>(), new List<double>()},
                new[] {new List<double>(), new List<double>(), new List<double>()}
            };
        }

        /// <summary>
        /// positions data.
        /// index 0 = first hand.
        /// index 1 = second hand.
        /// each cell contains list of grab / pinch strength.
        /// </summary>
        /// <returns></returns>
        private List<double>[] GetStrengthArray()
        {
            return new[]
            {
                new List<double>(),
                new List<double>()
            };
        }

        /// <summary>
        /// Measure Synchronization from loaded DataFrame.
        /// </summary>
        /// <param name="bgWorker"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResultStatus MeasureSynchronization(BackgroundWorker bgWorker, DoWorkEventArgs args)
        {
            try
            {
                /* prepare objects for R parsing */
                var timestamps = new List<int>();
                var armsPos = GetSyncArray();
                var handsPos = GetSyncArray();
                var elbowsPos = GetSyncArray();
                var grabStrength = GetStrengthArray();
                var pinchStrength = GetStrengthArray();

                foreach (var frame in _frames)
                {
                    timestamps.Add(frame.Timestamp);
                    for (var i = 0; i < 2; ++i) // do for both hands.
                    {
                        armsPos[i][0].Add(frame.Hands[i].ArmPosition.X);
                        armsPos[i][1].Add(frame.Hands[i].ArmPosition.Y);
                        armsPos[i][2].Add(frame.Hands[i].ArmPosition.Z);

                        elbowsPos[i][0].Add(frame.Hands[i].ElbowPosition.X);
                        elbowsPos[i][1].Add(frame.Hands[i].ElbowPosition.Y);
                        elbowsPos[i][2].Add(frame.Hands[i].ElbowPosition.Z);

                        handsPos[i][0].Add(frame.Hands[i].Position.X);
                        handsPos[i][1].Add(frame.Hands[i].Position.Y);
                        handsPos[i][2].Add(frame.Hands[i].Position.Z);

                        grabStrength[i].Add(frame.Hands[i].GrabStrength);
                        pinchStrength[i].Add(frame.Hands[i].PinchStrength);
                    }
                }
                
                _engine.SetSymbol("timestamps", _engine.CreateIntegerVector(timestamps));
                _engine.Evaluate("minTime <- timestamps[1]");
                _engine.Evaluate("maxTime <- timestamps[length(timestamps)]");
                _engine.Evaluate("laggedTs <- timestamps + " + _timeLag);           // to be used with grab/pinch
                _engine.Evaluate("laggedTs <- replace(laggedTs, laggedTs < minTime, NA)");
                _engine.Evaluate("laggedTs <- replace(laggedTs, laggedTs > maxTime, NA)");
                _engine.Evaluate("omittedTs <- as.integer(na.omit(laggedTs))");     // to be used with cvv
                RemoveFromR("minTime", "maxTime");

                for (var i = 0; i < 2; ++i)     // for both hands.
                {
                    _engine.SetSymbol("hand" + i + ".pos.x", _engine.CreateNumericVector(handsPos[i][0]));
                    _engine.SetSymbol("hand" + i + ".pos.y", _engine.CreateNumericVector(handsPos[i][1]));
                    _engine.SetSymbol("hand" + i + ".pos.z", _engine.CreateNumericVector(handsPos[i][2]));

                    _engine.SetSymbol("arm" + i + ".pos.x", _engine.CreateNumericVector(armsPos[i][0]));
                    _engine.SetSymbol("arm" + i + ".pos.y", _engine.CreateNumericVector(armsPos[i][1]));
                    _engine.SetSymbol("arm" + i + ".pos.z", _engine.CreateNumericVector(armsPos[i][2]));

                    _engine.SetSymbol("elbow" + i + ".pos.x", _engine.CreateNumericVector(elbowsPos[i][0]));
                    _engine.SetSymbol("elbow" + i + ".pos.y", _engine.CreateNumericVector(elbowsPos[i][1]));
                    _engine.SetSymbol("elbow" + i + ".pos.z", _engine.CreateNumericVector(elbowsPos[i][2]));

                    _engine.SetSymbol("grab" + i, _engine.CreateNumericVector(grabStrength[i]));
                    _engine.SetSymbol("pinch" + i, _engine.CreateNumericVector(pinchStrength[i]));
                }

                /* Build grab and pinch */
                _engine.Evaluate("grab <- 1 - abs(grab1 - grab0)");
                _engine.Evaluate("pinch <- 1 - abs(pinch1 - pinch0)");

                if (ReportProgress(20, bgWorker, args)) return null;

                var cvvCommand = " cosfunc(make.fd(timestamps, pos_1, pos_2), lag = " + (_timeLag / 1000.0) + ")";
                string assign = " <- ";
                // ToDo: Enable parallel calculations?
                // _engine.Evaluate("plan(multiprocess)");
                // assign = "%<-%";        // parallel assign.  
                /* Measure Arm, Elbow and Hand cvv */

                // Hand
                _engine.Evaluate("pos_1 <- cbind(hand0.pos.x, hand0.pos.y, hand0.pos.z)");
                _engine.Evaluate("pos_2 <- cbind(hand1.pos.x, hand1.pos.y, hand1.pos.z)");
                _engine.Evaluate("hand.cvv" + assign + cvvCommand);

                if (ReportProgress(40, bgWorker, args)) return null;

                // Arm
                _engine.Evaluate("pos_1 <- cbind(arm0.pos.x, arm0.pos.y, arm0.pos.z)");
                _engine.Evaluate("pos_2 <- cbind(arm1.pos.x, arm1.pos.y, arm1.pos.z)");                
                _engine.Evaluate("arm.cvv" + assign + cvvCommand);

                if (ReportProgress(60, bgWorker, args)) return null;

                // Elbow
                _engine.Evaluate("pos_1 <- cbind(elbow0.pos.x, elbow0.pos.y, elbow0.pos.z)");
                _engine.Evaluate("pos_2 <- cbind(elbow1.pos.x, elbow1.pos.y, elbow1.pos.z)");
                _engine.Evaluate("elbow.cvv" + assign + cvvCommand);

                if (ReportProgress(80, bgWorker, args)) return null;

                _engine.Evaluate("hcvv <- hand.cvv(omittedTs)");
                _engine.Evaluate("acvv <- arm.cvv(omittedTs)");
                _engine.Evaluate("ecvv <- elbow.cvv(omittedTs)");
                if (_cvvMethod == ECvv.ABS_CVV)
                {
                    _engine.Evaluate("hcvv <- abs(hcvv)");
                    _engine.Evaluate("acvv <- abs(acvv)");
                    _engine.Evaluate("ecvv <- abs(ecvv)");
                }
                else if (_cvvMethod == ECvv.SQUARE_CVV)
                {
                    _engine.Evaluate("hcvv <- '^'(hcvv, 2)");
                    _engine.Evaluate("acvv <- '^'(acvv, 2)");
                    _engine.Evaluate("ecvv <- '^'(ecvv, 2)");
                }

                _engine.Evaluate("ymin <- min(hcvv, acvv, ecvv)");
                _engine.Evaluate("ymax <- max(hcvv, acvv, ecvv)");
                _engine.Evaluate("weight.func <- function(h,a,e,g,p) { " +
                                 "return(h*" + _weights[Resources.HAND] +
                                 " + a*" + _weights[Resources.ARM] +
                                 " + e*" + _weights[Resources.ELBOW] +
                                 " + g*" + _weights[Resources.GRAB] +
                                 " + p*" + _weights[Resources.PINCH] + ") }");
                _engine.Evaluate("weighted <- weight.func(hcvv, acvv, ecvv, grab, pinch)");
                _engine.Evaluate("avg.weighted <- mean(weighted)");
                _engine.Evaluate("avg.hands.cvv <- mean(hcvv)");
                _engine.Evaluate("avg.arms.cvv <- mean(acvv)");
                _engine.Evaluate("avg.elbows.cvv <- mean(ecvv)");
                _engine.Evaluate("avg.grab <- mean(grab)");
                _engine.Evaluate("avg.pinch <- mean(pinch)");


                if (ReportProgress(90, bgWorker, args)) return null;

                string ylim = "ylim=c(0,1)";
                if (_cvvMethod == ECvv.CVV)
                {
                    ylim = "ylim=c(-1,1)";
                }

                /* Plot all cvv in one graph */
                var allGraph = Path.GetFullPath(Resources.ALL_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("allGraph", _engine.CreateCharacterVector(new[] {allGraph}));
                _engine.Evaluate("png(filename=allGraph)");
                _engine.Evaluate("plot(x = laggedTs, y = weighted, type = '" + _graphic + "', main = 'Average', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'Avg'," + ylim + ", pch = 20)");
                _engine.Evaluate("dev.off()");

                /* Plot hand cvv */
                var handsGraph = Path.GetFullPath(Resources.HAND_CVV_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("handsGraph", _engine.CreateCharacterVector(new[] {handsGraph}));
                _engine.Evaluate("png(filename=handsGraph)");
                _engine.Evaluate("plot(x = omittedTs, y = hcvv, type = '" + _graphic + "', main = 'Hands cvv = f(Timestamp)', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'hands cvv', col = 'red'," + ylim + ", pch = 20)");
                _engine.Evaluate("dev.off()");

                /* Plot arm cvv */
                var armsGraph = Path.GetFullPath(Resources.ARM_CVV_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("armsGraph", _engine.CreateCharacterVector(new[] {armsGraph}));
                _engine.Evaluate("png(filename=armsGraph)");
                _engine.Evaluate("plot(x = omittedTs, y = acvv, type = '" + _graphic + "', main = 'arm cvv = f(Timestamp)', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'arms cvv', col = 'green'," + ylim + ", pch = 20)");
                _engine.Evaluate("dev.off()");

                /* Plot elbow cvv */
                var elbowGraph = Path.GetFullPath(Resources.ELBOW_CVV_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("elbowsGraph", _engine.CreateCharacterVector(new[] {elbowGraph}));
                _engine.Evaluate("png(filename=elbowsGraph)");
                _engine.Evaluate("plot(x = omittedTs, y = ecvv, type = '" + _graphic +   "', main = 'elbow cvv = f(Timestamp)', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'elbows cvv', col = 'blue'," + ylim + ", pch = 20)");
                _engine.Evaluate("dev.off()");

                /* Plot Grab Strength */
                var grabGraph = Path.GetFullPath(Resources.GRAB_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("grabGraph", _engine.CreateCharacterVector(new[] {grabGraph}));
                _engine.Evaluate("png(filename=grabGraph)");
                _engine.Evaluate("plot(x = laggedTs, y = grab, type = '" + _graphic + "', main = 'Grab Strength synchrony', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'grab strength diff',ylim=c(0,1), pch = 20)");
                _engine.Evaluate("dev.off()");

                /* Plot Pinch Strength */
                var pinchGraph = Path.GetFullPath(Resources.PINCH_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("pinchGraph", _engine.CreateCharacterVector(new[] {pinchGraph}));
                _engine.Evaluate("png(filename=pinchGraph)");
                _engine.Evaluate("plot(x = laggedTs, y = pinch, type = '" + _graphic + "', main = 'Pinch Strength synchrony', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'pinch strength diff',ylim=c(0,1), pch = 20)");
                _engine.Evaluate("dev.off()");


                _engine.Evaluate("grab <- mean(grab)");
                _engine.Evaluate("pinch <- mean(pinch)");

                // release resources.
                RemovePosFromR("hand0", "hand1", "arm0", "arm1", "elbow0", "elbow1");
                RemoveFromR("pos_1", "pos_2", "hcvv", "acvv", "ecvv", "ymin", "hand.cvv", "arm.cvv", "elbow.cvv",
                    "grab0", "grab1", "pinch0", "pinch1", "weighted", "weight.func", "laggedTs", "timestamps", "omittedTs");

                if (ReportProgress(100, bgWorker, args)) return null;
                var errorMessage = "";
                return new ResultStatus(true, errorMessage);
            }
            catch (Exception e)
            {
                return new ResultStatus(false, e.Message);
            }
        }

        /// <summary>
        /// Load a file to R Environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private ResultStatus LoadFileToR(string name, string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new ResultStatus(false, @"File doesn't exists!");
            }

            try
            {
                /* Load csv file to memory. */
                filePath = filePath.Replace("\\", "/"); // fix for [R].
                var cmd = name + " <- fread(file=file.path('" + filePath + "'), " +
                          "header = T, sep = ',', dec='.', stringsAsFactors = F, verbose = F, " +
                          "check.names = T, blank.lines.skip = T,  encoding = 'UTF-8')";
                _engine.Evaluate(cmd);

                /* Validate column names */
                var colNames = _engine.Evaluate("colnames(" + name + ")").AsVector();

                foreach (var cName in (GeMandatoryColumnNames()))
                {
                    if (!colNames.Contains(cName))
                    {
                        var errorMessage = @"Provided file doesn't contain column name: " + cName +
                                           Environment.NewLine +
                                           @"Try changing Default _format in settings or rename column name.";
                        return new ResultStatus(false, errorMessage);
                    }
                }

                /* Align timestamps - timestamp(i) = timestamp(i) - timestamp(0) */
                _engine.Evaluate(name + "[," + _colNames[Resources.TIMESTAMP] + " := " + name + "[," +
                                 _colNames[Resources.TIMESTAMP] + "] - " + name + "[1," +
                                 _colNames[Resources.TIMESTAMP] + "]]");

                return new ResultStatus(true, "");
            }
            catch (Exception)
            {
                return new ResultStatus(false, @"Failed to load file " + Path.GetFileNameWithoutExtension(filePath));
            }
        }

        /// <summary>
        /// Load an "Alone" file to R Environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ResultStatus LoadAloneFileToR(string name, string filePath)
        {
            var res = LoadFileToR(name, filePath);
            if (!res.Status) return res;

            /* Filter non single hand frames */
            _engine.Evaluate(name + " <- " + name + "[" + name + "$" + _colNames[Resources.HANDS_IN_FRAME] + " == 1,]");
            var size = _engine.Evaluate("nrow(" + name + ")").AsInteger()[0];
            if (size <= 0)
            {
                RemoveFromR(name);
                return new ResultStatus(false, @"File loaded doesn't contain frames with single hand!");
            }
            return new ResultStatus(true, "");
        }

        /// <summary>
        /// Parse csv output file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="bgWorker"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResultStatus LoadCsvFile(string filePath, BackgroundWorker bgWorker, DoWorkEventArgs args)
        {
            var resultStatus = LoadFileToR("dataset", filePath);
            if (!resultStatus.Status) return resultStatus;      // if loading to R failed.

            try
            {
                /* filter only the frames with two hands */
                _engine.Evaluate("dataset <- dataset[dataset$" + _colNames[Resources.HANDS_IN_FRAME] + " == 2,]");

                // retrieve the data frame
                var dataFrame = _engine.GetSymbol("dataset").AsDataFrame();
                
                if (dataFrame == null)
                {
                    return new ResultStatus(false, @"Failed to load '" + Path.GetFileName(filePath));
                }

                string errorMessage;
                if (dataFrame.RowCount == 0)
                {
                    errorMessage = @"Only one hand detected in frames. File might be representing 'Alone Mode'.";
                    errorMessage += @" Please load Autonomous or Sync mode file.";
                    return new ResultStatus(false, errorMessage);
                }

                var frames = new List<Frame>();
                int firstHandId = -1;
                int secondHandId = -1;
                for (var i = 0; i < dataFrame.RowCount - 1; i = i + 2)
                {
                    var percent = (double) i / dataFrame.RowCount * 100;
                    if (ReportProgress((int)percent, bgWorker, args)) return null;

                    var hand1 = GetHand(dataFrame, i, out errorMessage);
                    var hand2 = GetHand(dataFrame, i + 1, out errorMessage);
                    if (hand1 == null || hand2 == null)
                    {
                        return new ResultStatus(false, errorMessage);
                    }

                    var fid1 = (int) dataFrame[i, _colNames[Resources.FRAME_ID]];
                    var fid2 = (int) dataFrame[i + 1, _colNames[Resources.FRAME_ID]];
                    if (fid1 != fid2)
                    {
                        errorMessage = @"Invalid frameIDs in single frame: " + fid1 + ", " + fid2;
                        return new ResultStatus(false, errorMessage);
                    }

                    /* Try to recognize hands that went out of frame */
                    if (firstHandId == -1 || secondHandId == -1)     // Initialize
                    {
                        firstHandId = hand1.Id;
                        secondHandId = hand2.Id;
                    }
                    else
                    {
                        if (firstHandId != hand1.Id && secondHandId != hand2.Id)
                        {
                            /* Both hands went out of frame at the same time */
                            var firstHandLeft = frames[frames.Count - 1].Hands[0].IsLeft;
                            var secondHandLeft = frames[frames.Count - 1].Hands[1].IsLeft;
                            if (firstHandLeft == secondHandLeft)
                            {
                                hand1.Id = firstHandId;
                                hand2.Id = secondHandId;
                            }
                            else
                            {
                                var sameType = (firstHandLeft == hand1.IsLeft);
                                hand1.Id = sameType ? firstHandId : secondHandId;
                                hand2.Id = sameType ? secondHandId : firstHandId;
                            }
                        }
                        else if (firstHandId != hand1.Id)     // only 1st hand went out of frame.
                        {
                            hand1.Id = firstHandId;           // Reconnecting ID is enough.
                        }
                        else  // only 2nd hand went out of frame.
                        {
                            hand2.Id = secondHandId;           // Reconnecting ID is enough.
                        }
                    }

                    int timestamp, ts1, ts2;
                    if (_format.Equals(EFormat.NEW))
                    {
                        ts1 = (int)(1000 * (double)dataFrame[i, _colNames[Resources.TIMESTAMP]]);
                        ts2 = (int)(1000 * (double)dataFrame[i + 1, _colNames[Resources.TIMESTAMP]]);
                    } 
                    else 
                    {
                        ts1 = (int)dataFrame[i, _colNames[Resources.TIMESTAMP]];
                        ts2 = (int)dataFrame[i + 1, _colNames[Resources.TIMESTAMP]];
                    }
                    
                    var diff = ts2 - ts1;
                    if (diff == 0)
                    {
                        timestamp = ts1;
                    }
                    else
                    {
                        timestamp = ts1 + diff / 2;
                    }

                    var frame = new Frame(fid1, new List<Hand> {hand1, hand2}, timestamp);
                    frames.Add(frame);
                }

                if (ReportProgress(99, bgWorker, args)) return null;
                _frames = frames;
                RemoveFromR("dataset");
                return new ResultStatus(true, "");
            }
            catch (Exception e)
            {
                return new ResultStatus(false, e.Message);
            }
        }

        /// <summary>
        /// Build Hand with given index and parsed member DataFrame.
        /// </summary>
        /// <param name="dataFrame"></param>
        /// <param name="index"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private Hand GetHand(DataFrame dataFrame, int index, out string errMsg)
        {
            try
            {
                var hand = new Hand
                {
                    FrameId = (int) dataFrame[index, _colNames[Resources.FRAME_ID]],
                    PinchStrength = (double) dataFrame[index, _colNames[Resources.PINCH_STRENGTH]],
                    GrabStrength = (double) dataFrame[index, _colNames[Resources.GRAB_STRENGTH]],
                    IsLeft = dataFrame[index, _colNames[Resources.HAND_TYPE]].ToString().ToLower().Contains("left")
                };

                switch (_format)
                {
                    case EFormat.NEW:
                        hand.Id = hand.IsLeft ? 0 : 1;
                        break;
                    case EFormat.OLD:
                        hand.Id = (int) dataFrame[index, _colNames[Resources.HAND_ID]];
                        break;
                }

                var x = (double) dataFrame[index, _colNames[Resources.ELBOW_POS_X]];
                var y = (double) dataFrame[index, _colNames[Resources.ELBOW_POS_Y]];
                var z = (double) dataFrame[index, _colNames[Resources.ELBOW_POS_Z]];
                var elbowPos = new Vector(x, y, z);

                x = (double) dataFrame[index, _colNames[Resources.ARM_POS_X]];
                y = (double) dataFrame[index, _colNames[Resources.ARM_POS_Y]];
                z = (double) dataFrame[index, _colNames[Resources.ARM_POS_Z]];
                var armPos = new Vector(x, y, z);

                x = (double) dataFrame[index, _colNames[Resources.HAND_POS_X]];
                y = (double) dataFrame[index, _colNames[Resources.HAND_POS_Y]];
                z = (double) dataFrame[index, _colNames[Resources.HAND_POS_Z]];
                var handPos = new Vector(x, y, z);

                hand.ElbowPosition = elbowPos;
                hand.Position = handPos;
                hand.ArmPosition = armPos;

                errMsg = "";
                return hand;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// Combine two alone files.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="firstFrameId"></param>
        /// <param name="secondFrameId"></param>
        /// <returns></returns>
        public ResultStatus CombineAloneFiles(string filePath, int firstFrameId, int secondFrameId)
        {
            var file1Exists = _engine.Evaluate("exists('file1')").AsLogical()[0];
            var file2Exists = _engine.Evaluate("exists('file2')").AsLogical()[0];
            if (!file1Exists || !file2Exists)
            {
                return new ResultStatus(false, @"Alone files are not loaded.");
            }

            try
            {
                /* Save reference to loaded files */
                _engine.Evaluate("dt1 <- file1");
                _engine.Evaluate("dt2 <- file2");

                /* Filter initial Frame Ids by the user */
                if (firstFrameId > 0)
                {
                    _engine.Evaluate("dt1 <- dt1[dt1$" + _colNames[Resources.FRAME_ID] + " >= " + firstFrameId + ",]");
                }

                if (secondFrameId > 0)
                {
                    _engine.Evaluate("dt2 <- dt2[dt2$" + _colNames[Resources.FRAME_ID] + " >= " + secondFrameId + ",]");
                }

                /* Make data frames same row size */
                _engine.Evaluate("size <- min(nrow(dt1), nrow(dt2))");
                var size = _engine.GetSymbol("size").AsInteger()[0];
                if (size == 0)
                {
                    return new ResultStatus(false,
                        "Failed to merge. A given file might be not 'Alone' or the initial frame id is invalid.");
                }

                /* Align rows */
                _engine.Evaluate("dt1 <- dt1[1:" + size + ",]");
                _engine.Evaluate("dt2 <- dt2[1:" + size + ",]");

                /* Combine rows alternately */
                _engine.Evaluate("combined <- rbind(dt1,dt2)[rep(seq_len(size),each=2)+c(0,size),]");

                /* Update number of hands */
                _engine.Evaluate("combined[, " + _colNames[Resources.HANDS_IN_FRAME] + " := 2]");

                /* Build frame ids */
                _engine.Evaluate("for (i in 1:nrow(combined)) combined[i, " + _colNames[Resources.FRAME_ID] +
                                 " := i / 2 + i %% 2]");

                filePath = filePath.Replace("\\", "/"); // fix for [R].
                _engine.Evaluate("fwrite(combined, file=file.path('" + filePath + "'), sep=',', quote = F, verbose = F)");
                RemoveFromR("dt1", "dt2", "size");
                return new ResultStatus(true, "");
            }
            catch (Exception e)
            {
                return new ResultStatus(false, @"Failed to save merged file:" + Environment.NewLine + e.Message);
            }
        }

        /************************************* USER SETTINGS ***************************************/
        public Dictionary<string, double> GetWeights()
        {
            return _weights;
        }

        public Dictionary<string, string> GetColNames()
        {
            return _colNames;
        }

        /// <summary>
        /// Get list of column names that are used by SyncMeasure.
        /// </summary>
        /// <returns></returns>
        private List<string> GeMandatoryColumnNames()
        {
            return new List<string>
            {
                _colNames[Resources.TIMESTAMP],
                _colNames[Resources.FRAME_ID],
                _colNames[Resources.HANDS_IN_FRAME],
                _colNames[Resources.HAND_POS_X],
                _colNames[Resources.HAND_POS_Y],
                _colNames[Resources.HAND_POS_Z],
                _colNames[Resources.GRAB_STRENGTH],
                _colNames[Resources.PINCH_STRENGTH],
                _colNames[Resources.ARM_POS_X],
                _colNames[Resources.ARM_POS_Y],
                _colNames[Resources.ARM_POS_Z],
                _colNames[Resources.ELBOW_POS_X],
                _colNames[Resources.ELBOW_POS_Y],
                _colNames[Resources.ELBOW_POS_Z]
            };
        }

        /// <summary>
        /// Set sync measuring weight. Sum of all weights must be 1.
        /// </summary>
        /// <param name="arm">Arm cvv weight</param>
        /// <param name="elbow">Elbow cvv weight</param>
        /// <param name="hand">Hand cvv weight</param>
        /// <param name="grab">grab and pinch weight</param>
        /// <param name="pinch"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool SetWeight(double arm, double elbow, double hand, double grab, double pinch, out string errorMessage)
        {
            var weightSum = arm + elbow + hand + grab + pinch;
            if (Math.Abs(weightSum - 1.0) > 0.0001)  // for not losing precision.
            {
                errorMessage = @"Invalid weights sum: Must be equal to 1.";
                return false;
            }

            errorMessage = "";
            _weights[Resources.ARM] = arm;
            _weights[Resources.ELBOW] = elbow;
            _weights[Resources.HAND] = hand;
            _weights[Resources.GRAB] = grab;
            _weights[Resources.GRAB] = grab;
            _weights[Resources.PINCH] = pinch;
            return true;
        }

        /// <summary>
        /// Set a column name inside the column dictionary.
        /// </summary>
        /// <param name="colKey"></param>
        /// <param name="csvColName"></param>
        /// <returns></returns>
        public bool SetColName(string colKey, string csvColName)
        {
            if (string.IsNullOrEmpty(colKey) || string.IsNullOrEmpty(csvColName) || !_colNames.ContainsKey(colKey))
            {
                return false;
            }
            _colNames[colKey] = csvColName;
            return true;
        }

        /// <summary>
        /// Create and return a valid R column name from a given string.
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public string CreateValidColumnName(string colName)
        {
            var charVec = _engine.CreateCharacterVector(new[] { colName });
            _engine.SetSymbol("col.name", charVec);
            _engine.Evaluate("col.name <- make.names(col.name)");
            charVec = _engine.GetSymbol("col.name").AsCharacter();
            return charVec.ToArray()[0];
        }

        /// <summary>
        /// Set old format defaults column names.
        /// </summary>
        public void SetOldFormatDefaults()
        {
            _format = EFormat.OLD;
            AmendColumnsByFormat();

            /* Set column names as appear in R env. */
            _colNames[Resources.TIMESTAMP] =      "Utc.Time";
            _colNames[Resources.FRAME_ID] =       "Frame..";
            _colNames[Resources.HAND_ID] =        "hand.ID";
            _colNames[Resources.HANDS_IN_FRAME] = "X..hands.in.Frame";
            _colNames[Resources.HAND_TYPE] =      "hand.";
            _colNames[Resources.HAND_POS_X] =     "X.pos";
            _colNames[Resources.HAND_POS_Y] =     "Y.pos";
            _colNames[Resources.HAND_POS_Z] =     "Z.pos";
            _colNames[Resources.HAND_VEL_X] =     "X_velocity";
            _colNames[Resources.HAND_VEL_Y] =     "Y_velocity";
            _colNames[Resources.HAND_VEL_Z] =     "Z_velocity";
            _colNames[Resources.PITCH] =          "Pitch";
            _colNames[Resources.ROLL] =           "Roll";
            _colNames[Resources.YAW] =            "yaw";
            _colNames[Resources.GRAB_STRENGTH] =  "Grab.Strength";
            _colNames[Resources.PINCH_STRENGTH] = "Pinch.Strenght";
            _colNames[Resources.ARM_POS_X] =      "arm.pos.x";
            _colNames[Resources.ARM_POS_Y] =      "arm.pos.y";
            _colNames[Resources.ARM_POS_Z] =      "arm.pos.z";
            _colNames[Resources.ELBOW_POS_X] =    "elbow.pos.x";
            _colNames[Resources.ELBOW_POS_Y] =    "elbow.pos.y";
            _colNames[Resources.ELBOW_POS_Z] =    "elbow.pos.z";
        }

        /// <summary>
        /// Set new format defaults column names.
        /// </summary>
        public void SetNewFormatDefaults()
        {
            _format = EFormat.NEW;
            AmendColumnsByFormat();

            /* Set column names as appear in R env. */
            _colNames[Resources.TIMESTAMP] = "Time";
            _colNames[Resources.FRAME_ID] = "Frame.ID";
            _colNames[Resources.HANDS_IN_FRAME] = "X..hands";
            _colNames[Resources.HAND_TYPE] = "Hand.Type";
            _colNames[Resources.HAND_POS_X] = "Wrist.Pos.X";
            _colNames[Resources.HAND_POS_Y] = "Wrist.Pos.Y";
            _colNames[Resources.HAND_POS_Z] = "Wrist.Pos.Z";
            _colNames[Resources.HAND_VEL_X] = "Velocity.X";
            _colNames[Resources.HAND_VEL_Y] = "Velocity.Y";
            _colNames[Resources.HAND_VEL_Z] = "Velocity.Z";
            _colNames[Resources.PITCH] = "Pitch";
            _colNames[Resources.ROLL] = "Roll";
            _colNames[Resources.YAW] = "Yaw";
            _colNames[Resources.GRAB_STRENGTH] = "Grab.Strenth";
            _colNames[Resources.PINCH_STRENGTH] = "Pinch.Strength";
            _colNames[Resources.GRAB_ANGLE] = "Grab.Angle";
            _colNames[Resources.ARM_POS_X] = "Position.X";
            _colNames[Resources.ARM_POS_Y] = "Position.Y";
            _colNames[Resources.ARM_POS_Z] = "Position.Z";
            _colNames[Resources.ELBOW_POS_X] = "Elbow.pos.X";
            _colNames[Resources.ELBOW_POS_Y] = "Elbow.Pos.Y";
            _colNames[Resources.ELBOW_POS_Z] = "Elbow.Pos.Z"; 
        }

        public void SetGraphics(EGraphics graphics)
        {
            if (graphics == EGraphics.POINTS)
            {
                _graphic = "p";
            }
            else if (graphics == EGraphics.LINES)
            {
                _graphic = "l";
            }
            else
            {
                _graphic = "b";
            }
        }

        /// <summary>
        /// Set time lag [ms] for person 1 with respect to person 0. 
        /// </summary>
        /// <param name="timeLag"></param>
        public void SetTimeLag(int timeLag)
        {
            _timeLag = timeLag;
        }

        public void SetCvvMethod(ECvv cvvMethod)
        {
            _cvvMethod = cvvMethod;
        }

        public ECvv GetCvvMethod()
        {
            return _cvvMethod;
        }

        public void AmendColumnsByFormat()
        {
            if (_format == EFormat.OLD)
            {
                if (!_colNames.ContainsKey(Resources.HAND_ID)) _colNames.Add(Resources.HAND_ID, "");
                if (_colNames.ContainsKey(Resources.GRAB_ANGLE)) _colNames.Remove(Resources.GRAB_ANGLE);
            }
            else        // NEW
            {
                if (_colNames.ContainsKey(Resources.HAND_ID)) _colNames.Remove(Resources.HAND_ID);
                if (!_colNames.ContainsKey(Resources.GRAB_ANGLE)) _colNames.Add(Resources.GRAB_ANGLE, "");
            }
        }

        /// <summary>
        /// Load previously saved user settings.
        /// </summary>
        public void LoadUserSettings()
        {
            if (!File.Exists(Resources.SETTINGS))
                return;

            var xmlSettings = new XmlDocument();
            xmlSettings.Load(Resources.SETTINGS);
            var rootElement = xmlSettings.DocumentElement;
            if (rootElement == null || !Resources.TITLE.Equals(rootElement.Name) || !(rootElement.Attributes["Version"].Value.Equals(SyncMeasureForm.Version)))
                return;

            var cvv = rootElement.SelectSingleNode("CVV");
            if (cvv?.Attributes?["Method"] != null)
            {
                Enum.TryParse(cvv.Attributes["Method"].Value, out _cvvMethod);
            }

            var names = rootElement.SelectSingleNode("Names");

            XmlNode node;
            if (names != null)
            {
                if (names.Attributes?["Format"] != null)
                {
                    Enum.TryParse(names.Attributes["Format"].Value, out _format);
                    AmendColumnsByFormat();
                }
                foreach (var key in _colNames.Keys.ToList())
                {
                    node = names.SelectSingleNode(key);
                    if (node != null) _colNames[key] = node.InnerText;
                }
            }

            var weightsNode = rootElement.SelectSingleNode("Weights");
            if (weightsNode != null)
            {
                foreach (var w in _weights.Keys.ToList())
                {
                    node = weightsNode.SelectSingleNode(w);
                    if (node != null) _weights[w] = double.Parse(node.InnerText);
                }
            }
        }

        /// <summary>
        /// Save user settings.
        /// </summary>
        public void SaveUserSettings()
        {
            if (File.Exists(Resources.SETTINGS))
            {
                File.SetAttributes(Resources.SETTINGS, FileAttributes.Normal);
            }
            var set = new XmlWriterSettings()
            {
                Encoding = new UTF8Encoding(false),     // UTF8 without Bom
                IndentChars = "    ",
                Indent = true
            };
            var writer = XmlWriter.Create(Resources.SETTINGS, set);
            writer.WriteStartDocument();
            writer.WriteStartElement(Resources.TITLE);
            writer.WriteAttributeString("Version", SyncMeasureForm.Version);


            writer.WriteStartElement("CVV");
            writer.WriteAttributeString("Method", _cvvMethod.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Names");      // Column Names
            writer.WriteAttributeString("Format", _format.ToString());
            foreach (var colName in _colNames)
            {
                writer.WriteElementString(colName.Key, colName.Value);
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Weights");      // Sync Weights
            foreach (var keyValuePair in _weights)
            {
                writer.WriteElementString(keyValuePair.Key, keyValuePair.Value.ToString(CultureInfo.InvariantCulture));
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            File.SetAttributes(Resources.SETTINGS, FileAttributes.ReadOnly | FileAttributes.Hidden);
        }
    }
}
