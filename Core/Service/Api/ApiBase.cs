using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using Medibox.Service.Model;

namespace Medibox.Service.Api
{
    public class ApiBase
    {
        private string _className = "";
        private string ClassName
        {
            get
            {
                if (string.IsNullOrEmpty(this._className))
                {
                    this._className = Enumerable.Last<string>((IEnumerable<string>)this.ToString().Split('.'));
                }
                return this._className;
            }
        }

        protected string BaseUri
        {
            get
            {
                return WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri.ToString();
            }
        }

        protected string Callback
        {
            get
            {
                return WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["callback"];
            }
        }

        protected string RequestMessage
        {
            get
            {
                return OperationContext.Current.RequestContext.RequestMessage.ToString();
            }
        }

        protected HttpStatusCode StatusCode
        {
            get
            {
                return WebOperationContext.Current.OutgoingResponse.StatusCode;
            }
            set
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = value;
            }
        }

        protected string ContentType
        {
            get
            {
                return WebOperationContext.Current.OutgoingResponse.ContentType;
            }
            set
            {
                WebOperationContext.Current.OutgoingResponse.ContentType = value;
            }
        }

        protected string RequestContentType
        {
            get
            {
                return WebOperationContext.Current.IncomingRequest.ContentType;
            }
        }

        protected string RequestCharaset
        {
            get
            {
                return WebOperationContext.Current.IncomingRequest.Headers[HttpRequestHeader.AcceptCharset];
            }
        }

        protected WebMessageFormat? RequestFormat
        {
            get
            {
                string requestContentType = this.RequestContentType;
                if (string.IsNullOrWhiteSpace(requestContentType))
                {
                    return new WebMessageFormat?();
                }
                if (requestContentType.ToLower().Contains("application/xml") || requestContentType.ToLower().Contains("text/xml"))
                {
                    return new WebMessageFormat?(WebMessageFormat.Xml);
                }
                if (requestContentType.ToLower().Contains("application/json"))
                {
                    return new WebMessageFormat?(WebMessageFormat.Json);
                }
                return new WebMessageFormat?();
            }
        }

        protected WebMessageFormat? ResponseFormat
        {
            get
            {
                return WebOperationContext.Current.OutgoingResponse.Format;
            }
            set
            {
                WebOperationContext.Current.OutgoingResponse.Format = value;
            }
        }

        protected string Location
        {
            get
            {
                return WebOperationContext.Current.OutgoingResponse.Location;
            }
            set
            {
                WebOperationContext.Current.OutgoingResponse.Location = value;
            }
        }

        protected string ContentDisposition
        {
            get
            {
                return WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"];
            }
            set
            {
                WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = value;
            }
        }

        protected ApiBase()
        {

        }

        protected XmlDictionaryReader GetRequestXmlDictionaryReader()
        {
            return OperationContext.Current.RequestContext.RequestMessage.GetReaderAtBodyContents();
        }

        protected WebMessageFormat SetWebMessageFormat(string format)
        {
            WebOperationContext.Current.OutgoingResponse.Format = new WebMessageFormat?();
            WebMessageFormat webMessageFormat = WebMessageFormat.Xml;
            if (!string.IsNullOrWhiteSpace(format) && format.ToLower() == "json")
            {
                this.ContentType = "application/json";
                webMessageFormat = WebMessageFormat.Json;
            }
            else
            {
                this.ContentType = "application/xml";
            }
            this.ContentType = "text/plain; " + this.ContentType;
            this.ContentType += "; charset=utf-8";
            return webMessageFormat;
        }

        protected virtual Stream ToBinaryStream(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return (Stream)null;
            }
            return (Stream)new MemoryStream(Encoding.UTF8.GetBytes(value));
        }

        protected Stream GetStreamData<T>(T data, String format, HttpStatusCode code) where T : new()
        {
            ApiResult<T> mResult = new ApiResult<T>(code, data);
            this.StatusCode = mResult.HttpStatusCode;
            if (!String.IsNullOrEmpty(mResult.NewResource))
            {
                this.Location = this.BaseUri + "/" + mResult.NewResource + "/";
            }
            return MessageReader.ToMessageStream<T>(mResult.Result, this.SetWebMessageFormat(format), this.Callback);
        }
    }
}
