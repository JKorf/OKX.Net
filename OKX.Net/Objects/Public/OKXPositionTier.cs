namespace OKX.Net.Objects.Public;

/// <summary>
/// Position tier
/// </summary>
[SerializationModel]
public record OKXPositionTier
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instFamily</c>"] Instrument family
    /// </summary>
    [JsonPropertyName("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>uly</c>"] Underlying
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>tier</c>"] Tier
    /// </summary>
    [JsonPropertyName("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// ["<c>minSz</c>"] Minimum size
    /// </summary>
    [JsonPropertyName("minSz")]
    public decimal? MinimumSize { get; set; }

    /// <summary>
    /// ["<c>maxSz</c>"] Maximum size
    /// </summary>
    [JsonPropertyName("maxSz")]
    public decimal? MaximumSize { get; set; }

    /// <summary>
    /// ["<c>mmr</c>"] Maintenance margin requirement
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// ["<c>imr</c>"] Initial margin requirement
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// ["<c>maxLever</c>"] Max leverage
    /// </summary>
    [JsonPropertyName("maxLever")]
    public decimal? MaximumLeverage { get; set; }

    /// <summary>
    /// ["<c>optMgnFactor</c>"] Option margin coef
    /// </summary>
    [JsonPropertyName("optMgnFactor")]
    public decimal? OptionMarginCoefficient { get; set; }

    /// <summary>
    /// ["<c>quoteMaxLoan</c>"] Maximum quote loan
    /// </summary>
    [JsonPropertyName("quoteMaxLoan")]
    public decimal? MaximumQuoteLoan { get; set; }

    /// <summary>
    /// ["<c>baseMaxLoan</c>"] Maximum base loan
    /// </summary>
    [JsonPropertyName("baseMaxLoan")]
    public decimal? MaximumBaseLoan { get; set; }
}
