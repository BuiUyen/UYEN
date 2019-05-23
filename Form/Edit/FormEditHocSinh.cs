using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Medibox.Model;
using Medibox.Presenter;
using Sanita.Utility;
using Sanita.Utility.ExtendedThread;
using Sanita.Utility.UI;

namespace Medibox
{
    public partial class FormEditHocSinh : FormBase
    {
        private const String TAG = "FormEditHocSinh";
        //Private
        private FormProgress mProgress = new FormProgress();
        private ExBackgroundWorker mThread;
        

        private enum ProcessingType
        {
            SaveData,
        }

        public FormEditHocSinh()
        {
            InitializeComponent();
            this.Translate();
            this.UpdateUI();
            base.DoInit();

            //Create worker
            mThread = new ExBackgroundWorker();
            mThread.WorkerReportsProgress = true;
            mThread.WorkerSupportsCancellation = true;
            mThread.ProgressChanged += new ProgressChangedEventHandler(bwAsync_WorkerChanged);
            mThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAsync_WorkerCompleted);
            mThread.DoWork += new DoWorkEventHandler(bwAsync_Worker);

            //mHocSinh = _mHocSinh ?? new HocSinh();
            //mListHocSinh = _mListHocSinh;
                                   
            //txtHovaTen.Text = mHocSinh.HovaTen;            
            //maskedTextBox1.Text = String.Format("{0:d}", mHocSinh.NamSinh);
        }

        #region Worker thread

        private void bwAsync_Start(ProcessingType type)
        {
            if (!mThread.IsBusy)
            {
                mThread.RunWorkerAsync(type);
            }
        }

        private void bwAsync_WorkerChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage == -123456)
                {
                    mProgress = new FormProgress();
                    mProgress.Progress.TextVisible = false;
                    mProgress.ShowDialog();
                }
                else if (e.ProgressPercentage == 0)
                {
                    mProgress.Progress.Value = 0;
                    mProgress.Progress.Maximum = (int)e.UserState;
                }
                else if (e.ProgressPercentage > 0)
                {
                    mProgress.Progress.Value = e.ProgressPercentage;
                    mProgress.Progress.Text = string.Format("{0}%", (mProgress.Progress.Value * 100) / mProgress.Progress.Maximum);
                }
                else if (e.ProgressPercentage < 0)
                {
                    SanitaMessageBox.Show("Có lỗi xảy ra !", "Thông Báo".Translate());
                }
            }
            catch
            {
            }
        }

        private void bwAsync_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mProgress.DoClose();
            ProcessingType type = (ProcessingType)e.Result;

            switch (type)
            {
                case ProcessingType.SaveData:
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        private void bwAsync_Worker(object sender, DoWorkEventArgs e)
        {
            ProcessingType type = (ProcessingType)e.Argument;
            e.Result = type;

            switch (type)
            {
                case ProcessingType.SaveData:
                    {
                        //if (mHocSinh.HocSinhID > 0)
                        //{
                        //    HocSinhPresenter.UpdateHocSinh(mHocSinh);
                        //}
                        //else
                        //{                           
                        //    HocSinhPresenter.InsertHocSinh(mHocSinh);
                        //}
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S:
                    btnOK_Click(this, null);
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Database_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Validate
            if ( String.IsNullOrEmpty(txtHovaTen.Text.Trim()) || (radioButtonNam.Checked == false && radioButtonNu.Checked ==false))
            {
                SanitaMessageBox.Show("Chưa nhập đủ thông tin!!!", "Thông Báo".Translate());
                txtHovaTen.Focus();
                return;
            }            
            
            //mHocSinh.HovaTen = txtHovaTen.Text;
            //if (radioButtonNam.Checked == true)
            //{
            //    mHocSinh.GioiTinh = "Nam";
            //}
            //else mHocSinh.GioiTinh = "Nữ";
            //CultureInfo ci = new CultureInfo("en-IE");
            //DateTime a = new DateTime();
            //if (!DateTime.TryParseExact(maskedTextBox1.Text, "dd/MM/yyyy", ci, DateTimeStyles.None, out a))
            //{

            //    SanitaMessageBox.Show("Lỗi ngày tháng năm sinh !", "Thông Báo".Translate());

            //}
            //else
            //{
            //    mHocSinh.NamSinh = String.Format("'{0:d}'", a); ;
            //    bwAsync_Start(ProcessingType.SaveData);
            //}            
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 0x0D)
            {
                SendKeys.SendWait("{TAB}");
            }
        }

    }
}