namespace MvcSocialWeb.Middlewares.Extensions
{
    /// <summary>
    /// Расширения для string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Замена двойных кавычек в тексте на одинарные
        /// </summary>
        public static string ReplaceQuotes(this string str)
        {
            return str.Replace("\"", "\'");
        }
    }
}
