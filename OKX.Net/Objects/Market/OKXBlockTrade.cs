using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Trade
/// </summary>
public record OKXBlockTrade
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Trade id
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Implied volatility (Options only)
    /// </summary>
    [JsonProperty("fillVol")]
    public decimal ImpliedVolatility { get; set; }

    /// <summary>
    /// Index price (Options only)
    /// </summary>
    [JsonProperty("idxPx")]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// Forward price (Options only)
    /// </summary>
    [JsonProperty("fwdPx")]
    public decimal? ForwardPrice { get; set; }

    /// <summary>
    /// Mark price
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(EnumConverter))]
    public OrderSide Side { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
