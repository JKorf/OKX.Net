using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Trade
/// </summary>
[SerializationModel]
public record OKXBlockTrade
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
    /// ["<c>fillVol</c>"] Implied volatility (Options only)
    /// </summary>
    [JsonPropertyName("fillVol")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// ["<c>idxPx</c>"] Index price (Options only)
    /// </summary>
    [JsonPropertyName("idxPx")]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// ["<c>fwdPx</c>"] Forward price (Options only)
    /// </summary>
    [JsonPropertyName("fwdPx")]
    public decimal? ForwardPrice { get; set; }

    /// <summary>
    /// ["<c>markPx</c>"] Mark price
    /// </summary>
    [JsonPropertyName("markPx")]
    public decimal? MarkPrice { get; set; }

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
}
