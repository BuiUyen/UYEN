using System.Collections.Generic;

namespace Medibox.Service.Model
{
    public class CollectionAndValue<T> : List<CollectionAndValue<T>>
    {
        public T Value { get; set; }

        public CollectionAndValue()
        {
        }

        public CollectionAndValue(T value)
        {
            this.Value = value;
        }
    }
}
