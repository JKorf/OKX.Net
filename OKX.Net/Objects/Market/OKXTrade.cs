using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Trade
/// </summary>
[SerializationModel]
public record OKXTrade
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>tradeId</c>"] Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public long TradeId { get; set; }

    /// <summary>
    /// ["<c>px</c>"] Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide Side { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>source</c>"] Is ELP order
    /// </summary>
    [JsonPropertyName("source")]
    public bool ElpOrder { get; set; }

    /// <summary>
    /// ["<c>count</c>"] Number of trades if it is an aggregated trade
    /// </summary>
    [JsonPropertyName("count")]
    public int? TradeCount { get; set; }
}
