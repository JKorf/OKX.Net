using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums
{
    /// <summary>
    /// Order kind
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerOrderKind>))]
    public enum TriggerOrderKind
    {
        /// <summary>
        /// Condition
        /// </summary>
        [Map("condition")]
        Condition,
        /// <summary>
        /// Limit
        /// </summary>
        [Map("limit")]
        Limit
    }
}
