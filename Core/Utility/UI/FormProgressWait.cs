using System;
using DevComponents.DotNetBar;

namespace Sanita.Utility.UI
{
    public partial class FormProgressWait : System.Windows.Forms.Form
    {
        public FormProgressWait()
        {
            InitializeComponent();
            this.UpdateUI();

            mProgress.IsRunning = true;
        }

        public void DoClose()
        {
            this.Visible = false;
            this.Close();            
        }
    }
}
