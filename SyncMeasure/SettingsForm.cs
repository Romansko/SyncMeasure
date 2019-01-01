using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SyncMeasure.Properties;

namespace SyncMeasure
{
    public partial class SettingsForm : Form
    {
        public enum ESettings
        {
            WEIGHTS,
            NAMES
        }

        private readonly Handler _handler;
        private readonly ESettings _settings;
        
        public SettingsForm(Handler handler, ESettings settings)
        {
            InitializeComponent();
            Text = settings.Equals(ESettings.NAMES) ? @"CSV File column names. (R Format)." : @"Sync Weights";
            _handler = handler;
            _settings = settings;
            defaultsButton.Visible = settings.Equals(ESettings.NAMES);
            LoadSettings();
        }

        /// <summary>
        /// Reflect user settings from handler.
        /// </summary>
        private void LoadSettings()
        {
            dataGridView.Rows.Clear();

            /* Build Header Row */
            dataGridView.Rows.Add();
            var row = dataGridView.Rows[0];
            row.ReadOnly = true;
            row.Cells[0].Style.BackColor = row.Cells[1].Style.BackColor = row.Cells[2].Style.BackColor = Color.Gray;
            row.Cells[1].Style.SelectionForeColor = Color.Black;
            row.Cells[0].Style.SelectionBackColor = row.Cells[1].Style.SelectionBackColor = row.Cells[2].Style.SelectionBackColor = Color.Gray;

            if (_settings.Equals(ESettings.NAMES))
            {
                var colNames = _handler.GetColNames();        
                row.Cells[0].Value = @"Column Name";
                row.Cells[1].Value = @"Column R Name";

                foreach (var n in colNames)
                {
                    dataGridView.Rows.Add(n.Key, n.Value);
                    var thisRow = dataGridView.Rows[dataGridView.Rows.Count - 1];
                    thisRow.Cells[0].Style.BackColor = Color.AliceBlue;
                    thisRow.Cells[2].ReadOnly = true;
                }
            }
            else    // Weights
            {
                var weights = _handler.GetWeights();
                row.Cells[0].Value = @"Weight Name";
                row.Cells[1].Value = @"Weight Value";
                row.Cells[2].Value = @"Enabled";               

                foreach (var w in weights)
                {
                    dataGridView.Rows.Add(w.Key, w.Value.ToString(CultureInfo.CurrentCulture));
                    row = dataGridView.Rows[dataGridView.Rows.Count - 1];
                    row.Cells[2] = new DataGridViewCheckBoxCell { Value = !row.Cells[1].Value.Equals("0") };
                    HandleWeightRow(row);
                }
            }


        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        /// <summary>
        /// On apply button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyButton_Click(object sender, EventArgs e)
        {
            if (_settings.Equals(ESettings.NAMES))
            {
                var colNames = _handler.GetColNames().Keys.ToList();
                foreach (var col in colNames)
                {
                    var colValue = GetColName(col);
                    if (!_handler.SetColName(col, colValue))
                    {
                        MessageBox.Show(this, @"Failed changing column name.", Resources.TITLE + @" - Column name changing failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else    // Weights
            {
                var arm = GetWeightValue(Resources.ARM);
                var elbow = GetWeightValue(Resources.ELBOW);
                var hand = GetWeightValue(Resources.HAND);
                var grab = GetWeightValue(Resources.GRAB);
                var pinch = GetWeightValue(Resources.PINCH);
                // var gesture = GetWeightValue(Resources.GESTURE);

                if (!_handler.SetWeight(arm, elbow, hand, grab, pinch, out var errMsg))
                {
                    MessageBox.Show(this, errMsg, Resources.TITLE + @" - Weight setting failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _handler.SaveUserSettings();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Try to get csv column name.
        /// </summary>
        /// <param name="colKey"></param>
        /// <returns></returns>
        private string GetColName(string colKey)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[0].Value.Equals(colKey))
                    {
                        return (string) row.Cells[1].Value;
                    }
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Try to get weight from weightName.
        /// </summary>
        /// <param name="weightName"></param>
        /// <returns></returns>
        private double GetWeightValue(string weightName)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[0].Value.Equals(weightName))
                    {
                        return double.Parse((string) row.Cells[1].Value);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// On set defaults button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defaultsButton_Click(object sender, EventArgs e)
        {
            var msg = @"Set New Format Column Names?" + Environment.NewLine + @"Yes for New format. No for old format.";
            var res = MessageBox.Show(this, msg, @"Set Defaults", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (res.Equals(DialogResult.Yes))
            {
                _handler.SetNewFormatDefaults();
            }
            else if (res.Equals(DialogResult.No))
            {
                _handler.SetOldFormatDefaults();
            }
            LoadSettings();
        }

        /// <summary>
        /// Check and validate user input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.RowIndex < 1 || e.ColumnIndex != 1 || !(sender is DataGridView))
            {
                return;
            }

            var dgv = (DataGridView) sender;

            if (dgv[1, e.RowIndex].ReadOnly)
            {
                return; // Header rows.
            }

            /* Don't allow invalid weights input */
            if (_settings.Equals(ESettings.WEIGHTS))
            {
                var regex = new Regex("(0.\\d+$)|0$|1$");
                if (!regex.IsMatch((string) e.Value))
                {
                    e.Value = dgv[1, e.RowIndex].Value; // old value.
                    e.ParsingApplied = true;
                    MessageBox.Show(this, @"Invalid weight value!", Resources.TITLE + @" - Invalid Weight",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Math.Abs(Convert.ToDouble(e.Value)) < 0.0001)
                {
                    dgv[2, e.RowIndex].Value = false;
                    e.ParsingApplied = true;
                }
                return;
            }

            /* Col Names */
            if (_settings.Equals(ESettings.NAMES))
            {
                e.Value = _handler.CreateValidColumnName((string)e.Value);
                e.ParsingApplied = true;
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            (sender as DataGridView)?.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2 || e.RowIndex < 0 || !(sender is DataGridView))
                return;

            var dgv = (DataGridView) sender;
            if (_settings.Equals(ESettings.WEIGHTS))
                HandleWeightRow(dgv.Rows[e.RowIndex]);
        }

        /// <summary>
        /// Enable / Disable weight row.
        /// </summary>
        /// <param name="row"></param>
        private void HandleWeightRow(DataGridViewRow row)
        {
            if (row.Cells.Count != 3 || !(row.Cells[2] is DataGridViewCheckBoxCell))
                return;
            bool weightActive = (bool) row.Cells[2].Value;
            if (!weightActive)
            {
                row.Cells[1].Value = "0";
                row.Cells[1].ReadOnly = true;
                row.Cells[1].Style.BackColor = Color.LightGray;
            }
            else
            {
                row.Cells[1].ReadOnly = false;
                row.Cells[1].Style.BackColor = new Color();
            }
        }
    }
}
