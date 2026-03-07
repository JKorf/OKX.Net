using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Funding rate history
/// </summary>
[SerializationModel]
public record OKXFundingRateHistory
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>fundingTime</c>"] Funding time
    /// </summary>
    [JsonPropertyName("fundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FundingTime { get; set; }

    /// <summary>
    /// ["<c>fundingRate</c>"] Funding rate
    /// </summary>
    [JsonPropertyName("fundingRate")]
    public decimal FundingRate { get; set; }

    /// <summary>
    /// ["<c>realizedRate</c>"] Realized rate
    /// </summary>
    [JsonPropertyName("realizedRate")]
    public decimal RealizedRate { get; set; }

    /// <summary>
    /// ["<c>method</c>"] Funding rate mechanism
    /// </summary>
    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>formulaType</c>"] Formula type
    /// </summary>
    [JsonPropertyName("formulaType")]
    public FundingRateFormula FormulaType { get; set; }
}
