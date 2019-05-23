using System;
using Sanita.Utility.EasySetting;

namespace Medibox.Model
{
    [Serializable()]
    public class MediboxSetting : BaseModel
    {
        public bool DB_ENCRIPT { get; set; }
        public String DB_SERVER { get; set; }
        public String DB_NAME { get; set; }
        public String DB_USERID { get; set; }
        public String DB_USERPASSWORD { get; set; }
        public String DB_PORT { get; set; }
    }
}
