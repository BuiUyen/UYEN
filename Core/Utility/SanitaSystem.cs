using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using DevComponents.Editors.DateTimeAdv;
using Sanita.Utility;
using Sanita.Utility.UI;

namespace System
{
    //Delegate
    public delegate void OnDatabaseNotificationHandler(String channel, String data);
    public delegate void OnResultEventHandler_PacsDownLoad(bool isInvokeRequired, Object mStudy);

    public static class SanitaSystem
    {
        public static String GetTuoiText(this DateTime Birthday)
        {
            // Get the current date.
            DateTime thisDay = Sanita.Utility.SystemInfo.NOW;

            int ngay_tuoi = (thisDay - Birthday).Days;
            int thang_tuoi = ngay_tuoi / 30;
            if (thang_tuoi < 2)
            {
                return ngay_tuoi + " DAY".Translate();
            }
            else if (thang_tuoi < 72)
            {
                return thang_tuoi + " MTH".Translate();
            }
            else
            {
                return (thisDay.Year - Birthday.Year).ToString();
            }
        }

        public static DateTime GetDateTime(this String data)
        {
            DateTime value = new DateTime();
            var formats = new[] { "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "MM/yyyy", "M/yyyy", "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMdd", "yyyyMM", "yyyyM", "yyyy" };
            DateTime.TryParseExact(data.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out value);
            return value;
        }

        public static DateTime GetDateTime_XML_NGAY_SINH(this String data)
        {
            DateTime value = new DateTime();
            var formats = new[] { "yyyyMMdd", "yyyyMM", "yyyy" };
            DateTime.TryParseExact(data.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out value);
            return value;
        }

        public static DateTime GetDateTime_XML_NGAYTHANGNAM(this String data)
        {
            DateTime value = new DateTime();
            var formats = new[] { "yyyyMMdd" };
            DateTime.TryParseExact(data.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out value);
            return value;
        }

        public static DateTime GetDateTime_XML_THOIGIAN(this String data)
        {
            DateTime value = new DateTime();
            var formats = new[] { "yyyyMMddHHmm" };
            DateTime.TryParseExact(data.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out value);
            return value;
        }

        public static String GetSerializeObject_Json(this Object data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        public static String GetSerializeObject_Json_Format(this Object data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
        }

        public static String GetTimeDuration(this TimeSpan time_span)
        {
            if (time_span.TotalSeconds == 0)
            {
                return "";
            }
            if (time_span.TotalSeconds < 60)
            {
                return time_span.TotalSeconds + " giây";
            }
            if (time_span.TotalMinutes < 60)
            {
                return time_span.TotalMinutes.LamTron0SoThapPhan() + " phút";
            }
            if (time_span.TotalHours < 24)
            {
                double so_gio = time_span.TotalHours.LamTronDown();
                double so_phut = (time_span.TotalMinutes - (so_gio * 60)).LamTron0SoThapPhan();
                if (so_phut <= 0)
                {
                    return so_gio.GetTextDouble() + " giờ";
                }
                else
                {
                    return so_gio.GetTextDouble() + " giờ, " + so_phut + " phút";
                }
            }
            else
            {
                double so_ngay = time_span.TotalDays.LamTronDown();
                double so_gio = (time_span.TotalHours - (so_ngay * 24)).LamTron0SoThapPhan();
                if (so_gio <= 0)
                {
                    return so_ngay.GetTextDouble() + " ngày";
                }
                else
                {
                    return so_ngay.GetTextDouble() + " ngày, " + so_gio + " giờ";
                }
            }
        }

        //Đưa thời gian vào dưới dạng phút double
        public static String GetThoiGianThuc(this double tongsophut)
        {
            if (tongsophut < 1440)
            {
                double so_gio = (tongsophut / 60).LamTronDown();
                double so_phut = (tongsophut - (so_gio * 60)).LamTron0SoThapPhan();
                if (so_phut <= 0)
                {
                    if (so_gio > 0)
                    {
                        return so_gio.GetTextDouble() + " giờ";
                    }
                    else
                    {
                        return "1 phút";
                    }
                }
                else
                {
                    if (so_gio > 0)
                    {
                        return so_gio.GetTextDouble() + " giờ, " + so_phut + " phút";
                    }
                    else
                    {
                        return so_phut + " phút";
                    }
                }
            }
            else
            {
                double so_ngay = (tongsophut / 1440).LamTronDown();
                double so_gio = ((tongsophut / 60).LamTronDown() - (so_ngay * 24)).LamTron0SoThapPhan();
                if (so_gio <= 0)
                {
                    return so_ngay.GetTextDouble() + " ngày";
                }
                else
                {
                    return so_ngay.GetTextDouble() + " ngày, " + so_gio + " giờ";
                }
            }
        }


        public static void XuatDuLieuRaFile(this Object data)
        {
            String data_text = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "File file|*.json";
                saveFileDialog1.Title = "Xuất file...";
                saveFileDialog1.FileName = "";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    System.IO.File.WriteAllText(saveFileDialog1.FileName, data_text);
                }
            }
            catch { }
        }

        public static bool IsEqualDate(this DateTime date1, DateTime date2)
        {
            return
                (date1.Year == date2.Year) &&
                (date1.Month == date2.Month) &&
                (date1.Day == date2.Day);
        }

        public static String RemoveXML(this String a)
        {
            if (a != null)
            {
                return a.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
            }
            return a;
        }

        public static String GetTinhSimple(this String tinh)
        {
            return tinh.ToLower().
                Replace("tp ", "").
                Replace("tỉnh ", "").
                Replace("thành phố ", "").
                TrimSpace().Trim();
        }

        public static String GetTinhSimple2(this String tinh)
        {
            String tinh2 = tinh.GetTinhSimple();

            IList<String> list_name = tinh2.Split(' ').ToList();
            list_name = list_name.Where(p => !String.IsNullOrEmpty(p)).ToList();

            String ret = "";
            for (int i = 0; i < list_name.Count; i++)
            {
                ret += list_name[i][0].ToString();
            }

            return ret;
        }

        public static String GetHuyenSimple(this String huyen)
        {
            return huyen.ToLower().
                Replace("tp.", "").
                Replace("thành phố", "").
                Replace("h.", "").
                Replace("huyện", "").
                Replace("tx.", "").
                Replace("thị xã", "").
                Replace("q.", "").
                Replace("quận", "").
                TrimSpace().Trim();
        }

        public static String GetHuyenSimple2(this String tinh)
        {
            String tinh2 = tinh.GetHuyenSimple();
            IList<String> list_name = tinh2.Split(' ').ToList();
            list_name = list_name.Where(p => !String.IsNullOrEmpty(p)).ToList();

            String ret = "";
            for (int i = 0; i < list_name.Count; i++)
            {
                ret += list_name[i][0].ToString();
            }
            return ret;
        }

        public static String GetXaSimple(this String xa)
        {
            return xa.ToLower().
                Replace("thị xã", "").
                Replace("thị trấn", "").
                Replace("xã", "").
                Replace("phường", "").TrimSpace().Trim();
        }

        public static DateTime BeginQuy(this DateTime dt)
        {
            DateTime ngaycapphat_begin = new DateTime();
            if (dt.Month < 4)
            {
                ngaycapphat_begin = new DateTime((int)dt.Year, 1, 1, 0, 0, 0);
            }
            else if (dt.Month < 7)
            {
                ngaycapphat_begin = new DateTime((int)dt.Year, 4, 1, 0, 0, 0);
            }
            else if (dt.Month < 10)
            {
                ngaycapphat_begin = new DateTime((int)dt.Year, 7, 1, 0, 0, 0);
            }
            else
            {
                ngaycapphat_begin = new DateTime((int)dt.Year, 10, 1, 0, 0, 0);
            }
            return ngaycapphat_begin;
        }

        public static DateTime EndQuy(this DateTime dt)
        {
            DateTime ngaycapphat_begin = new DateTime();
            if (dt.Month < 4)
            {
                ngaycapphat_begin = new DateTime((int)dt.Year, 1, 1, 0, 0, 0);
            }
            else if (dt.Month < 7)
            {
                ngaycapphat_begin = new DateTime((int)dt.Year, 4, 1, 0, 0, 0);
            }
            else if (dt.Month < 10)
            {
                ngaycapphat_begin = new DateTime((int)dt.Year, 7, 1, 0, 0, 0);
            }
            else
            {
                ngaycapphat_begin = new DateTime((int)dt.Year, 10, 1, 0, 0, 0);
            }
            return ngaycapphat_begin.AddMonths(3);
        }

        public static DateTime RemoveSecond(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
        }

        public static DateTime BeginTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        public static DateTime EndTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }

        public static DateTime BeginYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1, 0, 0, 0);
        }

        public static DateTime EndYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 12, 31, 23, 59, 59);
        }

        public static DateTime BeginMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
        }

        public static DateTime EndMonth(this DateTime dt)
        {
            return BeginMonth(dt).AddMonths(1).AddMilliseconds(-1);
        }

        public static DateTime ClearDate(this DateTime dt)
        {
            return new DateTime(1, 1, 1, dt.Hour, dt.Minute, dt.Second);
        }

        public static bool IsToday(this DateTime dt, DateTime today)
        {
            return dt >= today.BeginTime() && dt <= today.EndTime();
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static bool EqualText(this string a, string b)
        {
            if (string.IsNullOrEmpty(a))
            {
                return string.IsNullOrEmpty(b);
            }
            else
            {
                return string.Equals(a.Trim(), b.Trim());
            }
        }

        public static double LamTron1SoThapPhan(this double data)
        {
            return Math.Round(data, 1, MidpointRounding.AwayFromZero);
        }

        public static double LamTron2SoThapPhan(this double data)
        {
            return Math.Round(data, 2, MidpointRounding.AwayFromZero);
        }

        public static double LamTron3SoThapPhan(this double data)
        {
            return Math.Round(data, 3, MidpointRounding.AwayFromZero);
        }

        public static double LamTron4SoThapPhan(this double data)
        {
            return Math.Round(data, 4, MidpointRounding.AwayFromZero);
        }

        public static double LamTron5SoThapPhan(this double data)
        {
            return Math.Round(data, 5, MidpointRounding.AwayFromZero);
        }

        //-2  -1.6  -1.3  -1  0  1  1.3  1.6  2
        public static double LamTron0SoThapPhan(this double data)
        {
            return Math.Round(data, 0, MidpointRounding.AwayFromZero);
        }

        //-2  -1.6  -1.3  -1  0  1  1.3  1.6  2
        public static double LamTronUp(this double data)
        {
            //1.3 -> 2
            //1.6 -> 2
            //-1.3 -> -1
            //-1.6 -> -1
            return Math.Ceiling(data);
        }

        //-2  -1.6  -1.3  -1  0  1  1.3  1.6  2
        public static double LamTronDown(this double data)
        {
            //1.3 -> 1
            //1.6 -> 1
            //-1.3 -> -2
            //-1.6 -> -2
            return Math.Floor(data);
        }

        public static int GetInteger(this String data)
        {
            int ret = 0;
            if (!String.IsNullOrEmpty(data))
            {
                int.TryParse(data, out ret);
            }
            return ret;
        }

        public static double GetDouble(this String data)
        {
            double ret = 0;
            if (!String.IsNullOrEmpty(data))
            {
                if (data.Contains(@"\"))
                {
                    String[] list_data = data.Split('\\');
                    if (list_data.Length == 2)
                    {
                        double ret_1 = 0;
                        double ret_2 = 0;

                        double.TryParse(list_data[0], out ret_1);
                        double.TryParse(list_data[1], out ret_2);

                        if (ret_2 > 0)
                        {
                            return ret_1 / ret_2;
                        }
                    }
                }
                else if (data.Contains(@"/"))
                {
                    String[] list_data = data.Split('/');
                    if (list_data.Length == 2)
                    {
                        double ret_1 = 0;
                        double ret_2 = 0;

                        double.TryParse(list_data[0], out ret_1);
                        double.TryParse(list_data[1], out ret_2);

                        if (ret_2 > 0)
                        {
                            return ret_1 / ret_2;
                        }
                    }
                }
                else
                {
                    double.TryParse(data, out ret);
                }
            }
            return ret;
        }

        public static bool IsDouble(this String data)
        {
            double ret = 0;
            if (!String.IsNullOrEmpty(data))
            {
                return double.TryParse(data, out ret);
            }
            return false;
        }

        public static bool IsIntegerXML(this String data)
        {
            if (!String.IsNullOrEmpty(data) && (data.Contains(",") || data.Contains(".")))
            {
                return false;
            }

            return IsInteger(data);
        }

        public static bool IsInteger(this String data)
        {
            int ret = 0;
            if (!String.IsNullOrEmpty(data))
            {
                return int.TryParse(data, out ret);
            }
            return false;
        }

        public static String GetPhanSo(this double data)
        {
            if (data == 0.2)
            {
                return "1/5";
            }
            else if (data == 0.25)
            {
                return "1/4";
            }
            else if (data == 0.3 || data == 0.33 || data == 0.333)
            {
                return "1/3";
            }
            else if (data == 0.5)
            {
                return "1/2";
            }
            else if (data == 0.6 || data == 0.66 || data == 0.666)
            {
                return "2/3";
            }
            else if (data == 0.75)
            {
                return "3/4";
            }
            else if (data == 0.8)
            {
                return "4/5";
            }

            return GetText(data);
        }

        public static String GetText(this int data)
        {
            return data.ToString(SystemInfo.MONEY_FORMAT, SystemInfo.CULTURE_INFO_REPORT);
        }

        public static String GetText(this double data)
        {
            if (data == double.NaN)
            {
                return "---";
            }

            return data.ToString(SystemInfo.MONEY_FORMAT, SystemInfo.CULTURE_INFO_REPORT);
        }

        public static String GetSoTienChu(this double data)
        {
            //Những số thập phân thì ko lấy chữ được
            if (data.LamTronUp() != data || data < 0)
            {
                return data.GetTextDouble();
            }

            return UtilityConvertMoney.Convert(data);
        }

        public static String GetChu(this int data)
        {
            if (data < 0)
            {
                return data.GetText();
            }

            return UtilityConvertMoney.Convert(data);
        }

        public static String GetSoTienChuFull(this double data)
        {
            //Những số thập phân thì ko lấy chữ được
            if (data.LamTronUp() != data || data < 0)
            {
                return data.GetTextDouble();
            }

            return UtilityConvertMoney.Convert(data) + " đồng";
        }

        public static String GetTextFull(this double data)
        {
            return data.ToString(SystemInfo.MONEY_FORMAT, SystemInfo.CULTURE_INFO_REPORT) + SystemInfo.MONEY_SIGN;
        }

        public static String GetTextFull(this int data)
        {
            return data.ToString(SystemInfo.MONEY_FORMAT, SystemInfo.CULTURE_INFO_REPORT) + SystemInfo.MONEY_SIGN;
        }

        public static String GetTextDouble(this double data)
        {
            if (double.IsNaN(data))
            {
                return "---";
            }
            return data.ToString(SystemInfo.MONEY_FORMAT_DOUBLE, SystemInfo.CULTURE_INFO_REPORT);
        }

        public static String GetTextDoubleXML(this double data)
        {
            return data.ToString(SystemInfo.MONEY_FORMAT_DOUBLE_XML, SystemInfo.CULTURE_INFO_REPORT);
        }

        public static String ToUpperLeter(this String data)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(data.ToLower());
        }

        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = new List<PropertyInfo>();
            properties = properties.Where(p => p.CanWrite).ToList();
            if (table.Rows.Count > 0)
            {
                foreach (DataColumn col in table.Rows[0].Table.Columns)
                {
                    if (col.ColumnName == "sync_flag")
                    {
                        continue;
                    }
                    if (col.ColumnName == "update_flag")
                    {
                        continue;
                    }
                    if (col.ColumnName == "version")
                    {
                        continue;
                    }

                    PropertyInfo propertie = typeof(T).GetProperty(col.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertie != null && propertie.CanWrite)
                    {
                        properties.Add(propertie);
                    }
                }
            }

            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                property.SetValue(item, row[property.Name], null);
            }
            return item;
        }

        public static IList<T> MakeTableDatas<T>(this DataTable dt)
        {
            IList<T> list = new List<T>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MakeRowData<T>(row));
                }
            }

            return list;
        }

        public static T MakeRowData<T>(this DataRow row)
        {
            T record = (T)Activator.CreateInstance(typeof(T), null);
            if (row != null)
            {
                record.SetProperty(row);
            }

            return record;
        }

        public static void SetProperty(this object obj, DataRow row)
        {
            foreach (DataColumn col in row.Table.Columns)
            {
                if (col.ColumnName == "sync_flag" || col.ColumnName == "update_flag" || col.ColumnName == "version")
                {
                    continue;
                }

                object value = row[col.ColumnName];
                if (value != System.DBNull.Value)
                {
                    PropertyInfo properties = obj.GetType().GetProperty(col.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (properties != null && properties.CanWrite)
                    {
                        if (SystemInfo.DatabaseType == Sanita.Utility.Database.Utility.DatabaseUtility.DATABASE_TYPE.POSTGRESQL)
                        {
                            if (properties.PropertyType.FullName == "System.Int32")
                            {
                                properties.SetValue(obj, Int32.Parse(value.ToString()), null);
                            }
                            else if (properties.PropertyType.FullName == "System.DateTime")
                            {
                                properties.SetValue(obj, DateTime.Parse(value.ToString()), null);
                            }
                            else
                            {
                                properties.SetValue(obj, value, null);
                            }
                        }
                        else if (SystemInfo.DatabaseType == Sanita.Utility.Database.Utility.DatabaseUtility.DATABASE_TYPE.SQLITE)
                        {
                            if (properties.PropertyType.FullName == "System.Int32")
                            {
                                properties.SetValue(obj, Int32.Parse(value.ToString()), null);
                            }
                            else if (properties.PropertyType.FullName == "System.DateTime")
                            {
                                properties.SetValue(obj, DateTime.Parse(value.ToString()), null);
                            }
                            else
                            {
                                properties.SetValue(obj, value, null);
                            }
                        }
                    }
                }
            }
        }

        public static String GetDiffData(this object obj_old, object obj_new)
        {
            PropertyInfo[] list_properties = obj_old.GetType().GetProperties();
            IList<String> list_data = new List<String>();
            foreach (PropertyInfo properties in list_properties)
            {
                if (properties.PropertyType.FullName == "System.Drawing.Image")
                {
                    continue;
                }
                if (properties.PropertyType.Namespace == "System.Collections.Generic")
                {
                    continue;
                }
                if (
                    (properties.PropertyType.FullName == "System.Byte[]") ||
                    (properties.PropertyType.FullName == "System.String") ||
                    (properties.PropertyType.FullName == "System.Int32") ||
                    (properties.PropertyType.FullName == "System.Int64") ||
                    (properties.PropertyType.FullName == "System.Double")
                    )
                {
                    //ok
                }
                else
                {
                    continue;
                }

                if (properties.CanRead)
                {
                    object value_old = properties.GetValue(obj_old, null);
                    String text_old = (value_old == null) ? "null" : value_old.ToString();

                    object value_new = properties.GetValue(obj_new, null);
                    String text_new = (value_new == null) ? "null" : value_new.ToString();

                    if (text_old != text_new)
                    {
                        if (properties.PropertyType.FullName == "System.Byte[]")
                        {
                            String data_diff = String.Format("{0}:(change)", properties.Name);
                            list_data.Add(data_diff);
                        }
                        else
                        {
                            String data_diff = String.Format("{0}:{1} -> {2}", properties.Name, text_old, text_new);
                            list_data.Add(data_diff);
                        }
                    }
                }
            }

            return String.Join(" | ", list_data);
        }

        public static void Refresh(this ObjectListView obj, IEnumerable datas)
        {
            UtilityListView.ListViewRefresh(obj, datas);
        }

        public static void Refresh(this ObjectListView obj, IEnumerable datas, String text, int index)
        {
            UtilityListView.ListViewRefresh(obj, datas, text, index);
        }

        public static void DoFilter(this ObjectListView olv, String txtInput)
        {
            UtilityListView.DoListViewFilter(olv, txtInput);
        }

        public static double Round(this double data)
        {
            return Math.Round(data * 100000) / 100000;
        }

        public static String TRIM(this String data_control)
        {
            if (String.IsNullOrEmpty(data_control))
            {
                return data_control;
            }
            return data_control.Trim();
        }

        public static String TrimSpace(this String data_control)
        {
            if (data_control != null)
            {
                return Regex.Replace(data_control, @"\s+", " ").Trim();
            }
            return "";
        }

        public static String ToUpperFirst(this String data_control)
        {
            if (data_control != null && data_control.Length >= 1)
            {
                return data_control.First().ToString().ToUpper() + data_control.Substring(1);
            }
            return data_control;
        }

        public static String RemoveSpace(this String data_control)
        {
            return Regex.Replace(data_control, @"\s+", "").Trim();
        }

        public static String RemoveSpaceTo(this String data_control, String data_to)
        {
            return Regex.Replace(data_control.TrimSpace(), @"\s+", data_to).Trim();
        }

        public static String RemoveVietNamSign(this String data_control)
        {
            return SanitaUtility.RemoveSign4VN(data_control);
        }

        public static byte[] ToByteArray(this Stream inputStream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                inputStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static bool EqualCase(this String data, String data_next)
        {
            String new_data = data ?? "";
            String new_data_next = data_next ?? "";
            return new_data == new_data_next;
        }

        public static bool EqualNoCase(this String data, String data_next)
        {
            String new_data = data ?? "";
            String new_data_next = data_next ?? "";
            return new_data.ToLower() == new_data_next.ToLower();
        }
    }

}
