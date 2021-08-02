namespace Toolsed.Shared
{
    public static class StringExtensions
    {
        public static string UseIfEmpty(this string text, string replacemet)
            => string.IsNullOrWhiteSpace(text) ? replacemet : text;
        public static bool HasValue(this string text)
            => !string.IsNullOrWhiteSpace(text);

        public static string AppendIfNotPresent(this string text, char toAppend)
        => text.HasValue()
          ? text.EndsWith(toAppend)
              ? text
              : $"{text}{toAppend}"
          : string.Empty;
    }
}
