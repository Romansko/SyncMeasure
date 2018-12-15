using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SyncMeasure
{
    public partial class Combiner : Form
    {
        private readonly Handler _handler;

        public Combiner(Handler handler)
        {
            InitializeComponent();
            /* Remove up down arrows */
            numericUpDown1.Controls.RemoveAt(0);
            numericUpDown2.Controls.RemoveAt(0);
            _handler = handler;
            groupBox1.AllowDrop = groupBox2.AllowDrop = true;
        }

        ~Combiner()
        {
            _handler.RemoveFromR("file1", "file2", "dt1", "dt2", "size", "combined");
        }

        /// <summary>
        /// Select all text upon NumericUpDown enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown_Enter(object sender, EventArgs e)
        {
            if (!((sender as NumericUpDown)?.Controls[0] is TextBox))
                return;
            var tb = (TextBox) ((NumericUpDown) sender).Controls[0];
            BeginInvoke((Action) delegate { tb.SelectAll(); });         // must be executed async or otherwise won't work.
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBox1.Checked;
            if (!numericUpDown1.Enabled)
            {
                numericUpDown1.Value = 0;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown2.Enabled = checkBox2.Checked;
            if (!numericUpDown2.Enabled)
            {
                numericUpDown2.Value = 0;
            }
        }

        private void LoadFile(Label label, string filePath, string fileName)
        {
            label.Text = Path.GetFileNameWithoutExtension(filePath);
            var res = _handler.LoadAloneFileToR(fileName, filePath);
            if (!res.Status)
            {
                MessageBox.Show(this, res.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label.Visible = res.Status;
            mergeButton.Enabled = label1.Visible && label2.Visible;
        }

        private void loadButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            LoadFile(label1, openFileDialog.FileName, "file1");
        }

        private void loadButton2_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            LoadFile(label2, openFileDialog.FileName, "file2");
        }

        /// <summary>
        /// Allow the drag drop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aloneFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void groupBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var filePath = ((string[]) (e.Data.GetData(DataFormats.FileDrop)))[0];
                LoadFile(label1, filePath, "file1");
            }
        }

        private void groupBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var filePath = ((string[])(e.Data.GetData(DataFormats.FileDrop)))[0];
                LoadFile(label2, filePath, "file2");
            }
        }


        private void mergeButton_Click(object sender, EventArgs e)
        {
            if (!(label1.Visible && label2.Visible))
            {
                MessageBox.Show(this, @"Please load both files.", @"Merging", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            Application.UseWaitCursor = true;
            mergeButton.Enabled = false;
            var res = _handler.CombineAloneFiles(saveFileDialog.FileName, (int)numericUpDown1.Value, (int)numericUpDown2.Value);
            Application.UseWaitCursor = false;
            if (!res.Status)
            {
                MessageBox.Show(this, res.Message, @"Merging error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mergeButton.Enabled = true;
                return;
            }

            if (mergeCB.Checked && File.Exists(saveFileDialog.FileName))
            {
                Process.Start("explorer.exe", "/select, " + saveFileDialog.FileName);
            }
            Close();
        }

    }
}



