using System;
using System.Text;

namespace Tres.UtilityNumber
{
    public static class Comparer
    {      
        public static bool IsGreaterThan(this int i, int val)
        {
            return i > val;
        }

        public static bool IsGreaterThanOrEqualTo(this int i, int val)
        {
            return i >= val;
        }

        public static bool IsBetween(this int i, int begin, int end)
        {
            return i >= begin && i<= end;
        }

    }
}
