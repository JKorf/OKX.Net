using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Trade
/// </summary>
public class OKXTrade
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
    /// Side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(TradeSideConverter))]
    public OKXTradeSide Side { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
