using System.Linq;
using System.Text.RegularExpressions;

namespace ConventionsAide.Core.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        private static readonly Regex RegexScript = new Regex(@"<\s*/?\s*script", RegexOptions.IgnoreCase);

        public static string PascalToKebabCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Regex.Replace(
                value,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                "-$1",
                RegexOptions.Compiled)
                .Trim()
                .ToLower();
        }

        public static string ToSnakeCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var isOnlyUpperCase = value
                .All(x => !char.IsLower(x));

            if (isOnlyUpperCase)
            {
                return value.ToLower();
            }

            return string
                .Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
                .ToLower();
        }

        public static string ReplaceForbiddenScriptTags(this string str, string defaultStr)
        {
            return RegexScript.Replace(str, defaultStr);
        }

        // moved "as is" from Consumer.Infrastructures.CommonServices.DataSerializer
        public static string RemoveXmlXtras(this string value)
        {
            const string Delimiter = "\"";
            const string Encoding = " encoding=\"";
            const string Xsi = " xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'";
            const string Xsd = " xmlns:xsd=\"";

            int startIndex, endIndex = 0;

            if (value.Contains(Xsi))
            {
                startIndex = value.IndexOf(Xsi);
                endIndex = value.IndexOf(Delimiter, startIndex + Xsi.Length);
                value = value.Remove(startIndex, endIndex - startIndex + Delimiter.Length);
            }

            if (value.Contains(Xsd))
            {
                startIndex = value.IndexOf(Xsd);
                endIndex = value.IndexOf(Delimiter, startIndex + Xsd.Length);
                value = value.Remove(startIndex, endIndex - startIndex + Delimiter.Length);
            }

            if (value.Contains(Encoding))
            {
                startIndex = value.IndexOf(Encoding);
                endIndex = value.IndexOf(Delimiter, startIndex + Encoding.Length);
                value = value.Remove(startIndex, endIndex - startIndex + Delimiter.Length);
            }

            // remove invalid  characters
            value = Regex.Replace(value, @"&#x[A-Fa-z0-9]+;", " ");
            return value;
        }

        public static string CutByMaxLength(this string str, int length)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > length)
            {
                str = $"{str.Substring(0, length)}...";
            }

            return str;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static int ToInt(this string input)
        {
            return int.TryParse(input, out int result) ? result : default;
        }

        public static double ToDouble(this string input)
        {
            return double.TryParse(input, out double result) ? result : default;
        }
    }
}
