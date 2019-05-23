using System;

namespace Medibox.Service.Model
{
    internal static class StringExtensions
    {
        public static int ToInt32(this string val)
        {
            int result = 0;
            int.TryParse(val, out result);
            return result;
        }

        public static Guid ToGuid(this string val)
        {
            Guid result = Guid.Empty;
            Guid.TryParse(val, out result);
            return result;
        }
    }
}
