namespace OKX.Net.Objects.Trade;

using OKX.Net.Enums;

/// <summary>
/// Order info update
/// </summary>
[SerializationModel]
public record OKXUserTradeUpdate
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>fillSz</c>"] Trade quantity
    /// </summary>
    [JsonPropertyName("fillSz")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>fillPx</c>"] Trade price
    /// </summary>
    [JsonPropertyName("fillPx")]
    public decimal Price { get; set; }
    /// <summary>
    /// ["<c>side</c>"] Side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide Side { get; set; }
    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>tradeId</c>"] Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string TradeId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>execType</c>"] Role
    /// </summary>
    [JsonPropertyName("execType")]
    public OrderFlowType Role { get; set; }
    /// <summary>
    /// ["<c>count</c>"] Aggregated trade count
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}


