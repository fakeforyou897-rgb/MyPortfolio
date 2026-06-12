using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MyPortfolio.Extensions
{
    /// <summary>
    /// Extension methods for string operations
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to a URL-friendly slug
        /// </summary>
        public static string ToSlug(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // Convert to lowercase
            text = text.ToLowerInvariant();

            // Remove accents/diacritics
            text = RemoveDiacritics(text);

            // Replace spaces and special characters with hyphens
            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
            text = Regex.Replace(text, @"\s+", "-");
            text = Regex.Replace(text, @"-+", "-");

            // Remove leading and trailing hyphens
            text = text.Trim('-');

            // Limit length to 100 characters
            if (text.Length > 100)
                text = text.Substring(0, 100).Trim('-');

            return text;
        }

        /// <summary>
        /// Removes diacritics (accents) from characters
        /// </summary>
        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Truncates a string to a specified length and adds ellipsis
        /// </summary>
        public static string Truncate(this string text, int maxLength, string suffix = "...")
        {
            if (string.IsNullOrWhiteSpace(text) || text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength - suffix.Length) + suffix;
        }

        /// <summary>
        /// Extracts plain text from HTML
        /// </summary>
        public static string StripHtml(this string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            return Regex.Replace(html, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Calculates estimated reading time in minutes
        /// </summary>
        public static int CalculateReadingTime(this string text, int wordsPerMinute = 200)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            var wordCount = text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
            var readingTime = (int)Math.Ceiling((double)wordCount / wordsPerMinute);

            return Math.Max(1, readingTime);
        }
    }
}
