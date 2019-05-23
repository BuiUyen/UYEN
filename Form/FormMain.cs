using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Medibox.Model;
using Medibox.Presenter;
using Sanita.Utility;
using Sanita.Utility.Logger;
using Sanita.Utility.SplashScreen;
using System.IO;
using System.Linq;
using Medibox.Utility;

namespace Medibox
{
    public partial class FormMain : FormBase
    {
        //Constant
        private const String TAG = "FormMain";

        //Private
        private static readonly Object lockAlert = new Object();
        private TaiKhoan mTaiKhoan = new TaiKhoan();

        #region Init

        public FormMain(TaiKhoan _TaiKhoan)
        {
            //Init control
            InitializeComponent();
            base.DoInit();
            this.Translate();
            this.UpdateUI();
            mTaiKhoan = _TaiKhoan;

            UtilityCache.mInstance.Start();
            while (!UtilityCache.mInstance.IsCacheCompleted)
            {
                System.Threading.Thread.Sleep(100);
            }

            //Init
            this.Text = "Project - " + MyVar.mAppVersion + " - " + MyVar.mAppDescription;
            CurrentTimer.Enabled = true;

            //Init form            
            txtStatusWeb_LinkClicked(null, null);

            //Load time
            CurrentTimer_Tick(null, null);

            //txtLinkWeb.Text = "http://" + SystemInfo.IPAddress1 + ":" + UtilityWebServer.mInstance.WebServerPort;

            //Start timer
            UtilityUpdateTime.mInstance.Start();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
        }

        private void FormMedibox_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region MENU


        private void CurrentTimer_Tick(object sender, EventArgs e)
        {

        }

        private void NOW_Timer_Tick(object sender, EventArgs e)
        {
            SystemInfo.NOW = SystemInfo.NOW.AddMilliseconds(10);
        }

        private async void txtStatusWeb_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (UtilityWebServer.mInstance.WebServerRunnning)
            {
                try
                {
                    await UtilityWebServer.mInstance.StopWebServer();
                }
                catch (Exception ex)
                {
                    SanitaLog.e(TAG, ex);
                }
            }
            else
            {
                try
                {
                    int num = await UtilityWebServer.mInstance.StartWebServer();
                }
                catch (Exception ex)
                {
                    SanitaLog.e(TAG, ex);
                }
            }
        }
        #endregion
      
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            using (FormLogin form = new FormLogin())
            {
                this.Hide();
                form.ShowDialog();
                this.Close();
            }
        }

        private void btnQLTaiKhoan_Click(object sender, EventArgs e)
        {
            using (FormQLTaiKhoan form = new FormQLTaiKhoan())
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            using (FormViewNhanVien form = new FormViewNhanVien())
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            using (FormEditMatKhau form = new FormEditMatKhau(mTaiKhoan))
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            using (FormViewPhong2 form = new FormViewPhong2())
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            using (FormViewDangKi form = new FormViewDangKi())
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }

        private void btnTraPhong_Click(object sender, EventArgs e)
        {
            using (FormViewTraPhong form = new FormViewTraPhong())
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }
        
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            using (FormViewKHHienTai form = new FormViewKHHienTai())
            {
                form.ShowDialog();
            }
        }

        private void btnLoaiPhong_Click(object sender, EventArgs e)
        {
            using (FormViewLoaiPhong form = new FormViewLoaiPhong())
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }

        private void btnQLDichVu_Click(object sender, EventArgs e)
        {
            using (FormViewDichVu form = new FormViewDichVu())
            {
                this.Hide();
                form.ShowDialog();
                this.Show();
            }

        }

        private void btnTCKH_Click(object sender, EventArgs e)
        {
            using (FormViewKhachHang form = new FormViewKhachHang())
            {
                form.ShowDialog();
            }
        }



        
    }
}