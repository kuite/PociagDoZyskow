using System.Text.RegularExpressions;

namespace PociagDoZyskow.ExternalDataHandler.Helpers
{
    public static class StringHelper
    {
        public static string CleanString(this string input)
        {
            var replaced = input.Replace("\n", "").Replace("\r", "")
                .Replace(" ", "").Trim();
            return Regex.Replace(replaced, @"s", "");
        }
    }
}
