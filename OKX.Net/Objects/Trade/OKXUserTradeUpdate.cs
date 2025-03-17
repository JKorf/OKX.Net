using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Trade;

using OKX.Net.Enums;

/// <summary>
/// Order info update
/// </summary>
[SerializationModel]
public record OKXUserTradeUpdate
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// Trade quantity
    /// </summary>
    [JsonPropertyName("fillSz")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// Trade price
    /// </summary>
    [JsonPropertyName("fillPx")]
    public decimal Price { get; set; }
    /// <summary>
    /// Side
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide Side { get; set; }
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// Trade id
    /// </summary>
    [JsonPropertyName("tradeId")]
    public string TradeId { get; set; } = string.Empty;
    /// <summary>
    /// Role
    /// </summary>
    [JsonPropertyName("execType")]
    public OrderFlowType Role { get; set; }
    /// <summary>
    /// Aggregated trade count
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}


