using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medibox.Service.Net;
using Medibox.Model;
using Sanita.Utility.Logger;

namespace Medibox
{
    public class UtilityWebServer
    {
        //Tag
        private const String TAG = "UtilityWebServer";

        //Singleton
        private static UtilityWebServer _instance;
        public static UtilityWebServer mInstance
        {
            get
            {
                _instance = _instance ?? new UtilityWebServer();
                return _instance;
            }
        }
        
        //Member
        private WebHost mWebHost;

        //Public
        public bool WebServerRunnning;
        public int WebServerPort;
        public String WebServerDirectoryPath;
        public String ErrorMessage;

        public static String SubDomain = "";

        //Constructor
        public UtilityWebServer()
        {
            WebServerRunnning = false;            
            WebServerDirectoryPath = System.Windows.Forms.Application.StartupPath + "\\Public";
        }

        //Public
        public Task<int> StartWebServer()
        {
            WebServerPort = 7001;
            ErrorMessage = "";

            return Task<int>.Factory.StartNew((Func<int>)(() =>
            {
                try
                {
                    if (mWebHost != null && mWebHost.Running)
                    {
                        if (mWebHost.Port == WebServerPort)
                        {
                            return 1;
                        }
                        this.StopWebServer().Wait();
                    }
                    if (this.mWebHost == null)
                    {
                        mWebHost = new WebHost();
                        mWebHost.Port = this.WebServerPort;
                        mWebHost.RootDirectoryPath = this.WebServerDirectoryPath;
                        mWebHost.Start();
                    }
                    this.WebServerRunnning = true;
                    return 0;
                }
                catch (Exception ex)
                {
                    if (mWebHost != null)
                    {
                        mWebHost.Dispose();
                        mWebHost = (WebHost)null;
                    }

                    SanitaLog.e(TAG, "StartWebServer error !", ex);

                    ErrorMessage = ex.ToString();
                    WebServerRunnning = false;
                    return -1;
                }
            }));
        }

        public Task StopWebServer()
        {
            ErrorMessage = "";

            return Task.Factory.StartNew((Action)(() =>
            {
                if (mWebHost != null)
                {
                    mWebHost.Dispose();
                    mWebHost = (WebHost)null;
                }
                if (!WebServerRunnning)
                {
                    return;
                }
                WebServerRunnning = false;
            }));
        }

    }
}
