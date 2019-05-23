using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using Sanita.Utility.Database.BaseDao;
using Sanita.Utility.Logger;
using System.Linq;
using System.IO;

namespace Sanita.Utility.Database.Utility
{
    public class ClassDatabase
    {
        public string Database;
    }

    public class ClassTable
    {
        public string Table;
        public IList<ClassColumn> listColumn;
        public IList<ClassColumn> listIndex;
    }

    public class ClassColumn
    {
        public string TableName;
        public string ColumnName;
        public string ColumnDefine;
        public string ColumnAfter;
        public bool isPRIMARY;
        public bool isIndex;

        public bool isUpdated;
    }

    public static class DatabaseUtility2
    {
        public static string Escape_TSQuery(this String s)
        {
            return String.Format("plainto_tsquery('vietnamese','{0}')", s);
        }

        public static string Escape(this object s)
        {
            if (s is String || s == null)
            {
                return DatabaseUtility.Escape((String)s);
            }
            if (s is bool)
            {
                return DatabaseUtility.Escape((bool)s);
            }
            if (s is int)
            {
                return DatabaseUtility.Escape((int)s);
            }
            if (s is IList<int>)
            {
                return DatabaseUtility.Escape((IList<int>)s);
            }
            if (s is long)
            {
                return DatabaseUtility.Escape((long)s);
            }
            if (s is byte[])
            {
                return DatabaseUtility.Escape((byte[])s);
            }
            if (s is double)
            {
                return DatabaseUtility.Escape((double)s);
            }
            if (s is ulong)
            {
                return DatabaseUtility.Escape((ulong)s);
            }
            if (s is long)
            {
                return Escape((long)s);
            }
            if (s is DateTime)
            {
                return DatabaseUtility.Escape((DateTime)s);
            }
            return "";
        }
    }

    public class DatabaseUtility
    {
        private const String TAG = "DatabaseUtility";

        public enum DATABASE_TYPE
        {
            MYSQL = 0,
            POSTGRESQL = 1,
            MSSQL = 2,
            SQLITE = 3,
        }

        private IBaseDao _baseDAO = null;
        private DatabaseImplementUtility _DatabaseUtility = null;

        #region Public

        public static DATABASE_TYPE GetDatabaseType()
        {
            return SystemInfo.DatabaseType;
        }

        public IBaseDao GetDatabaseDAO()
        {
            if (_baseDAO == null)
            {
                if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL)
                {
                    _baseDAO = new NpgsqlBaseDao();
                }
            }
            return _baseDAO;
        }

        public DatabaseImplementUtility GetDatabaseImplementUtility()
        {
            if (_DatabaseUtility == null)
            {
                if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL)
                {
                    _DatabaseUtility = new NpgsqlDatabaseUtility();
                }
                else if (GetDatabaseType() == DATABASE_TYPE.SQLITE)
                {
                    _DatabaseUtility = new SqliteDatabaseUtility();
                }
            }
            return _DatabaseUtility;
        }

        public void SetConnectionConfig(String host, String user, String password, String database, String port)
        {
            _baseDAO = null;
            _DatabaseUtility = null;
            GetDatabaseDAO().SetConnectionConfig(host, user, password, database, port);
        }

        public bool CheckConnection(String host, String user, String password, String database, String port)
        {
            try
            {
                GetDatabaseDAO().SetConnectionConfig(host, user, password, database, port);

                if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" SELECT NOW() AS TIME_NOW ");
                    DataRow row = GetDatabaseDAO().DoGetDataRow(sql.ToString());
                    if (row != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (GetDatabaseType() == DATABASE_TYPE.SQLITE)
                {
                    return File.Exists("Database\\" + database + ".db");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, "Check connection error !", ex);
                return false;
            }
        }

        public DateTime GetCurrentTime(IDbConnection connection, IDbTransaction trans)
        {
            StringBuilder sql = new StringBuilder();

            if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL)
            {
                sql.Append(" SELECT NOW()::TIMESTAMP WITHOUT TIME ZONE AS TIME_NOW ");
            }
            else if (GetDatabaseType() == DATABASE_TYPE.MYSQL)
            {
                sql.Append(" SELECT NOW() AS TIME_NOW ");
            }
            else
            {
                return DateTime.Now;
            }

            DataRow row = GetDatabaseDAO().DoGetDataRow(connection, trans, sql.ToString());

            DateTime dt = new DateTime();
            if (row != null)
            {
                if (row["TIME_NOW"] != System.DBNull.Value)
                {
                    DateTime.TryParse(row["TIME_NOW"].ToString(), out dt);
                }
                else
                {
                    SanitaLogEx.e("X100", "GetCurrentTime.row.TIME = null");
                }
            }
            else
            {
                SanitaLogEx.e("X100", "GetCurrentTime.row = null");
            }

#if false
            //Update local time
            if (dt.Year > 2001)
            {
                UtilityDate.SetLocalTime(dt);
            }
#endif

            return dt;
        }

        public int GetCurrentConnectionCount(IDbConnection connection, IDbTransaction trans)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT sum(numbackends) as CNT FROM pg_stat_database; ");

            DataRow row = GetDatabaseDAO().DoGetDataRow(connection, trans, sql.ToString());

            int count = 0;
            if (row != null)
            {
                if (row["CNT"] != System.DBNull.Value)
                {
                    int.TryParse(row["CNT"].ToString(), out count);
                }
            }

            return count;
        }

        public int GetCurrentConnectionCount_Max(IDbConnection connection, IDbTransaction trans)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT setting as CNT FROM   pg_settings WHERE  name = 'max_connections'; ");

            DataRow row = GetDatabaseDAO().DoGetDataRow(connection, trans, sql.ToString());

            int count = 0;
            if (row != null)
            {
                if (row["CNT"] != System.DBNull.Value)
                {
                    int.TryParse(row["CNT"].ToString(), out count);
                }
            }

            return count;
        }

        #endregion


        #region Utility methods

        public static string Escape(string s)// Lấy chuyển string thành 'string'
        {
            if (String.IsNullOrEmpty(s))
            {
                if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL || GetDatabaseType() == DATABASE_TYPE.SQLITE)
                {
                    return "''";
                }
                else
                {
                    return "NULL";
                }
            }
            else
            {
                return "'" + s.Replace("'", "''") + "'";
            }
        }

        public static string Escape(bool s)
        {
            if (s)
            {
                return "'1'";
            }
            else
            {
                return "'0'";
            }
        }

        public static string Escape(int s)
        {
            return "'" + s.ToString() + "'";
        }

        public static string Escape(IList<int> s)
        {
            return "(" + String.Join(",", s.Select(p => p.ToString()).ToArray()) + ")";
        }

        public static string Escape(long s)
        {
            return "'" + s.ToString() + "'";
        }

        public static string Escape(byte[] s)
        {
            if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL)
            {
                return SanitaUtility.ConvertBinary2HexString_POSTGRES(s);
            }
            else if (GetDatabaseType() == DATABASE_TYPE.MSSQL)
            {
                return SanitaUtility.ConvertBinary2HexString_MYSQL(s);
            }
            else if (GetDatabaseType() == DATABASE_TYPE.SQLITE)
            {
                return SanitaUtility.ConvertBinary2HexString_SQLITE(s);
            }

            return "";
        }

        public static string Escape(double s)
        {
            return "'" + s.ToString(CultureInfo.InvariantCulture) + "'";
        }

        public static string Escape(ulong s)
        {
            if (s == 0)
            {
                return "NULL";
            }
            else
            {
                DateTime dt = DateTime.FromBinary((long)s);
                return string.Format("'{0:yyyy-MM-dd HH:mm:ss}'", dt);
            }
        }

        public static string Escape(DateTime s)
        {
            return String.Format("'{0:d}'", s);
            /*return string.Format("'{0:yyyy-MM-dd HH:mm:ss}'", s);*/// thay doi cach nhap thoi gian
        }

        #endregion

    }
}
