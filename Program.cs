using DevComponents.DotNetBar;
using Medibox.Model;
using Medibox.Presenter;
using Newtonsoft.Json;
using Sanita.Utility;
using Sanita.Utility.Encryption;
using Sanita.Utility.Logger;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Medibox
{
    static class Program
    {
        private const String TAG = "Program";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SanitaLog.SetLogName("Medibox");
            SanitaLogEx.SetLogName("Medibox");

            //Create temp            
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data"));
            }
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Log")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Log"));
            }

            //Get info
            object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(false);
            foreach (object attribute in customAttributes)
            {
                if (attribute.GetType() == typeof(AssemblyDescriptionAttribute))
                {
                    MyVar.mAppDescription = ((AssemblyDescriptionAttribute)attribute).Description;
                    SanitaLog.d(TAG, "Description:" + MyVar.mAppDescription);
                }
                if (attribute.GetType() == typeof(AssemblyTitleAttribute))
                {
                    MyVar.mAppName = ((AssemblyTitleAttribute)attribute).Title;
                    SanitaLog.d(TAG, "Name:" + MyVar.mAppName);
                }
                if (attribute.GetType() == typeof(AssemblyFileVersionAttribute))
                {
                    MyVar.mAppVersion = ((AssemblyFileVersionAttribute)attribute).Version;
                    SanitaLog.d(TAG, "Version:" + MyVar.mAppVersion);
                }
            }

            //Init Toast
            ToastNotification.DefaultToastGlowColor = eToastGlowColor.Red;
            ToastNotification.DefaultToastPosition = eToastPosition.MiddleCenter;
            ToastNotification.ToastFont = new System.Drawing.Font("Tahoma", 10);

            //Load setting
            try
            {
                MyVar.mMediboxSetting = JsonConvert.DeserializeObject<MediboxSetting>(File.ReadAllText(MyVar.SettingsFileName));
                if (MyVar.mMediboxSetting.DB_ENCRIPT)
                {
                    MyVar.mMediboxSetting.DB_ENCRIPT = false;
                    MyVar.mMediboxSetting.DB_SERVER = CryptorEngine.Decrypt(MyVar.mMediboxSetting.DB_SERVER, true);
                    MyVar.mMediboxSetting.DB_USERID = CryptorEngine.Decrypt(MyVar.mMediboxSetting.DB_USERID, true);
                    MyVar.mMediboxSetting.DB_PORT = CryptorEngine.Decrypt(MyVar.mMediboxSetting.DB_PORT, true);
                    MyVar.mMediboxSetting.DB_USERPASSWORD = CryptorEngine.Decrypt(MyVar.mMediboxSetting.DB_USERPASSWORD, true);
                    MyVar.mMediboxSetting.DB_NAME = CryptorEngine.Decrypt(MyVar.mMediboxSetting.DB_NAME, true);
                }
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }            
            
            //Start application            
            {
                //Check database                
                while (true)
                {
                    String Localserver = MyVar.mMediboxSetting.DB_SERVER;
                    String Localdatabase = MyVar.mMediboxSetting.DB_NAME;
                    String Localuserid = MyVar.mMediboxSetting.DB_USERID;
                    String Localpassword = MyVar.mMediboxSetting.DB_USERPASSWORD;

                    SystemInfo.DatabaseType = Sanita.Utility.Database.Utility.DatabaseUtility.DATABASE_TYPE.POSTGRESQL;
                    SoftUpdatePresenter.SetConnectionConfig(Localserver, Localuserid, Localpassword, Localdatabase, MyVar.DEFAULT_PORT);

                    //Init database
                    SoftUpdatePresenter.InitDatabase();

                    //Check database OK
                    if (!SoftUpdatePresenter.IsDatabaseOK())
                    {
                        using (FormConfigDatabase form = new FormConfigDatabase())
                        {
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                continue;
                            }
                            else
                            {
                                SanitaMessageBox.Show("Lỗi kết nối cơ sở dữ liệu !", "Thông Báo");
                                return;
                            }
                        }
                    }

                    break;
                }

                //Start main form
                FormLogin mFormLogin = new FormLogin();
                Application.Run(mFormLogin);
            }
        }
    }
}
