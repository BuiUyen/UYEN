using Medibox.Presenter;
using DevComponents.DotNetBar;
using Sanita.Utility.ExtendedThread;
using Sanita.Utility.UI;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Medibox
{
    public partial class FormConfigSchema : FormBase
    {
        //Background Worker
        private ExBackgroundWorker m_BackgroundWorker;
        private bool isOK = true;
        public bool isFromStartup = false;
        public bool isNeedbackup = false;

        private DialogResult result = DialogResult.Cancel;

        public FormConfigSchema()
        {
            InitializeComponent(); this.Translate(); this.UpdateUI(); base.DoInit();

            //Create worker
            m_BackgroundWorker = new ExBackgroundWorker();
            m_BackgroundWorker.WorkerReportsProgress = true;
            m_BackgroundWorker.WorkerSupportsCancellation = true;
            m_BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bwAsync_WorkerChanged);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAsync_WorkerCompleted);
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(bwAsync_Worker);
        }

        #region Worker thread

        private void bwAsync_Start()
        {
            if (!m_BackgroundWorker.IsBusy)
            {
                m_BackgroundWorker.RunWorkerAsync();
            }
        }

        private void bwAsync_WorkerChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage == 0)
                {
                    printProgress.TextVisible = true;
                    printProgress.Value = 0;
                    if (e.UserState is int)
                    {
                        printProgress.Maximum = (int)e.UserState;
                    }
                }
                else if (e.ProgressPercentage < 0)
                {
                    if (e.UserState is string)
                    {
                        txtStatus.Text = e.UserState.ToString();
                    }

                    isOK = false;
                    SanitaMessageBox.Show("Không thể nâng cấp cơ sở dữ liệu !", "Nâng Cấp Cơ Sở Dữ Liệu");
                }
                else
                {
                    if (e.UserState is string)
                    {
                        txtStatus.Text = e.UserState.ToString();
                    }

                    printProgress.Value = e.ProgressPercentage;
                    printProgress.Text = string.Format("{0}%", (printProgress.Value * 100) / printProgress.Maximum);
                }
            }
            catch
            {
            }
        }

        private void bwAsync_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isOK)
            {
                result = System.Windows.Forms.DialogResult.OK;
                if (!isFromStartup)
                {
                    SanitaMessageBox.Show("Đã nâng cấp thành công !", "Nâng Cấp Cơ Sở Dữ Liệu");
                }
            }

            this.DialogResult = result;
            this.Close();
        }

        private void bwAsync_Worker(object sender, DoWorkEventArgs e)
        {
            if (SoftUpdatePresenter.SynchDatabase(m_BackgroundWorker, 0))
            {

            }
        }

        #endregion

        #region Public Method

        private void FormPrintProgress_Load(object sender, EventArgs e)
        {
            bwAsync_Start();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (m_BackgroundWorker.IsBusy)
            {
                m_BackgroundWorker.CurrentThread.Suspend();
                m_BackgroundWorker.StopImmediately();
            }
            this.Close();
        }

        #endregion

    }
}
