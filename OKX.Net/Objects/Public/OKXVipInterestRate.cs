﻿namespace OKX.Net.Objects.Public;

/// <summary>
/// Interest rate
/// </summary>
public record OKXVipInterestRate
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quota
    /// </summary>
    [JsonPropertyName("quota")]
    public decimal? Quota { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Level list
    /// </summary>
    [JsonPropertyName("levelList")]
    public IEnumerable<OKXVipInterestRateLevel> LevelList { get; set; } = Array.Empty<OKXVipInterestRateLevel>();
}

/// <summary>
/// Interest rate level
/// </summary>
public record OKXVipInterestRateLevel
{
    /// <summary>
    /// Level
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Loan quota
    /// </summary>
    [JsonPropertyName("loanQuota")]
    public decimal? LoanQuota { get; set; }
}
