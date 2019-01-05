using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SyncMeasure
{
    public partial class InputBox : Form
    {
        private string _lastText;
        private readonly bool _unsigned;

        public InputBox(string message, string title, int defaultValue, bool unsigned = false)
        {
            InitializeComponent();
            Text = title;
            label.Text = message;
            numericUpDown.Maximum = int.MaxValue;
            numericUpDown.Minimum = unsigned ? 0 : int.MinValue;
            numericUpDown.Controls[1].Enter += textBox_Enter;
            numericUpDown.Controls[1].TextChanged += textBox_TextChanged;
            _lastText = numericUpDown.Controls[1].Text;
            _unsigned = unsigned;
            try
            {
                numericUpDown.Value = defaultValue;
            }
            catch (Exception)
            {
                // Don't care
            }
        }

        public int GetValue()
        {
            return (int) numericUpDown.Value;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void apply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Select all text on enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Enter(object sender, EventArgs e)
        {
            if (!(sender is TextBox tb))
                return;
            BeginInvoke((Action)delegate { tb.SelectAll(); });
        }

        /// <summary>
        /// Allow only single '-' in the beginning of the string.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox tb))
                return;
            string regStr = _unsigned ? "^[0-9]*$" : "^-?[0-9]*$";
            var regex = new Regex(regStr);    // match to integer (signed/unsigned)
            if (!regex.IsMatch(tb.Text))
            {
                tb.Text = _lastText;        // recover last valid text.
                tb.Focus();                 // set cursor at the end of the string.
                tb.SelectionStart = tb.Text.Length;
            }
            else
            {
                _lastText = tb.Text;
            }
        }


    }
}
