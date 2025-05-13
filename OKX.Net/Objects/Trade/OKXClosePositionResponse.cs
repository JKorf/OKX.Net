using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Close position response
/// </summary>
[SerializationModel]
public record OKXClosePositionResponse
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    [JsonConverter(typeof(OKXClientIdConverter))]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }
}
