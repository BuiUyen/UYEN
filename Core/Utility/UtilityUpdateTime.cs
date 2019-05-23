using System;
using System.ComponentModel;
using System.Net.Sockets;
using Sanita.Utility.ExtendedThread;
using Sanita.Utility.Logger;
using Medibox.Model;
using System.Net.NetworkInformation;
using Sanita.Utility.Encryption;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Medibox.Presenter;
using Sanita.Utility;
using System.IO;
using System.Data;

namespace Medibox.Utility
{
    public class UtilityUpdateTime
    {
        //public
        public static UtilityUpdateTime mInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UtilityUpdateTime();
                }
                else
                {
                    //Check busy
                    if (_instance.IsBusy)
                    {
                        SanitaLogEx.e(TAG, "Current instance busy -> Create new instance");

                        _instance.StopTask();
                        _instance = new UtilityUpdateTime();
                    }
                }

                return _instance;
            }
        }

        //Private
        private const String TAG = "UtilityUpdateTime";
        private ExBackgroundWorker m_BackgroundWorker;
        private static UtilityUpdateTime _instance = null;

        //Contructor
        public UtilityUpdateTime()
        {
            //Create worker
            m_BackgroundWorker = new ExBackgroundWorker();
            m_BackgroundWorker.WorkerReportsProgress = true;
            m_BackgroundWorker.WorkerSupportsCancellation = true;
            m_BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bwAsync_WorkerChanged);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAsync_WorkerCompleted);
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(bwAsync_Worker);
        }

        public bool IsBusy
        {
            get
            {
                return m_BackgroundWorker.IsBusy;
            }
        }

        public void StopTask()
        {
            SanitaLogEx.e(TAG, "Stop task...");

            try
            {
                if(m_BackgroundWorker.CurrentThread!=null)
                {
                    m_BackgroundWorker.CurrentThread.Suspend();
                    m_BackgroundWorker.StopImmediately();
                }
            }
            catch(Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }
        }

        #region Worker thread

        private void bwAsync_Start()
        {
            if (!m_BackgroundWorker.IsBusy)
            {
                m_BackgroundWorker.RunWorkerAsync();
            }
            else
            {
                SanitaLogEx.e(TAG, "Task busy...");
            }
        }

        private void bwAsync_WorkerChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void bwAsync_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void bwAsync_Worker(object sender, DoWorkEventArgs e)
        {
            SanitaLogEx.e(TAG, "Start get timer...");

            try
            {
                using (IDbConnection connection = SoftUpdatePresenter.GetConnection())
                {
                    //Open connection
                    connection.Open();

                    //Get time
                    DateTime dt = SoftUpdatePresenter.GetCurrentTime(null, null);

                    //Close connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }
        }

        #endregion

        #region Public

        public void Start()
        {
            //Start service
            bwAsync_Start();
        }

        #endregion
    }
}
