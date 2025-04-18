using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Insurance fund
/// </summary>
[SerializationModel]
public record OKXInsuranceFund
{
    /// <summary>
    /// Total
    /// </summary>
    [JsonPropertyName("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonPropertyName("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonPropertyName("details")]
    public OKXInsuranceFundDetail[] Details { get; set; } = Array.Empty<OKXInsuranceFundDetail>();
}

/// <summary>
/// Fund details
/// </summary>
[SerializationModel]
public record OKXInsuranceFundDetail
{
    /// <summary>
    /// Amount
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonPropertyName("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Maximum insurance fund balance in the past eight hours. Only applicable when type is adl
    /// </summary>
    [JsonPropertyName("maxBal")]
    public decimal? MaxBalance { get; set; }
    /// <summary>
    /// Timestamp when insurance fund balance reached maximum in the past eight hours. Only applicable when type is adl
    /// </summary>
    [JsonPropertyName("maxBalTs"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? MaxBalanceTime { get; set; }
    /// <summary>
    /// Real-time insurance fund decline rate (compare balance and maxBal). Only applicable when type is adl
    /// </summary>
    [JsonPropertyName("decRate")]
    public decimal? DeclineRate { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Type
    /// </summary>
    [JsonPropertyName("type")]
    public InsuranceType Type { get; set; }

    /// <summary>
    /// Auto deleverage type
    /// </summary>
    [JsonPropertyName("adlType")]
    public string? AdlType { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}
