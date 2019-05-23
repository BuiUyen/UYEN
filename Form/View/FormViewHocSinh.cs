using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Medibox.Model;
using Medibox.Presenter;
using Sanita.Utility;
using Sanita.Utility.ExtendedThread;
using Sanita.Utility.UI;

namespace Medibox
{
    public partial class FormViewHocSinh : FormBase
    {
        private const String TAG = "FormViewHocSinh";
        //Private
        private FormProgress mProgress = new FormProgress();
        private ExBackgroundWorker mThread;

        

        private enum ProcessingType
        {
            LoadData,
        }

        public FormViewHocSinh()
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
            DoRefresh();
        }

        #region Worker thread

        private void bwAsync_Start(ProcessingType type)
        {
            if (!mThread.IsBusy)
            {
                if (type == ProcessingType.LoadData)
                {
                    DataProgress.Visible = DataProgress.IsRunning = true;
                }
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
            DataProgress.Visible = DataProgress.IsRunning = false;
            mProgress.DoClose();
            ProcessingType type = (ProcessingType)e.Result;

            switch (type)
            {
                case ProcessingType.LoadData:
                    {
                        //UtilityListView.ListViewRefresh(mListViewData, mListHocSinh);
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
                case ProcessingType.LoadData:
                    {
                        //mListHocSinh = HocSinhPresenter.GetHocSinhs();
                        //foreach (HocSinh HS in mListHocSinh)
                        //{
                        //    string[] cut = HS.HovaTen.Split(' ');
                        //    HS.Ten = cut[cut.Length - 1];
                        //}
                        }
                        break;
                default:
                    break;
            }
        }

        #endregion

        private void DoRefresh()
        {
            bwAsync_Start(ProcessingType.LoadData);
        }

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
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //using (FormEditHocSinh form = new FormEditHocSinh(null, mListHocSinh))
            //{
            //    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        DoRefresh();
            //    }
            //}
        }

        private void btnRefresh_Click(object sender, EventArgs e) // nhấn 'Làm mới'
        {
            DoRefresh();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //HocSinh data = mListViewData.SelectedObject as HocSinh;
            //if (data != null && data.HocSinhID > 0)
            //{
            //    if (SanitaMessageBox.Show("Bạn có chắc chắn muốn xóa học sinh này không ?", "Thông Báo", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        HocSinhPresenter.DeleteHocSinh(data);
            //        DoRefresh();
            //    }
            //}
        }

        private void mListViewData_CellClick(object sender, CellClickEventArgs e)
        {
            //HocSinh data = mListViewData.SelectedObject as HocSinh;

            //using (FormThongTinHS form = new FormThongTinHS(data))
            //{
            //    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        DoRefresh();
            //    }
            //}         
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}