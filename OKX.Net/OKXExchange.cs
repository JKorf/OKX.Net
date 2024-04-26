namespace OKX.Net
{
    /// <summary>
    /// OKX exchange information and configuration
    /// </summary>
    public static class OKXExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "OKX";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.okx.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://www.okx.com/docs-v5/en/"
            };
    }
}
