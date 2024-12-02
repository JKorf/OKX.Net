﻿using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;

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
        /// Exchange name
        /// </summary>
        public static string DisplayName => "OKX";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/OKX.Net/master/OKX.Net/Icon/icon.png";

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

        internal const string ClientOrderId = "1425d83a94fbBCDE";
        internal const string ClientOrderIdPrefix = ClientOrderId + LibraryHelpers.ClientOrderIdSeperator;

        /// <summary>
        /// Format a base and quote asset to an OKX recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            if (tradingMode == TradingMode.Spot)
                return baseAsset.ToUpperInvariant() + "-" + quoteAsset.ToUpperInvariant();

            if (deliverTime == null)
                return baseAsset.ToUpperInvariant() + "-" + quoteAsset.ToUpperInvariant() + "-SWAP";

            return baseAsset.ToUpperInvariant() + "-" + quoteAsset.ToUpperInvariant() + "-" + deliverTime.Value.ToString("yyMMdd");
        }

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
            EndpointGate = new RateLimitGate("Endpoint Gate");
            EndpointGate.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }


        internal IRateLimitGate EndpointGate { get; private set; }
    }
}
