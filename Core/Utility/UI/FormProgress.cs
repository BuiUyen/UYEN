using DevComponents.DotNetBar;
using System;
using System.Windows.Forms;
using Sanita.Utility.ExtendedThread;
using Sanita.Utility.UI;

namespace Sanita.Utility.UI
{
    public partial class FormProgress : FormBase
    {
        private double TotalTimeLeft = 0;
        public ExBackgroundWorker mWorker = null;

        public FormProgress()
        {
            InitializeComponent(); this.Translate(); this.UpdateUI();
        }

        public void DoClose()
        {
            mTimer.Enabled = false;
            TotalTimeLeft = 0;

            this.Visible = false;
            this.Close();
        }

        private void Progress_TextChanged(object sender, System.EventArgs e)
        {
            if (Progress.Focused)
            {
                lblTongSo.Focus();
            }
            if (Progress.Text == "")
            {
                this.Text = "Progress";
                lblProgress.Text = "";
            }
            else
            {
                this.Text = "Progress - " + Progress.Text;
                lblProgress.Text = Progress.Text;
            }
        }

        private void FormProgress_Load(object sender, System.EventArgs e)
        {
            lblTongSo.Focus();

            TotalTimeLeft = 0;
            mTimer.Enabled = true;

            lblTongSo.Text = "";
            lblDaXuLy.Text = "";
            lblConLai.Text = "";

            lblTocDoXuLy.Text = "";
            lblTongThoiGian.Text = "";
            lblThoiGianConLai.Text = "";

            if (mWorker == null)
            {
                btnTamDung.Enabled = false;
                btnThoat.Enabled = false;
            }
        }

        private void mTimer_Tick(object sender, System.EventArgs e)
        {
            TotalTimeLeft += mTimer.Interval;

            if (Progress.Value > 0)
            {
                double TimeLeft = ((double)(Progress.Maximum - Progress.Value) * TotalTimeLeft) / (double)Progress.Value;
                TimeSpan time_left = TimeSpan.FromMilliseconds(TimeLeft);

                //Update
                lblTongSo.Text = Progress.Maximum.ToString("N0");
                lblDaXuLy.Text = Progress.Value.ToString("N0");
                lblConLai.Text = (Progress.Maximum - Progress.Value).ToString("N0");

                lblTocDoXuLy.Text = ((double)Progress.Value * 1000 * 60 / TotalTimeLeft).ToString("N0");

                TimeSpan tongthoigian = TimeSpan.FromMilliseconds(TotalTimeLeft);
                lblTongThoiGian.Text = String.Format("{0:00}:{1:00}:{2:00}", tongthoigian.Hours, tongthoigian.Minutes, tongthoigian.Seconds);
                lblThoiGianConLai.Text = String.Format("{0:00}:{1:00}:{2:00}", time_left.Hours, time_left.Minutes, time_left.Seconds);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (mWorker == null)
            {
                return;
            }
            try
            {
                if (MessageBox.Show("Are you sure you want to stop this task ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    mWorker.CurrentThread.Suspend();
                    mWorker.StopImmediately();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Toast(this, "Dừng tiến trình thất bại !");
            }
        }

        private void btnTamDung_Click(object sender, EventArgs e)
        {
            if (mWorker == null)
            {
                return;
            }
            try
            {
                if (btnTamDung.Text == "Tạm Dừng".Translate())
                {
                    mTimer.Enabled = false;
                    mWorker.CurrentThread.Suspend();
                    btnTamDung.Text = "Tiếp Tục".Translate();
                }
                else
                {
                    mTimer.Enabled = true;
                    mWorker.CurrentThread.Resume();
                    btnTamDung.Text = "Tạm Dừng".Translate();
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Toast(this, "Dừng tiến trình thất bại !");
            }
        }

    }
}
