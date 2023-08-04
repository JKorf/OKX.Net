namespace OKX.Net.Objects.Public;

/// <summary>
/// Interest rate
/// </summary>
public class OKXVipInterestRate
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quota
    /// </summary>
    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Level list
    /// </summary>
    [JsonProperty("levelList")]
    public IEnumerable<OKXVipInterestRateLevel> LevelList { get; set; } = Array.Empty<OKXVipInterestRateLevel>();
}

/// <summary>
/// Interest rate level
/// </summary>
public class OKXVipInterestRateLevel
{
    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Loan quota
    /// </summary>
    [JsonProperty("loanQuota")]
    public decimal? LoanQuota { get; set; }
}
