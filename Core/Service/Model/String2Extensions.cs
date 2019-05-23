using System;
using System.Collections.Generic;
using System.Linq;

namespace Medibox.Service.Model
{
    internal static class String2Extensions
    {
        private static Type[] NumTypes = new Type[11]
            {
              typeof (sbyte),
              typeof (byte),
              typeof (short),
              typeof (ushort),
              typeof (int),
              typeof (uint),
              typeof (long),
              typeof (ulong),
              typeof (float),
              typeof (double),
              typeof (Decimal)
            };

        public static bool TryChangeType(this string val, Type changeType, out object result)
        {
            result = (object)null;
            if (Enumerable.Contains<Type>((IEnumerable<Type>)changeType.GetInterfaces(), typeof(IConvertible)))
            {
                return String2Extensions.TryConvertChangeType(val, changeType, out result);
            }
            if (changeType == typeof(Guid))
            {
                Guid result1 = Guid.Empty;
                if (!Guid.TryParse(val, out result1))
                {
                    return false;
                }
                result = (object)result1;
                return true;
            }
            if (!String2Extensions.IsNullableType(changeType))
            {
                return false;
            }
            Type underlyingType = Nullable.GetUnderlyingType(changeType);
            if (underlyingType != (Type)null)
            {
                return String2Extensions.TryConvertChangeType(val, underlyingType, out result);
            }
            return String2Extensions.TryConvertChangeType(val, changeType, out result);
        }

        public static bool TryConvertChangeType(string val, Type changeType, out object result)
        {
            result = (object)null;
            try
            {
                if (changeType.IsEnum)
                {
                    result = Enum.Parse(changeType, val);
                    return true;
                }
                if (Enumerable.Contains<Type>((IEnumerable<Type>)String2Extensions.NumTypes, changeType))
                {
                    double result1 = 0.0;
                    if (!double.TryParse(val, out result1))
                    {
                        return false;
                    }
                }
                result = Convert.ChangeType((object)val, changeType);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = (object)null;
                return false;
            }
        }

        public static bool IsNullableType(Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }
            return false;
        }
    }
}
