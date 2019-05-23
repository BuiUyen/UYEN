using System.Reflection;

namespace Medibox.Service.Model
{
    internal class MessageBody<T> where T : new()
    {
        private CollectionAndValue<PropertyInfo> _properties;

        public T Value { get; set; }

        public CollectionAndValue<PropertyInfo> ReadProperties
        {
            get
            {
                return this._properties ?? (this._properties = new CollectionAndValue<PropertyInfo>());
            }
        }

        public MessageBody()
        {
        }

        public MessageBody(T value)
        {
            this.Value = value;
        }
    }
}
