using DevComponents.DotNetBar;
using Sanita.Utility.Database.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using Sanita.Utility.Encryption;
using Medibox.Model;

namespace Sanita.Utility
{
    public static class SystemInfo
    {
        private const String TAG = "SystemInfo";


        public static int THEME_STYLE = 0;
        public static eStyle m_eStyle
        {
            get
            {
                return eStyle.Office2010Silver;
            }
        }
        public static Color m_PanelColor
        {
            get
            {
                if (THEME_STYLE == 0)
                {
                    return Color.FromArgb(246, 246, 246);
                }
                else if (THEME_STYLE == 1)
                {
                    return Color.FromArgb(245, 252, 254);
                }
                else if (THEME_STYLE == 2)
                {
                    return Color.FromArgb(246, 251, 255);
                }
                else if (THEME_STYLE == 3)
                {
                    return Color.FromArgb(255, 255, 255);
                }

                return Color.FromArgb(254, 254, 254);
            }
        }

        public static Color m_ListviewAlterRowBackColor
        {
            get
            {
                if (THEME_STYLE == 0)
                {
                    return Color.FromArgb(245, 245, 245);
                }
                else if (THEME_STYLE == 1)
                {
                    return Color.FromArgb(240, 250, 254);
                }
                else if (THEME_STYLE == 2)
                {
                    return Color.FromArgb(245, 245, 245);
                }
                else if (THEME_STYLE == 3)
                {
                    return Color.FromArgb(245, 245, 245);
                }

                return Color.FromArgb(245, 245, 245);
            }
        }

        public static Color m_ListviewSelectedColor
        {
            get
            {
                if (THEME_STYLE == 0)
                {
                    return Color.FromArgb(51, 153, 255);
                }
                else if (THEME_STYLE == 1)
                {
                    return Color.FromArgb(199, 237, 252);
                }
                else if (THEME_STYLE == 2)
                {
                    return Color.FromArgb(246, 251, 255);
                }

                return Color.FromArgb(246, 246, 246);
            }
        }


        public static Dictionary<String, Image> LIST_IMAGE = new Dictionary<string, Image>();

        public static String FontName = "Tahoma";
        public static DatabaseUtility.DATABASE_TYPE DatabaseType = DatabaseUtility.DATABASE_TYPE.POSTGRESQL;
        public static String FORMAT_NUMBER = "US";
        public static String FORMAT_NUMBER_REPORT = "US";
        private static DateTime _now = new DateTime(1, 1, 1);
        private static int _count_connection = 0;
        private static int _count_connection_max = 0;

        public static String MONEY_CODE = "VND";
        public static String MONEY_SIGN = "";
        public static String MONEY_FORMAT = "#,0";
        public static String MONEY_FORMAT_DOUBLE = "0.00";
        public static String MONEY_FORMAT_DOUBLE_XML = "0.##";

        public static bool IsHaveLogTable = false;
        public static String LastErrorMessage = "";
        public static String LastDBErrorMessage = "";

        public static String Software_Name = "";
        public static String Software_Version = "";
        public static String Software_Description = "";

        public static bool isWebMode = false;

        private static CultureInfo _mCultureInfo;
        public static CultureInfo CULTURE_INFO
        {
            get
            {
                if (_mCultureInfo == null)
                {
                    if (FORMAT_NUMBER == "US")
                    {
                        _mCultureInfo = new CultureInfo("en-US");
                        CultureInfo c = _mCultureInfo;
                        NumberFormatInfo nfi = c.NumberFormat;
                        nfi.CurrencyDecimalSeparator = ".";
                        nfi.CurrencyGroupSeparator = ",";
                        nfi.NumberDecimalSeparator = ".";
                        nfi.NumberGroupSeparator = ",";

                        DateTimeFormatInfo d = c.DateTimeFormat;
                        d.FullDateTimePattern = "dd/MM/yyyy";
                        d.ShortDatePattern = "dd/MM/yyyy";
                    }
                    else
                    {
                        _mCultureInfo = new CultureInfo("vi-VN");
                        CultureInfo c = _mCultureInfo;
                        NumberFormatInfo nfi = c.NumberFormat;
                        nfi.CurrencyDecimalSeparator = ",";
                        nfi.CurrencyGroupSeparator = ".";
                        nfi.NumberDecimalSeparator = ",";
                        nfi.NumberGroupSeparator = ".";

                        DateTimeFormatInfo d = c.DateTimeFormat;
                        d.FullDateTimePattern = "dd/MM/yyyy";
                        d.ShortDatePattern = "dd/MM/yyyy";
                    }
                }

                return _mCultureInfo;
            }
        }

        private static CultureInfo _mCultureInfoReport;
        public static CultureInfo CULTURE_INFO_REPORT
        {
            get
            {
                if (_mCultureInfoReport == null)
                {
                    if (FORMAT_NUMBER_REPORT == "US")
                    {
                        _mCultureInfoReport = new CultureInfo("en-US");
                        CultureInfo c = _mCultureInfoReport;
                        NumberFormatInfo nfi = c.NumberFormat;
                        nfi.CurrencyDecimalSeparator = ".";
                        nfi.CurrencyGroupSeparator = ",";
                        nfi.NumberDecimalSeparator = ".";
                        nfi.NumberGroupSeparator = ",";

                        DateTimeFormatInfo d = c.DateTimeFormat;
                        d.FullDateTimePattern = "dd/MM/yyyy";
                        d.ShortDatePattern = "dd/MM/yyyy";
                    }
                    else if (FORMAT_NUMBER_REPORT == "VN")
                    {
                        _mCultureInfoReport = new CultureInfo("vi-VN");
                        CultureInfo c = _mCultureInfoReport;
                        NumberFormatInfo nfi = c.NumberFormat;
                        nfi.CurrencyDecimalSeparator = ",";
                        nfi.CurrencyGroupSeparator = ".";
                        nfi.NumberDecimalSeparator = ",";
                        nfi.NumberGroupSeparator = ".";

                        DateTimeFormatInfo d = c.DateTimeFormat;
                        d.FullDateTimePattern = "dd/MM/yyyy";
                        d.ShortDatePattern = "dd/MM/yyyy";
                    }
                    else
                    {
                        return CULTURE_INFO;
                    }
                }

                return _mCultureInfoReport;
            }
        }

        public static DateTime NOW
        {
            get
            {
                if (_now.Year < 1900)
                {
                    return DateTime.Now;
                }
                return _now;
            }
            set
            {
                _now = value;
            }
        }

        public static int COUNT_CONNECTION
        {
            get
            {
                return _count_connection;
            }
            set
            {
                _count_connection = value;
            }
        }

        public static int COUNT_CONNECTION_MAX
        {
            get
            {
                return _count_connection_max;
            }
            set
            {
                _count_connection_max = value;
            }
        }

        public static bool IsKhoiVV
        {
            get
            {
                //‎E8:94:F6:D9:D0:18
                //khoivv
                string bbb = SystemInfo.NetworkAdapterMACAddress;
                string aaa = CryptorEngine.CreateMD5Hash(bbb).ToLower();
                if (aaa == "9e0112372b0bf0f789499ffeaaa2108f")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static String ProcessorId
        {
            get
            {
                return RunQuery("Processor", "ProcessorId");
            }
        }
        public static String BaseBoardProduct
        {
            get
            {
                return RunQuery("BaseBoard", "Product");
            }
        }
        public static String BaseBoardManufacturer
        {
            get
            {
                return RunQuery("BaseBoard", "Manufacturer");
            }
        }
        public static String DiskDriveSignature
        {
            get
            {
                return RunQuery("DiskDrive", "Signature");
            }
        }
        public static String VideoControllerCaption
        {
            get
            {
                return RunQuery("VideoController", "Caption");
            }
        }
        public static String PhysicalMediaSerialNumber
        {
            get
            {
                return RunQuery("PhysicalMedia", "SerialNumber");
            }
        }
        public static String BIOSVersion
        {
            get
            {
                return RunQuery("BIOS", "Version");
            }
        }
        public static String OperatingSystemSerialNumber
        {
            get
            {
                return RunQuery("OperatingSystem", "SerialNumber");
            }
        }

        public static String NetworkAdapterMACAddress
        {
            get
            {
                //return RunQuery("NetworkAdapter", "MACAddress");
                return KHOIVV_MACAddress;
            }
        }
        public static String ComputerName
        {
            get
            {
                return Dns.GetHostName();
            }
        }
        public static String IPAddress
        {
            get
            {
                String IPAddress = "";
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress ip_address in localIPs)
                {
                    if (ip_address.IsIPv6LinkLocal)
                    {
                        continue;
                    }
                    if (ip_address.ToString().Contains(":"))
                    {
                        continue;
                    }
                    if (IPAddress == "")
                    {
                        IPAddress = ip_address.ToString();
                    }
                    else
                    {
                        IPAddress += ";" + ip_address.ToString();
                    }
                }
                return IPAddress;
            }
        }

        public static String IPAddress1
        {
            get
            {
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress ip_address in localIPs)
                {
                    if (ip_address.IsIPv6LinkLocal)
                    {
                        continue;
                    }
                    if (ip_address.ToString().Contains(":"))
                    {
                        continue;
                    }
                    return ip_address.ToString();
                }
                return "";
            }
        }

        public static String UserName
        {
            get
            {
                return Environment.UserName;
            }
        }

        private static string RunQuery(string TableName, string MethodName)
        {
            try
            {
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * from Win32_" + TableName);
                foreach (ManagementObject MO in MOS.Get())
                {
                    if (MO[MethodName] != null)
                    {
                        return MO[MethodName].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        private static string _mac_address = "";
        public static string KHOIVV_MACAddress
        {
            get
            {
                if (!String.IsNullOrEmpty(_mac_address))
                {
                    //Nothing
                }
                else
                {
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    String sMacAddress = string.Empty;
                    foreach (NetworkInterface adapter in nics)
                    {
                        Sanita.Utility.Logger.SanitaLogEx.d(TAG, "adapter.Name = " + adapter.Name);
                        Sanita.Utility.Logger.SanitaLogEx.d(TAG, "adapter.Description = " + adapter.Description);
                        Sanita.Utility.Logger.SanitaLogEx.d(TAG, "adapter.NetworkInterfaceType = " + adapter.NetworkInterfaceType);
                        Sanita.Utility.Logger.SanitaLogEx.d(TAG, "adapter.OperationalStatus = " + adapter.OperationalStatus);
                        Sanita.Utility.Logger.SanitaLogEx.d(TAG, "adapter.GetPhysicalAddress() = " + adapter.GetPhysicalAddress().ToString());

                        if (adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback && adapter.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                        {
                            if (adapter.OperationalStatus == OperationalStatus.Up || adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet || adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                            {
                                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                                {
                                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                                    Sanita.Utility.Logger.SanitaLogEx.d(TAG, "Set MAC = " + sMacAddress);
                                }
                            }
                        }
                    }
                    _mac_address = sMacAddress;
                }

                return _mac_address;
            }
        }
        public static string KHOIVV_OSVersion
        {
            get
            {
                return System.Environment.OSVersion.ToString();
            }
        }
        public static string KHOIVV_ProcessorCount
        {
            get
            {
                return System.Environment.ProcessorCount.ToString();
            }
        }
        public static string KHOIVV_MachineName
        {
            get
            {
                return System.Environment.MachineName;
            }
        }
        public static string KHOIVV_UserName
        {
            get
            {
                return System.Environment.UserName;
            }
        }

        public static int CurrentProcessID
        {
            get
            {
                return System.Diagnostics.Process.GetCurrentProcess().Id;
            }
        }

        private static String _hardware_id = "";
        public static String HarwareID
        {
            get
            {
                if (String.IsNullOrEmpty(_hardware_id))
                {
                    _hardware_id = CryptorEngine.CreateMD5Hash(PhysicalMediaSerialNumber + DiskDriveSignature + BaseBoardManufacturer);
                }
                return _hardware_id;
            }
        }
    }
}
