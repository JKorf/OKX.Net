using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Chase type
/// </summary>
[JsonConverter(typeof(EnumConverter<ChaseType>))]
public enum ChaseType
{
    /// <summary>
    /// ["<c>distance</c>"] Distance from best bid/ask price. Default
    /// </summary>
    [Map("distance")]
    Distance,
    /// <summary>
    /// ["<c>ratio</c>"] Ratio
    /// </summary>
    [Map("ratio")]
    Ratio
}
