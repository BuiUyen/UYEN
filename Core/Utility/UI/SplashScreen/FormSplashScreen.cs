using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Medibox.Model;

namespace Sanita.Utility.SplashScreen
{
    public partial class FormSplashScreen : Form, ISplashForm
    {
        public FormSplashScreen()
        {
            InitializeComponent();
            txtProgress.IsRunning = true;
            txtTitle.Focus();

            txtHeader.Visible = false;
            txtTitle.Text = "Medibox Assistant";

            this.BackColor = Color.FromArgb(68, 144, 164);
            txtProgress.BackColor = Color.FromArgb(68, 144, 164);
        }

        public String Status
        {
            set
            {
                //txtHeader.Visible = true;
                txtLogo.Visible = true;
                txtTitle.Visible = true;
                txtProgress.Visible = true;
                lbStatusInfo.Visible = true;

                lbStatusInfo.Text = value;
            }
        }

        #region ISplashForm

        void ISplashForm.SetStatusInfo(string NewStatusInfo)
        {
            //txtHeader.Visible = true;
            txtLogo.Visible = true;
            txtTitle.Visible = true;
            txtProgress.Visible = true;
            lbStatusInfo.Visible = true;

            lbStatusInfo.Text = NewStatusInfo;
        }

        #endregion
    }
}