using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Sanita.Utility;

namespace Sanita.Utility.Logger
{
    public sealed class SanitaLog
    {
        public static LogLevel.LogLevelInfo Level { get; set; }
        public static ILogOutput LogOutput { get; set; }

        private static char[] SpaceLine = new char[72];
        private static readonly char[] HEXDIGIT = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        private static object lockObj = new object();

        private static String LogName = "";
        private static bool isAliboboOnline = false;
        private static String PathAliboboOnline = "";
        private static DateTime InitTime = new DateTime();

        static SanitaLog()
        {
            /* prep for hex dump */
            int i = SpaceLine.Length - 1;
            while (i >= 0)
            {
                SpaceLine[i--] = ' ';
            }
            SpaceLine[0] = SpaceLine[1] = SpaceLine[2] = SpaceLine[3] = '0';
            SpaceLine[4] = '-';
            Level = LogLevel.Verbose;
        }

        private SanitaLog()
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

        public static void SetLogName(String log_name)
        {
            LogName = log_name;
        }

        public static void SetAliboboOnline(String pathFolder)
        {
            isAliboboOnline = true;
            PathAliboboOnline = pathFolder;
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
            else
            {
                e(tag, "exception null");
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
            if (InitTime.Year < 2001)
            {
                InitTime = SystemInfo.NOW;
            }

            //lock (lockObj)
            {
                try
                {
                    //Create folder log
                    String FolderLog = "";
                    String FileLog = "";

                    if (!isAliboboOnline)
                    {
                        FolderLog = Path.Combine(Application.StartupPath, "Log");
                    }
                    else
                    {
                        FolderLog = Path.Combine(PathAliboboOnline, "Log");
                    }

                    if (!Directory.Exists(FolderLog))
                    {
                        Directory.CreateDirectory(FolderLog);
                    }

                    FileLog = Path.Combine(FolderLog, String.Format("{0}_{1:yyyyMMdd_HHmmss}.txt", LogName, InitTime));

                    //Check nếu file > 10M thì log file mới
                    if (File.Exists(FileLog) && new FileInfo(FileLog).Length > 10 * 1000 * 1000)
                    {
                        InitTime = SystemInfo.NOW;
                        FileLog = Path.Combine(FolderLog, String.Format("{0}_{1:yyyyMMdd_HHmmss}.txt", LogName, InitTime));
                    }
                    else
                    {
                        //Nếu khác ngày
                        if (InitTime.BeginTime() != SystemInfo.NOW.BeginTime())
                        {
                            InitTime = SystemInfo.NOW;
                            FileLog = Path.Combine(FolderLog, String.Format("{0}_{1:yyyyMMdd_HHmmss}.txt", LogName, InitTime));
                        }
                    }

                    //Write log
                    File.AppendAllText(FileLog, GetLogFormatString(logLevel, tag, message));
                }
                catch { }
            }
        }

        public static String GetLogFormatString(LogLevel.LogLevelInfo logLevel, String tag, String message)
        {
            return String.Format("[{0}] {1:HH:mm:ss.fff} {2}/{3}: {4}\r\n", SystemInfo.CurrentProcessID, SystemInfo.NOW, logLevel.Letter, tag, message);
        }

        internal static void d(string TAG, Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
