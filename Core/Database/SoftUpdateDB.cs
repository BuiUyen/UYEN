using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Sanita.Utility.Database.BaseDao;
using Sanita.Utility.Database.Utility;
using Medibox.Model;

namespace Medibox.Database
{
    public class SoftUpdateDB : BaseDB
    {
        //Constant
        private const String TAG = "SoftUpdateDB";
        //Singleton
        private static SoftUpdateDB _instance;
        public static SoftUpdateDB mInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SoftUpdateDB();
                }
                return _instance;
            }
        }
        public IList<SoftUpdate> GetSoftUpdates()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM tb_softupdate ");
            DataTable dt = baseDAO.DoGetDataTable(sql.ToString());
            return (MakeSoftUpdates(dt));
        }

        public SoftUpdate GetSoftUpdate()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM tb_softupdate ");
            DataRow row = baseDAO.DoGetDataRow(sql.ToString());
            return (MakeSoftUpdate(row));
        }

        public SoftUpdate GetSoftUpdate(int SoftUpdateID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM tb_softupdate ");
            sql.Append(" WHERE SoftUpdateID = " + SoftUpdateID.Escape());
            DataRow row = baseDAO.DoGetDataRow(sql.ToString());
            return (MakeSoftUpdate(row));
        }

        public int UpdateSoftUpdate(IDbConnection connection, IDbTransaction trans, SoftUpdate data)
        {
            lock (lockObject)
            {
                if (data == null)
                {
                    return DataTypeModel.RESULT_NG;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE tb_softupdate ");
                sql.Append("  SET  ");
                sql.Append("      SoftUpdateVersion = " + data.SoftUpdateVersion.Escape()).Append(", ");
                sql.Append("      SoftUpdateSQL = " + data.SoftUpdateSQL.Escape()).Append(", ");
                sql.Append("      SoftUpdateData = " + data.SoftUpdateData.Escape()).Append(", ");
                sql.Append("      SoftUpdateSize = " + data.SoftUpdateSize.Escape()).Append(", ");
                sql.Append("      SoftUpdateUser = " + data.SoftUpdateUser.Escape()).Append(", ");
                sql.Append("      SoftUpdateTime = " + data.SoftUpdateTime.Escape()).Append(", ");
                sql.Append("      SoftUpdateKey = " + data.SoftUpdateKey.Escape());
                sql.Append("  WHERE SoftUpdateID = " + data.SoftUpdateID);
                return baseDAO.DoUpdate(connection, trans, sql.ToString());
            }
        }
        public int InsertSoftUpdate(IDbConnection connection, IDbTransaction trans, SoftUpdate data)
        {
            lock (lockObject)
            {
                if (data == null)
                {
                    return DataTypeModel.RESULT_NG;
                }
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO tb_softupdate (");
                sql.Append("            SoftUpdateVersion,");
                sql.Append("            SoftUpdateSQL,");
                sql.Append("            SoftUpdateData,");
                sql.Append("            SoftUpdateSize,");
                sql.Append("            SoftUpdateUser,");
                sql.Append("            SoftUpdateTime,");
                sql.Append("            SoftUpdateKey)");
                sql.Append("  VALUES( ");
                sql.Append("          " + data.SoftUpdateVersion.Escape()).Append(", ");
                sql.Append("          " + data.SoftUpdateSQL.Escape()).Append(", ");
                sql.Append("          " + data.SoftUpdateData.Escape()).Append(", ");
                sql.Append("          " + data.SoftUpdateSize.Escape()).Append(", ");
                sql.Append("          " + data.SoftUpdateUser.Escape()).Append(", ");
                sql.Append("          " + data.SoftUpdateTime.Escape()).Append(", ");
                sql.Append("          " + data.SoftUpdateKey.Escape()).Append(") ");
                int newId = baseDAO.DoInsert(connection, trans, sql.ToString());
                data.SoftUpdateID = newId;
                if (newId > 0)
                {
                    return newId;
                }
                else
                {
                    return DataTypeModel.RESULT_NG;
                }
            }
        }

        public SoftUpdate GetSoftUpdate_Simple(IDbConnection connection, IDbTransaction trans)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT SoftUpdateID,SoftUpdateVersion,SoftUpdateSQL ");
            sql.Append(" FROM tb_softupdate order by SoftUpdateID desc;");
            DataRow row = baseDAO.DoGetDataRow(connection, trans, sql.ToString());
            return (MakeSoftUpdate(row));
        }

        public int UpdateSoftUpdate_SQL(IDbConnection connection, IDbTransaction trans, SoftUpdate data)
        {
            lock (lockObject)
            {
                if (data == null)
                {
                    return DataTypeModel.RESULT_NG;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE tb_softupdate ");
                sql.Append("  SET  ");
                sql.Append("      SoftUpdateSQL = " + data.SoftUpdateSQL.Escape()).Append(" ");
                sql.Append("  WHERE SoftUpdateID = " + data.SoftUpdateID);
                return baseDAO.DoUpdate(connection, trans, sql.ToString());
            }
        }

        public int DeleteSoftUpdate(SoftUpdate data)
        {
            lock (lockObject)
            {
                if (data == null)
                {
                    return DataTypeModel.RESULT_NG;
                }
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM tb_softupdate  ");
                sql.Append("  WHERE SoftUpdateID = " + DatabaseUtility.Escape(data.SoftUpdateID));
                return baseDAO.DoUpdate(sql.ToString());
            }
        }
        #region Utility
        private IList<SoftUpdate> MakeSoftUpdates(DataTable dt)
        {
            IList<SoftUpdate> list = new List<SoftUpdate>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MakeSoftUpdate(row));
                }
            }
            return list;
        }
        private SoftUpdate MakeSoftUpdate(DataRow row)
        {
            SoftUpdate record = new SoftUpdate();
            if (row != null)
            {
                record.SetProperty(row);
            }
            return record;
        }
        #endregion
    }
}
