using System;
using System.Collections.Generic;
using System.Text;

namespace Medibox.Model
{
    public class SoftUpdate : BaseModel
    {
        public int SoftUpdateID { get; set; }
        public string SoftUpdateVersion { get; set; }
        public string SoftUpdateSQL { get; set; }
        public Byte[] SoftUpdateData { get; set; }
        public int SoftUpdateSize { get; set; }
        public int SoftUpdateUser { get; set; }
        public DateTime SoftUpdateTime { get; set; }
        public String SoftUpdateKey { get; set; }
    }
}
