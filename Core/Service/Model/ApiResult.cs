using System.Net;

namespace Medibox.Service.Model
{
    public class ApiResult<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public T Result { get; set; }

        public string NewResource { get; set; }

        public ApiResult()
        {
            this.HttpStatusCode = HttpStatusCode.InternalServerError;
        }

        public ApiResult(HttpStatusCode code)
        {
            this.HttpStatusCode = code;
        }

        public ApiResult(HttpStatusCode code, T result)
        {
            this.HttpStatusCode = code;
            this.Result = result;
        }

        public ApiResult(HttpStatusCode code, T result, string newResource)
        {
            this.HttpStatusCode = code;
            this.Result = result;
            this.NewResource = newResource;
        }
    }
}
