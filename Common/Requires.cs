using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace LibClangSharp.Common
{
    internal static class Requires
    {
        public static void NotNullOrEmpty(string value, string paramName)
        {
            if (String.IsNullOrEmpty(value)) { 
                throw new ArgumentException(
                    String.Format(
                        CultureInfo.CurrentCulture, 
                        CommonResources.Argument_NotNullOrEmpty, 
                        paramName),
                    paramName); 
            }
        }

        public static void NotNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void InRange(bool condition, string paramName)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        public static void ValidEnumMember<TEnum>(TEnum value, TEnum min, TEnum max, string paramName)
        {
#if DEBUG
            bool isValid = Enum.IsDefined(typeof(TEnum), value);
#else
            // Enum.IsDefined is super-slow, too slow for Release builds
            bool isValid = value >= min && value <= max;
#endif
            if (!isValid)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    String.Format(CommonResources.Argument_InvalidEnumValue, paramName, typeof(TEnum).FullName));
            }
        }
    }
}