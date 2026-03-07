using OKX.Net.Enums;

namespace OKX.Net.Objects.Market;

/// <summary>
/// Ticker
/// </summary>
[SerializationModel]
public record OKXTicker
{
    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>last</c>"] Last price
    /// </summary>
    [JsonPropertyName("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// ["<c>lastSz</c>"] Last trade quantity
    /// </summary>
    [JsonPropertyName("lastSz")]
    public decimal? LastQuantity { get; set; }

    /// <summary>
    /// ["<c>askPx</c>"] Best ask price
    /// </summary>
    [JsonPropertyName("askPx")]
    public decimal? BestAskPrice { get; set; }

    /// <summary>
    /// ["<c>askSz</c>"] Best ask quantity
    /// </summary>
    [JsonPropertyName("askSz")]
    public decimal? BestAskQuantity { get; set; }

    /// <summary>
    /// ["<c>bidPx</c>"] Best bid price
    /// </summary>
    [JsonPropertyName("bidPx")]
    public decimal? BestBidPrice { get; set; }

    /// <summary>
    /// ["<c>bidSz</c>"] Best bid quantity
    /// </summary>
    [JsonPropertyName("bidSz")]
    public decimal? BestBidQuantity { get; set; }

    /// <summary>
    /// ["<c>open24h</c>"] Open price
    /// </summary>
    [JsonPropertyName("open24h")]
    public decimal? OpenPrice { get; set; }

    /// <summary>
    /// ["<c>high24h</c>"] High price
    /// </summary>
    [JsonPropertyName("high24h")]
    public decimal? HighPrice { get; set; }

    /// <summary>
    /// ["<c>low24h</c>"] Low price
    /// </summary>
    [JsonPropertyName("low24h")]
    public decimal? LowPrice { get; set; }

    /// <summary>
    /// ["<c>volCcy24h</c>"] Quote asset volume. For Spot/Margin this is the quantity in the quote asset. For derivatives it's the quantity in base asset.
    /// </summary>
    [JsonPropertyName("volCcy24h")]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// ["<c>vol24h</c>"] Volume. For Spot/Margin this is the volume in base asset. For derivatives it's the number of contracts.
    /// </summary>
    [JsonPropertyName("vol24h")]
    public decimal Volume { get; set; }

    /// <summary>
    /// ["<c>sodUtc0</c>"] Open price UTC 0
    /// </summary>
    [JsonPropertyName("sodUtc0")]
    public decimal? OpenPriceUtc0 { get; set; }

    /// <summary>
    /// ["<c>sodUtc8</c>"] Open price UTC 8
    /// </summary>
    [JsonPropertyName("sodUtc8")]
    public decimal? OpenPriceUtc8 { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
