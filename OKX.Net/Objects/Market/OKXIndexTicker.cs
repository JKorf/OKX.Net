namespace OKX.Net.Objects.Market;

/// <summary>
/// Index ticker
/// </summary>
public class OKXIndexTicker
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Index price
    /// </summary>
    [JsonProperty("idxPx")]
    public decimal IndexPrice { get; set; }

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
    /// Open price
    /// </summary>
    [JsonProperty("open24h")]
    public decimal OpenPrice { get; set; }

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
