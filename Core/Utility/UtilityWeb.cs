using System;
using System.Net;
using Medibox.Model;
using Newtonsoft.Json;
using Sanita.Utility.Encryption;
using Sanita.Utility.Logger;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Sanita.Utility;
using Medibox.Utility;
using System.Data;
using Medibox.Presenter;

namespace Medibox.Utility
{
    public class UtilityWeb
    {
        private const String TAG = "UtilityWeb";

        //Singleton
        private static UtilityWeb _UtilityCache;
        public static UtilityWeb mInstance
        {
            get
            {
                if (_UtilityCache == null)
                {
                    _UtilityCache = new UtilityWeb();
                }
                return _UtilityCache;
            }
        }

        private WebClient mWebClient;

        public UtilityWeb()
        {
            mWebClient = new WebClient();
            mWebClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
        }

        #region Public

        public Byte[] GetDataRaw(String url)
        {
            try
            {
                return mWebClient.DownloadData(url);
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }

            return null;
        }

        public T GetData<T>(String url)
        {
            try
            {
                var buf = mWebClient.DownloadData(url);
                if (buf != null)
                {
                    string download = System.Text.Encoding.ASCII.GetString(buf);
                    SanitaLog.d(TAG, download);

                    return JsonConvert.DeserializeObject<T>(download);
                }
                else
                {
                    SanitaLog.d(TAG, "buf=null");
                }
            }
            catch (Exception ex)
            {
                SanitaLog.e(TAG, ex);
            }

            return default(T);
        }

        #endregion

    }
}