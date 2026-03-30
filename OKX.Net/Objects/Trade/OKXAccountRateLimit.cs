namespace OKX.Net.Objects.Trade;

/// <summary>
/// Account Rate Limit
/// </summary>
[SerializationModel]
public record OKXAccountRateLimit
{
    /// <summary>
    /// ["<c>fillRatio</c>"] Sub account fill ratio during the monitoring period
    /// </summary>
    [JsonPropertyName("fillRatio")]
    public decimal? FillRatio { get; set; }

    /// <summary>
    /// ["<c>mainFillRatio</c>"] Master account aggregated fill ratio during the monitoring period
    /// </summary>
    [JsonPropertyName("mainFillRatio")]
    public decimal? MainFillRatio { get; set; }

    /// <summary>
    /// ["<c>accRateLimit</c>"] Current sub-account rate limit per 2 seconds
    /// </summary>
    [JsonPropertyName("accRateLimit")]
    public decimal? AccountRateLimit { get; set; }

    /// <summary>
    /// ["<c>nextAccRateLimit</c>"] Expected sub-account rate limit (per 2 seconds) in the next monitoring period
    /// </summary>
    [JsonPropertyName("nextAccRateLimit")]
    public decimal? NextAccountRateLimit { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Data update timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}