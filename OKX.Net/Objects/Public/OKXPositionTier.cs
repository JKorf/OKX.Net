namespace OKX.Net.Objects.Public;

/// <summary>
/// Position tier
/// </summary>
public class OKXPositionTier
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Tier
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// Minimum size
    /// </summary>
    [JsonProperty("minSz")]
    public decimal? MinimumSize { get; set; }

    /// <summary>
    /// Maximum size
    /// </summary>
    [JsonProperty("maxSz")]
    public decimal? MaximumSize { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Max leverage
    /// </summary>
    [JsonProperty("maxLever")]
    public decimal? MaximumLeverage { get; set; }

    /// <summary>
    /// Option margin coef
    /// </summary>
    [JsonProperty("optMgnFactor")]
    public decimal? OptionMarginCoefficient { get; set; }

    /// <summary>
    /// Maximum quote loan
    /// </summary>
    [JsonProperty("quoteMaxLoan")]
    public decimal? MaximumQuoteLoan { get; set; }

    /// <summary>
    /// Maximum base loan
    /// </summary>
    [JsonProperty("baseMaxLoan")]
    public decimal? MaximumBaseLoan { get; set; }
}