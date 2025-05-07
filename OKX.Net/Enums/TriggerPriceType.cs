using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Last price
        /// </summary>
        [Map("last")]
        Last,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("index")]
        Index,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("mark")]
        Mark
    }
}
