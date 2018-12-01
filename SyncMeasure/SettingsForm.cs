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
        private readonly Handler _handler;
        public SettingsForm(Handler handler)
        {
            InitializeComponent();
            Text = Resources.TITLE + @" - Settings";
            _handler = handler;
            LoadSettings();
        }

        /// <summary>
        /// Reflect user settings from handler.
        /// </summary>
        private void LoadSettings()
        {
            dataGridView.Rows.Clear();;
            var weights = _handler.GetWeights();
            dataGridView.Rows.Add();
            var row = dataGridView.Rows[0];
            row.Cells[0].Value = @"Weight Name";
            row.Cells[1].Value = @"Weight Value";
            row.ReadOnly = true;
            row.Cells[0].Style.BackColor = row.Cells[1].Style.BackColor = Color.Gray;
            foreach (var w in weights)
            {
                dataGridView.Rows.Add(w.Key, w.Value.ToString(CultureInfo.CurrentCulture));
                dataGridView.Rows[dataGridView.Rows.Count-1].Cells[0].Style.BackColor = Color.AliceBlue;
            }
            var colNames = _handler.GetColNames();
            dataGridView.Rows.Add();
            row = dataGridView.Rows[dataGridView.Rows.Count - 1];
            row.Cells[0].Value = @"Column Name";
            row.Cells[1].Value = @"Column R Name";
            row.ReadOnly = true;
            row.Cells[0].Style.BackColor = row.Cells[1].Style.BackColor = Color.Gray;
            
            foreach (var n in colNames)
            {
                dataGridView.Rows.Add(n.Key, n.Value);
                dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0].Style.BackColor = Color.AliceBlue;
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
            var colNames = _handler.GetColNames().Keys.ToList();
            foreach (var col in colNames)
            {
                var colValue = GetColName(col);
                if (!_handler.SetColName(col, colValue))
                {
                    MessageBox.Show(@"Failed changing column name.", Resources.TITLE + @" - Column name changing failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            double arm = GetWeightValue(Resources.ARM);
            double elbow = GetWeightValue(Resources.ELBOW);
            double hand = GetWeightValue(Resources.HAND);
            double grab = GetWeightValue(Resources.GRAB);
            double gesture = GetWeightValue(Resources.GESTURE);

            if (!_handler.SetWeight(arm, elbow, hand, grab, gesture, out string errMsg))
            {
                MessageBox.Show(errMsg, Resources.TITLE + @" - Weight setting failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _handler.SaveUserSettings();
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
        private void defaults_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show(@"Are you sure?", @"Set defaults", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (res.Equals(DialogResult.Yes))
            {
                _handler.SetDefaults();
                LoadSettings();
            }
        }

        /// <summary>
        /// Check and validate user input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !(sender is DataGridView))
            {
                return;
            }

            var cell = ((DataGridView) sender)[e.ColumnIndex, e.RowIndex];

            if (cell.ReadOnly)
            {
                return;     // Header rows.
            }
            var maxWeightIndex = _handler.GetWeights().Count + 1;   // + 1 for Header row.

            /* Don't allow invalid weights input */
            if (e.RowIndex < maxWeightIndex)
            {
                Regex regex = new Regex("(0.\\d+$)|0$");
                if (!regex.IsMatch((string)e.Value))
                {
                    e.Value = cell.Value;       // old value.
                    e.ParsingApplied = true;
                    MessageBox.Show(@"Invalid weight value!", Resources.TITLE + @" - Invalid Weight",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            /* Col Names */
            e.Value = _handler.CreateValidColumnName((string)e.Value);
            e.ParsingApplied = true;
        }
    }
}
