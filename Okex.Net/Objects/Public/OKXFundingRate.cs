using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Funding rate
/// </summary>
public class OKXFundingRate
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
    /// Next funding time
    /// </summary>
    [JsonProperty("nextFundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime NextFundingTime { get; set; }

    /// <summary>
    /// Next funding rate
    /// </summary>
    [JsonProperty("nextFundingRate")]
    public decimal NextFundingRate { get; set; }
}
