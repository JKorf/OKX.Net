namespace OKX.Net.Objects.Public;

/// <summary>
/// Interest rate
/// </summary>
[SerializationModel]
public record OKXVipInterestRate
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>quota</c>"] Quota
    /// </summary>
    [JsonPropertyName("quota")]
    public decimal? Quota { get; set; }

    /// <summary>
    /// ["<c>rate</c>"] Rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// ["<c>levelList</c>"] Level list
    /// </summary>
    [JsonPropertyName("levelList")]
    public OKXVipInterestRateLevel[] LevelList { get; set; } = Array.Empty<OKXVipInterestRateLevel>();
}

/// <summary>
/// Interest rate level
/// </summary>
[SerializationModel]
public record OKXVipInterestRateLevel
{
    /// <summary>
    /// ["<c>level</c>"] Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>loanQuota</c>"] Loan quota
    /// </summary>
    [JsonPropertyName("loanQuota")]
    public decimal? LoanQuota { get; set; }
}
