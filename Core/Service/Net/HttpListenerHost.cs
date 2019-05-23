using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Medibox.Service.Net
{
    public class HttpListenerHost
    {
        public Func<string, string> ConvertExtensionToMimeType = (Func<string, string>)(s => "application/octet-stream");
        private ConcurrentDictionary<string, byte[]> mCache;

        private HttpListener HttpListener { get; set; }

        public bool Running
        {
            get
            {
                if (this.HttpListener != null)
                {
                    return this.HttpListener.IsListening;
                }
                return false;
            }
        }

        public string RootDirectoryPath { get; set; }

        public string DefaultFileName { get; set; }

        public HttpListenerHost()
        {
            this.mCache = new ConcurrentDictionary<string, byte[]>();
            this.DefaultFileName = "index.html";
        }

        public HttpListenerHost(string rootDirectoryPath)
            : this()
        {
            this.RootDirectoryPath = rootDirectoryPath;
        }

        public HttpListenerHost(string rootDirectoryPath, string defaultFileName)
            : this()
        {
            this.RootDirectoryPath = rootDirectoryPath;
            this.DefaultFileName = defaultFileName;
        }

        public void Start(params string[] prefixes)
        {
            if (this.Running)
            {
                return;
            }

            this.HttpListener = new HttpListener();

            foreach (string uriPrefix in prefixes)
            {
                this.HttpListener.Prefixes.Add(uriPrefix);
            }

            this.HttpListener.Realm = "DICOM External Service";            

            this.HttpListener.Start();
            this.WaitRequest();
        }

        public void Stop()
        {
            if (this.HttpListener != null)
            {
                this.HttpListener.Abort();
                this.HttpListener = (HttpListener)null;
            }
            this.mCache = (ConcurrentDictionary<string, byte[]>)null;
        }

        private async void WaitRequest()
        {
            try
            {
                while (this.HttpListener != null)
                {
                    if (this.HttpListener.IsListening)
                    {
                        ((HttpListenerHost)this).ProcessRequest((object)await this.HttpListener.GetContextAsync());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        private string GetFilePath(string rawUrl)
        {
            string str1 = rawUrl.ToLower().Trim();
            int num = str1.IndexOf('?');
            if (num > -1)
            {
                str1 = str1.Substring(0, str1.Length - (str1.Length - num));
            }
            if (str1.Length == 6)
            {
                str1 += "/";
            }
            string str2 = str1.Substring(6).Replace('/', '\\');
            if (str2.EndsWith("\\"))
            {
                str2 += this.DefaultFileName;
            }
            return Path.Combine(this.RootDirectoryPath, str2.Substring(1));
        }

        public void ProcessRequest(object context)
        {
            HttpListenerContext ctx = (HttpListenerContext)context;
            HttpListenerWorkerRequest listenerWorkerRequest = new HttpListenerWorkerRequest(ctx);

            HttpResponseMessageProperty prop = new HttpResponseMessageProperty();

            ctx.Request.Headers.Add("Access-Control-Allow-Origin", "*");
            ctx.Request.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            ctx.Request.Headers.Add("Access-Control-Max-Age", "1000");
            ctx.Request.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Content-Length");
            ctx.Request.Headers.Add("Access-Control-Expose-Headers", "dicom_length");

            ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            ctx.Response.Headers.Add("Access-Control-Max-Age", "1000");
            ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Content-Length");
            ctx.Response.Headers.Add("Access-Control-Expose-Headers", "dicom_length");

            //string[] domainParts = app.Context.Request.Url.Host.Split(“.”.ToCharArray());
            //if (domainParts.Length > 2)
            // ctx.Server.Transfer(“/explore/explore.aspx?q=” + domainParts[0]);

            try
            {
                string str1 = listenerWorkerRequest.GetRawUrl().ToLower().Trim();
                int startIndex = str1.IndexOf('?');
                string str2 = "";
                if (startIndex > -1)
                {
                    string str3 = listenerWorkerRequest.GetRawUrl().ToLower().Trim();
                    str1 = str1.Substring(0, str1.Length - (str1.Length - startIndex));
                    str2 = str3.Substring(startIndex, str3.Length - startIndex);
                }

                if (str1.Length == UtilityWebServer.SubDomain.Length)
                {
                    string str3 = ctx.Request.Url.ToString().ToLower();
                    int length = str3.IndexOf(str1);
                    ctx.Response.Redirect(str3.Substring(0, length) + str1 + "/" + str2);
                    listenerWorkerRequest.SendStatus(302);
                }
                else
                {
                    string str3 = str1.Substring(UtilityWebServer.SubDomain.Length).Replace('/', '\\');
                    if (str3.EndsWith("\\"))
                    {
                        str3 += this.DefaultFileName;
                    }
                    string str4 = Path.Combine(this.RootDirectoryPath, str3.Substring(1));
                    FileInfo fileInfo = new FileInfo(str4);
                    if (!fileInfo.Exists)
                    {
                        if (string.IsNullOrEmpty(Path.GetExtension(str4)) && string.IsNullOrEmpty(str2))
                        {
                            str4 = Path.Combine(str4, this.DefaultFileName);
                            fileInfo = new FileInfo(str4);
                        }
                        if (!fileInfo.Exists)
                        {
                            listenerWorkerRequest.SendStatus(404);
                            return;
                        }
                    }
                    listenerWorkerRequest.SendStatus(200);
                    string str5 = ctx.Request.Headers["Accept-Encoding"];
                    bool flag = !string.IsNullOrEmpty(str5) && str5.ToLower().Contains("gzip");
                    if (flag)
                    {
                        ctx.Response.AddHeader("Content-Encoding", "gzip");
                    }
                    ctx.Response.ContentType = this.ConvertExtensionToMimeType(Path.GetExtension(str4));
                    byte[] data = (byte[])null;
                    string key = str4 + fileInfo.LastWriteTime.ToString();
                    if (flag)
                    {
                        key += ".gzip";
                    }
                    if (this.mCache.ContainsKey(key))
                    {
                        data = this.mCache[key];
                    }
                    else
                    {
                        if (flag)
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                using (GZipStream gzipStream = new GZipStream((Stream)memoryStream, CompressionMode.Compress, true))
                                {
                                    gzipStream.Write(System.IO.File.ReadAllBytes(str4), 0, (int)fileInfo.Length);
                                }
                                data = memoryStream.ToArray();
                            }
                        }
                        else
                        {
                            data = System.IO.File.ReadAllBytes(str4);
                        }
                        this.mCache.TryAdd(key, data);
                    }
                    ctx.Response.ContentLength64 = (long)data.Length;
                    listenerWorkerRequest.SendResponseFromMemory(data, data.Length);
                }
            }
            catch
            {
                listenerWorkerRequest.SendStatus(500);
            }
            finally
            {
                listenerWorkerRequest.EndOfRequest();
            }
        }
    }
}
