using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Ticker
/// </summary>
public class OKXTicker
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Last price
    /// </summary>
    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Last trade quantity
    /// </summary>
    [JsonProperty("lastSz")]
    public decimal? LastQuantity { get; set; }

    /// <summary>
    /// Best ask price
    /// </summary>
    [JsonProperty("askPx")]
    public decimal? BestAskPrice { get; set; }

    /// <summary>
    /// Best ask quantity
    /// </summary>
    [JsonProperty("askSz")]
    public decimal? BestAskSize { get; set; }

    /// <summary>
    /// Best bid price
    /// </summary>
    [JsonProperty("bidPx")]
    public decimal? BestBidPrice { get; set; }

    /// <summary>
    /// Best bid quantity
    /// </summary>
    [JsonProperty("bidSz")]
    public decimal? BestBidQuantity { get; set; }

    /// <summary>
    /// Open price
    /// </summary>
    [JsonProperty("open24h")]
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// High price
    /// </summary>
    [JsonProperty("high24h")]
    public decimal HighPrice { get; set; }

    /// <summary>
    /// Low price
    /// </summary>
    [JsonProperty("low24h")]
    public decimal LowPrice { get; set; }

    /// <summary>
    /// Quote Volume
    /// </summary>
    [JsonProperty("volCcy24h")]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// Base Volume
    /// </summary>
    [JsonProperty("vol24h")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Open price UTC 0
    /// </summary>
    [JsonProperty("sodUtc0")]
    public decimal OpenPriceUtc0 { get; set; }

    /// <summary>
    /// Open price UTC 8
    /// </summary>
    [JsonProperty("sodUtc8")]
    public decimal OpenPriceUtc8 { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
