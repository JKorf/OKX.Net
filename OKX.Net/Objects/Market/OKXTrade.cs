using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Trade
/// </summary>
[SerializationModel]
public record OKXTrade
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public long TradeId { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide Side { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Number of trades if it is an aggregated trade
    /// </summary>
    [JsonPropertyName("count")]
    public int? TradeCount { get; set; }
}
