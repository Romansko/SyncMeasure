using System;
using System.Windows.Forms;

namespace SyncMeasure
{
    public partial class FormatDefaultMsgBox : Form
    {
        private Handler.EFormat _format;

        public FormatDefaultMsgBox()
        {
            InitializeComponent();
        }

        public Handler.EFormat GetFormat()
        {
            return _format;
        }

        private void newFormat_Click(object sender, EventArgs e)
        {
            _format = Handler.EFormat.NEW;
            Close();
        }

        private void oldFormat_Click(object sender, EventArgs e)
        {
            _format = Handler.EFormat.OLD;
            Close();
        }
    }
}
