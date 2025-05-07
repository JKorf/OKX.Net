using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Chase type
/// </summary>
[JsonConverter(typeof(EnumConverter<ChaseType>))]
public enum ChaseType
{
    /// <summary>
    /// Distance from best bid/ask price. Default
    /// </summary>
    [Map("distance")]
    Distance,
    /// <summary>
    /// Ratio
    /// </summary>
    [Map("ratio")]
    Ratio
}
