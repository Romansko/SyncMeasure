using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using SyncMeasure.Properties;
using Path = System.IO.Path;

namespace SyncMeasure
{
    public partial class MainForm : Form
    {
        private readonly Handler _handler;
        private readonly string _title;
        private string _loadedFile;
        private string _prevLoadedFile;

        public MainForm()
        {
            InitializeComponent();
            Text += @" - v" + Resources.VERSION;
            _loadedFile = _prevLoadedFile = "";
            _title = Text;
            openFileDialog.InitialDirectory = Path.GetFullPath("..\\..\\..\\Example Data");

            /* BackgroundWorkers initialize */
            loadingBackgroundWorker.WorkerReportsProgress         = true;
            loadingBackgroundWorker.WorkerSupportsCancellation    = true;
            loadingBackgroundWorker.ProgressChanged              += backgroundWorker_ProgressChanged;
            loadingBackgroundWorker.DoWork                       += loadingBackgroundWorker_DoWork;
            loadingBackgroundWorker.RunWorkerCompleted           += loadingBackgroundWorker_RunWorkerCompleted;
            measureBackgroundWorker.WorkerReportsProgress         = true;
            measureBackgroundWorker.WorkerSupportsCancellation    = true;
            measureBackgroundWorker.ProgressChanged              += backgroundWorker_ProgressChanged;
            measureBackgroundWorker.DoWork                       += MeasureBackgroundWorkerDoWork;
            measureBackgroundWorker.RunWorkerCompleted           += measureBackgroundWorker_RunWorkerCompleted;

            _handler = new Handler(out var resultStatus);
            if (resultStatus.Status) return;
            var errMsg = resultStatus.Message.Equals(@"RENGINE")
                ? @"Failed to initialize[R] Engine.Please make sure R v3.4.0 is installed."
                : resultStatus.Message;
            MessageBox.Show(this, errMsg + Environment.NewLine + @"Application will exit.",
                Resources.TITLE + @" -Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Shown += CloseOnStart;
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
                    Image = ((ImageBox)sender).Image,
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
                form.Show();        // Allow multiple windows
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
        }

        /// <summary>
        /// main work for loading worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (sender is BackgroundWorker worker)
            {
                var filePath = (string) e.Argument;
                e.Result = _handler.LoadLeapMotionOutputFile(filePath, worker, e);
            }
        }

        /// <summary>
        /// Loading worker completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Text = _title;  // main program title
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
                    msg = @"File successfully loaded.";
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
            if (loadingBackgroundWorker.IsBusy || measureBackgroundWorker.IsBusy) return;
            menuStrip.Enabled = false;
            circularProgressBar.Text = @"Loading.." + Environment.NewLine + @"0%";
            cancelButton.Enabled = true;
            progGroupBox.Show();
            _prevLoadedFile = _loadedFile;
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
            e.Result = _handler.MeasureSynchronization(measureBackgroundWorker, e);
        }

        /// <summary>
        /// Measure end
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measureBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progGroupBox.Hide();
            menuStrip.Enabled = true;
            graphicsGB.Enabled = true;
            if (e.Cancelled)
            {
                MessageBox.Show(this, @"Sync Measurement cancelled by user.", Resources.TITLE + @" - Sync Measurement cancelled.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var status = (ResultStatus)e.Result;
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
                    allLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.weighted")[0]:0.00}";
                    handsLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.hands.cvv")[0]:0.00}";
                    armsLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.arms.cvv")[0]:0.00}";
                    elbowsLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.elbows.cvv")[0]:0.00}";
                    grabLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.grab")[0]:0.00}";
                    pinchLabel.Text = $@"{_handler.GetRSymbolAsVector("avg.pinch")[0]:0.00}";

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

                    sumGroupBox.Show();
                    MessageBox.Show(this, @"Successfully measured sync.", Resources.TITLE + @" - Success.",
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
        /// Measure Sync
        /// </summary>
        private void MeasureSync()
        {
            if (measureBackgroundWorker.IsBusy || loadingBackgroundWorker.IsBusy) return;
            Clear();
            menuStrip.Enabled = false;
            graphicsGB.Enabled = false;
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
            (new SettingsForm(_handler, SettingsForm.ESettings.WEIGHTS)).ShowDialog();
        }

        private void cSVFileColumnNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new SettingsForm(_handler, SettingsForm.ESettings.NAMES)).ShowDialog();
        }

        private void cVVMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new CvvSelector(_handler)).ShowDialog();
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

        /// <summary>
        /// Open "Alone" files combiner dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Combiner(_handler)).ShowDialog();
        }

    }
}

