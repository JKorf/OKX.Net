using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Funding rate history
/// </summary>
public record OKXFundingRateHistory
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Funding time
    /// </summary>
    [JsonPropertyName("fundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FundingTime { get; set; }

    /// <summary>
    /// Funding rate
    /// </summary>
    [JsonPropertyName("fundingRate")]
    public decimal FundingRate { get; set; }

    /// <summary>
    /// Realized rate
    /// </summary>
    [JsonPropertyName("realizedRate")]
    public decimal RealizedRate { get; set; }

    /// <summary>
    /// Funding rate mechanism
    /// </summary>
    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;
}
