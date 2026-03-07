using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Funding rate
/// </summary>
[SerializationModel]
public record OKXFundingRate
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
    public decimal? FundingRate { get; set; }

    /// <summary>
    /// ["<c>nextFundingTime</c>"] Next funding time
    /// </summary>
    [JsonPropertyName("nextFundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime NextFundingTime { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// ["<c>nextFundingRate</c>"] Next funding rate
    /// </summary>
    [JsonPropertyName("nextFundingRate")]
    public decimal? NextFundingRate { get; set; }

    /// <summary>
    /// ["<c>method</c>"] Method
    /// </summary>
    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>settState</c>"] Settlement state
    /// </summary>
    [JsonPropertyName("settState")]
    public string SettleState { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>maxFundingRate</c>"] Max funding rate
    /// </summary>
    [JsonPropertyName("maxFundingRate")]
    public decimal? MaxFundingRate { get; set; }
    /// <summary>
    /// ["<c>minFundingRate</c>"] Min funding rate
    /// </summary>
    [JsonPropertyName("minFundingRate")]
    public decimal? MinFundingRate { get; set; }
    /// <summary>
    /// ["<c>premium</c>"] Premium between the mid price of perps market and the index price
    /// </summary>
    [JsonPropertyName("premium")]
    public decimal? Premium { get; set; }
    /// <summary>
    /// ["<c>settFundingRate</c>"] If settState = processing, it is the funding rate that is being used for current settlement cycle. If settState = settled, it is the funding rate that is being used for previous settlement cycle
    /// </summary>
    [JsonPropertyName("settFundingRate")]
    public decimal? SettFundingRate { get; set; }
    /// <summary>
    /// ["<c>interestRate</c>"] Interest rate
    /// </summary>
    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; set; }
    /// <summary>
    /// ["<c>impactValue</c>"] Depth weighted amount (in the unit of quote currency)
    /// </summary>
    [JsonPropertyName("impactValue")]
    public decimal? ImpactValue { get; set; }
    /// <summary>
    /// ["<c>formulaType</c>"] Formula type
    /// </summary>
    [JsonPropertyName("formulaType")]
    public FundingRateFormula FormulaType { get;set; }
}
