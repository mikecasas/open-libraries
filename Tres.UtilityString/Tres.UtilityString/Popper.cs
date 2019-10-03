using System;

namespace Tres.UtilityString
{
    public static class Popper
    {
        public static string PopFromFrontN(this string s, int n)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var l = s.Length;
            return s.Substring(0, (l - n));
        }

        public static string PopFirst(this string s)
        {
            return PopFromFrontN(s, 1);
        }

        public static string PopFirstTwo(this string s)
        {
            return PopFromFrontN(s, 2);
        }       

        public static string PopFromBackN(this string s, int n)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var l = s.Length;

            return s.Substring((l-n), l);
        }
    }
}
