using System.Windows.Forms;
using SyncMeasure.Properties;

namespace SyncMeasure
{
    public partial class ControlsForm : Form
    {
        public ControlsForm()
        {
            InitializeComponent();
            richTextBox.Rtf = Resources.Controls;
            Text = Resources.TITLE + @" Controls";
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
