namespace Modules.Proget.Uploader.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string text)
            => !string.IsNullOrWhiteSpace(text);

        public static string UseIfEmpty(this string text, string replacement)
           => text.HasValue() ? text : replacement;

        public static string AppendIfNotExist(this string text, char toAppend)
          => text.HasValue()
            ? text.EndsWith(toAppend)
                ? text
                : $"{text}{toAppend}"
            : string.Empty;
    }
}
