using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Interfaces;

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

        /// <summary>
        /// Rate limiter configuration for the OKX API
        /// </summary>
        public static OKXRateLimiters RateLimiter { get; } = new OKXRateLimiters();
    }
    /// <summary>
    /// Rate limiter configuration for the GateIo API
    /// </summary>
    public class OKXRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal OKXRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            Public = new RateLimitGate("Public");
            Public.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }


        internal IRateLimitGate Public { get; private set; }
    }
}
