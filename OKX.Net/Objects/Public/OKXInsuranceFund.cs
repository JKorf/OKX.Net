using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Insurance fund
/// </summary>
[SerializationModel]
public record OKXInsuranceFund
{
    /// <summary>
    /// ["<c>total</c>"] Total
    /// </summary>
    [JsonPropertyName("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// ["<c>instFamily</c>"] Instrument family
    /// </summary>
    [JsonPropertyName("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>details</c>"] Details
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
    /// ["<c>amt</c>"] Amount
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// ["<c>balance</c>"] Balance
    /// </summary>
    [JsonPropertyName("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// ["<c>maxBal</c>"] Maximum insurance fund balance in the past eight hours. Only applicable when type is adl
    /// </summary>
    [JsonPropertyName("maxBal")]
    public decimal? MaxBalance { get; set; }
    /// <summary>
    /// ["<c>maxBalTs</c>"] Timestamp when insurance fund balance reached maximum in the past eight hours. Only applicable when type is adl
    /// </summary>
    [JsonPropertyName("maxBalTs"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? MaxBalanceTime { get; set; }
    /// <summary>
    /// ["<c>decRate</c>"] Real-time insurance fund decline rate (compare balance and maxBal). Only applicable when type is adl
    /// </summary>
    [JsonPropertyName("decRate")]
    public decimal? DeclineRate { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public InsuranceType Type { get; set; }

    /// <summary>
    /// ["<c>adlType</c>"] Auto deleverage type
    /// </summary>
    [JsonPropertyName("adlType")]
    public string? AdlType { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}
