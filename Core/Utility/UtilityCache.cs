using Medibox.Model;
using Medibox.Presenter;
using Sanita.Utility;
using Sanita.Utility.ExtendedThread;
using Sanita.Utility.Logger;
using Sanita.Utility.SplashScreen;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Medibox.Utility
{
    public class UtilityCache
    {
        private enum ProcessingType
        {
            Doing = 0,
        }

        private const String TAG = "UtilityCache";
        private ExBackgroundWorker m_AsyncWorker = new ExBackgroundWorker();
        public bool IsCacheCompleted = false;

        //Singleton
        private static UtilityCache _UtilityCache;
        public static UtilityCache mInstance
        {
            get
            {
                if (_UtilityCache == null)
                {
                    _UtilityCache = new UtilityCache();
                }
                return _UtilityCache;
            }
        }

        public UtilityCache()
        {
            //Worker thread
            m_AsyncWorker.WorkerReportsProgress = true;
            m_AsyncWorker.WorkerSupportsCancellation = true;
            m_AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(bwAsync_ProgressChanged);
            m_AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAsync_RunWorkerCompleted);
            m_AsyncWorker.DoWork += new DoWorkEventHandler(bwAsync_DoWork);
        }

        private void PostMessage(String message)
        {

        }

        #region WorkerThread

        private void bwAsync_Start(ProcessingType type)
        {
            if (!m_AsyncWorker.IsBusy)
            {
                m_AsyncWorker.RunWorkerAsync(type);
            }
        }

        private void bwAsync_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch mStopWatch = new Stopwatch();
            mStopWatch.Start();

            SanitaLogEx.d(TAG, "Start database cache...");
            PostMessage("Start database cache ...");
            IsCacheCompleted = false;

            try
            {
                //Update timer
                SystemInfo.NOW = SoftUpdatePresenter.GetCurrentTime(null, null);

                //Kiểm tra và nâng cấp cấu trúc database
                Splasher.Status = "Check and update database...";
                SoftUpdatePresenter.DoUpdateDatabaseSQL();                                

                using (IDbConnection connection = SoftUpdatePresenter.GetConnection())
                {
                    //Open connection
                    connection.Open();

                    //Begin transtation
                    using (IDbTransaction trans = connection.BeginTransaction())
                    {

                        MyVar.mListLoaiPhong = LoaiPhongPresenter.GetLoaiPhongs();






                        
                        //-----------------------------------------------------------------------------

                        //Commit transtation
                        trans.Commit();

                        //Close connection
                        connection.Close();
                    }
                }

                SanitaLogEx.d(TAG, "End database cache...");
            }
            catch (Exception ex)
            {
                SanitaLogEx.e(TAG, "bwAsync_DoWork error !", ex);
            }

            IsCacheCompleted = true;
        }

        private void bwAsync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                PostMessage("End database cache !");
            }
            catch (Exception ex)
            {
                SanitaLogEx.e(TAG, ex);
            }
        }

        private void bwAsync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        #endregion

        #region Public

        public void Start()
        {
            IsCacheCompleted = false;
            bwAsync_Start(ProcessingType.Doing);
        }

        #endregion
    }
}