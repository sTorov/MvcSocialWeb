namespace MvcSocialWeb.Middlewares.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceQuotes(this string str)
        {
            return str.Replace("\"", "\'");
        }
    }
}
