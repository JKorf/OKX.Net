using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums
{
    /// <summary>
    /// Open type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OpenType>))]
    public enum OpenType
    {
        /// <summary>
        /// Fix price
        /// </summary>
        [Map("fix_price")]
        FixPrice,
        /// <summary>
        /// Prequote
        /// </summary>
        [Map("pre_quote")]
        PreQuote,
        /// <summary>
        /// Call auction
        /// </summary>
        [Map("call_auction")]
        CallAuction
    }
}
