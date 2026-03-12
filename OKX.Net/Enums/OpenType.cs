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
        /// ["<c>fix_price</c>"] Fix price
        /// </summary>
        [Map("fix_price")]
        FixPrice,
        /// <summary>
        /// ["<c>pre_quote</c>"] Prequote
        /// </summary>
        [Map("pre_quote")]
        PreQuote,
        /// <summary>
        /// ["<c>call_auction</c>"] Call auction
        /// </summary>
        [Map("call_auction")]
        CallAuction
    }
}
