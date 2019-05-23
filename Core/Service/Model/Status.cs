using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Medibox.Service.Model
{
    [DataContract(Namespace = "")]
    public class Status : IComparable<Status>
    {
        [DataMember]
        public int Result { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public TraceLevel TraceLevel { get; set; }

        [DataMember]
        public DateTime RegisteredDateTime { get; set; }

        public Status()
        {
            this.RegisteredDateTime = DateTime.Now;
        }

        public Status(int result, string name, string message, TraceLevel traceLevel)
        {
            this.Result = result;
            this.Name = name;
            this.Message = message;
            this.TraceLevel = traceLevel;
            this.RegisteredDateTime = DateTime.Now;
        }

        public int CompareTo(Status other)
        {
            if (other == null)
                return 1;
            int num = this.RegisteredDateTime.CompareTo(other.RegisteredDateTime);
            if (num == 0)
                num = this.TraceLevel.CompareTo((object)other.TraceLevel);
            if (num == 0)
                num = this.Result.CompareTo(other.Result);
            if (num == 0 && this.Name != null)
                num = this.Name.CompareTo(other.Name);
            if (num == 0 && this.Message != null)
                num = this.Message.CompareTo(other.Message);
            return num;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}] [{2}] [{3}] {4}", (object)this.RegisteredDateTime.ToString("yyyy/MM/dd HH:mm:ss"), (object)this.TraceLevel.ToString(), (object)this.Result, (object)this.Name, (object)this.Message);
        }

        public override int GetHashCode()
        {
            return (this.GetType().FullName + "=" + this.ToString()).GetHashCode();
        }
    }
}
