using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Ticker
/// </summary>
[SerializationModel]
public record OKXTicker
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Last price
    /// </summary>
    [JsonPropertyName("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Last trade quantity
    /// </summary>
    [JsonPropertyName("lastSz")]
    public decimal? LastQuantity { get; set; }

    /// <summary>
    /// Best ask price
    /// </summary>
    [JsonPropertyName("askPx")]
    public decimal? BestAskPrice { get; set; }

    /// <summary>
    /// Best ask quantity
    /// </summary>
    [JsonPropertyName("askSz")]
    public decimal? BestAskQuantity { get; set; }

    /// <summary>
    /// Best bid price
    /// </summary>
    [JsonPropertyName("bidPx")]
    public decimal? BestBidPrice { get; set; }

    /// <summary>
    /// Best bid quantity
    /// </summary>
    [JsonPropertyName("bidSz")]
    public decimal? BestBidQuantity { get; set; }

    /// <summary>
    /// Open price
    /// </summary>
    [JsonPropertyName("open24h")]
    public decimal? OpenPrice { get; set; }

    /// <summary>
    /// High price
    /// </summary>
    [JsonPropertyName("high24h")]
    public decimal? HighPrice { get; set; }

    /// <summary>
    /// Low price
    /// </summary>
    [JsonPropertyName("low24h")]
    public decimal? LowPrice { get; set; }

    /// <summary>
    /// Quote asset volume. For Spot/Margin this is the quantity in the quote asset. For derivatives it's the quantity in base asset.
    /// </summary>
    [JsonPropertyName("volCcy24h")]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// Volume. For Spot/Margin this is the volume in base asset. For derivatives it's the number of contracts.
    /// </summary>
    [JsonPropertyName("vol24h")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Open price UTC 0
    /// </summary>
    [JsonPropertyName("sodUtc0")]
    public decimal? OpenPriceUtc0 { get; set; }

    /// <summary>
    /// Open price UTC 8
    /// </summary>
    [JsonPropertyName("sodUtc8")]
    public decimal? OpenPriceUtc8 { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
