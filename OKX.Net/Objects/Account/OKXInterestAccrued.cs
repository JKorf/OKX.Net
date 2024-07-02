using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Interest accrued
/// </summary>
public record OKXInterestAccrued
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(EnumConverter))]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Interest rate
    /// </summary>
    [JsonProperty("interestRate")]
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Liabilities
    /// </summary>
    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Loan type
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(EnumConverter))]
    public LoanType Type { get; set; }
}
