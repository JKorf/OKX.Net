using OKX.Net.Converters;
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
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Funding time
    /// </summary>
    [JsonProperty("fundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FundingTime { get; set; }

    /// <summary>
    /// Funding rate
    /// </summary>
    [JsonProperty("fundingRate")]
    public decimal FundingRate { get; set; }

    /// <summary>
    /// Realized rate
    /// </summary>
    [JsonProperty("realizedRate")]
    public decimal RealizedRate { get; set; }

    /// <summary>
    /// Funding rate mechanism
    /// </summary>
    [JsonProperty("method")]
    public string Method { get; set; } = string.Empty;
}
