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
        /// ["<c>condition</c>"] Condition
        /// </summary>
        [Map("condition")]
        Condition,
        /// <summary>
        /// ["<c>limit</c>"] Limit
        /// </summary>
        [Map("limit")]
        Limit
    }
}
