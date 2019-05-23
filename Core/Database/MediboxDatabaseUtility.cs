using System;
using System.Collections.Generic;
using Sanita.Utility.Database.BaseDao;
using Sanita.Utility.Database.Utility;

namespace Medibox.Database
{
    public class MediboxDatabaseUtility
    {
        private const String TAG = "MediboxDatabaseUtility";
        private static DatabaseUtility mDatabaseUtility_Main = new DatabaseUtility();
        public static void InitDatabase()
        {
            mDatabaseUtility_Main.GetDatabaseImplementUtility().InitDatabase(GetDatabaseDAO(), InitTable());
        }
        public static IBaseDao GetDatabaseDAO()
        {
            return mDatabaseUtility_Main.GetDatabaseDAO();
        }
        public static DatabaseUtility GetDatabaseUtility()
        {
            return mDatabaseUtility_Main;
        }
        public static string GetDatabaseVersion()
        {
            return "10";
        }
        public static List<ClassTable> InitTable()
        {
            List<ClassTable> listFixTable = new List<ClassTable>();


            #region tb_thanhtoan
            ClassTable tb_thanhtoan = new ClassTable();
            tb_thanhtoan.Table = "tb_thanhtoan";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "ThanhToanID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "HoaDonID";
                    Column.ColumnDefine = " int(10) DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "GiaTien";
                    Column.ColumnDefine = " double DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "HinhThucThanhToan";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "NgayThanhToan";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "GhiChu";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_thanhtoan.listColumn = listColumn;
            }
            listFixTable.Add(tb_thanhtoan);
            #endregion

            #region tb_hoadoninfo
            ClassTable tb_hoadoninfo = new ClassTable();
            tb_hoadoninfo.Table = "tb_hoadoninfo";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "HoaDonInfoID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "HoaDonID";
                    Column.ColumnDefine = " int(10) DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "DichVuID";
                    Column.ColumnDefine = " int(10) DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TimeSuDung";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "DonGia";
                    Column.ColumnDefine = " double DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_hoadoninfo.listColumn = listColumn;
            }
            listFixTable.Add(tb_hoadoninfo);
            #endregion

            #region tb_hoadon
            ClassTable tb_hoadon = new ClassTable();
            tb_hoadon.Table = "tb_hoadon";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "HoaDonID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "KhachHangID";
                    Column.ColumnDefine = " int(10) DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "PhongID";
                    Column.ColumnDefine = " int(10) DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TimeIn";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TimeOut";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "DatCoc";
                    Column.ColumnDefine = " double DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Status";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_hoadon.listColumn = listColumn;
            }
            listFixTable.Add(tb_hoadon);
            #endregion

            #region tb_dichvu
            ClassTable tb_dichvu = new ClassTable();
            tb_dichvu.Table = "tb_dichvu";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "DichVuID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TenDichVu";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "GiaTien";
                    Column.ColumnDefine = " double DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_dichvu.listColumn = listColumn;
            }
            listFixTable.Add(tb_dichvu);
            #endregion

            #region tb_khachhang
            ClassTable tb_khachhang = new ClassTable();
            tb_khachhang.Table = "tb_khachhang";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "KhachHangID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TenKhachHang";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoCMND";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoDienThoai";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_khachhang.listColumn = listColumn;
            }
            listFixTable.Add(tb_khachhang);
            #endregion

            #region tb_nhanvien
            ClassTable tb_nhanvien = new ClassTable();
            tb_nhanvien.Table = "tb_nhanvien";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "NhanVienID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TenNhanVien";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "NgaySinh";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoDienThoai";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "ChucVu";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_nhanvien.listColumn = listColumn;
            }
            listFixTable.Add(tb_nhanvien);
            #endregion

            #region tb_loaiphong
            ClassTable tb_loaiphong = new ClassTable();
            tb_loaiphong.Table = "tb_loaiphong";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "ID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "MaLoai";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TenLoai";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "GhiChu";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_loaiphong.listColumn = listColumn;
            }
            listFixTable.Add(tb_loaiphong);
            #endregion

            #region tb_phong
            ClassTable tb_phong = new ClassTable();
            tb_phong.Table = "tb_phong";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "PhongID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TenPhong";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "MaLoai";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "DienTich";
                    Column.ColumnDefine = " double DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "GiaThue";
                    Column.ColumnDefine = " double DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Status";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_phong.listColumn = listColumn;
            }
            listFixTable.Add(tb_phong);
            #endregion

            #region tb_taikhoan
            ClassTable tb_taikhoan = new ClassTable();
            tb_taikhoan.Table = "tb_taikhoan";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TaiKhoanID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "TenTaiKhoan";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "MatKhau";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "QuyenTruyCap";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "MaNhanVien";
                    Column.ColumnDefine = " int(10) DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_taikhoan.listColumn = listColumn;
            }
            listFixTable.Add(tb_taikhoan);
            #endregion

            #region tb_softupdate
            ClassTable tb_softupdate = new ClassTable();
            tb_softupdate.Table = "tb_softupdate";
            {
                IList<ClassColumn> listColumn = new List<ClassColumn>();
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoftUpdateID";
                    Column.ColumnDefine = " int(10) unsigned NOT NULL auto_increment ";
                    Column.isPRIMARY = true;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoftUpdateVersion";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoftUpdateSQL";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoftUpdateData";
                    Column.ColumnDefine = " longblob ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoftUpdateSize";
                    Column.ColumnDefine = " int(10) DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoftUpdateUser";
                    Column.ColumnDefine = " int(10) DEFAULT '0' ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoftUpdateTime";
                    Column.ColumnDefine = " datetime ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "SoftUpdateKey";
                    Column.ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                {
                    ClassColumn Column = new ClassColumn();
                    Column.ColumnName = "Version";
                    Column.ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ";
                    Column.isPRIMARY = false;
                    listColumn.Add(Column);
                }
                tb_softupdate.listColumn = listColumn;
            }
            listFixTable.Add(tb_softupdate);
            #endregion

            return listFixTable;
        }
    }
}
