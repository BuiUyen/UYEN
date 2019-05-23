using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Medibox.Service.Net
{
    public class MyServiceAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            HttpResponseMessageProperty prop = new HttpResponseMessageProperty();


            prop.Headers.Add("Access-Control-Allow-Origin: *");
            prop.Headers.Add("Access-Control-Allow-Methods: POST, GET, OPTIONS");
            prop.Headers.Add("Access-Control-Max-Age: 1000");
            prop.Headers.Add("Access-Control-Allow-Headers: Content-Type, Content-Length");
            prop.Headers.Add("Access-Control-Expose-Headers: dicom_length");

            operationContext.OutgoingMessageProperties.Add(HttpResponseMessageProperty.Name, prop);

            return true;
        }

    }
}
