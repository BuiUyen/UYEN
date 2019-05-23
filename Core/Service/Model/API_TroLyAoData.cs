using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Medibox.Service.Model
{
    [DataContract(Namespace = "")]
    public class API_TroLyAoData_In
    {
        [DataMember]
        public String DeviceID { get; set; }

        [DataMember]
        public String Data { get; set; }

        public API_TroLyAoData_In()
        {

        }
    }

    [DataContract(Namespace = "")]
    public class API_TroLyAoData_Out
    {
        [DataMember]
        public String ID { get; set; }

        [DataMember]
        public String Language { get; set; }

        [DataMember]
        public String Data { get; set; }

        public API_TroLyAoData_Out()
        {            
            ID = "";
            Language = "";
            Data = "";          
        }
    }
}
