using System.Text.RegularExpressions;

namespace PociagDoZyskow.Services.Helpers
{
    public static class StringHelper
    {
        public static string CleanString(this string input)
        {
            var replaced = input.Replace("\n", "").Replace("\r", "")
                .Replace(" ", "").Replace("#", "").Trim();
            return Regex.Replace(replaced, @"s", "");
        }
    }
}
