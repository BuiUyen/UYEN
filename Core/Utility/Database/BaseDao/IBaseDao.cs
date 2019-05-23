using System;
using System.Data;
using Sanita.Utility.Database.Utility;
using System.Collections.Generic;

namespace Sanita.Utility.Database.BaseDao
{
    public interface IBaseDao
    {
        void SetDatabaseImplementUtility(DatabaseImplementUtility utility);
        void SetConnectionConfig(String host, String user, String password, String database, String port);
        IDbConnection GetConnection();
        IDbConnection GetConnection_Store();
        String GetConnectionString();
        String GetConnectionString_Host();
        String GetConnectionString_Database();
        String GetDatabaseName();
        DataSet DoGetDataSet(string sql);
        DataTable DoGetDataTable(string sql);
        DataTable DoGetDataTable(IDbConnection connection, IDbTransaction trans, string sql);
        IDataReader DoGetDataReader(string sql);
        IDataReader DoGetDataReader(IDbConnection connection, IDbTransaction trans, string sql);
        DataRow DoGetDataRow(string sql);
        DataRow DoGetDataRow_SQL(string sql);

        //Retun null là lỗi connection
        //Return khác null nhưng có ID = 0 là DB không có row nào
        //Return khác null nhưng có ID > 0 là DB có ít nhất 1 row
        DataRow DoGetDataRow(IDbConnection connection, IDbTransaction trans, string sql);
        DataTable DoShowDatabase(string sql);
        int DoCreateDatabase(string sql);
        int DoInsert(string sql);
        int DoInsert(IDbConnection connection, IDbTransaction trans, string sql);
        int DoInsert(IDbConnection connection, IDbTransaction trans, string sql, string return_data);
        int DoUpdate(string sql);
        int UpdateSimple(string sql);
        int DoUpdate(IDbConnection connection, IDbTransaction trans, string sql);
        int DoUpdate_Backup(IDbConnection connection, IDbTransaction trans, string sql);

        int DoLock(IDbConnection connection, IDbTransaction trans, string table, string table_key, int key, bool IsPing);
        int DoUnLock(IDbConnection connection, IDbTransaction trans, string table, string table_key, int key);
        int DoUnLockAll(IDbConnection connection, IDbTransaction trans);

        void InitNotification(IList<String> mListChannel, OnDatabaseNotificationHandler mCallback);
        void DoNotification(String chanel, String data);

        String GetEcriptKey();
    }
}
