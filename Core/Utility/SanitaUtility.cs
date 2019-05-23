using DevComponents.DotNetBar;
using Microsoft.VisualBasic.Compatibility.VB6;
using Sanita.Utility.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Sanita.Utility
{
    public class ReportXMLData
    {
        public String DataName { get; set; }
        public String DataType { get; set; }
        public String DataObject { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public static class SanitaUtility
    {
        private const String TAG = "SanitaUtility";

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        //MemoryStream ms = new MemoryStream();
        //BinaryFormatter bf1 = new BinaryFormatter();
        //bf1.Serialize(ms, dt);
        //File.WriteAllBytes(@"c://xx.dat", ms.ToArray());   

        public static IList<String> SplitStringsByLength(this String str_org, int len)
        {
            String str = str_org;
            IList<String> list_data = new List<String>();

            while (!String.IsNullOrEmpty(str))
            {
                if (str.Length <= len)
                {
                    list_data.Add(str);
                    break;
                }
                else
                {
                    list_data.Add(str.Substring(0, len));
                    str = str.Remove(0, len);
                }
            }

            return list_data;
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 0)
            {
                return list.Select(t => new T[] { });
            }
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static MemoryStream Serialize<T>(T record) where T : class
        {
            if (null == record) return null;

            var stream = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(stream, record);

            return stream;
        }

        public static T Deserialize<T>(Stream stream) where T : class
        {
            if (null == stream) return null;

            BinaryFormatter bf1 = new BinaryFormatter();
            return (T)bf1.Deserialize(stream);
        }

        public static byte[] GetByteFromStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }

                return mso.ToArray();
            }
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static Image DrawToCircle(Image srcImage, int x, int y, int w, int h)
        {
            Color backGround = Color.FromArgb(255, 255, 0);
            Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            Graphics g = Graphics.FromImage(dstImage);
            g.DrawImage(srcImage, 0, 0);
            using (Pen pen = new Pen(backGround, 3))
            {
                g.DrawEllipse(pen, x, y, w, h);
            }

            return dstImage;
        }

        public static Image DrawToRectangle(Image srcImage, int x, int y, int w, int h)
        {
            Color backGround = Color.FromArgb(0, 255, 0);
            Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            Graphics g = Graphics.FromImage(dstImage);
            g.DrawImage(srcImage, 0, 0);
            using (Pen pen = new Pen(backGround, 3))
            {
                g.DrawRectangle(pen, x, y, w, h);
            }

            return dstImage;
        }

        //x=370,y=290,w=577,h=632       
        //path.AddEllipse(50, -20, 577, 632); 
        public static Image ClipToCircle(Image srcImage, int x, int y, int w, int h)
        {
            Color backGround = Color.FromArgb(255, 255, 255);
            Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            Graphics g = Graphics.FromImage(dstImage);
            using (Brush br = new SolidBrush(backGround))
            {
                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
            }

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(x, y, w, h);
            g.SetClip(path);
            g.DrawImage(srcImage, 0, 0);

            return dstImage;
        }

        public static Image ClipToRectangle(Image srcImage, int x, int y, int w, int h)
        {
            Color backGround = Color.FromArgb(255, 255, 255);
            Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            Graphics g = Graphics.FromImage(dstImage);
            using (Brush br = new SolidBrush(backGround))
            {
                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
            }

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(x, y, w, h));
            g.SetClip(path);
            g.DrawImage(srcImage, 0, 0);

            return dstImage;
        }

        public static Image CropToRectangle(Image srcImage, int x, int y, int w, int h)
        {
            Rectangle cropRect = new Rectangle(x, y, w, h);
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(srcImage, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
            }

            return target;
        }

        private static bool Inbound(int x, int y, int wid, int hei)
        {
            if (x < 0 || x >= wid) return false;
            if (y < 0 || y >= hei) return false;
            return true;
        }

        public static Bitmap LoadSelection(Bitmap source, int start_x, int start_y, int distance, int color)
        {
            Color c = source.GetPixel(start_x, start_y);
            int wid = source.Width;
            int hei = source.Height;

            int[,] sel = new int[wid, hei];
            for (int x = 0; x < wid; x++)
            {
                for (int y = 0; y < hei; y++)
                    sel[x, y] = 0;

            }

            for (int x = 0; x < wid; x++)
            {
                for (int y = 0; y < hei; y++)
                    sel[x, y] = 0;
            }

            sel[start_x, start_y] = 1;
            Stack st = new Stack();
            st.Push(new Point(start_x, start_y));


            do
            {
                Point p = (Point)st.Pop();

                Color c2 = source.GetPixel(p.X, p.Y);
                if (System.Math.Abs(c2.R - c.R) < distance)
                {
                    int x = p.X;
                    int y = p.Y;
                    sel[x, y] = 1;
                    if (Inbound(x - 1, y, wid, hei))
                        if (sel[x - 1, y] == 0) st.Push(new Point(x - 1, y));
                    if (Inbound(x + 1, y, wid, hei))
                        if (sel[x + 1, y] == 0) st.Push(new Point(x + 1, y));
                    if (Inbound(x, y - 1, wid, hei))
                        if (sel[x, y - 1] == 0) st.Push(new Point(x, y - 1));
                    if (Inbound(x, y + 1, wid, hei))
                        if (sel[x, y + 1] == 0) st.Push(new Point(x, y + 1));
                }
            }
            while (st.Count > 0);

            return FillSelection(source, sel, color);
        }

        public static Bitmap FillSelection(Bitmap source, int[,] sel, int c)
        {
            Color color = Color.FromArgb(c, c, c);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    if (sel[x, y] == 1)
                        source.SetPixel(x, y, color);
                }
            }
            return source;
        }

        public static IList<String> AddSoundNumber(IList<String> listSound, int nummber)
        {
            String startNumber = UtilityConvertMoney.ConvertSound(nummber).ToLower();
            if (nummber >= 1000)
            {
                startNumber =
                    UtilityConvertMoney.ConvertSound(int.Parse(nummber.ToString().Substring(0, 1))).ToLower() + " " +
                    UtilityConvertMoney.ConvertSound(int.Parse(nummber.ToString().Substring(1, 1))).ToLower() + " " +
                    UtilityConvertMoney.ConvertSound(int.Parse(nummber.ToString().Substring(2, 1))).ToLower() + " " +
                    UtilityConvertMoney.ConvertSound(int.Parse(nummber.ToString().Substring(3, 1))).ToLower();
            }
            else if (nummber >= 100)
            {
                if (nummber % 10 != 0)
                {
                    startNumber = startNumber.Replace("mươi", "");
                }
            }
            else
            {
                if (nummber % 10 != 0)
                {
                    startNumber = startNumber.Replace("mươi", "");
                }
            }

            String[] listStart = startNumber.Split(' ');
            if (nummber < 1000 && listStart.Length >= 2 && listStart[listStart.Length - 2].ToLower() != "mười" && listStart[listStart.Length - 1].ToLower() == "bốn")
            {
                listStart[listStart.Length - 1] = "tư";
            }

            foreach (String data in listStart)
            {
                String data_new = data.Trim().ToLower();
                if (!String.IsNullOrEmpty(data_new))
                {
                    data_new = data_new.Replace("mốt", "mot2");
                    data_new = data_new.Replace("mươi", "muoi2");
                    data_new = SanitaUtility.RemoveSign4VN(data_new);
                    listSound.Add(@"Number\" + data_new + ".wav");
                }
            }

            return listSound;
        }

        public static IList<String> AddSoundNumberTuoi(IList<String> listSound, int nummber)
        {
            String startNumber = UtilityConvertMoney.ConvertSound(nummber).ToLower();
            if (nummber >= 1000)
            {
                startNumber =
                    UtilityConvertMoney.ConvertSound(int.Parse(nummber.ToString().Substring(0, 1))).ToLower() + " " +
                    UtilityConvertMoney.ConvertSound(int.Parse(nummber.ToString().Substring(1, 1))).ToLower() + " " +
                    UtilityConvertMoney.ConvertSound(int.Parse(nummber.ToString().Substring(2, 1))).ToLower() + " " +
                    UtilityConvertMoney.ConvertSound(int.Parse(nummber.ToString().Substring(3, 1))).ToLower();
            }
            else if (nummber >= 100)
            {
                if (nummber % 10 != 0)
                {
                    startNumber = startNumber.Replace("mươi", "");
                }
            }
            else
            {
                if (nummber % 10 != 0)
                {
                    startNumber = startNumber.Replace("mươi", "");
                }
            }

            String[] listStart = startNumber.Split(' ');
            if (listStart.Length >= 2 && listStart[0].ToLower() != "mười" && listStart[listStart.Length - 1].ToLower() == "bốn")
            {
                listStart[listStart.Length - 1] = "tư";
            }

            foreach (String data in listStart)
            {
                String data_new = data.Trim().ToLower();
                if (!String.IsNullOrEmpty(data_new))
                {
                    data_new = data_new.Replace("mốt", "mot2");
                    data_new = data_new.Replace("mươi", "muoi2");
                    data_new = SanitaUtility.RemoveSign4VN(data_new);
                    listSound.Add(@"NumberTuoi\" + data_new + ".wav");
                }
            }

            return listSound;
        }

        public static string GetDataSizeSimple(Int64 value)
        {
            string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            if (value < 0) { return "-" + GetDataSizeSimple(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public static bool Check_PhoneNumber(string input)
        {
            string pattent = @"^(\+?([-. ])?(\d[\d-. ]+))?(\+?([-. ])?\([\d-. ]+\))?(\([\+\d-. ]+\))?[\d-. ]+\d$";
            if (Regex.IsMatch(input, pattent))
            {
                return true;
            }
            return false;
        }

        public static bool Check_Email(string Email)
        {
            try
            {
                MailAddress m = new MailAddress(Email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool Check_StringToNumber(string input)
        {
            string pattent = @"^\d+$";
            if (Regex.IsMatch(input, pattent))
            {
                return true;
            }
            return false;
        }

        public static bool Check_ColorTranslator(string input)
        {
            string pattent = @"^#[0-9A-F]{6}$";
            if (Regex.IsMatch(input, pattent))
            {
                return true;
            }
            return false;
        }

        public static String Convert_SoLaMa(int input)
        {
            String ketqua = "";
            if (input > 0)
            {
                Boolean _bool = true;
                string[] so_lama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
                int[] so_thapphan = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
                int i = 0;
                while (_bool)
                {
                    while (input >= so_thapphan[i])
                    {
                        input -= so_thapphan[i];
                        ketqua += so_lama[i];
                        if (input < 1)
                            _bool = false;
                    }
                    i++;
                }
            }
            return ketqua;
        }

        public static void ExitApp()
        {
#if false
            Application.ExitThread();
            Application.Exit();
            Environment.Exit(0);
#else
            System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
        }

        public static string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static Image ResizeImage(Image imgPhoto, int Width, int Height)
        {
            //Convert
            float scaleHeight = (float)Height / (float)imgPhoto.Height;
            float scaleWidth = (float)Width / (float)imgPhoto.Width;

#if false
            float scale = Math.Min(scaleHeight, scaleWidth);
#else
            float scale = scaleWidth;           //Fix theo width scale
#endif

            Bitmap bitmap = new Bitmap(imgPhoto, (int)(imgPhoto.Width * scale), (int)(imgPhoto.Height * scale));
            MemoryStream jpegStream = new MemoryStream();
#if true
            bitmap.Save(jpegStream, System.Drawing.Imaging.ImageFormat.Png);
#else
            bitmap.Save(jpegStream, System.Drawing.Imaging.ImageFormat.Jpeg);
#endif

            bitmap.Dispose();
            bitmap = null;

            //Return
            return Image.FromStream(jpegStream);
        }

        public static IList<String> GetListPrinter()
        {
            IList<String> ListPrinter = new List<String>();
            ListPrinter.Add("");

            try
            {
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    ListPrinter.Add(printer);
                }
            }
            catch { }

            return ListPrinter;
        }

        public static IList<String> GetListTray(String printer_name)
        {
            IList<String> ListTray = new List<String>();

            if (!String.IsNullOrEmpty(printer_name))
            {
                try
                {
                    using (PrintDocument doc = new PrintDocument())
                    {
                        doc.DefaultPageSettings.PrinterSettings.PrinterName = printer_name;
                        foreach (System.Drawing.Printing.PaperSource _pSource in doc.PrinterSettings.PaperSources)
                        {
                            ListTray.Add(_pSource.SourceName);
                        }
                    }
                }
                catch { }
            }

            return ListTray;
        }

        public static int ConvertTwipsToPixel_Width(int width)
        {
            return (int)Support.TwipsToPixelsX(width);
        }

        public static int ConvertPointToPixel_Width(double width)
        {
            return (int)Support.ToPixelsX(width, ScaleMode.Points);
        }

        public static int ConvertTwipsToPixel_Height(int height)
        {
            return (int)Support.TwipsToPixelsY(height);
        }

        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }

        public static String GetResourceFileStreamText(string fileName)
        {
            //Get from resource
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string[] arrResources = currentAssembly.GetManifestResourceNames();
            foreach (string resourceName in arrResources)
            {
                if (resourceName.ToLower().Contains(fileName.ToLower()))
                {
                    using (Stream mStream = currentAssembly.GetManifestResourceStream(resourceName))
                    {
                        using (StreamReader reader = new StreamReader(mStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }

            return null;
        }

        public static String GetExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return ""; }
        }

        public static double GetDataText(String strFormula)
        {
            //Evulate formula
            try
            {
                //Parse formula
                UtilityFormulaParse parser = new UtilityFormulaParse();
                ArrayList arrExpr = parser.GetPostFixNotation(strFormula, Type.GetType("System.Double"), false);
                string szResult = parser.Convert2String(arrExpr);
                return ((double)parser.EvaluateRPN(arrExpr, Type.GetType("System.Double"), null));
            }
            catch (Exception ex)
            {
                return double.NaN;
            }
        }

        public static int GetRanDomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(0, 1000000);
        }

        public static int GetRanDomNumber(int from, int to)
        {
            Random rnd = new Random();
            return rnd.Next(from, to);
        }

        public static int GetRanDomNumber(int seed)
        {
            Random rnd = new Random();
            return rnd.Next(0, seed);
        }


        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public static ImageList GetImageList()
        {
            return null;
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            DayOfWeek firstDay = DayOfWeek.Monday;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }
            return firstDayInWeek;
        }

        public static DateTime GetLastDayOfWeek(DateTime dayInWeek)
        {
            return GetFirstDayOfWeek(dayInWeek).AddDays(6);
        }

        static SanitaUtility()
        {

        }

        public static Byte[] GetImageData(Image image)
        {
            Byte[] ImageData = null;
            try
            {
                MemoryStream jpegStream = new MemoryStream();
                image.Save(jpegStream, System.Drawing.Imaging.ImageFormat.Png);
                ImageData = jpegStream.ToArray();
            }
            catch (Exception)
            {
                ImageData = null;
            }

            return ImageData;
        }
        public static Image GetDataImage(Byte[] image)
        {
            if (image != null)
            {
                Image image_data = Image.FromStream(new MemoryStream(image));
                return image_data;
            }
            return null;
        }

        public static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Red);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static double ScreenDPI(int monitorSize, int resolutionWidth, int resolutionHeight)
        {
            //int resolutionWidth = 1600;
            //int resolutionHeight = 1200;
            //int monitorSize = 19;
            if (0 < monitorSize)
            {
                double screenDpi = Math.Sqrt(Math.Pow(resolutionWidth, 2) + Math.Pow(resolutionHeight, 2)) / monitorSize;
                return screenDpi;
            }
            return 0;
        }

        public static String GetString(Byte[] data)
        {
            return UTF8Encoding.Unicode.GetString(data);
        }

        public static bool isToday(DateTime CURRENT_DATE)
        {
            if ((CURRENT_DATE.Year == Sanita.Utility.SystemInfo.NOW.Year) &&
                (CURRENT_DATE.Month == Sanita.Utility.SystemInfo.NOW.Month) &&
                (CURRENT_DATE.Day == Sanita.Utility.SystemInfo.NOW.Day))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isEqualDate(DateTime date1, DateTime date2)
        {
            return
                (date1.Year == date2.Year) &&
                (date1.Month == date2.Month) &&
                (date1.Day == date2.Day);
        }

        public static void ShowAlert(String msg)
        {
            //Show alert
            String strAlert = String.Format(msg);
            AlertCustom m_AlertOnLoad = new AlertCustom(strAlert);
            Rectangle r = Screen.GetWorkingArea(m_AlertOnLoad);
            m_AlertOnLoad.Location = new Point(r.Right - m_AlertOnLoad.Width, r.Bottom - m_AlertOnLoad.Height);
            m_AlertOnLoad.AutoClose = true;
            m_AlertOnLoad.AutoCloseTimeOut = 3;
            m_AlertOnLoad.AlertAnimation = eAlertAnimation.BottomToTop;
            m_AlertOnLoad.AlertAnimationDuration = 1000;
            m_AlertOnLoad.Show(false);
        }

        public static IList<String> DoParseICDCode(String code)
        {
            IList<String> ListICD = new List<String>();

            IList<String> list1 = code.Split(',').ToList();
            foreach (String list1_code in list1)
            {
                if (!list1_code.Contains('-'))
                {
                    ListICD.Add(list1_code.Trim());
                }
                else
                {
                    IList<String> list2 = list1_code.Split('-').ToList();
                    if (list2.Count == 2)
                    {
                        String list2_1 = list2[0].Trim();
                        String list2_2 = list2[1].Trim();

                        String strText = "";
                        int index1 = 0;
                        int index2 = 0;
                        if ((list2_1.Length == 3) && (list2_2.Length == 3))
                        {
                            strText = list2_1[0].ToString();
                            index1 = int.Parse(list2_1.Remove(0, 1));
                            index2 = int.Parse(list2_2.Remove(0, 1));
                        }
                        for (int i = index1; i <= index2; i++)
                        {
                            ListICD.Add(String.Format("{0}{1:00}", strText, i));
                        }
                    }
                }
            }

            return ListICD;
        }

        //Unicode dựng sẵn
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        //Unicode tổ hợp
        private static readonly string[] VietnameseSigns_UnicodeToHop = new string[]
        {
            "áàạảã",
        };

        public static string RemoveSign4VN(string str)
        {
            String ssss = str;

            if (!String.IsNullOrEmpty(ssss))
            {
                ssss = ssss.Trim();
            }
            else
            {
                return "";
            }

            if (ssss.Any(p => VietnameseSigns_UnicodeToHop.Any(q => q.Any(x => x > 255 && x == p))))
            {
                for (int i = 0; i < VietnameseSigns_UnicodeToHop.Length; i++)
                {
                    for (int j = 0; j < VietnameseSigns_UnicodeToHop[i].Length; j++)
                    {
                        if (VietnameseSigns_UnicodeToHop[i][j] > 255)
                        {
                            ssss = ssss.Replace(VietnameseSigns_UnicodeToHop[i][j].ToString(), "");
                        }
                    }
                }
            }

            if (ssss.Any(p => VietnameseSigns.Any(q => q.Any(x => x == p))))
            {
                for (int i = 1; i < VietnameseSigns.Length; i++)
                {
                    for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    {
                        ssss = ssss.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                    }
                }
            }

            return ssss;
        }

        public static string ConvertHexToString_UTF8(string HexValue)
        {
            byte[] dBytes = StringToByteArray(HexValue);
            return System.Text.Encoding.UTF8.GetString(dBytes);
        }

        public static string ConvertHexToString_UCS2(string HexValue)
        {
            byte[] dBytes = StringToByteArray(HexValue);

            byte[] utf8Bytes = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.UTF8, dBytes);
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

            return System.Text.Encoding.UTF8.GetString(utf8Bytes);
        }

        public static string ConvertHexToString_ASCII(string HexValue)
        {
            byte[] dBytes = StringToByteArray(HexValue);
            return System.Text.Encoding.ASCII.GetString(dBytes);
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length / 2;
            byte[] bytes = new byte[NumberChars];
            using (var sr = new StringReader(hex))
            {
                for (int i = 0; i < NumberChars; i++)
                    bytes[i] =
                      Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return bytes;
        }

        public static String[] GetDateTimeDetail(DateTime dt)
        {
            String[] listDatetime = new String[12];

            String strDate = dt.ToString("HHmmddMMyyyy");
            for (int i = 0; i < 12; i++)
            {
                listDatetime[i] = new StringBuilder().Append(strDate[i]).ToString();
            }

            return listDatetime;
        }

        //Index = 1,2,3..length
        public static String GetStringIndex(String input, int length, int index)
        {
            if (input == null)
            {
                return "";
            }

            if (input.Length < length)
            {
                int input_length = input.Length;
                for (int i = 0; i < length - input_length; i++)
                {
                    input = "0" + input;
                }
            }
            if ((index < 1) || (index > length))
            {
                return "";
            }
            return new StringBuilder().Append(input[index - 1]).ToString();
        }

        public static String Encode_Base64(byte[] data)
        {
            if (data == null)
            {
                return "";
            }

            // Convert the binary input into Base64 UUEncoded output
            string base64String;
            try
            {
                base64String = System.Convert.ToBase64String(data, 0, data.Length);
            }
            catch (Exception)
            {
                return "";
            }

            return base64String;
        }

        public static byte[] Decode_Base64(String text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            // Convert the Base64 UUEncoded input into binary output.
            byte[] binaryData = null;
            try
            {
                binaryData = System.Convert.FromBase64String(text);
            }
            catch (Exception)
            {
                return null;
            }

            return binaryData;
        }

        public static Image Decode_Base64_Image(String text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            // Convert the Base64 UUEncoded input into binary output.
            try
            {
                byte[] binaryData = System.Convert.FromBase64String(text);
                return Image.FromStream(new MemoryStream(binaryData));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T DeserializerObject_XML<T>(this String fromSerialize)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                using (StringReader textReader = new StringReader(fromSerialize))
                {
                    T mObject = (T)xmlSerializer.Deserialize(textReader);
                    return (T)Convert.ChangeType(mObject, typeof(T));
                }
            }
            catch (Exception ex)
            {
                return (T)Convert.ChangeType(null, typeof(T));
            }
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static String ConvertBinary2HexString_MYSQL(Byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return "NULL";
            }
            else
            {
                StringBuilder sb = new StringBuilder(data.Length * 2);
                foreach (byte b in data)
                {
                    sb.AppendFormat("{0:x2}", b);
                }
                return "0x" + sb.ToString();
            }
        }

        public static String ConvertBinary2HexString_POSTGRES(Byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return "NULL";
            }
            else
            {
                StringBuilder sb = new StringBuilder(data.Length * 2);
                foreach (byte b in data)
                {
                    sb.AppendFormat("{0:X2}", b);
                }

                return String.Format("decode('{0}', 'hex')", sb.ToString());
            }
        }

        public static String ConvertBinary2HexString_SQLITE(Byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return "NULL";
            }
            else
            {
                StringBuilder sb = new StringBuilder(data.Length * 2);
                foreach (byte b in data)
                {
                    sb.AppendFormat("{0:X2}", b);
                }

                return String.Format("X'{0}'", sb.ToString());
            }
        }


        #region Convert TCVN3-Unicode

        private static int[] tcvnchars = {              
                'µ', '¸', '¶', '·', '¹',               
                'µ', '¸', '¶', '·', '¹', 

                '¨', '»', '¾', '¼', '½', 'Æ', 
                '¡', '»', '¾', '¼', '½', 'Æ', 

                '©', 'Ç', 'Ê', 'È', 'É', 'Ë', 
                '¢', 'Ç', 'Ê', 'È', 'É', 'Ë', 

                '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ', 
                '§', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ',

                'ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö', 
                '£', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö', 

                '×', 'Ý', 'Ø', 'Ü', 'Þ', 
                '×', 'Ý', 'Ø', 'Ü', 'Þ',

                'ß', 'ã', 'á', 'â', 'ä', 
                'ß', 'ã', 'á', 'â', 'ä',

                '«', 'å', 'è', 'æ', 'ç', 'é',
                '¤', 'å', 'è', 'æ', 'ç', 'é',

                '¬', 'ê', 'í', 'ë', 'ì', 'î', 
                '¥', 'ê', 'í', 'ë', 'ì', 'î',

                'ï', 'ó', 'ñ', 'ò', 'ô', 
                'ï', 'ó', 'ñ', 'ò', 'ô', 

                '­', 'õ', 'ø', 'ö', '÷', 'ù', 
                '¦', 'õ', 'ø', 'ö', '÷', 'ù',

                'ú', 'ý', 'û', 'ü', 'þ', 
                'ú', 'ý', 'û', 'ü', 'þ',
        };
        private static int[] unichars = {
                'à', 'á', 'ả', 'ã', 'ạ', 
                'À', 'Á', 'Ả', 'Ã', 'Ạ', 

                'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ', 
                'Ă', 'Ằ', 'Ắ', 'Ẳ', 'Ẵ', 'Ặ', 

                'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ',
                'Â', 'Ầ', 'Ấ', 'Ẩ', 'Ẫ', 'Ậ',

                'đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ', 
                'Đ', 'È', 'É', 'Ẻ', 'Ẽ', 'Ẹ',

                'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ', 
                'Ê', 'Ề', 'Ế', 'Ể', 'Ễ', 'Ệ',

                'ì', 'í', 'ỉ', 'ĩ', 'ị', 
                'Ì', 'Í', 'Ỉ', 'Ĩ', 'Ị',

                'ò', 'ó', 'ỏ', 'õ', 'ọ', 
                'Ò', 'Ó', 'Ỏ', 'Õ', 'Ọ',

                'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ', 
                'Ô', 'Ồ', 'Ố', 'Ổ', 'Ỗ', 'Ộ',

                'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ', 
                'Ơ', 'Ờ', 'Ớ', 'Ở', 'Ỡ', 'Ợ',

                'ù', 'ú', 'ủ', 'ũ', 'ụ', 
                'Ù', 'Ú', 'Ủ', 'Ũ', 'Ụ',

                'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự', 
                'Ư', 'Ừ', 'Ứ', 'Ử', 'Ữ', 'Ự',

                'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ', 
                'Ỳ', 'Ý', 'Ỷ', 'Ỹ', 'Ỵ', 
        };

        public static string UnicodeToTCVN3(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return "";
            }

            char[] chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int index = unichars.ToList().IndexOf(chars[i]);
                if (index >= 0)
                {
                    chars[i] = (char)tcvnchars[index];
                }
            }
            return new string(chars);
        }

        public static string ToRomanNumeral(int number)
        {
            var retVal = new StringBuilder(5);
            var valueMap = new SortedDictionary<int, string>
                               {
                                   { 1, "I" },
                                   { 4, "IV" },
                                   { 5, "V" },
                                   { 9, "IX" },
                                   { 10, "X" },
                                   { 40, "XL" },
                                   { 50, "L" },
                                   { 90, "XC" },
                                   { 100, "C" },
                                   { 400, "CD" },
                                   { 500, "D" },
                                   { 900, "CM" },
                                   { 1000, "M" },
                               };

            foreach (var kvp in valueMap.Reverse())
            {
                while (number >= kvp.Key)
                {
                    number -= kvp.Key;
                    retVal.Append(kvp.Value);
                }
            }

            return retVal.ToString();
        }

        public static string ReplaceNamedGroup(string input, string groupName, string replacement)
        {
            try
            {
                String FirstText = "(?<" + groupName + ">";
                int startIndex = input.IndexOf(FirstText);
                int endIndex = startIndex;
                for (int i = startIndex + FirstText.Length; i < input.Length; i++)
                {
                    if (input[i] == ')')
                    {
                        endIndex = i;
                        break;
                    }
                }

                String fullText = input.Substring(startIndex, endIndex - startIndex + 1);
                String new_replacement =
                    replacement
                    .Replace(".", @"\.")
                    .Replace("+", @"\+")
                    .Replace("*", @"\*")
                    .Replace("^", @"\^")
                    .Replace("$", @"\$")
                    .Replace("?", @"\?")
                    .Replace("|", @"\|");
                return input.Replace(fullText, new_replacement);
            }
            catch { }

            return "";
        }

        #endregion
    }
}
