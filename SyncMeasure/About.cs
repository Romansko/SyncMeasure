using System.Windows.Forms;
using SyncMeasure.Properties;

namespace SyncMeasure
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            richTextBox.Rtf = Resources.About;
            Text += Resources.TITLE;
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
