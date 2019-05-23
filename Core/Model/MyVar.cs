using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using Sanita.Utility;
using Sanita.Utility.Encryption;
using System.Threading;
using System.Globalization;
using Sanita.Utility.EasySetting;
using Sanita.Utility.UI;

namespace Medibox.Model
{
    public static class MyVar
    {
        //LICENSE TYPE
        public static int LICENSE_TYPE_NONE = 0;
        public static int LICENSE_TYPE_FULL = 1;
        public static int LICENSE_TYPE_DEMO_100 = 2;
        public static int LICENSE_TYPE_DEMO_500 = 3;
        public static int LICENSE_TYPE_DEMO_1000 = 4;

        public static string mAppVersion = "";
        public static String mAppName = "";
        public static String mAppDescription = "";
        public static String DEFAULT_PORT = "5432";

        public static int USE_PORT = 0;
        public static String USE_HOST = "";

        public static int LicenseType = LICENSE_TYPE_NONE;

        public static String SettingsFileName
        {
            get
            {
                return Path.Combine(Application.StartupPath, @"Data\Setting.xml");
            }
        }

        //Setting
        public static MediboxSetting mMediboxSetting = new MediboxSetting();
        public static int RESOLUTION_WIDTH = 1024;

        //Last data
        public static String LastErrorMessage = "";

        //Data
        public static IList<Phong> mListPhong = new List<Phong>();

        public static IList<LoaiPhong> mListLoaiPhong = new List<LoaiPhong>();
    }
}
