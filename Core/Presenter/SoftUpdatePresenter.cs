using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Medibox.Database;
using Medibox.Model;
using Sanita.Utility.Database.BaseDao;
using Sanita.Utility.Database.Utility;
using Sanita.Utility.ExtendedThread;
using Sanita.Utility.Logger;

namespace Medibox.Presenter
{
    public class SoftUpdatePresenter : BasePresenter
    {
        private const String TAG = "SoftUpdatePresenter";

        protected static DatabaseUtility mDatabaseUtility = MediboxDatabaseUtility.GetDatabaseUtility();
        protected static DatabaseImplementUtility mLocalDatabase = mDatabaseUtility.GetDatabaseImplementUtility();

        public static void InitDatabase()
        {
            MediboxDatabaseUtility.InitDatabase();
        }

        public static void SetConnectionConfig(String host, String user, String password, String database, String port)
        {
            try
            {
                MyVar.USE_HOST = host;
                MyVar.USE_PORT = int.Parse(port);
            }
            catch (Exception ex)
            {

            }

            mDatabaseUtility.SetConnectionConfig(host, user, password, database, port);
            mLocalDatabase = mDatabaseUtility.GetDatabaseImplementUtility();
        }

        public static bool CheckConnection(String host, String user, String password, String database, String port)
        {
            return mDatabaseUtility.CheckConnection(host, user, password, database, port);
        }

        public static IDbConnection GetConnection()
        {
            return baseDAO.GetConnection();
        }

        public static DateTime GetCurrentTime(IDbConnection connection, IDbTransaction trans)
        {
            return mDatabaseUtility.GetCurrentTime(connection, trans);
        }

        public static bool IsDatabaseOK()
        {
            return mLocalDatabase.isDatabaseOK();
        }

        public static bool SynchDatabase(ExBackgroundWorker worker, int number_hosobenhan)
        {
            return mLocalDatabase.SynchDatabase(null, null, worker, number_hosobenhan, true);
        }

        public static void InitNotification(IList<String> mListChannel, OnDatabaseNotificationHandler mCallback)
        {
            mLocalDatabase.InitNotification(mListChannel, mCallback);
        }

        public static void DoNotification(String chanel, String data)
        {
            mLocalDatabase.DoNotification(chanel, data);
        }

        public static void DoUpdateDatabaseSQL()
        {
            SanitaLog.d(TAG, "DoUpdateDatabaseSQL--START");

            try
            {
                IBaseDao baseDAO = MediboxDatabaseUtility.GetDatabaseDAO();

                //Get connection
                using (IDbConnection connection = baseDAO.GetConnection())
                {
                    //Open connection
                    connection.Open();

                    //Begin transtation
                    using (IDbTransaction trans = connection.BeginTransaction())
                    {
                        //Check sql
                        SoftUpdate mSoftUpdate = SoftUpdateDB.mInstance.GetSoftUpdate_Simple(connection, trans);
                        SanitaLog.d(TAG, "Database sql = " + mSoftUpdate.SoftUpdateSQL);
                        SanitaLog.d(TAG, "Software sql = " + MediboxDatabaseUtility.GetDatabaseVersion());

                        if (MediboxDatabaseUtility.GetDatabaseVersion().GetDouble() > mSoftUpdate.SoftUpdateSQL.GetDouble())
                        {
                            SanitaLog.d(TAG, "BEGIN UPDATE SQL");
                            int hosobenhan_partion = 0;

                            if (mLocalDatabase.SynchDatabase(connection, trans, null, hosobenhan_partion, false))
                            {
                                SanitaLog.d(TAG, "UPDATE SUCCESS !");
                                mSoftUpdate.SoftUpdateSQL = MediboxDatabaseUtility.GetDatabaseVersion();
                                if (mSoftUpdate.SoftUpdateID > 0)
                                {
                                    if (SoftUpdateDB.mInstance.UpdateSoftUpdate_SQL(connection, trans, mSoftUpdate) < 0)
                                    {
                                        SanitaLog.d(TAG, "UPDATE SQL VERSION TO DATABASE NG !");

                                        //Rollback
                                        trans.Rollback();
                                        connection.Close();
                                        return;
                                    }
                                }
                                else
                                {
                                    if (SoftUpdateDB.mInstance.InsertSoftUpdate(connection, trans, mSoftUpdate) < 0)
                                    {
                                        SanitaLog.d(TAG, "INSERT SQL VERSION TO DATABASE NG !");

                                        //Rollback
                                        trans.Rollback();
                                        connection.Close();
                                        return;
                                    }
                                }
                            }

                            SanitaLog.d(TAG, "END UPDATE SQL");
                        }                  

                        //-----------------------------------------------------------------------------
                        //Commit transtation
                        trans.Commit();

                        //Close connection
                        connection.Close();
                    }
                }

                SanitaLog.d(TAG, "DoUpdateDatabaseSQL--END");
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }
        }
    }
}
