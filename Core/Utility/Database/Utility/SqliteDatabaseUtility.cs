using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Sanita.Utility.ExtendedThread;
using Sanita.Utility.Logger;
using Npgsql;
using Sanita.Utility.Database.BaseDao;

namespace Sanita.Utility.Database.Utility
{
    public class SqliteDatabaseUtility : DatabaseImplementUtility
    {
        private const String TAG = "SqliteDatabaseUtility";

        public override bool isDatabaseOK()
        {
            //Get database name
            String DatabaseName = myBaseDao.GetDatabaseName();
            String file_name = "Database\\" + DatabaseName + ".db";

            SanitaLog.d(TAG, "isDatabaseOK");

            //If have no database -> create datbase
            if (!File.Exists(file_name))
            {
                SanitaLog.d(TAG, "Create database : " + file_name);
                CreateDatabase(DatabaseName);
            }

            System.Threading.Thread.Sleep(1000);

            //If have no database -> create datbase
            if (!File.Exists(file_name))
            {
                SanitaLog.d(TAG, "false");
                return false;
            }
            else
            {
                SanitaLog.d(TAG, "true");
                return true;
            }
        }


        public override bool UpdateDatabaseSequence(IDbConnection connection, IDbTransaction trans, ExBackgroundWorker worker)
        {
            return false;
        }

        public override bool SynchDatabase(IDbConnection connection, IDbTransaction trans, ExBackgroundWorker worker, int number_hosobenhan, bool IsCapNhatTrigger)
        {
            synch_worker = worker;

            //Get database name
            String DatabaseName = myBaseDao.GetDatabaseName();
            String file_name = "Database\\" + DatabaseName + ".db";

            SanitaLogEx.d(TAG, "[SynchDatabase] Database = [" + DatabaseName + "]");

            //If have no database -> create datbase
            if (!File.Exists("Database\\" + DatabaseName + ".db"))
            {
                SanitaLog.e(TAG, "[SynchDatabase] Database chưa tồn tại");
                CreateDatabase(DatabaseName);
            }

            //Get all base table
            IList<ClassTable> listTable = GetListTable(connection, trans, DatabaseName);

            //Synch database
            if (synch_worker != null)
            {
                synch_worker.ReportProgress(0, listFixTable.Count);
            }
            for (int i = 0; i < listFixTable.Count; i++)
            {
                if (synch_worker != null)
                {
                    synch_worker.ReportProgress(i + 1, "Nâng cấp table '" + listFixTable[i].Table + "...");
                }

                //Convert mysql to sqlite
                for (int j = 0; j < listFixTable[i].listColumn.Count; j++)
                {
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10) unsigned NOT NULL auto_increment", "INTEGER");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10) unsigned", "INTEGER");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10)", "INTEGER");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(11)", "INTEGER");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("text CHARACTER SET utf8 COLLATE utf8_unicode_ci", "text");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("longblob", "BLOB");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("double", "REAL");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("Datetime DEFAULT '1-1-1'", "TEXT");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("datetime DEFAULT '1-1-1'", "TEXT");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("Datetime", "TEXT");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("datetime", "TEXT");
                    listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP", "TEXT");
                }

                //Tim xem co table ton tai chua
                var foundTable = listTable.FirstOrDefault(p => p.Table.Equals(listFixTable[i].Table, StringComparison.CurrentCultureIgnoreCase));
                if (foundTable == null)
                {
                    //Create table
                    SanitaLogEx.d(TAG, "[SynchDatabase] Create table '" + listFixTable[i].Table + "'");
                    if (CreateTable(connection, trans, DatabaseName, listFixTable[i]) <= -100)
                    {
                        SanitaLogEx.e(TAG, "[SynchDatabase] Create table lỗi");
                        if (synch_worker != null)
                        {
                            synch_worker.ReportProgress(-1, "Tạo table '" + listFixTable[i].Table + "' lỗi !");
                        }
                        return false;
                    }
                }
                else
                {
                    //Alter table
                    SanitaLogEx.e(TAG, "[SynchDatabase] Alter table '" + listFixTable[i].Table + "'");
                    if (AlterTableTable(connection, trans, DatabaseName, listFixTable[i], foundTable) <= -100)
                    {
                        SanitaLogEx.e(TAG, "[SynchDatabase] Alter table lỗi");
                        if (synch_worker != null)
                        {
                            synch_worker.ReportProgress(-1, "Update table '" + listFixTable[i].Table + "' lỗi !");
                        }
                        return false;
                    }
                }

                if (synch_worker != null)
                {
                    synch_worker.ReportProgress(i + 1, "Nâng cấp table '" + listFixTable[i].Table + "' OK !");
                }
            }

            return true;
        }

        public override int AlterTableTable(IDbConnection connection, IDbTransaction trans, String DatabaseName, ClassTable Fixtable, ClassTable NewTable)
        {            
            try
            {
                //Update column
                for (int i = 0; i < Fixtable.listColumn.Count; i++)
                {
                    //Tim column tuong ung trong NewTable
                    var foundColumn = NewTable.listColumn.FirstOrDefault(p => p.ColumnName.ToLower() == Fixtable.listColumn[i].ColumnName.ToLower());
                    if (foundColumn == null)
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.Append(" ALTER TABLE " + Fixtable.Table + "     ");
                        sql.Append(" ADD COLUMN " + Fixtable.listColumn[i].ColumnName + " " + Fixtable.listColumn[i].ColumnDefine + Fixtable.listColumn[i].ColumnAfter + "  ");

                        int ret = myBaseDao.DoUpdate(connection, trans, sql.ToString());                        
                        if (ret < 0)
                        {
                            return ret;
                        }

                        SanitaLogEx.e(TAG, "      >>Add column '" + Fixtable.listColumn[i].ColumnName + "'");
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                SanitaLogEx.e(TAG, ex);
                return -100;
            }            
        }

        public override int CreateTable(IDbConnection connection, IDbTransaction trans, String DatabaseName, ClassTable table)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.Append(" CREATE TABLE " + table.Table + " (  ");
                for (int i = 0; i < table.listColumn.Count; i++)
                {
                    sql.Append(" " + table.listColumn[i].ColumnName + " " + table.listColumn[i].ColumnDefine);
                    if (table.listColumn[i].isPRIMARY)
                    {
                        sql.Append(" PRIMARY KEY ");
                    }

                    if (i < table.listColumn.Count - 1)
                    {
                        sql.Append(" , ");
                    }
                }

                sql.Append(" );  ");
            }
            catch (Exception ex)
            {
                SanitaLogEx.e(TAG, ex);
            }

            int ret = -100;
            try
            {
                ret = myBaseDao.DoUpdate(connection, trans, sql.ToString());
                return ret;
            }

            catch { return -100; }
        }

        public override void GetSchema(IDbConnection connection, IDbTransaction trans, string DatabaseName)
        {
            IList<ClassTable> listTable = GetListTable(connection, trans, DatabaseName);
            StreamWriter writer = new StreamWriter("C://test.txt", false, Encoding.UTF8);
            for (int i = 0; i < listTable.Count; i++)
            {
                writer.WriteLine("#region " + listTable[i].Table);
                writer.WriteLine("ClassTable " + listTable[i].Table + " = new ClassTable();");
                writer.WriteLine("" + listTable[i].Table + ".Table = \"" + listTable[i].Table + "\";");
                writer.WriteLine("{");
                writer.WriteLine("    IList<ClassColumn> listColumn = new List<ClassColumn>();");
                for (int j = 0; j < listTable[i].listColumn.Count; j++)
                {
                    writer.WriteLine("    {");
                    writer.WriteLine("        ClassColumn Column = new ClassColumn();");
                    writer.WriteLine("        Column.ColumnName = \"" + listTable[i].listColumn[j].ColumnName + "\";");
                    writer.WriteLine("        Column.ColumnDefine = \"" + listTable[i].listColumn[j].ColumnDefine + "\";");
                    if (listTable[i].listColumn[j].isPRIMARY)
                    {
                        writer.WriteLine("        Column.isPRIMARY = true;");
                    }
                    else
                    {
                        writer.WriteLine("        Column.isPRIMARY = false;");
                    }

                    writer.WriteLine("        listColumn.Add(Column);");
                    writer.WriteLine("    }");
                }
                writer.WriteLine("    " + listTable[i].Table + ".listColumn = listColumn;");
                writer.WriteLine("}");
                writer.WriteLine("listFixTable.Add(" + listTable[i].Table + ");");

                writer.WriteLine("#endregion");
            }
            writer.Close();
        }

        public override int CreateDatabase(string DatabaseName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SHOW DATABASES ; ");

            int ret = -100;

            try
            {
                ret = myBaseDao.DoCreateDatabase(sql.ToString());
                return ret;
            }

            catch { return -100; }
        }

        public override IList<ClassTable> GetListTable(IDbConnection connection, IDbTransaction trans, string DatabaseName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM sqlite_master WHERE type='table'; ");

            DataTable dt = myBaseDao.DoGetDataTable(connection, trans, sql.ToString());
            IList<ClassTable> list = new List<ClassTable>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ClassTable record = new ClassTable();
                    if (row != null)
                    {
                        if (row["name"] != System.DBNull.Value)
                        {
                            record.Table = row["name"].ToString();
                            record.listColumn = GetListColumn(connection, trans, DatabaseName, record.Table);
                            record.listIndex = GetListIndex(connection, trans, DatabaseName, record.Table);
                        }
                    }
                    list.Add(record);
                }
            }
            return list;
        }

        public override IList<ClassColumn> GetListColumn(IDbConnection connection, IDbTransaction trans, string DatabaseName, string TableName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" pragma table_info(" + TableName + "); ");

            DataTable dt = myBaseDao.DoGetDataTable(connection, trans, sql.ToString());
            IList<ClassColumn> list = new List<ClassColumn>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ClassColumn record = new ClassColumn();
                    if (row != null)
                    {
                        if (row["name"] != System.DBNull.Value)
                        {
                            record.ColumnName = row["name"].ToString();
                        }
                        if (row["Type"] != System.DBNull.Value)
                        {
                            record.ColumnDefine += " " + row["Type"].ToString();
                        }
                        record.isPRIMARY = false;
                        if (row["pk"] != System.DBNull.Value)
                        {
                            if (row["pk"].ToString() == "1")
                            {
                                record.isPRIMARY = true;
                                record.ColumnDefine += " PRIMARY KEY ";
                            }
                        }
                    }
                    list.Add(record);
                }
            }
            return list;
        }

        public override IList<ClassColumn> GetListIndex(IDbConnection connection, IDbTransaction trans, string DatabaseName, string TableName)
        {
            IList<ClassColumn> list = new List<ClassColumn>();
            return list;
        }
    }
}