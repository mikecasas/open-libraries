using System;
using System.Text;

namespace Tres.UtilityDate
{
    public static class Comparer
    {           
        public static bool IsBetween(this DateTime i, DateTime begin, DateTime end)
        {
            return i >= begin && i<= end;
        }        

        public static bool IsBefore(this DateTime i, DateTime val)
        {
            return i > val;
        }

        public static bool IsOnOrBefore(this DateTime i, DateTime val)
        {
            return i >= val;
        }

        public static bool IsAfter(this DateTime i, DateTime val)
        {
            return i <val;
        }

        public static bool IsOnOrAfter(this DateTime i, DateTime val)
        {
            return i =< val;
        }
    }
}