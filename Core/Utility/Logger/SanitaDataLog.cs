using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sanita.Utility.Database.BaseDao;
using Sanita.Utility.Database.Utility;
using Sanita.Utility.Encryption;
using Sanita.Utility.ExtendedThread;

namespace Sanita.Utility.Logger
{
    public class SanitaDataLogModel
    {
        public String App { get; set; }
        public String User { get; set; }
        public String SoftVersion { get; set; }
        public String IPAddress { get; set; }
        public String ComputerName { get; set; }
        public DateTime LogTime { get; set; }
        public String LogValue { get; set; }
        public String LogException { get; set; }
    }

    public sealed class SanitaDataLog
    {
        public static LogLevel.LogLevelInfo Level { get; set; }
        public static ILogOutput LogOutput { get; set; }

        private static char[] SpaceLine = new char[72];
        private static readonly char[] HEXDIGIT = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        private static object lockObj = new object();

        private static String LogName = "";
        private static String LogUser = "";
        private static String LogVersion = "";

        private static IBaseDao baseDAO = new DatabaseUtility().GetDatabaseDAO();
        private static ExBackgroundWorker m_BackgroundWorker;
        private static Queue<SanitaDataLogModel> QueueLogRecord = new Queue<SanitaDataLogModel>();

        private static String[] TableLogFilter = new String[]
        {
            "option_ref",
            "icd10",

            //HIS
            //"bhyt",
            //"hosobenhan",
            "bill",
            "medicalrecord",
            "patient",
            "vienphi",
            "maubenhpham",
            "tbuser",
            "servicepriceref",
            "serviceprice",
            //"sothutubenhpham",
            //"sothutuphongkham",
            "tblreport",

            //Pharma
            "medicine_period",
            "medicine_ref",
            "medicine_store_bill",
            "medicine",
            "medicine_store_ref",
            "medicine_store",
            "medicinekiemke",
            "medicinephongluu",
        };

        static SanitaDataLog()
        {
            try
            {
                ////Set config
                //{
                //    string path = Path.Combine(Application.StartupPath, "Config//Config.xml");
                //    Xmlconfig config = new Xmlconfig(path, true);

                //    try
                //    {
                //        String Localserver = CryptorEngine.Decrypt(config.Settings["DataBase/Local/SERVER"].Value, true);
                //        String Localdatabase = CryptorEngine.Decrypt(config.Settings["DataBase/Local/DATABASE"].Value, true);
                //        String Localuserid = CryptorEngine.Decrypt(config.Settings["DataBase/Local/UID"].Value, true);
                //        String Localpassword = CryptorEngine.Decrypt(config.Settings["DataBase/Local/PWD"].Value, true);
                //        baseDAO.SetConnectionConfig(Localserver, Localuserid, Localpassword, Localdatabase);
                //    }
                //    catch { }
                //}
            }
            catch { }


            /* prep for hex dump */
            int i = SpaceLine.Length - 1;
            while (i >= 0)
            {
                SpaceLine[i--] = ' ';
            }
            SpaceLine[0] = SpaceLine[1] = SpaceLine[2] = SpaceLine[3] = '0';
            SpaceLine[4] = '-';
            Level = LogLevel.Verbose;

            /* init */
            //Create worker
            m_BackgroundWorker = new ExBackgroundWorker();
            m_BackgroundWorker.CurrentPriority = System.Threading.ThreadPriority.Lowest;
            m_BackgroundWorker.WorkerReportsProgress = true;
            m_BackgroundWorker.WorkerSupportsCancellation = true;
            m_BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bwAsync_WorkerChanged);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAsync_WorkerCompleted);
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(bwAsync_Worker);
            bwAsync_Start();
        }

        #region Worker thread

        private static void bwAsync_Start()
        {
            if (!m_BackgroundWorker.IsBusy)
            {
                m_BackgroundWorker.RunWorkerAsync();
            }
        }

        private static void bwAsync_WorkerChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private static void bwAsync_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private static void bwAsync_Worker(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                System.Threading.Thread.Sleep(300);

                try
                {
                    if (QueueLogRecord.Count > 0)
                    {
                        SanitaDataLogModel result = QueueLogRecord.Dequeue();

                        StringBuilder sql = new StringBuilder();
                        sql.Append(" INSERT INTO tblLogData (");
                        sql.Append("            LogApp,");
                        sql.Append("            LogUser,");
                        sql.Append("            SoftVersion,");
                        sql.Append("            LogTime,");
                        sql.Append("            IPAddress,");
                        sql.Append("            ComputerName,");
                        sql.Append("            LogValue) ");
                        sql.Append("  VALUES( " + DatabaseUtility.Escape(result.App) + ", ");
                        sql.Append("          " + DatabaseUtility.Escape(result.User) + ", ");
                        sql.Append("          " + DatabaseUtility.Escape(result.SoftVersion) + ", ");
                        sql.Append("          " + DatabaseUtility.Escape(result.LogTime) + ", ");
                        sql.Append("          " + DatabaseUtility.Escape(result.IPAddress) + ", ");
                        sql.Append("          " + DatabaseUtility.Escape(result.ComputerName) + ", ");
                        sql.Append("          " + DatabaseUtility.Escape(result.LogValue) + ") ");

                        // Assign new customer Id back to business object
                        baseDAO.DoInsert(sql.ToString());
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion

        private SanitaDataLog()
        {

        }

        sealed class Config
        {
            /// <summary>
            /// Log Verbose
            /// </summary>
            public const bool LOGV = true;
            /// <summary>
            /// Log Debug
            /// </summary>
            public const bool LOGD = true;
        };

        public static void SetLogName(String log_name, String user, String version)
        {
            LogName = log_name;
            LogUser = user;
            LogVersion = version;
        }

        public static void v(String tag, String message)
        {
            WriteLine(LogLevel.Verbose, tag, message);
        }

        public static void v(String tag, String format, params object[] args)
        {
            WriteLine(LogLevel.Verbose, tag, String.Format(format, args));
        }

        public static void d(String tag, String message)
        {
            WriteLine(LogLevel.Debug, tag, message);
        }

        public static void d(String tag, String format, params object[] args)
        {
            WriteLine(LogLevel.Debug, tag, String.Format(format, args));
        }

        public static void i(String tag, String message)
        {
            WriteLine(LogLevel.Info, tag, message);
        }

        public static void i(String tag, String format, params object[] args)
        {
            WriteLine(LogLevel.Info, tag, String.Format(format, args));
        }

        public static void w(String tag, String message)
        {
            WriteLine(LogLevel.Warn, tag, message);
        }

        public static void w(String tag, String format, params object[] args)
        {
            WriteLine(LogLevel.Warn, tag, String.Format(format, args));
        }

        public static void w(String tag, Exception exception)
        {
            if (exception != null)
            {
                w(tag, exception.ToString());
            }
        }

        public static void w(String tag, String message, Exception exception)
        {
            w(tag, "{0}\n{1}", message, exception);
        }

        public static void e(String tag, String message)
        {
            WriteLine(LogLevel.Error, tag, message);
        }

        public static void e(String tag, String format, params object[] args)
        {
            WriteLine(LogLevel.Error, tag, String.Format(format, args));
        }

        public static void e(String tag, Exception exception)
        {
            if (exception != null)
            {
                e(tag, exception.ToString());
            }
        }

        public static void e(String tag, String message, Exception exception)
        {
            e(tag, "{0}\n{1}", message, exception);
        }

        internal static void HexDump(String tag, LogLevel.LogLevelInfo level, byte[] data, int offset, int length)
        {
            int kHexOffset = 6;
            int kAscOffset = 55;
            char[] line = new char[SpaceLine.Length];
            int addr, baseAddr, count;
            int i, ch;
            bool needErase = true;

            baseAddr = 0;
            while (length != 0)
            {
                if (length > 16)
                {
                    // full line
                    count = 16;
                }
                else
                {
                    // partial line; re-copy blanks to clear end
                    count = length;
                    needErase = true;
                }

                if (needErase)
                {
                    Array.Copy(SpaceLine, 0, line, 0, SpaceLine.Length);
                    needErase = false;
                }

                // output the address (currently limited to 4 hex digits)
                addr = baseAddr;
                addr &= 0xffff;
                ch = 3;
                while (addr != 0)
                {
                    line[ch] = HEXDIGIT[addr & 0x0f];
                    ch--;
                    addr >>= 4;
                }

                // output hex digits and ASCII chars
                ch = kHexOffset;
                for (i = 0; i < count; i++)
                {
                    byte val = data[offset + i];

                    line[ch++] = HEXDIGIT[(val >> 4) & 0x0f];
                    line[ch++] = HEXDIGIT[val & 0x0f];
                    ch++;

                    if (val >= 0x20 && val < 0x7f)
                        line[kAscOffset + i] = (char)val;
                    else
                        line[kAscOffset + i] = '.';
                }

                WriteLine(level, tag, new String(line));

                // advance to next chunk of data
                length -= count;
                offset += count;
                baseAddr += count;
            }
        }

        internal static void HexDump(byte[] data)
        {
            HexDump("DUMP", LogLevel.Debug, data, 0, data.Length);
        }

        private static void WriteLine(LogLevel.LogLevelInfo logLevel, String tag, String message)
        {
            if (logLevel.Priority >= Level.Priority)
            {
                //Write to log file
                Write(logLevel, tag, message);

                //Write to output
                if (LogOutput != null)
                {
                    LogOutput.Write(logLevel, tag, message);
                }
            }
        }

        private static void Write(LogLevel.LogLevelInfo logLevel, String tag, String message)
        {
            try
            {
                if (tag == "KHOIVV")
                {
                    //Insert database
                    SanitaDataLogModel log = new SanitaDataLogModel();
                    log.App = LogName;
                    log.User = LogUser;
                    log.LogTime = SystemInfo.NOW;
                    log.LogValue = GetLogFormatString(logLevel, tag, message);
                    log.SoftVersion = LogVersion;
                    log.ComputerName = Sanita.Utility.SystemInfo.ComputerName;
                    log.IPAddress = Sanita.Utility.SystemInfo.IPAddress;

                    QueueLogRecord.Enqueue(log);
                }
                else
                {
                    if (!String.IsNullOrEmpty(LogName))
                    {
                        //Filter
                        String data = message.Trim();
                        data = data.ToUpper();
                        data = data.Replace("INSERT INTO", "");
                        data = data.Replace("UPDATE", "");
                        data = data.Replace("DELETE FROM", "");
                        data = data.Trim();
                        String table_name = data.Split(' ')[0];
                        if (!TableLogFilter.Contains(table_name.ToLower()))
                        {
                            return;
                        }

#if false
                        if ((table_name.ToLower() == "sothutuphongkham" || table_name.ToLower() == "sothutubenhpham") && SystemInfo.NOW > new DateTime(2015, 02, 01))
                        {
                            return;
                        }
#endif

                        //Insert database
                        SanitaDataLogModel log = new SanitaDataLogModel();
                        log.App = LogName;
                        log.User = LogUser;
                        log.LogTime = Sanita.Utility.SystemInfo.NOW;
                        log.ComputerName = Sanita.Utility.SystemInfo.ComputerName;
                        log.IPAddress = Sanita.Utility.SystemInfo.IPAddress;
                        log.LogValue = GetLogFormatString(logLevel, tag, message);
                        log.SoftVersion = LogVersion;
                        QueueLogRecord.Enqueue(log);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static String GetLogFormatString(LogLevel.LogLevelInfo logLevel, String tag, String message)
        {
            String new_message = System.Text.RegularExpressions.Regex.Replace(message, @"(\s){2,}", " ");
            return new_message;
        }
    }
}
