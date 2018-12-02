using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Leap;
using RDotNet;
using SyncMeasure.Properties;


namespace SyncMeasure
{
    public class Handler
    {
        private readonly REngine _engine;
        private readonly Dictionary<string, double> _weights;
        private readonly Dictionary<string, string> _colNames;  // csv file column names. (to be read with R).
        private List<Frame> _frames;                            // The parsed data.

        public Handler(out ResultStatus resultStatus)
        {
            /* [R] initialzation */
            _engine = REngine.GetInstance();
            var loadedPackages = LoadRPackages(out var errMsg);
            resultStatus = new ResultStatus(loadedPackages, errMsg);

            /* .NET initialization */
            _weights = new Dictionary<string, double>
            {
                {Resources.ARM, 0}, // Arm's CVV weight.
                {Resources.ELBOW, 0}, // Elbow's CVV weight.
                {Resources.HAND, 0}, // Hand's CVV weight.
                {Resources.GRAB, 0}, // Grab and Pitch weight.
                {Resources.GESTURE, 0} // Gesture's weight.
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
                {Resources.SYNC_MODE, ""}
            };
            SetDefaults();
            LoadUserSettings();
        }

        public bool LoadRPackages(out string errorMessage)
        {
            if (_engine.Evaluate("require(cvv)").AsInteger()[0] == 0)
            {
                errorMessage = @"Please install cvv package!";
                return false;
            }

            if (_engine.Evaluate("require(data.table)").AsInteger()[0] == 0)
            {
                errorMessage = @"Please install data.table package!";
                return false;
            }

            errorMessage = "";
            return true;
        }

        private void RemoveFromR(string toRemove)
        {
            _engine.Evaluate("remove(" + toRemove + ")");
        }

        private void RemovePosFromR(string posName)
        {
            RemoveFromR(posName + ".pos.x");
            RemoveFromR(posName + ".pos.y");
            RemoveFromR(posName + ".pos.z");
        }

        public DynamicVector GetRSymbolAsVector(string symbol)
        {
            return _engine.GetSymbol(symbol).AsVector();
        }

        /// <summary>
        /// Calculate Synchronization from loaded DataFrame.
        /// </summary>
        /// <param name="bgWorker"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResultStatus CalculateSynchronization(BackgroundWorker bgWorker, DoWorkEventArgs args)
        {
            try
            {
                /* prepare objects for R parsing */
                var timestamps = new List<int>();

                // positions data.
                // index 0 = first hand.
                // index 1 = second hand.
                // each cell contains list of coordinates.
                var armsPos = new[]
                {
                    new[] {new List<double>(), new List<double>(), new List<double>()},
                    new[] {new List<double>(), new List<double>(), new List<double>()}
                };
                var handsPos = new[]
                {
                    new[] {new List<double>(), new List<double>(), new List<double>()},
                    new[] {new List<double>(), new List<double>(), new List<double>()}
                };
                var elbowsPos = new[]
                {
                    new[] {new List<double>(), new List<double>(), new List<double>()},
                    new[] {new List<double>(), new List<double>(), new List<double>()}
                };
                foreach (var frame in _frames)
                {
                    timestamps.Add((int) frame.Timestamp);
                    for (int i = 0; i < 2; ++i) // do for both hands.
                    {
                        armsPos[i][0].Add(frame.Hands[i].Arm.Center.x);
                        armsPos[i][1].Add(frame.Hands[i].Arm.Center.y);
                        armsPos[i][2].Add(frame.Hands[i].Arm.Center.z);

                        elbowsPos[i][0].Add(frame.Hands[i].Arm.ElbowPosition.x);
                        elbowsPos[i][1].Add(frame.Hands[i].Arm.ElbowPosition.y);
                        elbowsPos[i][2].Add(frame.Hands[i].Arm.ElbowPosition.z);

                        handsPos[i][0].Add(frame.Hands[i].Arm.WristPosition.x);
                        handsPos[i][1].Add(frame.Hands[i].Arm.WristPosition.y);
                        handsPos[i][2].Add(frame.Hands[i].Arm.WristPosition.z);
                    }
                }

                _engine.SetSymbol("timestamps", _engine.CreateIntegerVector(timestamps));
                _engine.Evaluate("diff <- tail(timestamps, 1) - head(timestamps, 1)");
                _engine.Evaluate("func <- function(x) { return(x%%(diff+1)) }");
                _engine.Evaluate("timestamps <- sapply(timestamps, func)");
                RemoveFromR("diff");
                RemoveFromR("func");

                for (int i = 0; i < 2; ++i)     // for both hands.
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
                }
                bgWorker.ReportProgress(20);
                if (bgWorker.CancellationPending)
                {
                    args.Cancel = true;
                    return null;        // will be ignored.
                }

                /* Calculate Arm, Elbow and Hand cvv */
                // TODO: try to make parallel

                // Hand
                _engine.Evaluate("pos_1 <- cbind(hand0.pos.x, hand0.pos.y, hand0.pos.z)");
                _engine.Evaluate("pos_2 <- cbind(hand1.pos.x, hand1.pos.y, hand1.pos.z)");
                _engine.Evaluate("hand.cvv <- cosfunc(make.fd(timestamps, pos_1, pos_2))");
                RemovePosFromR("hand0");
                RemovePosFromR("hand1");
                bgWorker.ReportProgress(40);
                if (bgWorker.CancellationPending)
                {
                    args.Cancel = true;
                    return null;        // will be ignored.
                }

                // Arm
                _engine.Evaluate("pos_1 <- cbind(arm0.pos.x, arm0.pos.y, arm0.pos.z)");
                _engine.Evaluate("pos_2 <- cbind(arm1.pos.x, arm1.pos.y, arm1.pos.z)");                
                _engine.Evaluate("arm.cvv <- cosfunc(make.fd(timestamps, pos_1, pos_2))");
                RemovePosFromR("arm0");
                RemovePosFromR("arm1");
                bgWorker.ReportProgress(60);
                if (bgWorker.CancellationPending)
                {
                    args.Cancel = true;
                    return null;        // will be ignored.
                }

                // Elbow
                _engine.Evaluate("pos_1 <- cbind(elbow0.pos.x, elbow0.pos.y, elbow0.pos.z)");
                _engine.Evaluate("pos_2 <- cbind(elbow1.pos.x, elbow1.pos.y, elbow1.pos.z)");
                _engine.Evaluate("elbow.cvv <- cosfunc(make.fd(timestamps, pos_1, pos_2))");
                RemovePosFromR("elbow0");
                RemovePosFromR("elbow1");
                bgWorker.ReportProgress(80);
                if (bgWorker.CancellationPending)
                {
                    args.Cancel = true;
                    return null;        // will be ignored.
                }
                _engine.Evaluate("remove(pos_1)");
                _engine.Evaluate("remove(pos_2)");

                //ToDo: switch between cvv^2 and cvv. (by abs).
                _engine.Evaluate("hcvv <- abs(hand.cvv(timestamps))");
                _engine.Evaluate("acvv <- abs(arm.cvv(timestamps))");
                _engine.Evaluate("ecvv <- abs(elbow.cvv(timestamps))");
                _engine.Evaluate("ymin <- min(hcvv, acvv, ecvv)");
                _engine.Evaluate("ymax <- max(hcvv, acvv, ecvv)");
                _engine.Evaluate("avg.hands.cvv <- mean(hcvv)");
                _engine.Evaluate("avg.arms.cvv <- mean(acvv)");
                _engine.Evaluate("avg.elbows.cvv <- mean(ecvv)");
                _engine.Evaluate("avg.all.cvv <- (avg.hands.cvv + avg.arms.cvv + avg.elbows.cvv)/3");
                bgWorker.ReportProgress(90);

                /* Plot all cvv in one graph */
                string allGraph = Path.GetFullPath(Resources.ALL_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("allGraph", _engine.CreateCharacterVector(new[] { allGraph }));
                _engine.Evaluate("png(filename=allGraph)");
                _engine.Evaluate("plot(x = timestamps, y = hand.cvv(timestamps), type = 'l', main = 'CVV = f(Timestamp)', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'cvv', col = 'red', ylim=c(ymin,ymax))");
                _engine.Evaluate("lines(x = timestamps, y = arm.cvv(timestamps), col='green')");
                _engine.Evaluate("lines(x = timestamps, y = elbow.cvv(timestamps), col='blue')");
                _engine.Evaluate("dev.off()");

                /* Plot hand cvv */
                string handsGraph = Path.GetFullPath(Resources.HAND_CVV_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("handsGraph", _engine.CreateCharacterVector(new[] { handsGraph }));
                _engine.Evaluate("png(filename=handsGraph)");
                _engine.Evaluate("plot(x = timestamps, y = hand.cvv(timestamps), type = 'l', main = 'Hands cvv = f(Timestamp)', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'hands cvv', col = 'red', ylim=c(ymin,ymax))");
                _engine.Evaluate("dev.off()");

                /* Plot arm cvv */
                string armsGraph = Path.GetFullPath(Resources.ARM_CVV_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("armsGraph", _engine.CreateCharacterVector(new[] { armsGraph }));
                _engine.Evaluate("png(filename=armsGraph)");
                _engine.Evaluate("plot(x = timestamps, y = arm.cvv(timestamps), type = 'l', main = 'arm cvv = f(Timestamp)', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'arms cvv', col = 'green', ylim=c(ymin,ymax))");
                _engine.Evaluate("dev.off()");

                /* Plot elbow cvv */
                string elbowGraph = Path.GetFullPath(Resources.ELBOW_CVV_GRAPH).Replace("\\", "/");
                _engine.SetSymbol("elbowsGraph", _engine.CreateCharacterVector(new[] { elbowGraph }));
                _engine.Evaluate("png(filename=elbowsGraph)");
                _engine.Evaluate("plot(x = timestamps, y = hand.cvv(timestamps), type = 'l', main = 'elbow cvv = f(Timestamp)', " +
                                 "xlab = 'Timestamp [ms]', ylab = 'elbows cvv', col = 'blue', ylim=c(ymin,ymax))");
                _engine.Evaluate("dev.off()");

                // release resources.
                RemoveFromR("hcvv");
                RemoveFromR("acvv");
                RemoveFromR("ecvv");
                RemoveFromR("ymin");
                RemoveFromR("hand.cvv");
                RemoveFromR("arm.cvv");
                RemoveFromR("elbow.cvv");

                bgWorker.ReportProgress(100);
                string errorMessage = "";
                return new ResultStatus(true, errorMessage);
            }
            catch (Exception e)
            {
                return new ResultStatus(false, e.Message);
            }
        }


        /// <summary>
        /// Parse leapmotion output file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="bgWorker"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ResultStatus LoadLeapMotionOutputFile(string filePath, BackgroundWorker bgWorker, DoWorkEventArgs args)
        {
            if (!File.Exists(filePath))
            {
                return new ResultStatus(false, @"Invalid file path!");
            }

            string errorMessage = "";
            try
            {
                // import csv file
                filePath = filePath.Replace("\\", "/");   // fix for [R].

                // Load csv file to memory.
                string cmd = "dataset <- fread(file='" + filePath + "', header = T, sep = ',', dec='.', " +
                             "stringsAsFactors = F, verbose = F, check.names = T, blank.lines.skip = T)";
                _engine.Evaluate(cmd);

                /* Validate column names */
                var colNames = _engine.Evaluate("colnames(dataset)").AsVector();
                
                foreach (var cName in (GeNecessaryColumnNames()))
                {
                    if (!colNames.Contains(cName))
                    {
                        return new ResultStatus(false, @"Provided file doesn't contain column name: " + cName);
                    }
                }

                /* filter only the frames with two hands */
                _engine.Evaluate("dataset <- dataset[dataset$" + _colNames[Resources.HANDS_IN_FRAME] + " == 2,]");

                // retrieve the data frame
                DataFrame dataFrame = _engine.GetSymbol("dataset").AsDataFrame();
                
                if (dataFrame == null)
                {
                    return new ResultStatus(false, @"Failed to load '" + Path.GetFileName(filePath));
                }

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
                    if (bgWorker.CancellationPending)
                    {
                        args.Cancel = true;
                        return null;        // will be ignored.
                    }

                    double percent = (double) i / dataFrame.RowCount * 100;
                    bgWorker.ReportProgress((int) percent);
                    
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
                            bool firstHandLeft = frames[frames.Count - 1].Hands[0].IsLeft;
                            bool secondHandLeft = frames[frames.Count - 1].Hands[1].IsLeft;
                            if (firstHandLeft == secondHandLeft)
                            {
                                errorMessage =
                                    @"Both hands went out of frame at the same time, SyncMeasure tried to re-map the hand and therefore hands might be swapped.";
                                hand1.Id = firstHandId;
                                hand2.Id = secondHandId;
                            }
                            else
                            {
                                if (firstHandLeft == hand1.IsLeft)
                                {
                                    hand1.Id = firstHandId;
                                    hand2.Id = secondHandId;
                                }
                                else
                                {
                                    hand1.Id = secondHandId;
                                    hand2.Id = firstHandId;
                                }
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

                    int timestamp;
                    var ts1 = (int) dataFrame[i, _colNames[Resources.TIMESTAMP]];
                    var ts2 = (int) dataFrame[i + 1, _colNames[Resources.TIMESTAMP]];
                    var diff = ts2 - ts1;
                    if (diff == 0)
                    {
                        timestamp = ts1;
                    }
                    else
                    {
                        timestamp = ts1 + diff / 2;
                    }

                    var frame = new Frame
                    {
                        Hands = new List<Hand> { hand1, hand2 },
                        Timestamp = timestamp,
                        Id = fid1
                    };
                    frames.Add(frame);
                }
                bgWorker.ReportProgress(99);
                _frames = frames;
                RemoveFromR("dataset");
                return new ResultStatus(true, errorMessage);
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
                    Id = (int) dataFrame[index, _colNames[Resources.HAND_ID]],
                    FrameId = (int) dataFrame[index, _colNames[Resources.FRAME_ID]],
                    PinchStrength = (float) (double) dataFrame[index, _colNames[Resources.PINCH_STRENGTH]],
                    GrabStrength = (float) (double) dataFrame[index, _colNames[Resources.GRAB_STRENGTH]],
                    IsLeft = dataFrame[index, _colNames[Resources.HAND_TYPE]].ToString().ToLower().Contains("left")
                };
                float x, y, z;
                // unbox double and cast to float.
                x = (float) (double) dataFrame[index, _colNames[Resources.ELBOW_POS_X]];
                y = (float) (double) dataFrame[index, _colNames[Resources.ELBOW_POS_Y]];
                z = (float) (double) dataFrame[index, _colNames[Resources.ELBOW_POS_Z]];
                Vector elbowPos = new Vector(x, y, z);

                // unbox double and cast to float.
                x = (float) (double) dataFrame[index, _colNames[Resources.ARM_POS_X]];
                y = (float) (double) dataFrame[index, _colNames[Resources.ARM_POS_Y]];
                z = (float) (double) dataFrame[index, _colNames[Resources.ARM_POS_Z]];
                var armPos = new Vector(x, y, z);

                // unbox double and cast to float.
                x = (float) (double) dataFrame[index, _colNames[Resources.HAND_POS_X]];
                y = (float) (double) dataFrame[index, _colNames[Resources.HAND_POS_Y]];
                z = (float) (double) dataFrame[index, _colNames[Resources.HAND_POS_Z]];
                var handPos = new Vector(x, y, z);

                var dummy = new Vector();
                hand.Arm = new Arm(elbowPos, handPos, armPos, dummy, 0, 0, LeapQuaternion.Identity);
                errMsg = "";
                return hand;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
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
        private List<string> GeNecessaryColumnNames()
        {
            return new List<string>()
            {
                _colNames[Resources.TIMESTAMP],
                _colNames[Resources.FRAME_ID],
                _colNames[Resources.HAND_ID],
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
        /// <param name="grab">grab and pitch weight</param>
        /// <param name="gesture">gesture weight</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool SetWeight(double arm, double elbow, double hand, double grab, double gesture,
            out string errorMessage)
        {
            double weightSum = arm + elbow + hand + grab + gesture;
            if (Math.Abs(weightSum - 1.0) > 0)  // for not losing precision.
            {
                errorMessage = @"Invalid weights sum: Must be equal to 1.";
                return false;
            }

            errorMessage = "";
            _weights[Resources.ARM] = arm;
            _weights[Resources.ELBOW] = elbow;
            _weights[Resources.HAND] = hand;
            _weights[Resources.GRAB] = grab;
            _weights[Resources.GESTURE] = gesture;
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
        /// Set defaults settings.
        /// </summary>
        public void SetDefaults()
        {
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
            _colNames[Resources.SYNC_MODE] =      "Sync.Mode";

            /* Default weight initialization */
            SetWeight(0.15, 0.15, 0.5, 0.1, 0.1, out _);
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
            if (rootElement == null || !Resources.TITLE.Equals(rootElement.Name))
                return;

            var names = rootElement.SelectSingleNode("Names");
            XmlNode node;
            if (names != null)
            {
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
            writer.WriteAttributeString("Version", Resources.VERSION);

            writer.WriteStartElement("Names");      // Column Names
            foreach (var colName in _colNames)
            {
                writer.WriteElementString(colName.Key, colName.Value);
            }

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
