using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Funding rate
/// </summary>
public record OKXFundingRate
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

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
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Next funding rate
    /// </summary>
    [JsonProperty("nextFundingRate")]
    public decimal? NextFundingRate { get; set; }

    /// <summary>
    /// Method
    /// </summary>
    [JsonProperty("method")]
    public string Method { get; set; } = string.Empty;

    /// <summary>
    /// Settlement state
    /// </summary>
    [JsonProperty("settState")]
    public string SettleState { get; set; } = string.Empty;

    /// <summary>
    /// Max funding rate
    /// </summary>
    [JsonProperty("maxFundingRate")]
    public decimal? MaxFundingRate { get; set; }
    /// <summary>
    /// Min funding rate
    /// </summary>
    [JsonProperty("minFundingRate")]
    public decimal? MinFundingRate { get; set; }
    /// <summary>
    /// Premium between the mid price of perps market and the index price
    /// </summary>
    [JsonProperty("premium")]
    public decimal? Premium { get; set; }
    /// <summary>
    /// If settState = processing, it is the funding rate that is being used for current settlement cycle. If settState = settled, it is the funding rate that is being used for previous settlement cycle
    /// </summary>
    [JsonProperty("settFundingRate")]
    public decimal? SettFundingRate { get; set; }
}
