using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;

namespace Medibox.Service.Net
{
    public class HttpListenerWorkerRequest : HttpWorkerRequest
    {
        private HttpListenerContext mContext;

        public HttpListenerWorkerRequest(HttpListenerContext ctx)
        {
            this.mContext = ctx;
        }

        public override int ReadEntityBody(byte[] buffer, int size)
        {
            return this.mContext.Request.InputStream.Read(buffer, 0, size);
        }

        public override string GetUriPath()
        {
            return this.mContext.Request.Url.AbsolutePath;
        }

        public override string GetQueryString()
        {
            return this.mContext.Request.Url.Query.TrimStart('?');
        }

        public override string GetRawUrl()
        {
            return this.mContext.Request.RawUrl;
        }

        public override string GetHttpVerbName()
        {
            return this.mContext.Request.HttpMethod;
        }

        public override string GetHttpVersion()
        {
            return string.Format("HTTP/{0}.{1}", (object)this.mContext.Request.ProtocolVersion.Major, (object)this.mContext.Request.ProtocolVersion.Minor);
        }

        public override string GetKnownRequestHeader(int index)
        {
            return this.mContext.Request.Headers[HttpWorkerRequest.GetKnownRequestHeaderName(index)];
        }

        public override string GetRemoteAddress()
        {
            return this.mContext.Request.RemoteEndPoint.Address.ToString();
        }

        public override int GetRemotePort()
        {
            return this.mContext.Request.RemoteEndPoint.Port;
        }

        public override string GetLocalAddress()
        {
            return this.mContext.Request.LocalEndPoint.Address.ToString();
        }

        public override int GetLocalPort()
        {
            return this.mContext.Request.LocalEndPoint.Port;
        }

        public override void SendStatus(int statusCode, string statusDescription)
        {
            this.mContext.Response.StatusCode = statusCode;
            this.mContext.Response.StatusDescription = statusDescription;
        }

        public void SendStatus(int statusCode)
        {
            this.mContext.Response.StatusCode = statusCode;
            this.mContext.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(statusCode);
        }

        public override void SendKnownResponseHeader(int index, string value)
        {
            this.SendUnknownResponseHeader(HttpWorkerRequest.GetKnownResponseHeaderName(index), value);
        }

        public override void SendUnknownResponseHeader(string name, string value)
        {
            this.mContext.Response.Headers[name] = value;
        }

        public override void SendResponseFromFile(string filename, long offset, long length)
        {
            try
            {
                using (FileStream fileStream1 = new FileStream(filename, FileMode.Open))
                {
                    fileStream1.Seek(offset, SeekOrigin.Begin);
                    byte[] buffer1 = new byte[4096];
                    while (true)
                    {
                        FileStream fileStream2 = fileStream1;
                        byte[] buffer2 = buffer1;
                        int offset1 = 0;
                        int count1 = length < (long)buffer1.Length ? (int)length : buffer1.Length;
                        int count2;
                        if ((count2 = fileStream2.Read(buffer2, offset1, count1)) != 0)
                        {
                            this.mContext.Response.OutputStream.Write(buffer1, 0, count2);
                            length -= (long)count2;
                        }
                        else
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void SendResponseFromMemory(IntPtr data, int length)
        {
            byte[] numArray = new byte[length];
            this.CopyMemory(data, 0, numArray, length);
            this.SendResponseFromMemory(numArray, length);
        }

        public override void SendResponseFromMemory(byte[] data, int length)
        {
            this.mContext.Response.OutputStream.Write(data, 0, length);
        }

        private void CopyMemory(IntPtr src, int srcOffset, byte[] dest, int size)
        {
            Marshal.Copy(new IntPtr(src.ToInt64() + (long)srcOffset), dest, 0, size);
        }

        public override void SendResponseFromFile(IntPtr handle, long offset, long length)
        {
            try
            {
                using (SafeFileHandle handle1 = new SafeFileHandle(handle, false))
                {
                    using (FileStream fileStream = new FileStream(handle1, FileAccess.Read))
                    {
                        byte[] buffer = new byte[length];
                        int count = fileStream.Read(buffer, (int)offset, (int)length);
                        this.mContext.Response.OutputStream.Write(buffer, 0, count);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void FlushResponse(bool finalFlush)
        {
            this.mContext.Response.OutputStream.Flush();
        }

        public override void EndOfRequest()
        {
            this.mContext.Response.OutputStream.Close();
            this.mContext.Response.Close();
        }
    }
}
