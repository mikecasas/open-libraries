using System;
using System.Text;

namespace Tres.UtilityString
{
    public static class Casing
    {
        public static string ProperCase(this string s, int n)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            string[] j = s.Split(' ');
            var sb = new StringBuilder();

            foreach (var i in j)
            {
                string back = i.TrimFromBackN(i.Length - 1);
                sb.Append($"{i.PopFirst().ToUpper()}{back} ");
            }

            return sb.ToString();
        }
    }
}
