using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Market;

/// <summary>
/// Index ticker
/// </summary>
[SerializationModel]
public record OKXIndexTicker
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Index price
    /// </summary>
    [JsonPropertyName("idxPx")]
    public decimal? IndexPrice { get; set; }

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
    /// Open price
    /// </summary>
    [JsonPropertyName("open24h")]
    public decimal? OpenPrice { get; set; }

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
