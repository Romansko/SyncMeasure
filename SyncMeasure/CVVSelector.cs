using System.Windows.Forms;

namespace SyncMeasure
{
    public partial class CvvSelector : Form
    {
        private readonly Handler _handler;
        public CvvSelector(Handler handler)
        {
            InitializeComponent();
            _handler = handler;
            var cvv = _handler.GetCvvMethod();
            if (cvv == Handler.ECvv.CVV)
            {
                regularCvv.Checked = true;
            }
            else if (cvv == Handler.ECvv.ABS_CVV)
            {
                absCvv.Checked = true;
            }
            else
            {
                squareSvv.Checked = true;
            }
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            if (regularCvv.Checked)
            {
                _handler.SetCvvMethod(Handler.ECvv.CVV);
            }
            else if (absCvv.Checked)
            {
                _handler.SetCvvMethod(Handler.ECvv.ABS_CVV);
            }
            else
            {
                _handler.SetCvvMethod(Handler.ECvv.SQUARE_CVV);
            }
            _handler.SaveUserSettings();
            Close();
        }
    }
}
