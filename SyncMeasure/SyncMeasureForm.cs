using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using SyncMeasure.Properties;
using Path = System.IO.Path;

namespace SyncMeasure
{
    public partial class SyncMeasureForm : Form
    {
        private readonly Handler _handler;
        private readonly string _title;
        private string _loadedFile;
        private string _prevLoadedFile;
        public static string Version;

        public SyncMeasureForm()
        {
            InitializeComponent();
            Size = new Size(757, 617); // optimal size.
            timeLagGB.Click += SetTimeLag;
            timeLagLabel.Click += SetTimeLag;
            Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            Text += @" - v" + Version;
            _loadedFile = _prevLoadedFile = "";
            _title = Text;
            openFileDialog.InitialDirectory = Path.GetFullPath("..\\..\\..\\Example Data");

            /* BackgroundWorkers initialize */
            loadingBackgroundWorker.WorkerReportsProgress = true;
            loadingBackgroundWorker.WorkerSupportsCancellation = true;
            loadingBackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            loadingBackgroundWorker.DoWork += loadingBackgroundWorker_DoWork;
            loadingBackgroundWorker.RunWorkerCompleted += loadingBackgroundWorker_RunWorkerCompleted;
            measureBackgroundWorker.WorkerReportsProgress = true;
            measureBackgroundWorker.WorkerSupportsCancellation = true;
            measureBackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            measureBackgroundWorker.DoWork += MeasureBackgroundWorkerDoWork;
            measureBackgroundWorker.RunWorkerCompleted += measureBackgroundWorker_RunWorkerCompleted;
            bulkParserBackgroundWorker.WorkerReportsProgress = true;
            bulkParserBackgroundWorker.WorkerSupportsCancellation = true;
            bulkParserBackgroundWorker.ProgressChanged += bulkBackgroundWorker_ProgressChanged;
            bulkParserBackgroundWorker.DoWork += bulkParserBackgroundWorker_DoWork;
            bulkParserBackgroundWorker.RunWorkerCompleted += bulkParserBackgroundWorker_RunWorkerCompleted;

            _handler = new Handler(out var resultStatus);
            if (!resultStatus.Status)
            {
                var errMsg = resultStatus.Message.Equals(@"RENGINE")
                    ? @"Failed to initialize[R] Engine.Please make sure R v3.4.0 is installed."
                    : resultStatus.Message;
                MessageBox.Show(this, errMsg + Environment.NewLine + @"Application will exit.",
                    Resources.TITLE + @" -Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Shown += CloseOnStart;
            }

            switch (_handler.GetCvvMethod())
            {
                case Handler.ECvv.CVV:
                    cvvButton.Checked = true;
                    break;
                case Handler.ECvv.ABS_CVV:
                    absCvvButton.Checked = true;
                    break;
                case Handler.ECvv.SQUARE_CVV:
                    squareCvvButton.Checked = true;
                    break;
            }
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void CloseOnStart(object sender, EventArgs e)
        {
            Close();
        }

        private void DisableControls()
        {
            menuStrip.Enabled = false;
            graphicsGB.Enabled = false;
            cvvMethodGB.Enabled = false;
            timeLagGB.Enabled = false;
        }

        private void EnableControls()
        {
            menuStrip.Enabled = true;
            graphicsGB.Enabled = true;
            cvvMethodGB.Enabled = true;
            timeLagGB.Enabled = true;
        }

        /// <summary>
        /// Recalculate and update the average by provided weights.
        /// </summary>
        private void UpdateAverageLabel()
        {
            if (!sumGroupBox.Visible)
            {
                return;
            }

            try
            {
                double hands = double.Parse(handsLabel.Text);
                double arms = double.Parse(armsLabel.Text);
                double elbow = double.Parse(elbowsLabel.Text);
                double grab = double.Parse(grabLabel.Text);
                double pinch = double.Parse(pinchLabel.Text);

                var weights = _handler.GetWeights();
                double avg = weights[Resources.HAND] * hands + weights[Resources.ARM] * arms +
                             weights[Resources.ELBOW] * elbow + weights[Resources.GRAB] * grab +
                             weights[Resources.PINCH] * pinch;
                avgLabel.Text = $@"{avg:0.00}";
            }
            catch (Exception)
            {
                // Don't care
            }
        }

        /// <summary>
        /// On GraphBox double click, open a dialog that fits the drawn graph.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphBox_DoubleClick(object sender, EventArgs e)
        {
            if ((sender as ImageBox)?.Image == null)
            {
                return;
            }

            try
            {
                var imgBox = new ImageBox
                {
                    Image = ((ImageBox) sender).Image,
                    Dock = DockStyle.Fill,
                    Zoom = 90
                };
                var form = new Form
                {
                    FormBorderStyle = FormBorderStyle.FixedToolWindow,
                    StartPosition = FormStartPosition.CenterParent,
                    Size = imgBox.Image.Size,
                    Text = Resources.TITLE + @" - Graph Plot"
                };
                var graphName = ((Control) sender).Name;
                if (graphName.StartsWith("arm") || graphName.StartsWith("elbow") || graphName.StartsWith("hand"))
                {
                    form.Text += @" (" + _handler.GetCvvMethod() + @")";
                }

                form.Controls.Add(imgBox);
                form.ShowIcon = false;
                form.Show(); // Allow multiple windows
            }
            catch (Exception)
            {
                // Don't care
            }
        }

        private void Clear()
        {
            allGraphBox.Image?.Dispose();
            allGraphBox.Refresh();
            handGraphBox.Image?.Dispose();
            handGraphBox.Refresh();
            armGraphBox.Image?.Dispose();
            armGraphBox.Refresh();
            elbowGraphBox.Image?.Dispose();
            elbowGraphBox.Refresh();
            grabStrBox.Image?.Dispose();
            grabStrBox.Refresh();
            pinchStrBox.Image?.Dispose();
            pinchStrBox.Refresh();
        }

        /************************************** BackgroundWorker functions **********************/

        private bool IsBusy()
        {
            return (loadingBackgroundWorker.IsBusy || measureBackgroundWorker.IsBusy ||
                    bulkParserBackgroundWorker.IsBusy);
        }

        /// <summary>
        /// update % for workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var regex = "([1-9]?[0-9]|100)%$";
            circularProgressBar.Text = Regex.Replace(circularProgressBar.Text, regex, e.ProgressPercentage + "%");
        }

        /// <summary>
        /// Cancel background workers operations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelButton.Enabled = false;
            if (loadingBackgroundWorker.IsBusy && loadingBackgroundWorker.WorkerSupportsCancellation)
            {
                loadingBackgroundWorker.CancelAsync();
            }

            if (measureBackgroundWorker.IsBusy && measureBackgroundWorker.WorkerSupportsCancellation)
            {
                measureBackgroundWorker.CancelAsync();
            }

            if (bulkParserBackgroundWorker.IsBusy && bulkParserBackgroundWorker.WorkerSupportsCancellation)
            {
                bulkParserBackgroundWorker.CancelAsync();
            }
        }

        /// <summary>
        /// main work for loading worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TimeCounter.Start();
            var filePath = (string)e.Argument;
            e.Result = _handler.LoadCsvFile(filePath, loadingBackgroundWorker, e);
            TimeCounter.Stop();
        }

        /// <summary>
        /// Loading worker completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Text = _title; // main program title
            progGroupBox.Hide();
            menuStrip.Enabled = true;
            string msg;
            MessageBoxIcon icon;
            string title;
            if (e.Cancelled)
            {
                msg = @"Loading Cancelled by user.";
                title = Resources.TITLE + @" - Loading cancelled.";
                icon = MessageBoxIcon.Information;
                if (sumGroupBox.Enabled)
                {
                    sumGroupBox.Show();
                }
            }
            else
            {
                var status = (ResultStatus) e.Result;
                msg = status.Message;
                if (status.Status)
                {
                    Clear();
                    sumGroupBox.Hide();
                    measureToolStripMenuItem.Enabled = true;
                    msg = @"File successfully loaded." + Environment.NewLine + TimeCounter.ElapsedString();
                    title = Resources.TITLE + @" - File loaded.";
                    icon = MessageBoxIcon.Information;
                    _prevLoadedFile = _loadedFile;
                    if (measureOnLoad.Checked)
                    {
                        MeasureSync();
                    }
                }
                else
                {
                    if (sumGroupBox.Enabled)
                    {
                        sumGroupBox.Show();
                    }

                    measureToolStripMenuItem.Enabled = false;
                    icon = MessageBoxIcon.Error;
                    title = Resources.TITLE + @" - File loading failed.";
                }
            }

            if (!string.IsNullOrEmpty(_prevLoadedFile))
                Text += @" - Loaded file: " + _prevLoadedFile;
            MessageBox.Show(this, msg, title, MessageBoxButtons.OK, icon);
        }

        private void LoadFile(string filePath)
        {
            if (IsBusy()) return;
            
            menuStrip.Enabled = false;
            circularProgressBar.Text = @"Loading.." + Environment.NewLine + @"0%";
            cancelButton.Enabled = true;
            progGroupBox.Show();
            sumGroupBox.Hide();
            _loadedFile = Path.GetFileNameWithoutExtension(filePath);
            loadingBackgroundWorker.RunWorkerAsync(filePath);
        }

        /// <summary>
        /// main work for sync measure worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeasureBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            TimeCounter.Start();
            e.Result = _handler.MeasureSynchronization(measureBackgroundWorker, e);
            TimeCounter.Stop();
        }

        /// <summary>
        /// Measure end
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measureBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progGroupBox.Hide();
            EnableControls();
            if (e.Cancelled)
            {
                MessageBox.Show(this, @"Sync Measurement cancelled by user.",
                    Resources.TITLE + @" - Sync Measurement cancelled.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var status = (ResultStatus) e.Result;
            if (status.Status)
            {
                try
                {
                    Clear();

                    /* Loaded graphs images */
                    allGraphBox.Image = Image.FromFile(Path.GetFullPath(Resources.ALL_GRAPH));
                    handGraphBox.Image = Image.FromFile(Path.GetFullPath(Resources.HAND_CVV_GRAPH));
                    armGraphBox.Image = Image.FromFile(Path.GetFullPath(Resources.ARM_CVV_GRAPH));
                    elbowGraphBox.Image = Image.FromFile(Path.GetFullPath(Resources.ELBOW_CVV_GRAPH));
                    grabStrBox.Image = Image.FromFile(Path.GetFullPath(Resources.GRAB_GRAPH));
                    pinchStrBox.Image = Image.FromFile(Path.GetFullPath(Resources.PINCH_GRAPH));

                    /* Loaded avg sync measurements */
                    avgLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.weighted")[0]:0.00}";
                    handsLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.hands.cvv")[0]:0.00}";
                    armsLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.arms.cvv")[0]:0.00}";
                    elbowsLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.elbows.cvv")[0]:0.00}";
                    grabLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.grab")[0]:0.00}";
                    pinchLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.pinch")[0]:0.00}";
                    nbasisLabel.Text = _handler.GetNBasis().ToString(); // Get used nbasis.

                    var cvv = _handler.GetCvvMethod();
                    var handsText = @"Avg Hands CVV:";
                    var elbowText = @"Avg Elbows CVV:";
                    var armsText = @"Avg Arms CVV:";
                    if (cvv == Handler.ECvv.SQUARE_CVV)
                    {
                        handsCvvText.Text = handsText.Replace("CVV", "CVV^2");
                        elbowCvvText.Text = elbowText.Replace("CVV", "CVV^2");
                        armsCvvText.Text = armsText.Replace("CVV", "CVV^2");
                    }
                    else if (cvv == Handler.ECvv.ABS_CVV)
                    {
                        handsCvvText.Text = handsText.Replace("CVV", "|CVV|");
                        elbowCvvText.Text = elbowText.Replace("CVV", "|CVV|");
                        armsCvvText.Text = armsText.Replace("CVV", "|CVV|");
                    }
                    else
                    {
                        handsCvvText.Text = handsText;
                        elbowCvvText.Text = elbowText;
                        armsCvvText.Text = armsText;
                    }

                    sumGroupBox.Enabled = true;
                    sumGroupBox.Show();
                    var msg = @"Successfully measured sync." + Environment.NewLine + TimeCounter.ElapsedString();
                    MessageBox.Show(this, msg, Resources.TITLE + @" - Success.",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                measureToolStripMenuItem.Enabled = true;
                MessageBox.Show(this, status.Message, Resources.TITLE + @" - sync measurement failed.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Main work for bulk parser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bulkParserBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _bulkSteps = 0;
            TimeCounter.Start();
            _handler.ParseBulkFiles(bulkParserBackgroundWorker, e);
            TimeCounter.Stop();
        }

        /// <summary>
        /// bulk parsing completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bulkParserBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progGroupBox.Hide();
            EnableControls();
            if (e.Cancelled)
            {
                MessageBox.Show(this, @"Sync Measurement cancelled by user.",
                    Resources.TITLE + @" - Sync Measurement cancelled.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = _handler.ExportSyncParams((Dictionary<string, SyncParams>) e.Result);
            if (result.Status)
            {
                var argument = "/select, \"" + result.Message + "\"";
                Process.Start("explorer.exe", argument);
                var msg = @"Sync Report generated successfully" + Environment.NewLine + TimeCounter.ElapsedString();
                MessageBox.Show(this, msg, @"Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, result.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private int _bulkSteps;
        /// <summary>
        /// update % for workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bulkBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var checkpoints = bulkOpenFileDialog.FileNames.Length * 2;  // #files - Loading, Measuring.
            var regex = "([1-9]?[0-9]|100)%$";
            if (e.UserState != null && e.UserState.Equals(Resources.BULK))
            {
                _bulkSteps = e.ProgressPercentage;
            }
            else
            {
                var per = (100 * _bulkSteps + e.ProgressPercentage) / checkpoints;
                circularProgressBar.Text = Regex.Replace(circularProgressBar.Text, regex, per + "%");
            }
        }

        /// <summary>
        /// Measure Sync
        /// </summary>
        private void MeasureSync()
        {
            if (IsBusy()) return;
            Clear();
            sumGroupBox.Hide();
            DisableControls();
            circularProgressBar.Text = @"Measuring" + Environment.NewLine + @"0%";
            cancelButton.Enabled = true;
            progGroupBox.Show();
            measureBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Load file by drag and drop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                LoadFile(filePaths[0]);
            }
        }

        /// <summary>
        /// Allow the drag drop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        /// <summary>
        /// If sync measurement is enabled and enter is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && measureToolStripMenuItem.Enabled)
            {
                MeasureSync();
            }
        }


        /// <summary>
        /// Set time lag [ms]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTimeLag(object sender, EventArgs e)
        {
            var inputBox = new InputBox(@"Enter Time Lag (milliseconds) for person 1 with respect to person 0:",
                @"Set Time Lag [ms]", _handler.GetTimeLag());
            var res = inputBox.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                _handler.SetTimeLag(inputBox.GetValue());
                timeLagLabel.Text = inputBox.GetValue() + @" [ms]";
            } 
        }

        private void SetNBasis(object sender, EventArgs e)
        {
            var inputBox = new InputBox(@"Enter nbasis (default = 300):", @"nbasis", _handler.GetNBasis(), true);
            var res = inputBox.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                _handler.SetNBasis(inputBox.GetValue());
            }
        }

        /// <summary>
        /// User's radio box selection changed. lines / points graph representation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphics_CheckedChanged(object sender, EventArgs e)
        {
            if (pointsRB.Checked)
            {
                _handler.SetGraphics(Handler.EGraphics.POINTS);
            }
            else if (linesRB.Checked)
            {
                _handler.SetGraphics(Handler.EGraphics.LINES);
            }
            else if (bothRB.Checked)
            {
                _handler.SetGraphics(Handler.EGraphics.BOTH);
            }
        }


        private void cvvMethod_CheckedChanged(object sender, EventArgs e)
        {
            if (cvvButton.Checked)
            {
                _handler.SetCvvMethod(Handler.ECvv.CVV);
            }
            else if (absCvvButton.Checked)
            {
                _handler.SetCvvMethod(Handler.ECvv.ABS_CVV);
            }
            else if(squareCvvButton.Checked)
            {
                _handler.SetCvvMethod(Handler.ECvv.SQUARE_CVV);
            }
            _handler.SaveUserSettings();
        }

        /******************************* menu strip buttons ******************************/

        /// <summary>
        /// On load button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            LoadFile(openFileDialog.FileName);
        }

        /// <summary>
        /// On measure button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeasureSync();
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new ControlsForm()).ShowDialog();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog();
        }

        private void weightsColNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = (new SettingsForm(_handler, SettingsForm.ESettings.WEIGHTS)).ShowDialog();
            if (res == DialogResult.OK)
            {
                UpdateAverageLabel();
            }
        }

        private void cSVFileColumnNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new SettingsForm(_handler, SettingsForm.ESettings.NAMES)).ShowDialog();
        }


        /// <summary>
        /// Open "Alone" files combiner dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Combiner(_handler)).ShowDialog();
        }

        /// <summary>
        /// On Parse tool strip click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParseMultipleFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bulkOpenFileDialog.ShowDialog() != DialogResult.OK) return;
            DisableControls();
            cancelButton.Enabled = true;
            circularProgressBar.Text = @"Parsing.." + Environment.NewLine + @"0%";
            progGroupBox.Show();
            sumGroupBox.Hide();
            Text = _title;
            measureToolStripMenuItem.Enabled = false;
            bulkParserBackgroundWorker.RunWorkerAsync(bulkOpenFileDialog.FileNames);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openReportsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Handler.REPORT_DIR);
            }
            catch (Exception)
            {
                MessageBox.Show(this, @"Reports folder doesn't exists", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

