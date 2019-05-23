using System;
using System.Collections.Generic;
using System.Linq;
using Sanita.Utility.Database.BaseDao;
using Sanita.Utility.ExtendedThread;
using System.Data;

namespace Sanita.Utility.Database.Utility
{
    public class DatabaseImplementUtility
    {
        private const String TAG = "DatabaseImplementUtility";

        protected IList<ClassTable> listFixTable = new List<ClassTable>();
        protected ExBackgroundWorker synch_worker;
        protected IBaseDao myBaseDao;
        public bool IsHaveLogTable = false;

        public void InitDatabase(IBaseDao _baseDAO, IList<ClassTable> _listFixTable)
        {
            myBaseDao = _baseDAO;
            listFixTable = _listFixTable;

            IsHaveLogTable = listFixTable.Any(p => p.Table.ToLower() == "tb_mediboxlogsql");
            IsHaveLogTable = false;

            myBaseDao.SetDatabaseImplementUtility(this);
        }

        public virtual void InitNotification(IList<String> mListChannel, OnDatabaseNotificationHandler mCallback)
        {

        }

        public virtual void DoNotification(String chanel, String data)
        {

        }

        public virtual bool isDatabaseOK()
        {
            return false;
        }

        public virtual bool UpdateDatabaseSequence(IDbConnection connection, IDbTransaction trans, ExBackgroundWorker worker)
        {
            return false;
        }

        public virtual bool SynchDatabase(IDbConnection connection, IDbTransaction trans, ExBackgroundWorker worker, int number_hosobenhan, bool IsCapNhatTrigger)
        {
            return false;
        }

        public virtual int AlterTableTable(IDbConnection connection, IDbTransaction trans, String DatabaseName, ClassTable Fixtable, ClassTable NewTable)
        {
            return -100;
        }

        public virtual int CreateTable(IDbConnection connection, IDbTransaction trans, String DatabaseName, ClassTable table)
        {
            return -100;
        }

        public virtual void GetSchema(IDbConnection connection, IDbTransaction trans, string DatabaseName)
        {

        }

        public virtual IList<ClassDatabase> GetListDatabase()
        {
            return new List<ClassDatabase>();
        }

        public virtual int CreateDatabase(string DatabaseName)
        {
            return -100;
        }

        public virtual int UseDatabase(string DatabaseName)
        {
            return -100;
        }

        public virtual IList<ClassTable> GetListTable(IDbConnection connection, IDbTransaction trans, string DatabaseName)
        {
            return new List<ClassTable>();
        }

        public virtual IList<ClassColumn> GetListColumn(IDbConnection connection, IDbTransaction trans, string DatabaseName, string TableName)
        {
            return new List<ClassColumn>();
        }

        public virtual IList<ClassColumn> GetListIndex(IDbConnection connection, IDbTransaction trans, string DatabaseName, string TableName)
        {
            return new List<ClassColumn>();
        }

        public string GetTableID(String sql)
        {
            //Get table name
            String data = sql;
            if (data.Length > 1000)
            {
                data = data.Substring(0, 500);
            }

            data = data.Trim();
            data = data.ToUpper();
            if (data.StartsWith("INSERT INTO"))
            {
                data = data.Substring("INSERT INTO".Length);
            }
            else if (data.StartsWith("UPDATE"))
            {
                data = data.Substring("UPDATE".Length);
            }
            else if (data.StartsWith("DELETE FROM"))
            {
                data = data.Substring("DELETE FROM".Length);
            }
            data = data.Trim();
            String table_name = data.Split(' ')[0];

            //Get table id
            ClassTable table = listFixTable.FirstOrDefault(p => p.Table.Equals(table_name, StringComparison.CurrentCultureIgnoreCase));
            if (table == null)
            {
                return "";
            }
            else
            {
                return table.listColumn[0].ColumnName;
            }
        }
    }
}
