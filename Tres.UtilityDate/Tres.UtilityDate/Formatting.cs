using System;
using System.Text;

namespace Tres.UtilityDate
{
    public static class Formatting
    {
        //public enum FormatStyle
        //{
        //    CommasNoDecimals,
        //    Money
        //}

        public static string DateDashed(this DateTime i)
        {
            var d = i.Day;
            var m = i.Month;
            string dd;
            string mm;

            if (d < 10)
            {
                dd = $"0{d}";
            }
            else
            {
                dd = $"{d}";
            }

            if (m < 10)
            {
                mm = $"0{m}";
            }
            else
            {
                mm = $"{m}";
            }

            return $"{i.Year}-{mm}-{dd}";
        }
    }
}
