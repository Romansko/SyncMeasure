using System;
using System.ComponentModel;
using System.IO;
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

        public MainForm()
        {
            InitializeComponent();
            openFileDialog.InitialDirectory = Path.GetFullPath("..\\..\\..\\Example Data");

            /* BackgroundWorkers initialize */
            loadingBackgroundWorker.WorkerReportsProgress      = true;
            loadingBackgroundWorker.WorkerSupportsCancellation = true;
            loadingBackgroundWorker.ProgressChanged           += backgroundWorker_ProgressChanged;
            loadingBackgroundWorker.DoWork                    += loadingBackgroundWorker_DoWork;
            loadingBackgroundWorker.RunWorkerCompleted        += loadingBackgroundWorker_RunWorkerCompleted;
            calcBackgroundWorker.WorkerReportsProgress         = true;
            calcBackgroundWorker.WorkerSupportsCancellation    = true;
            calcBackgroundWorker.ProgressChanged              += backgroundWorker_ProgressChanged;
            calcBackgroundWorker.DoWork                       += calcBackgroundWorker_DoWork;
            calcBackgroundWorker.RunWorkerCompleted           += calcBackgroundWorker_RunWorkerCompleted;

            _handler = new Handler(out var resultStatus);
            if (!resultStatus.Status)
            {
                Shown += CloseOnStart;
                MessageBox.Show(resultStatus.Message + ".\n Application will exit.", Resources.TITLE + @" - R packages are no installed!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            form.Controls.Add(imgBox);
            form.ShowDialog();
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
        }

        /************************************** BackgroundWorker functions **********************/

        /// <summary>
        /// update % for workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string regex = "([1-9]?[0-9]|100)%$";
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
            if (calcBackgroundWorker.IsBusy && calcBackgroundWorker.WorkerSupportsCancellation)
            {
                calcBackgroundWorker.CancelAsync();
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
                string filePath = (string) e.Argument;
                e.Result = _handler.LoadLeapMotionOutputFile(filePath, worker, e);
            }
        }

        /// <summary>
        /// main work for calculation worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calcBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = _handler.CalculateSynchronization(calcBackgroundWorker, e);
        }

        /// <summary>
        /// Loading worker completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
                ResultStatus status = (ResultStatus) e.Result;
                msg = status.Message;
                if (status.Status)
                {
                    Clear();
                    sumGroupBox.Hide();
                    calculateToolStripMenuItem.Enabled = true;
                    if (string.IsNullOrEmpty(status.Message))
                    {
                        msg = @"File successfully loaded.";
                        title = Resources.TITLE + @" - File loaded.";
                        icon = MessageBoxIcon.Information;
                    }
                    else
                    {
                        title = Resources.TITLE + @" - File loaded.";
                        icon = MessageBoxIcon.Warning;
                    }
                }
                else
                {
                    calculateToolStripMenuItem.Enabled = false;
                    icon = MessageBoxIcon.Error;
                    title = Resources.TITLE + @" - File loading failed.";
                }
            }
            MessageBox.Show(msg, title, MessageBoxButtons.OK, icon);
        }

        private void calcBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progGroupBox.Hide();
            menuStrip.Enabled = true;
            if (e.Cancelled)
            {
                MessageBox.Show(@"Calculation cancelled by user.", Resources.TITLE + @" - Calculation cancelled.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ResultStatus status = (ResultStatus)e.Result;
            if (status.Status)
            {
                try
                {
                    Clear();
                    allGraphBox.Image = System.Drawing.Image.FromFile(Path.GetFullPath(Resources.ALL_GRAPH));
                    handGraphBox.Image = System.Drawing.Image.FromFile(Path.GetFullPath(Resources.HAND_CVV_GRAPH));
                    armGraphBox.Image = System.Drawing.Image.FromFile(Path.GetFullPath(Resources.ARM_CVV_GRAPH));
                    elbowGraphBox.Image = System.Drawing.Image.FromFile(Path.GetFullPath(Resources.ELBOW_CVV_GRAPH));

                    handsLabel.Text = $"{_handler.GetRSymbolAsVector("avg.hands.cvv")[0]:0.00}";
                    armsLabel.Text = $"{_handler.GetRSymbolAsVector("avg.arms.cvv")[0]:0.00}";
                    elbowsLabel.Text = $"{_handler.GetRSymbolAsVector("avg.elbows.cvv")[0]:0.00}";
                    allLabel.Text = $"{_handler.GetRSymbolAsVector("avg.all.cvv")[0]:0.00}";
                    sumGroupBox.Show();
                    calculateToolStripMenuItem.Enabled = false;
                    MessageBox.Show(@"Successfully calculated.", Resources.TITLE + @" - Success.",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                calculateToolStripMenuItem.Enabled = true;
                MessageBox.Show(status.Message, Resources.TITLE + @" - calculation failed failed.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LoadFile(string filePath)
        {
            menuStrip.Enabled = false;
            circularProgressBar.Text = "Loading..\n0%";
            cancelButton.Enabled = true;
            progGroupBox.Show();
            if (!loadingBackgroundWorker.IsBusy)
            {
                loadingBackgroundWorker.RunWorkerAsync(filePath);
            }
        }

        /// <summary>
        /// On settings button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingForm = new SettingsForm(_handler);
            settingForm.ShowDialog();
        }

        /// <summary>
        /// On calculate button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.Enabled = false;
            circularProgressBar.Text = "Calculating\n0%";
            cancelButton.Enabled = true;
            progGroupBox.Show();

            if (!calcBackgroundWorker.IsBusy)
            {
                calcBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Application exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string tempGraph = Path.GetFullPath(Resources.HAND_CVV_GRAPH);
            try
            {
                File.Delete(tempGraph);     // Delete temporary image.
            }
            catch (Exception)
            {
                // Don't care
            }
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
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
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
        /// If calculate is enabled and enter is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && calculateToolStripMenuItem.Enabled)
            {
                calculateToolStripMenuItem_Click(sender, e);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new About()).ShowDialog();
        }

    }
}

