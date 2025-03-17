using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Trade
/// </summary>
[SerializationModel]
public record OKXBlockTrade
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
    /// Implied volatility (Options only)
    /// </summary>
    [JsonPropertyName("fillVol")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// Index price (Options only)
    /// </summary>
    [JsonPropertyName("idxPx")]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// Forward price (Options only)
    /// </summary>
    [JsonPropertyName("fwdPx")]
    public decimal? ForwardPrice { get; set; }

    /// <summary>
    /// Mark price
    /// </summary>
    [JsonPropertyName("markPx")]
    public decimal? MarkPrice { get; set; }

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
}
