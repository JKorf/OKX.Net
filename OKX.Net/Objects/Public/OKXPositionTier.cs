using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Public;

/// <summary>
/// Position tier
/// </summary>
[SerializationModel]
public record OKXPositionTier
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonPropertyName("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonPropertyName("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Tier
    /// </summary>
    [JsonPropertyName("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// Minimum size
    /// </summary>
    [JsonPropertyName("minSz")]
    public decimal? MinimumSize { get; set; }

    /// <summary>
    /// Maximum size
    /// </summary>
    [JsonPropertyName("maxSz")]
    public decimal? MaximumSize { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonPropertyName("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonPropertyName("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Max leverage
    /// </summary>
    [JsonPropertyName("maxLever")]
    public decimal? MaximumLeverage { get; set; }

    /// <summary>
    /// Option margin coef
    /// </summary>
    [JsonPropertyName("optMgnFactor")]
    public decimal? OptionMarginCoefficient { get; set; }

    /// <summary>
    /// Maximum quote loan
    /// </summary>
    [JsonPropertyName("quoteMaxLoan")]
    public decimal? MaximumQuoteLoan { get; set; }

    /// <summary>
    /// Maximum base loan
    /// </summary>
    [JsonPropertyName("baseMaxLoan")]
    public decimal? MaximumBaseLoan { get; set; }
}
