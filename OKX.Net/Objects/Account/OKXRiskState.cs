namespace OKX.Net.Objects.Account;

/// <summary>
/// Account risk state
/// </summary>
[SerializationModel]
public record OKXRiskState
{
    /// <summary>
    /// ["<c>atRisk</c>"] At risk
    /// </summary>
    [JsonPropertyName("atRisk")]
    public bool AtRisk { get; set; }

    /// <summary>
    /// ["<c>atRiskLevel</c>"] At risk level
    /// </summary>
    [JsonPropertyName("atRiskLevel")]
    public string AtRiskLevel { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>atRiskIdx</c>"] At risk index
    /// </summary>
    [JsonPropertyName("atRiskIdx")]
    public string[] AtRiskIndex { get; set; } = Array.Empty<string>();

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}