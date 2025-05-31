using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Funding rate
/// </summary>
[SerializationModel]
public record OKXFundingRate
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
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
    public decimal? FundingRate { get; set; }

    /// <summary>
    /// Next funding time
    /// </summary>
    [JsonPropertyName("nextFundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime NextFundingTime { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Next funding rate
    /// </summary>
    [JsonPropertyName("nextFundingRate")]
    public decimal? NextFundingRate { get; set; }

    /// <summary>
    /// Method
    /// </summary>
    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;

    /// <summary>
    /// Settlement state
    /// </summary>
    [JsonPropertyName("settState")]
    public string SettleState { get; set; } = string.Empty;

    /// <summary>
    /// Max funding rate
    /// </summary>
    [JsonPropertyName("maxFundingRate")]
    public decimal? MaxFundingRate { get; set; }
    /// <summary>
    /// Min funding rate
    /// </summary>
    [JsonPropertyName("minFundingRate")]
    public decimal? MinFundingRate { get; set; }
    /// <summary>
    /// Premium between the mid price of perps market and the index price
    /// </summary>
    [JsonPropertyName("premium")]
    public decimal? Premium { get; set; }
    /// <summary>
    /// If settState = processing, it is the funding rate that is being used for current settlement cycle. If settState = settled, it is the funding rate that is being used for previous settlement cycle
    /// </summary>
    [JsonPropertyName("settFundingRate")]
    public decimal? SettFundingRate { get; set; }
    /// <summary>
    /// Interest rate
    /// </summary>
    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; set; }
    /// <summary>
    /// Depth weighted amount (in the unit of quote currency)
    /// </summary>
    [JsonPropertyName("impactValue")]
    public decimal? ImpactValue { get; set; }
    /// <summary>
    /// Formula type
    /// </summary>
    [JsonPropertyName("formulaType")]
    public FundingRateFormula FormulaType { get;set; }
}
