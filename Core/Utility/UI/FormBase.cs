using System;
using System.Windows.Forms;
using Sanita.Utility;
using System.Drawing;
using System.IO;
using Medibox.Model;

namespace System
{
    public partial class FormBase : Form
    {
        public bool mIsDiposed = false;
        private String OrgText = "";

        public FormBase()
        {
            InitializeComponent();
        }       

        public void DoInit_ClearText()
        {
            OrgText = "";
        }

        public void DoInit()
        {            
            if (String.IsNullOrEmpty(OrgText))
            {
                OrgText = this.Text;
            }

            this.Icon = MediboxAssistant.Properties.Resources.icon;
            this.Text = "Medibox - " + OrgText.Translate();

            this.Translate(); 
            this.UpdateUI();            

            if (this.Width >= 1000 && this.MaximizeBox)
            {
                if (SystemInformation.PrimaryMonitorSize.Width <= 1366)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    //this.Width = 1366;
                    //this.CenterToScreen();
                }
            }
        }

        private void FormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            mIsDiposed = true;
        }
    }
}