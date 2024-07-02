namespace OKX.Net.ExtensionMethods
{
    /// <summary>
    /// Extension methods specific to using the OKX API
    /// </summary>
    public static class OKXExtensionMethods
    {
        internal static bool IsNotIn<T>(this T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}
