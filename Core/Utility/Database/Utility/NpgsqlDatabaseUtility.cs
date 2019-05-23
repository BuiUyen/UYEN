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
    public class NpgsqlDatabaseUtility : DatabaseImplementUtility
    {
        private const String TAG = "NpgsqlDatabaseUtility";

        public override void InitNotification(IList<String> mListChannel, OnDatabaseNotificationHandler mCallback)
        {
            myBaseDao.InitNotification(mListChannel, mCallback);
        }

        public override void DoNotification(String chanel, String data)
        {
            myBaseDao.DoNotification(chanel, data);
        }

        public override bool isDatabaseOK()
        {
            //Get database name
            String DatabaseName = myBaseDao.GetDatabaseName();

            //Get list database
            IList<ClassDatabase> listDatabase = GetListDatabase();

            //If have no database -> create datbase
            if (!listDatabase.Any(p => p.Database.Equals(DatabaseName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool UpdateDatabaseSequence(IDbConnection connection, IDbTransaction trans, ExBackgroundWorker worker)
        {
            synch_worker = worker;

            String sql = SanitaUtility.GetResourceFileStreamText("update_sequence");
            if (!String.IsNullOrEmpty(sql))
            {
                DataTable mDataTable = myBaseDao.DoGetDataTable(connection, trans, sql);
                if (mDataTable != null)
                {
                    //Synch database
                    if (synch_worker != null)
                    {
                        synch_worker.ReportProgress(0, mDataTable.Rows.Count);
                    }
                    int index_sql = 0;
                    foreach (DataRow row in mDataTable.Rows)
                    {
                        if (row["sql_column"] != null)
                        {
                            String sql_update = row["sql_column"].ToString();
                            myBaseDao.DoUpdate(connection, trans, sql_update);
                        }

                        index_sql++;
                        synch_worker.ReportProgress(index_sql, mDataTable.Rows.Count);
                    }
                }
            }

            return true;
        }

        public override bool SynchDatabase(IDbConnection connection, IDbTransaction trans, ExBackgroundWorker worker, int number_hosobenhan, bool IsCapNhatTrigger)
        {
            synch_worker = worker;

            //Get database name
            String DatabaseName = myBaseDao.GetDatabaseName();
            SanitaLogEx.d(TAG, "[SynchDatabase] Database = [" + DatabaseName + "]");

            //Get all base table
            IList<ClassTable> listTable = GetListTable(connection, trans, DatabaseName);

            //Add new table
            {
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

                    //Convert mysql to postgres
                    for (int j = 0; j < listFixTable[i].listColumn.Count; j++)
                    {
                        if (!listFixTable[i].listColumn[j].isUpdated)
                        {
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10) unsigned NOT NULL auto_increment", "serial");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10) unsigned", "INTEGER");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10) unsigned NOT NULL", "INTEGER");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10)", "INTEGER");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("int(11)", "INTEGER");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("text CHARACTER SET utf8 COLLATE utf8_unicode_ci", "text");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("longblob", "bytea");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("double", "double precision");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("Datetime DEFAULT '1-1-1'", "TIMESTAMP");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("datetime DEFAULT '1-1-1'", "TIMESTAMP");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("Datetime", "TIMESTAMP");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("datetime", "TIMESTAMP");
                            listFixTable[i].listColumn[j].ColumnDefine = listFixTable[i].listColumn[j].ColumnDefine.Replace("timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP", "TIMESTAMP");

                            listFixTable[i].listColumn[j].isUpdated = true;
                        }
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
            }

            //Delete old table
            if(false)
            {
                //Synch database
                if (synch_worker != null)
                {
                    synch_worker.ReportProgress(0, listTable.Count);
                }
                for (int i = 0; i < listTable.Count; i++)
                {
                    if (synch_worker != null)
                    {
                        synch_worker.ReportProgress(i + 1, "Check cấp table '" + listTable[i].Table + "...");
                    }

                    //Tim xem co table ton tai chua
                    var foundTable = listFixTable.FirstOrDefault(p => p.Table.Equals(listTable[i].Table, StringComparison.CurrentCultureIgnoreCase));
                    if (foundTable == null)
                    {
                        //Create table
                        SanitaLogEx.d(TAG, "[SynchDatabase] Delete table '" + listTable[i].Table + "'");
                        if (DeleteTable(connection, trans, DatabaseName, listTable[i]) <= -100)
                        {
                            SanitaLogEx.e(TAG, "[SynchDatabase] Delete table lỗi");
                            if (synch_worker != null)
                            {
                                synch_worker.ReportProgress(-1, "Delete table '" + listTable[i].Table + "' lỗi !");
                            }
                            return false;
                        }
                    }
                    else
                    {
                        //Alter table
                        SanitaLogEx.e(TAG, "[SynchDatabase] Alter table '" + listTable[i].Table + "'");
                        if (AlterTableTable_DeleteColumn(connection, trans, DatabaseName, listTable[i], foundTable) <= -100)
                        {
                            SanitaLogEx.e(TAG, "[SynchDatabase] Alter table lỗi");
                            if (synch_worker != null)
                            {
                                synch_worker.ReportProgress(-1, "Update table '" + listTable[i].Table + "' lỗi !");
                            }
                            return false;
                        }
                    }

                    if (synch_worker != null)
                    {
                        synch_worker.ReportProgress(i + 1, "Nâng cấp table '" + listTable[i].Table + "' OK !");
                    }
                }
            }

            return true;
        }

        public override int AlterTableTable(IDbConnection connection, IDbTransaction trans, String DatabaseName, ClassTable Fixtable, ClassTable NewTable)
        {
            StringBuilder sql = new StringBuilder();
            bool isNeedAddColumn = false;
            bool isNeedAddIndex = false;
            string strPRIMARY = "";

            try
            {
                sql.Append(" ALTER TABLE " + Fixtable.Table + "     ");

                //Update column
                for (int i = 0; i < Fixtable.listColumn.Count; i++)
                {
                    //Add PRIMARY KEY
                    if (Fixtable.listColumn[i].isPRIMARY)
                    {
                        if (string.IsNullOrEmpty(strPRIMARY))
                        {
                            strPRIMARY = "'" + Fixtable.listColumn[i].ColumnName + "'";
                        }
                        else
                        {
                            strPRIMARY += " , " + "'" + Fixtable.listColumn[i].ColumnName + "'";
                        }
                    }

                    //Tim column tuong ung trong NewTable
                    bool isupdateSQL = false;
                    var foundColumn = NewTable.listColumn.FirstOrDefault(p => p.ColumnName.ToLower() == Fixtable.listColumn[i].ColumnName.ToLower());
                    if (foundColumn == null)
                    {
                        //AliboboLog.e("[SynchSchema] Add column '" + Fixtable.listColumn[i].ColumnName + "'");
                        sql.Append(" ADD COLUMN " + Fixtable.listColumn[i].ColumnName + " " + Fixtable.listColumn[i].ColumnDefine + "  ");
                        isNeedAddColumn = true;
                        isupdateSQL = true;

                        SanitaLogEx.e(TAG, "      >>Add column '" + Fixtable.listColumn[i].ColumnName + "'");
                    }
                    if (i < Fixtable.listColumn.Count - 1)
                    {
                        if (isupdateSQL)
                        {
                            sql.Append(" , ");
                        }
                    }
                    else //Da check den column cuoi cung
                    {
                        //Them dau ket thuc lenh
                        sql.Append(" ; ");
                    }
                }

                //Reset
                if (!isNeedAddColumn)
                {
                    sql = new StringBuilder();
                }

                //Update index
                for (int i = 0; i < Fixtable.listColumn.Count; i++)
                {
                    if (Fixtable.listColumn[i].isIndex)
                    {
                        String index_name = String.Format("{0}_{1}_idx", Fixtable.Table.ToLower(), Fixtable.listColumn[i].ColumnName.ToLower().Replace(Fixtable.Table.ToLower().Replace("tb_", ""), ""));
                        var foundIndex = NewTable.listIndex.FirstOrDefault(p => p.ColumnName.Equals(index_name, StringComparison.CurrentCultureIgnoreCase));
                        if (foundIndex == null)
                        {
                            sql.Append(String.Format("CREATE INDEX {2} ON {0} USING btree ({1});", Fixtable.Table, Fixtable.listColumn[i].ColumnName, index_name));
                            isNeedAddIndex = true;
                            SanitaLogEx.e(TAG, "      >>Add index '" + Fixtable.listColumn[i].ColumnName + "'");
                        }
                    }
                }
                sql.Replace(",  ; ", "; ");
            }
            catch (Exception ex)
            {
                SanitaLogEx.e(TAG, ex);
            }

            int ret = -100;
            try
            {
                if (isNeedAddColumn || isNeedAddIndex)
                {
                    ret = myBaseDao.DoUpdate(connection, trans, sql.ToString());
                    return ret;
                }
                else
                {
                    return 0;
                }
            }

            catch { return -100; }
        }

        public int AlterTableTable_DeleteColumn(IDbConnection connection, IDbTransaction trans, String DatabaseName, ClassTable NewTable, ClassTable Fixtable)
        {
            StringBuilder sql = new StringBuilder();
            bool isNeedDeleteColumn = false;

            try
            {
                //Update column
                for (int i = 0; i < NewTable.listColumn.Count; i++)
                {
                    //Tim column tuong ung trong NewTable                    
                    var foundColumn = Fixtable.listColumn.FirstOrDefault(p => p.ColumnName.ToLower() == NewTable.listColumn[i].ColumnName.ToLower());
                    if (foundColumn == null)
                    {
                        sql.Append(" ALTER TABLE " + NewTable.Table + " DROP COLUMN " + NewTable.listColumn[i].ColumnName + "; ");
                        isNeedDeleteColumn = true;
                        SanitaLogEx.e(TAG, "      >>Drop column '" + NewTable.listColumn[i].ColumnName + "'");
                    }
                }
                sql.Replace(",  ; ", "; ");
            }
            catch (Exception ex)
            {
                SanitaLogEx.e(TAG, ex);
            }

            int ret = -100;
            try
            {
                if (isNeedDeleteColumn)
                {
                    ret = myBaseDao.DoUpdate(connection, trans, sql.ToString());
                    return ret;
                }
                else
                {
                    return 0;
                }
            }

            catch { return -100; }
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

                sql.Append(" ) WITH OIDS ;  ");
                //sql.Append(" );  ");

                //Update index
                for (int i = 0; i < table.listColumn.Count; i++)
                {
                    if (table.listColumn[i].isIndex)
                    {
                        sql.Append(String.Format("CREATE INDEX {0}_{1}_idx ON {0} USING btree ({2});", table.Table.ToLower(), table.listColumn[i].ColumnName.ToLower().Replace(table.Table.ToLower().Replace("tb_", ""), ""), table.listColumn[i].ColumnName));
                    }
                }
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

        public int DeleteTable(IDbConnection connection, IDbTransaction trans, String DatabaseName, ClassTable table)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.Append(" DROP TABLE " + table.Table + " ;  ");
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

        public override IList<ClassDatabase> GetListDatabase()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT datname FROM pg_database; ");

            DataTable dt = myBaseDao.DoShowDatabase(sql.ToString());
            IList<ClassDatabase> list = new List<ClassDatabase>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ClassDatabase record = new ClassDatabase();
                    if (row != null)
                    {
                        if (row["datname"] != System.DBNull.Value)
                        {
                            record.Database = row["datname"].ToString();
                        }
                    }
                    list.Add(record);
                }
            }
            return list;
        }

        public override int CreateDatabase(string DatabaseName)
        {
            if (string.IsNullOrEmpty(DatabaseName))
            {
                return -100;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(" CREATE DATABASE " + DatabaseName);

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
            IList<ClassColumn> list_Column_All = GetListColumn(connection, trans, DatabaseName, "");
            IList<ClassColumn> list_Index_All = GetListIndex(connection, trans, DatabaseName, "");

            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from information_schema.tables where table_schema = '" + "public" + "'  ");

            DataTable dt = myBaseDao.DoGetDataTable(connection, trans, sql.ToString());
            IList<ClassTable> list = new List<ClassTable>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ClassTable record = new ClassTable();
                    if (row != null)
                    {
                        if (row["table_name"] != System.DBNull.Value)
                        {
                            record.Table = row["table_name"].ToString();
                            record.listColumn = list_Column_All.Where(p => p.TableName.EqualText(record.Table)).ToList();
                            record.listIndex = list_Index_All.Where(p => p.TableName.EqualText(record.Table)).ToList();
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
            if (!String.IsNullOrEmpty(TableName))
            {
                sql.Append(" select * from information_schema.columns where table_schema = '" + "public" + "' and table_name ='" + TableName + "'  ");
            }
            else
            {
                sql.Append(" select * from information_schema.columns where table_schema = '" + "public" + "' ");
            }

            DataTable dt = myBaseDao.DoGetDataTable(connection, trans, sql.ToString());
            IList<ClassColumn> list = new List<ClassColumn>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ClassColumn record = new ClassColumn();
                    if (row != null)
                    {
                        if (row["table_name"] != System.DBNull.Value)
                        {
                            record.TableName = row["table_name"].ToString();
                        }
                        if (row["column_name"] != System.DBNull.Value)
                        {
                            record.ColumnName = row["column_name"].ToString();
                        }
                        if (row["data_type"] != System.DBNull.Value)
                        {
                            record.ColumnDefine += " " + row["data_type"].ToString();
                        }
                        if (row["is_nullable"] != System.DBNull.Value)
                        {
                            if (row["is_nullable"].ToString() == "NO")
                            {
                                record.ColumnDefine += " " + "NOT NULL";

                                if (row["ordinal_position"] != System.DBNull.Value)
                                {
                                    if ((int)row["ordinal_position"] == 1)
                                    {
                                        record.isPRIMARY = true;
                                    }
                                }
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
            StringBuilder sql = new StringBuilder();
            if (!String.IsNullOrEmpty(TableName))
            {
                sql.Append(String.Format("SELECT  relname FROM pg_class WHERE oid IN (SELECT indexrelid FROM pg_index, pg_class WHERE pg_class.relname='{0}' AND pg_class.oid=pg_index.indrelid AND indisunique != 't' AND indisprimary != 't')", TableName));

                DataTable dt = myBaseDao.DoGetDataTable(connection, trans, sql.ToString());
                IList<ClassColumn> list = new List<ClassColumn>();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ClassColumn record = new ClassColumn();
                        if (row != null)
                        {
                            if (row["relname"] != System.DBNull.Value)
                            {
                                record.ColumnName = row["relname"].ToString();
                            }
                        }
                        list.Add(record);
                    }
                }
                return list;

            }
            else
            {
                sql.Append("SELECT idx.indrelid :: REGCLASS AS table_name, i.relname AS index_name FROM pg_index AS idx JOIN pg_class AS i ON i.oid = idx.indexrelid JOIN pg_am AS am ON i.relam = am.oid JOIN pg_namespace AS NS ON i.relnamespace = NS.OID JOIN pg_user AS U ON i.relowner = U.usesysid WHERE NOT nspname LIKE 'pg%' and idx.indisunique != 't' and idx.indisprimary != 't';");

                DataTable dt = myBaseDao.DoGetDataTable(connection, trans, sql.ToString());
                IList<ClassColumn> list = new List<ClassColumn>();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ClassColumn record = new ClassColumn();
                        if (row != null)
                        {
                            if (row["table_name"] != System.DBNull.Value)
                            {
                                record.TableName = row["table_name"].ToString();
                            }
                            if (row["index_name"] != System.DBNull.Value)
                            {
                                record.ColumnName = row["index_name"].ToString();
                            }
                        }
                        list.Add(record);
                    }
                }
                return list;
            }
        }
    }
}