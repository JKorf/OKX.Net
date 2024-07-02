using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Insurance fund
/// </summary>
public record OKXInsuranceFund
{
    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public IEnumerable<OKXInsuranceFundDetail> Details { get; set; } = Array.Empty<OKXInsuranceFundDetail>();
}

/// <summary>
/// Fund details
/// </summary>
public record OKXInsuranceFundDetail
{
    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Maximum insurance fund balance in the past eight hours. Only applicable when type is adl
    /// </summary>
    [JsonProperty("maxBal")]
    public decimal? MaxBalance { get; set; }
    /// <summary>
    /// Timestamp when insurance fund balance reached maximum in the past eight hours. Only applicable when type is adl
    /// </summary>
    [JsonProperty("maxBalTs"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? MaxBalanceTime { get; set; }
    /// <summary>
    /// Real-time insurance fund decline rate (compare balance and maxBal). Only applicable when type is adl
    /// </summary>
    [JsonProperty("decRate")]
    public decimal? DeclineRate { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(EnumConverter))]
    public InsuranceType Type { get; set; }

    /// <summary>
    /// Auto deleverage type
    /// </summary>
    [JsonProperty("adlType")]
    public string? AdlType { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}
