using System;
using System.Data;
using Npgsql;
using Sanita.Utility.Logger;
using Sanita.Utility.Database.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sanita.Utility.Database.BaseDao
{
    public class NpgsqlBaseDao : IBaseDao
    {
        //Public
        public bool DataBase_Ready = false;
        public bool Connection_String_Ready = false;        

        //Constant
        private const string TAG = "NpgsqlBaseDao";

        //Private
        private object lockDBObject = new object();
        private DatabaseImplementUtility mUtility;

        private string LocalConnectionString = "";
        private string Localserver = "";
        private string Localdatabase = "";
        private string Localuserid = "";
        private string Localpassword = "";
        private string Localport = "";
        private string PrivateKey = "";

        private NpgsqlConnection mNotifyConnection;
        private OnDatabaseNotificationHandler mOnDatabaseNotificationHandler;

        #region Implement Interface

        public int DoLock(IDbConnection connection, IDbTransaction trans, string table, string table_key, int key, bool IsPing)
        {
            //Error
            SanitaLog.e(TAG, "DoLock -> " + table + " -> " + key + " -> IsPing = " + IsPing);
            if (key <= 0)
            {
                return -100;
            }

            String sql = String.Format("SELECT pg_advisory_xact_lock({0}) FROM {1} WHERE {0} = {2};", table_key, table, key);

            //Chỉ cho phép lock ở trong transaction
            if (connection == null)
            {
                return -100;
            }

            if (IsPing)
            {
                if (DoTryLockPing(connection, trans, table, table_key, key) == 1)
                {
                    //OK
                }
                else
                {
                    return -100;
                }
            }
            else
            {
                //Try log
                if (DoTryLock(connection, trans, table, table_key, key) == 1)
                {
                    //OK
                }
                else
                {
                    return -100;
                }
            }

            //START-LOCK
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection as NpgsqlConnection;
                command.Transaction = trans as NpgsqlTransaction;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.CommandTimeout = 5;        //Timeout 5s

                try
                {
                    DataTable dt = new DataTable();
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        adapter.Fill(dt);

                        SanitaLog.e(TAG, "DoLock OK : " + sql);

                        //Insert vào DB PID lock
                        {

                        }

                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    //Error
                    SanitaLog.e(TAG, "DoLock error : " + sql, ex);
                    SanitaLog.d(TAG, "Table = " + table);
                    SanitaLog.d(TAG, "Key = " + key);
                    return -100;
                }
            }
        }

        public int DoTryLock(IDbConnection connection, IDbTransaction trans, string table, string table_key, int key)
        {
            String sql = String.Format("SELECT pg_try_advisory_xact_lock({0}) FROM {1} WHERE {0} = {2};", table_key, table, key);

            //Chỉ cho phép lock ở trong transaction
            if (connection == null)
            {
                return -100;
            }

            int time = 0;
            while (true)
            {
                SanitaLog.e(TAG, "DoTryLock -> " + table + " -> " + key);

                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection as NpgsqlConnection;
                    command.Transaction = trans as NpgsqlTransaction;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 5;        //Timeout 5s

                    try
                    {
                        DataTable dt = new DataTable();
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);

                            if (dt != null)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    String query = "";

                                    if (row["pg_try_advisory_xact_lock"] != null)
                                    {
                                        query = row["pg_try_advisory_xact_lock"].ToString();
                                        if (query == "True")
                                        {
                                            SanitaLog.e(TAG, "DoTryLock OK -> " + table + " -> " + key);
                                            return 1;
                                        }
                                    }
                                }
                            }
                        }

                        SanitaLog.e(TAG, "DoTryLock NG -> " + table + " -> " + key);

                        //Log NG
                        SanitaLog.d(TAG, "Table = " + table);
                        SanitaLog.d(TAG, "Table_key = " + table_key);
                        SanitaLog.d(TAG, "Key = " + key);

                        //Sleep 1s
                        System.Threading.Thread.Sleep(1000);
                        time++;
                        if (time >= 20)
                        {
#if false
                            //Đóng tất cả kết nối đang khóa và thử lại                            
                            DoClearAllLock(connection, trans);
                            retry = 1;
#else
                            if (table == "tb_room" || table == "tb_dm_sampletype")
                            {
                                DoClearLock(connection, trans, table);
                            }

                            //Error
                            SanitaLog.e(TAG, "DoTryLock timeout : " + sql);
                            return -100;
#endif
                        }
                    }
                    catch (Exception ex)
                    {
                        //Error
                        SanitaLog.e(TAG, "DoTryLock Exception : " + sql, ex);
                        SanitaLog.d(TAG, "Table = " + table);
                        SanitaLog.d(TAG, "Key = " + key);
                        return -100;
                    }
                }
            }
        }

        public int DoTryLockPing(IDbConnection connection, IDbTransaction trans, string table, string table_key, int key)
        {
            String sql = String.Format("SELECT pg_try_advisory_xact_lock({0}) FROM {1} WHERE {0} = {2};", table_key, table, key);

            //Chỉ cho phép lock ở trong transaction
            if (connection == null)
            {
                return -100;
            }

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection as NpgsqlConnection;
                command.Transaction = trans as NpgsqlTransaction;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.CommandTimeout = 5;        //Timeout 5s

                try
                {
                    DataTable dt = new DataTable();
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        adapter.Fill(dt);

                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                String query = "";

                                if (row["pg_try_advisory_xact_lock"] != null)
                                {
                                    query = row["pg_try_advisory_xact_lock"].ToString();
                                    if (query == "True")
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Error
                    SanitaLog.e(TAG, "DoTryLockPing Exception : " + sql, ex);
                    SanitaLog.d(TAG, "Table = " + table);
                    SanitaLog.d(TAG, "Key = " + key);
                    return -100;
                }
            }

            return -100;
        }

        public int DoUnLock(IDbConnection connection, IDbTransaction trans, string table, string table_key, int key)
        {
#if false
            String sql = String.Format("SELECT pg_advisory_unlock({0}) FROM {1};", key, table);

            //Chỉ cho phép unlock ở trong transaction
            if (connection == null)
            {
                return -100;
            }

            while (true)
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection as NpgsqlConnection;
                    command.Transaction = trans as NpgsqlTransaction;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 10;        //Timeout 10s

                    try
                    {
                        DataTable dt = new DataTable();
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);

                            //Return
                            return 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Error
                        SanitaLog.e(TAG, "DoUnLock error : " + sql, ex);
                        SanitaLog.d(TAG, "Table = " + table);
                        SanitaLog.d(TAG, "Key = " + key);
                        return -100;
                    }
                }
            }
#else
            //Return
            return 1;
#endif
        }

        public int DoUnLockAll(IDbConnection connection, IDbTransaction trans)
        {
#if false
            String sql = String.Format("SELECT pg_advisory_unlock_all();");

            //Chỉ cho phép unlock ở trong transaction
            if (connection == null)
            {
                return -100;
            }

            while (true)
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection as NpgsqlConnection;
                    command.Transaction = trans as NpgsqlTransaction;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 10;        //Timeout 10s

                    try
                    {
                        DataTable dt = new DataTable();
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);

                            //Return
                            return 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Error
                        SanitaLog.e(TAG, "DoUnLockAll error : " + sql, ex);
                        return -100;
                    }
                }
            }
#else
            //Return
            return 1;
#endif
        }

        public void DoClearAllLock(IDbConnection connection, IDbTransaction trans)
        {
            SanitaLog.d(TAG, "DoClearAllLock...");
            String sql = String.Format("SELECT pg_terminate_backend(subquery.pid) FROM (select pg_stat_activity.pid from pg_locks JOIN pg_stat_activity on pg_locks.pid = pg_stat_activity.pid WHERE pg_locks.locktype = 'advisory' and pg_stat_activity.pid <> pg_backend_pid() and pg_locks.pid  <> pg_backend_pid()) as subquery;");

            using (NpgsqlConnection my_connection = CreateConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = my_connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 10;        //Timeout 10s

                    try
                    {
                        DataTable dt = new DataTable();
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Error
                        SanitaLog.e(TAG, "DoClearAllLock error : " + sql, ex);
                    }
                }
            }
        }

        public void DoClearLock(IDbConnection connection, IDbTransaction trans, String table)
        {
            SanitaLog.d(TAG, "DoClearLock >> " + table);
            String sql = String.Format("SELECT pg_terminate_backend(subquery.pid) FROM (SELECT pg_locks.pid FROM pg_locks JOIN pg_class ON pg_locks.relation = pg_class.oid WHERE pg_locks.mode = 'AccessShareLock' and pg_class.relname = '{0}' and pg_locks.granted = 't') as subquery;", table);

            using (NpgsqlConnection my_connection = CreateConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = my_connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 10;        //Timeout 10s

                    try
                    {
                        DataTable dt = new DataTable();
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Error
                        SanitaLog.e(TAG, "DoClearLock error : " + sql, ex);
                    }
                }
            }
        }

        public String GetEcriptKey()
        {
            return PrivateKey;
        }

        public void SetDatabaseImplementUtility(DatabaseImplementUtility utility)
        {
            mUtility = utility;
        }

        public void InitNotification(IList<String> mListChannel, OnDatabaseNotificationHandler mCallback)
        {
            mOnDatabaseNotificationHandler = mCallback;

            try
            {
                if (mNotifyConnection != null && mNotifyConnection.State == ConnectionState.Open)
                {
                    mNotifyConnection.Close();
                    mNotifyConnection = null;
                }
                mNotifyConnection = CreateConnection();
                mNotifyConnection.Open();

                String sql = String.Join(";", mListChannel.Select(p => "listen " + p).ToArray());
                using (NpgsqlCommand command = new NpgsqlCommand(sql, mNotifyConnection))
                {
                    int ret = command.ExecuteNonQuery();
                    SanitaLog.d(TAG, "InitNotification = " + sql);
                    SanitaLog.d(TAG, "InitNotification = " + ret);
                }

                mNotifyConnection.Notification += mNpgsqlConnection_Notification;
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }
        }

        public void DoNotification(String chanel, String data)
        {
            try
            {
                String sql = "notify " + chanel + ", '" + data + "'";
                DoGetDataRow(sql);
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }
        }

        public void SetConnectionConfig(String host, String user, String password, String database, String port)
        {
            Localserver = host;
            Localdatabase = database;
            Localuserid = user;
            Localpassword = password;
            Localport = port;
        }

        void mNpgsqlConnection_Notification(object sender, NpgsqlNotificationEventArgs e)
        {
            if (mOnDatabaseNotificationHandler != null)
            {
                mOnDatabaseNotificationHandler(e.Condition, e.AdditionalInformation);
            }
        }

        public IDbConnection GetConnection()
        {
            return CreateConnection();
        }

        public IDbConnection GetConnection_Store()
        {
            return CreateConnection_Store();
        }

        public String GetConnectionString()
        {
            lock (lockDBObject)
            {
                //LocalConnectionString = String.Format("server={0};UserId={1}; Password={2}; Database={3};port={4};MINPOOLSIZE=0;POOLING=FALSE",Localserver, Localuserid, Localpassword, Localdatabase, Localport);
                LocalConnectionString = String.Format("server={0};UserId={1}; Password={2}; Database={3};port={4};", Localserver, Localuserid, Localpassword, Localdatabase, Localport);

                return LocalConnectionString;
            }
        }

        public String GetConnectionString_Store()
        {
            lock (lockDBObject)
            {
                String RemoteConnectionString = String.Format("server={0};UserId={1}; Password={2}; Database={3};port={4};SYNCNOTIFICATION=true",
                "ehc.vn", "postgres", "dk#423(24A*34k%*92$3", "ehchis_backup", "5432");

                return RemoteConnectionString;
            }
        }        

        public String GetConnectionString_Host()
        {
            return Localserver.ToLower();
        }

        public String GetConnectionString_Database()
        {
            return Localdatabase.ToLower();
        }

        public String GetDatabaseName()
        {
            lock (lockDBObject)
            {
                if (DataBase_Ready)
                {
                    if (Localdatabase != "")
                    {
                        return Localdatabase;
                    }
                    else
                    {
                        DataBase_Ready = false;
                    }
                }

                if (!DataBase_Ready)
                {
                    GetConnectionString();
                }

                return Localdatabase;
            }
        }

        public DataSet DoGetDataSet(string sql)
        {
            return GetDataSet(sql);
        }

        public DataTable DoGetDataTable(string sql)
        {
            return GetDataTable(sql);
        }

        public DataTable DoGetDataTable(IDbConnection connection, IDbTransaction trans, string sql)
        {
            if (connection != null)
            {
                return GetDataTable(connection, trans, sql);
            }
            else
            {
                return GetDataTable(sql);
            }
        }

        public DataTable DoShowDatabase(string sql)
        {
            return ShowDatabase(sql);
        }

        public DataRow DoGetDataRow(string sql)
        {
            return GetDataRow(sql);
        }

        public DataRow DoGetDataRow_SQL(string sql)
        {
            try
            {
                DataRow row = null;

                DataTable dt = GetDataTable_SQL(sql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                    }
                    else
                    {
                        if (sql.ToLower().StartsWith("notify"))
                        {
                            return null;
                        }
                        else
                        {
                            dt.Rows.Add(0);
                            row = dt.Rows[0];
                        }
                    }
                }

                return row;
            }
            catch (Exception ex)
            {
                if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                {
                    SystemInfo.LastDBErrorMessage = "the connection is broken";
                }
                if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                {
                    SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                }

                SanitaLogEx.e(TAG, sql);
                SanitaLogEx.e(TAG, "GetDataRow 1 error !", ex);
                return null;
            } 
        }

        //Retun null là lỗi connection
        //Return khác null nhưng có ID = 0 là DB không có row nào
        //Return khác null nhưng có ID > 0 là DB có ít nhất 1 row
        public DataRow DoGetDataRow(IDbConnection connection, IDbTransaction trans, string sql)
        {
            if (connection != null)
            {
                return GetDataRow(connection, trans, sql);
            }
            else
            {
                return GetDataRow(sql);
            }
        }

        public int DoCreateDatabase(string sql)
        {
            return CreateDatabase(sql);
        }

        public int DoInsert(string sql)
        {
            //Database log
            //SanitaDataLog.d(TAG, sql);
            return Insert(sql, true);
        }

        public int DoInsert(IDbConnection connection, IDbTransaction trans, string sql)
        {
            //Database log
            //SanitaDataLog.d(TAG, sql);
            if (connection != null)
            {
                return Insert(connection, trans, sql, true, "");
            }
            else
            {
                return Insert(sql, true);
            }
        }

        public int DoInsert(IDbConnection connection, IDbTransaction trans, string sql, string return_data)
        {
            //Database log
            //SanitaDataLog.d(TAG, sql);
            if (connection != null)
            {
                return Insert(connection, trans, sql, true, return_data);
            }
            else
            {
                return Insert(sql, true);
            }
        }

        public int DoUpdate(string sql)
        {
            //Database log
            //SanitaDataLog.d(TAG, sql);
            return Update(sql);
        }

        public int DoUpdate(IDbConnection connection, IDbTransaction trans, string sql)
        {
            //Database log
            //SanitaDataLog.d(TAG, sql);
            if (connection != null)
            {
                return Update(connection, trans, sql);
            }
            else
            {
                return Update(sql);
            }
        }

        public int DoUpdate_Backup(IDbConnection connection, IDbTransaction trans, string sql)
        {
            return Update_Backup(connection, trans, sql);
        }

        #endregion

        #region Get Connection

        public NpgsqlConnection CreateConnection()
        {
            //SanitaLogEx.d(TAG, "CreateConnection--START");

            NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString());
            return connection;
        }

        public NpgsqlConnection CreateConnection_Store()
        {
            return new NpgsqlConnection(GetConnectionString_Store());
        }

        public NpgsqlConnection CreateConnection4CreateDatabase()
        {
            lock (lockDBObject)
            {
                GetConnectionString();

                String connStr = String.Format("server={0};UserId={1}; Password={2};port={3}",
               Localserver, Localuserid, Localpassword, Localport);

                NpgsqlConnection connection;

                try
                {
                    connection = new NpgsqlConnection(connStr);
                }
                catch (Exception ex)
                {
                    SanitaLogEx.e(TAG, "CreateConnection4CreateDatabase error !", ex);
                    connection = null;
                }

                return connection;
            }
        }

        #endregion

        #region Data Update handlers

        public DataTable ShowDatabase(string sql)
        {
            //SanitaLogEx.d(TAG, "ShowDatabase:" + sql);

            lock (lockDBObject)
            {
                try
                {
                    using (NpgsqlConnection my_connection = CreateConnection4CreateDatabase())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            command.Connection = my_connection;
                            command.CommandType = CommandType.Text;
                            command.CommandText = sql;

                            try
                            {
                                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                                {
                                    adapter.SelectCommand = command;
                                    DataTable dt = new DataTable();

                                    adapter.Fill(dt);

                                    return dt;
                                }
                            }
                            catch (Exception ex)
                            {
                                SanitaLogEx.e(TAG, "ShowDatabase error !", ex);
                            }

                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SanitaLogEx.e(TAG, "ShowDatabase error !", ex);
                    return null;
                }
            }
        }

        public int CreateDatabase(string sql)
        {
            lock (lockDBObject)
            {
                try
                {
                    using (NpgsqlConnection my_connection = CreateConnection4CreateDatabase())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            command.Connection = my_connection;
                            command.CommandText = sql;

                            my_connection.Open();
                            return command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    SanitaLogEx.e(TAG, "CreateDatabase error !", ex);
                    return -100;
                }
            }
        }

        public int Update(string _sql)
        {
            //SanitaLogEx.d(TAG, "Update:" + _sql);
            SystemInfo.LastDBErrorMessage = "";

            lock (lockDBObject)
            {
                //update sql with flag
                String sql = _sql;

                try
                {
                    int TryMax = 0;
                    while (true)
                    {
                        using (NpgsqlConnection my_connection = CreateConnection())
                        {
                            using (NpgsqlCommand command = new NpgsqlCommand())
                            {
                                try
                                {
                                    command.Connection = my_connection;
                                    command.CommandText = sql;
                                    command.CommandTimeout = 10 * 60;

                                    my_connection.Open();
                                    int ret = command.ExecuteNonQuery();

                                    if (
                                        (mUtility != null && mUtility.IsHaveLogTable) &&
                                        !sql.ToLower().Contains("tb_softupdate") &&
                                        !sql.ToLower().Contains("tb_mediboxlogsql") &&
                                        !sql.ToLower().Contains("insert into tb_mediboxlog") &&
                                        !sql.ToLower().Contains("insert into tb_nhanvienlog") &&
                                        !sql.ToLower().Contains("update tb_roomstatus")
                                        )
                                    {
                                        StringBuilder new_sql = new StringBuilder();
                                        new_sql.Append(" INSERT INTO tb_mediboxlogsql (");
                                        new_sql.Append("            NgayLog,");
                                        new_sql.Append("            NoiDung,");
                                        new_sql.Append("            NguoiLog)");
                                        new_sql.Append("  VALUES( ");
                                        new_sql.Append("          " + SystemInfo.NOW.Escape()).Append(", ");
                                        new_sql.Append("          " + sql.Escape()).Append(", ");
                                        new_sql.Append("          " + 0.Escape()).Append(") ");
                                        command.CommandText = new_sql.ToString();
                                        command.ExecuteScalar();
                                    }

                                    return ret;
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message != null && ex.Message.ToLower().Contains("No space left on device"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "No space left on device";
                                    }
                                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                                    }
                                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                                    }  

                                    SystemInfo.LastErrorMessage = ex.Message;
                                    SanitaLogEx.e(TAG, "Update('" + sql + "') error !", ex);
                                }

                                TryMax++;
                                if (TryMax > 3)
                                {
                                    SanitaLogEx.e(TAG, "Update('" + sql + "') error !");
                                    return -100;
                                }

                                SanitaLogEx.e(TAG, "Try Update N = " + TryMax.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("No space left on device"))
                    {
                        SystemInfo.LastDBErrorMessage = "No space left on device";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }                    

                    SanitaLogEx.e(TAG, "Update('" + sql + "') error !", ex);
                    return -100;
                }
            }
        }

        public int UpdateSimple(string sql)
        {
            //SanitaLogEx.d(TAG, "UpdateSimple:" + sql);
            SystemInfo.LastDBErrorMessage = "";

            lock (lockDBObject)
            {
                try
                {
                    using (NpgsqlConnection my_connection = CreateConnection())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            try
                            {
                                command.Connection = my_connection;
                                command.CommandText = sql;
                                command.CommandTimeout = 1000;

                                my_connection.Open();
                                int ret = command.ExecuteNonQuery();

                                if (ret > 0 && (mUtility != null && mUtility.IsHaveLogTable))
                                {
                                    StringBuilder new_sql = new StringBuilder();
                                    new_sql.Append(" INSERT INTO tb_mediboxlogsql (");
                                    new_sql.Append("            NgayLog,");
                                    new_sql.Append("            NoiDung,");
                                    new_sql.Append("            NguoiLog)");
                                    new_sql.Append("  VALUES( ");
                                    new_sql.Append("          " + SystemInfo.NOW.Escape()).Append(", ");
                                    new_sql.Append("          " + sql.Escape()).Append(", ");
                                    new_sql.Append("          " + 0.Escape()).Append(") ");
                                    command.CommandText = new_sql.ToString();
                                    command.ExecuteScalar();
                                }

                                return ret;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message != null && ex.Message.ToLower().Contains("No space left on device"))
                                {
                                    SystemInfo.LastDBErrorMessage = "No space left on device";
                                }
                                if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                                {
                                    SystemInfo.LastDBErrorMessage = "the connection is broken";
                                }
                                if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                                {
                                    SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                                }  

                                return -100;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemInfo.LastErrorMessage = ex.Message;
                    return -100;
                }
            }
        }

        public int Update(IDbConnection connection, IDbTransaction trans, string _sql)
        {
            //SanitaLogEx.d(TAG, "Update 22:" + _sql);
            SystemInfo.LastDBErrorMessage = "";

            //lock (lockDBObject)
            {
                //update sql with flag
                String sql = _sql;

#if false
                bool isDelete = false;
                if (sql.Trim().ToUpper().StartsWith("UPDATE"))
                {
                    sql = sql.Replace(" SET ", " SET sync_flag = '0',update_flag = '0', ");
                    sql = sql.Replace(" set ", " SET sync_flag = '0',update_flag = '0', ");
                    sql = sql.Replace(" Set ", " SET sync_flag = '0',update_flag = '0', ");
                }
                else if (sql.Trim().ToUpper().StartsWith("DELETE"))
                {
                    isDelete = true;
                }
#endif

                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection as NpgsqlConnection;
                        command.Transaction = trans as NpgsqlTransaction;
                        command.CommandText = sql;
                        command.CommandTimeout = 10 * 60;

                        int ret = command.ExecuteNonQuery();

                        if (
                            (mUtility != null && mUtility.IsHaveLogTable) &&
                            !sql.ToLower().Contains("tb_softupdate") &&
                            !sql.ToLower().Contains("tb_mediboxlogsql") &&
                            !sql.ToLower().Contains("insert into tb_mediboxlog") &&
                            !sql.ToLower().Contains("insert into tb_nhanvienlog") &&
                            !sql.ToLower().Contains("update tb_roomstatus")
                            )
                        {
                            StringBuilder new_sql = new StringBuilder();
                            new_sql.Append(" INSERT INTO tb_mediboxlogsql (");
                            new_sql.Append("            NgayLog,");
                            new_sql.Append("            NoiDung,");
                            new_sql.Append("            NguoiLog)");
                            new_sql.Append("  VALUES( ");
                            new_sql.Append("          " + SystemInfo.NOW.Escape()).Append(", ");
                            new_sql.Append("          " + sql.Escape()).Append(", ");
                            new_sql.Append("          " + 0.Escape()).Append(") ");
                            command.CommandText = new_sql.ToString();
                            command.ExecuteScalar();
                        }

                        return ret;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("No space left on device"))
                    {
                        SystemInfo.LastDBErrorMessage = "No space left on device";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SystemInfo.LastErrorMessage = ex.Message;
                    SanitaLogEx.e(TAG, "Update('" + sql + "') error !", ex);

                    SanitaLogEx.e(TAG, "=>Do rollback: --START");
                    try
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                            SanitaLogEx.e(TAG, "=>Do rollback OK");
                        }
                        if (connection != null)
                        {
                            connection.Close();
                            SanitaLogEx.e(TAG, "=>Do close connection OK");
                        }
                    }
                    catch (Exception ex1)
                    {
                        SanitaLogEx.e(TAG, "=>Do rollback NG", ex1);
                    }
                    SanitaLogEx.e(TAG, "=>Do rollback: --END");

                    return -100;
                }
            }
        }

        public int Update_Backup(IDbConnection connection, IDbTransaction trans, string _sql)
        {
            SystemInfo.LastDBErrorMessage = "";

            //lock (lockDBObject)
            {
                //update sql with flag
                String sql = _sql;

#if false
                bool isDelete = false;
                if (sql.Trim().ToUpper().StartsWith("UPDATE"))
                {
                    sql = sql.Replace(" SET ", " SET sync_flag = '0',update_flag = '0', ");
                    sql = sql.Replace(" set ", " SET sync_flag = '0',update_flag = '0', ");
                    sql = sql.Replace(" Set ", " SET sync_flag = '0',update_flag = '0', ");
                }
                else if (sql.Trim().ToUpper().StartsWith("DELETE"))
                {
                    isDelete = true;
                }
#endif

                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection as NpgsqlConnection;
                        command.Transaction = trans as NpgsqlTransaction;
                        command.CommandText = sql;
                        command.CommandTimeout = 30;

                        int ret = command.ExecuteNonQuery();

                        return ret;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("No space left on device"))
                    {
                        SystemInfo.LastDBErrorMessage = "No space left on device";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SystemInfo.LastErrorMessage = ex.Message;
                    SanitaLogEx.e(TAG, "Update('" + sql + "') error !", ex);

                    SanitaLogEx.e(TAG, "=>Do rollback: --START");
                    try
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                            SanitaLogEx.e(TAG, "=>Do rollback OK");
                        }
                        if (connection != null)
                        {
                            connection.Close();
                            SanitaLogEx.e(TAG, "=>Do close connection OK");
                        }
                    }
                    catch (Exception ex1)
                    {
                        SanitaLogEx.e(TAG, "=>Do rollback NG", ex1);
                    }
                    SanitaLogEx.e(TAG, "=>Do rollback: --END");

                    return -100;
                }
            }
        }

        public int Insert(string _sql, bool getId)
        {
            //SanitaLogEx.d(TAG, "Insert:" + _sql);
            SystemInfo.LastDBErrorMessage = "";

            //lock (lockDBObject)
            {
                //INSERT INTO AA(xx) VALUES(yy) -> "INSERT INTO AA(xx" + " VALUES(yy" + ""
                String sql = _sql;

#if false
                String[] list_data = _sql.Split(')');
                if (list_data.Length > 2)
                {
                    list_data[0] = list_data[0].Replace("(", "(sync_flag,update_flag,");
                    list_data[1] = list_data[1].Replace("VALUES(", "VALUES('0','0',");
                    sql = String.Join(")", list_data);
                }
#endif

                try
                {
                    using (NpgsqlConnection my_connection = CreateConnection())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            if (getId && mUtility != null)
                            {
                                String strID = mUtility.GetTableID(sql);
                                sql += " RETURNING " + strID;

                                command.Connection = my_connection;
                                command.CommandText = sql;
                                command.CommandTimeout = 30;

                                my_connection.Open();
                                int id = int.Parse(command.ExecuteScalar().ToString());

                                if (id > 0 &&
                                    (mUtility != null && mUtility.IsHaveLogTable) &&
                                    !sql.ToLower().Contains("tb_softupdate") &&
                                    !sql.ToLower().Contains("insert into tb_nhanvienlog") &&
                                    !sql.ToLower().Contains("insert into tb_mediboxlog") &&
                                    !sql.ToLower().Contains("tb_mediboxlogsql"))
                                {
                                    String[] list_data = _sql.Split(')');
                                    if (list_data.Length > 2)
                                    {
                                        list_data[0] = list_data[0].Replace("(", "(" + strID + ",");
                                        list_data[1] = list_data[1].Replace("VALUES(", "VALUES('" + id + "',");
                                        String log_sql = String.Join(")", list_data);

                                        StringBuilder new_sql = new StringBuilder();
                                        new_sql.Append(" INSERT INTO tb_mediboxlogsql (");
                                        new_sql.Append("            NgayLog,");
                                        new_sql.Append("            NoiDung,");
                                        new_sql.Append("            NguoiLog)");
                                        new_sql.Append("  VALUES( ");
                                        new_sql.Append("          " + SystemInfo.NOW.Escape()).Append(", ");
                                        new_sql.Append("          " + log_sql.Escape()).Append(", ");
                                        new_sql.Append("          " + 0.Escape()).Append(") ");
                                        command.CommandText = new_sql.ToString();
                                        command.ExecuteScalar();
                                    }
                                }

                                return id;
                            }
                            else
                            {
                                command.Connection = my_connection;
                                command.CommandText = sql;
                                command.CommandTimeout = 30;

                                my_connection.Open();
                                return command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("No space left on device"))
                    {
                        SystemInfo.LastDBErrorMessage = "No space left on device";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SanitaLogEx.e(TAG, "Insert('" + sql + "') error !", ex);
                    return -100;
                }
            }
        }

        public int Insert(IDbConnection connection, IDbTransaction trans, string _sql, bool getId, string _return_data)
        {
            //SanitaLogEx.d(TAG, "Insert22:" + _sql);
            SystemInfo.LastDBErrorMessage = "";

            //lock (lockDBObject)
            {
                //INSERT INTO AA(xx) VALUES(yy) -> "INSERT INTO AA(xx" + " VALUES(yy" + ""
                String sql = _sql;

#if false
                String[] list_data = _sql.Split(')');
                if(list_data.Length > 2)
                {
                    list_data[0] = list_data[0].Replace("(", "(sync_flag,update_flag,");
                    list_data[1] = list_data[1].Replace("VALUES(", "VALUES('0','0',");
                    sql = String.Join(")", list_data);
                }
#endif

                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        if (getId)
                        {
                            if (!String.IsNullOrEmpty(_return_data))
                            {
                                sql += " RETURNING " + _return_data;
                            }
                            else
                            {
                                String strID = mUtility.GetTableID(sql);
                                sql += " RETURNING " + strID;
                            }

                            //Set command
                            command.Connection = connection as NpgsqlConnection;
                            command.Transaction = trans as NpgsqlTransaction;
                            command.CommandText = sql;
                            command.CommandTimeout = 30;

                            int id = int.Parse(command.ExecuteScalar().ToString());

                            if (id > 0 &&
                                (mUtility != null && mUtility.IsHaveLogTable) &&
                                String.IsNullOrEmpty(_return_data) &&
                                !sql.ToLower().Contains("tb_softupdate") &&
                                !sql.ToLower().Contains("insert into tb_nhanvienlog") &&
                                !sql.ToLower().Contains("insert into tb_mediboxlog") &&
                                !sql.ToLower().Contains("tb_mediboxlogsql"))
                            {
                                String[] list_data = _sql.Split(')');
                                if (list_data.Length > 2)
                                {
                                    list_data[0] = list_data[0].Replace("(", "(" + mUtility.GetTableID(sql) + ",");
                                    list_data[1] = list_data[1].Replace("VALUES(", "VALUES('" + id + "',");

                                    String log_sql = String.Join(")", list_data);

                                    StringBuilder new_sql = new StringBuilder();
                                    new_sql.Append(" INSERT INTO tb_mediboxlogsql (");
                                    new_sql.Append("            NgayLog,");
                                    new_sql.Append("            NoiDung,");
                                    new_sql.Append("            NguoiLog)");
                                    new_sql.Append("  VALUES( ");
                                    new_sql.Append("          " + SystemInfo.NOW.Escape()).Append(", ");
                                    new_sql.Append("          " + log_sql.Escape()).Append(", ");
                                    new_sql.Append("          " + 0.Escape()).Append(") ");
                                    command.CommandText = new_sql.ToString();
                                    command.ExecuteScalar();
                                }
                            }

                            return id;
                        }
                        else
                        {
                            //Set command
                            command.Connection = connection as NpgsqlConnection;
                            command.Transaction = trans as NpgsqlTransaction;
                            command.CommandText = sql;
                            command.CommandTimeout = 30;

                            return command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("no space left on device"))
                    {
                        SystemInfo.LastDBErrorMessage = "No space left on device";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SanitaLogEx.e(TAG, "Insert('" + sql + "') error !", ex);

                    SanitaLogEx.e(TAG, "=>Do rollback: --START");
                    try
                    {
                        if (trans != null)
                        {
                            trans.Rollback();
                            SanitaLogEx.e(TAG, "=>Do rollback OK");
                        }
                        if (connection != null)
                        {
                            connection.Close();
                            SanitaLogEx.e(TAG, "=>Do close connection OK");
                        }
                    }
                    catch (Exception ex1)
                    {
                        SanitaLogEx.e(TAG, "=>Do rollback NG", ex1);
                    }
                    SanitaLogEx.e(TAG, "=>Do rollback: --END");

                    return -100;
                }
            }
        }

        public int GetID(string strID, string strTable)
        {
            lock (lockDBObject)
            {
                try
                {
                    using (NpgsqlConnection my_connection = CreateConnection())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            command.Connection = my_connection;
                            my_connection.Open();

                            int id = 0;

                            command.CommandText = "SELECT MAX(" + strID + ") FROM " + strTable;

                            if (int.TryParse(command.ExecuteScalar().ToString(), out id))
                            {
                                command.CommandText = "ALTER TABLE " + strTable + " AUTO_INCREMENT = " + (id + 1).ToString();
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                command.CommandText = "ALTER TABLE " + strTable + " AUTO_INCREMENT = 1";
                                command.ExecuteNonQuery();
                            }

                            return id;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SanitaLogEx.e(TAG, "GetID error !", ex);
                    return -100;
                }
            }
        }

        public void SetID(string strID, string strTable)
        {
            try
            {
                using (NpgsqlConnection my_connection = CreateConnection())
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = my_connection;
                        my_connection.Open();

                        int id = 0;

                        command.CommandText = "SELECT MAX(" + strID + ") FROM " + strTable;

                        if (int.TryParse(command.ExecuteScalar().ToString(), out id))
                        {
                            command.CommandText = "ALTER TABLE " + strTable + " AUTO_INCREMENT = " + (id + 1).ToString();
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            command.CommandText = "ALTER TABLE " + strTable + " AUTO_INCREMENT = 1";
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SanitaLogEx.e(TAG, "SetID error !", ex);
            }
        }

        public void SetID(string strID, string strTable, int ID)
        {
            try
            {
                using (NpgsqlConnection my_connection = CreateConnection())
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = my_connection;
                        my_connection.Open();

                        command.CommandText = "ALTER TABLE " + strTable + " AUTO_INCREMENT = " + ID.ToString();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                SanitaLogEx.e(TAG, "SetID error !", ex);
            }
        }

        #endregion

        #region Data Retrieve Handlers

        public DataSet GetDataSet(string sql)
        {
            //lock (lockDBObject)
            {
                try
                {
                    using (NpgsqlConnection my_connection = CreateConnection())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            command.Connection = my_connection;
                            command.CommandType = CommandType.Text;
                            command.CommandText = sql;
                            command.CommandTimeout = 1000;

                            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                            {
                                adapter.SelectCommand = command;
                                DataSet ds = new DataSet();

                                my_connection.Open();
                                adapter.Fill(ds);
                                my_connection.Close();

                                return ds;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SanitaLogEx.e(TAG, "GetDataSet error !", ex);
                    return null;
                }
            }
        }

        public DataTable GetDataTable(string sql)
        {
            //SanitaLogEx.d(TAG, "GetDataTable:" + sql);
            //lock (lockDBObject)
            {
                try
                {
                    int TryMax = 0;
                    DataTable dt = new DataTable();

                    while (true)
                    {
                        using (NpgsqlConnection my_connection = CreateConnection())
                        {
                            using (NpgsqlCommand command = new NpgsqlCommand())
                            {
                                command.Connection = my_connection;
                                command.CommandType = CommandType.Text;
                                command.CommandText = "SET enable_seqscan = off; " + sql;
                                command.CommandTimeout = 1000;

                                try
                                {
                                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                                    {
                                        adapter.SelectCommand = command;

                                        my_connection.Open();
                                        adapter.Fill(dt);
                                        my_connection.Close();

                                        SystemInfo.LastDBErrorMessage = "Connection OK";

                                        return dt;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                                    }
                                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                                    }

                                    SanitaLogEx.e(TAG, sql);
                                    SanitaLogEx.e(TAG, "GetDataTable 1 error !", ex);
                                }

                                TryMax++;
                                if (TryMax > 5)
                                {
                                    SanitaLogEx.e(TAG, sql);
                                    SanitaLogEx.e(TAG, "GetDataTable 2 error ! TryMax > 5");
                                    return null;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }

                    SanitaLogEx.e(TAG, sql);
                    SanitaLogEx.e(TAG, "GetDataTable 3 error !", ex);
                    return null;
                }
            }
        }

        public DataTable GetDataTable_SQL(string sql)
        {
            //SanitaLogEx.d(TAG, "GetDataTable:" + sql);
            //lock (lockDBObject)
            {
                try
                {
                    int TryMax = 0;
                    DataTable dt = new DataTable();

                    while (true)
                    {
                        using (NpgsqlConnection my_connection = CreateConnection())
                        {
                            using (NpgsqlCommand command = new NpgsqlCommand())
                            {
                                command.Connection = my_connection;
                                command.CommandType = CommandType.Text;
                                command.CommandText = "SET enable_seqscan = off; " + sql;
                                command.CommandTimeout = 1000;

                                try
                                {
                                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                                    {
                                        adapter.SelectCommand = command;

                                        my_connection.Open();
                                        adapter.Fill(dt);
                                        my_connection.Close();

                                        SystemInfo.LastDBErrorMessage = "Connection OK";

                                        return dt;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                                    }
                                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                                    }

                                    SanitaLogEx.e(TAG, sql);
                                    SanitaLogEx.e(TAG, "GetDataTable 1 error !", ex);
                                }

                                TryMax++;
                                if (TryMax > 5)
                                {
                                    SanitaLogEx.e(TAG, sql);
                                    SanitaLogEx.e(TAG, "GetDataTable 2 error ! TryMax > 5");
                                    return null;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }

                    SanitaLogEx.e(TAG, sql);
                    SanitaLogEx.e(TAG, "GetDataTable 3 error !", ex);
                    return null;
                }
            }
        }

        public DataTable GetDataTable(IDbConnection connection, IDbTransaction trans, string sql)
        {
            //SanitaLogEx.d(TAG, "GetDataTable 22:" + sql);
            //lock (lockDBObject)
            {
                try
                {
                    int TryMax = 0;
                    DataTable dt = new DataTable();

                    while (true)
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            command.Connection = connection as NpgsqlConnection;
                            command.Transaction = trans as NpgsqlTransaction;
                            command.CommandType = CommandType.Text;
                            command.CommandText = sql;
                            command.CommandTimeout = 1000;

                            try
                            {
                                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                                {
                                    adapter.SelectCommand = command;
                                    adapter.Fill(dt);

                                    SystemInfo.LastDBErrorMessage = "Connection OK";

                                    return dt;
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                                {
                                    SystemInfo.LastDBErrorMessage = "the connection is broken";
                                }
                                if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                                {
                                    SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                                }  

                                SanitaLogEx.e(TAG, sql);
                                SanitaLogEx.e(TAG, "GetDataTable 1 error !", ex);
                            }

                            TryMax++;
                            if (TryMax > 5)
                            {
                                SanitaLogEx.e(TAG, sql);
                                SanitaLogEx.e(TAG, "GetDataTable 2 error ! TryMax > 5");
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SanitaLogEx.e(TAG, sql);
                    SanitaLogEx.e(TAG, "GetDataTable 3 error !", ex);
                    return null;
                }
            }
        }

        public IDataReader DoGetDataReader(string sql)
        {
            //lock (lockDBObject)
            {
                try
                {
                    int TryMax = 0;
                    NpgsqlDataReader reader;

                    while (true)
                    {
                        using (NpgsqlConnection my_connection = CreateConnection())
                        {
                            using (NpgsqlCommand command = new NpgsqlCommand())
                            {
                                command.Connection = my_connection;
                                command.CommandType = CommandType.Text;
                                command.CommandText = sql;
                                command.CommandTimeout = 1000;

                                try
                                {
                                    my_connection.Open();
                                    reader = command.ExecuteReader();
                                    return reader;
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                                    }
                                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                                    {
                                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                                    }  

                                    SanitaLogEx.e(TAG, "GetDataTable error !", ex);
                                }

                                TryMax++;
                                if (TryMax > 5)
                                {
                                    SanitaLogEx.e(TAG, "GetDataTable error ! TryMax > 5");
                                    return null;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SanitaLogEx.e(TAG, "GetDataTable error !", ex);
                    return null;
                }
            }
        }

        public IDataReader DoGetDataReader(IDbConnection connection, IDbTransaction trans, string sql)
        {
            //lock (lockDBObject)
            {
                try
                {
                    NpgsqlDataReader reader;
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection as NpgsqlConnection;
                        command.Transaction = trans as NpgsqlTransaction;
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        command.CommandTimeout = 1000;

                        try
                        {
                            reader = command.ExecuteReader();
                            return reader;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                            {
                                SystemInfo.LastDBErrorMessage = "the connection is broken";
                            }
                            if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                            {
                                SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                            }  

                            SanitaLogEx.e(TAG, "GetDataReader error !", ex);
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SanitaLogEx.e(TAG, "GetDataReader error !", ex);
                    return null;
                }
            }
        }

        public DataRow GetDataRow(string sql)
        {
            try
            {
                DataRow row = null;

                DataTable dt = GetDataTable(sql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                    }
                    else
                    {
                        if (sql.ToLower().StartsWith("notify"))
                        {
                            return null;
                        }
                        else
                        {
                            dt.Rows.Add(0);
                            row = dt.Rows[0];
                        }
                    }
                }

                return row;
            }
            catch (Exception ex)
            {
                if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                {
                    SystemInfo.LastDBErrorMessage = "the connection is broken";
                }
                if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                {
                    SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                }

                SanitaLogEx.e(TAG, sql);
                SanitaLogEx.e(TAG, "GetDataRow 1 error !", ex);
                return null;
            }
        }

        public DataRow GetDataRow(IDbConnection connection, IDbTransaction trans, string sql)
        {
            //SanitaLogEx.d(TAG, "GetDataRow 22:" + sql);
            //lock (lockDBObject)
            {
                try
                {
                    DataRow row = null;

                    DataTable dt = GetDataTable(connection, trans, sql);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            row = dt.Rows[0];
                        }
                        else
                        {
                            dt.Rows.Add(0);
                            row = dt.Rows[0];
                        }
                    }

                    return row;
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SanitaLogEx.e(TAG, sql);
                    SanitaLogEx.e(TAG, "GetDataRow 1 error !", ex);
                    return null;
                }
            }
        }

        public object GetScalar(string sql)
        {
            //lock (lockDBObject)
            {
                try
                {
                    using (NpgsqlConnection my_connection = CreateConnection())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            command.Connection = my_connection;
                            command.CommandText = sql;
                            command.CommandTimeout = 1000;

                            my_connection.Open();
                            return command.ExecuteScalar();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != null && ex.Message.ToLower().Contains("the connection is broken"))
                    {
                        SystemInfo.LastDBErrorMessage = "the connection is broken";
                    }
                    if (ex.Message != null && ex.Message.ToLower().Contains("sorry, too many Clinics already"))
                    {
                        SystemInfo.LastDBErrorMessage = "sorry, too many Clinics already";
                    }  

                    SanitaLogEx.e(TAG, "GetScalar error !", ex);
                    return null;
                }
            }
        }

        #endregion
    }
}
