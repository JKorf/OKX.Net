using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums
{
    /// <summary>
    /// Trigger price tpye
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerPriceType>))]
    public enum TriggerPriceType
    {
        /// <summary>
        /// ["<c>last</c>"] Last price
        /// </summary>
        [Map("last")]
        Last,
        /// <summary>
        /// ["<c>index</c>"] Index price
        /// </summary>
        [Map("index")]
        Index,
        /// <summary>
        /// ["<c>mark</c>"] Mark price
        /// </summary>
        [Map("mark")]
        Mark
    }
}
