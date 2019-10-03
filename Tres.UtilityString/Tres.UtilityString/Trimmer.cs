using System;

namespace Tres.UtilityString
{
    public static class Trimmer
    {
        public static string TrimFromFrontN(this string s, int n)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var l = s.Length;
            return s.Substring(n, l);
        }
        
        public static string TrimLast(this string s)
        {
            return TrimFromBackN(s, 1);
        }

        public static string TrimFromBackN(this string s, int n)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var l = s.Length;

            return s.Substring(0, (l-n));
        }
    }
}