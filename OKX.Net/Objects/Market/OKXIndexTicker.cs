namespace OKX.Net.Objects.Market;

/// <summary>
/// Index ticker
/// </summary>
[SerializationModel]
public record OKXIndexTicker
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>idxPx</c>"] Index price
    /// </summary>
    [JsonPropertyName("idxPx")]
    public decimal? IndexPrice { get; set; }

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
    /// ["<c>open24h</c>"] Open price
    /// </summary>
    [JsonPropertyName("open24h")]
    public decimal? OpenPrice { get; set; }

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
