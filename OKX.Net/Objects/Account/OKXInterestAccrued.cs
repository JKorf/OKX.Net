using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Interest accrued
/// </summary>
[SerializationModel]
public record OKXInterestAccrued
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode? MarginMode { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Interest rate
    /// </summary>
    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Liabilities
    /// </summary>
    [JsonPropertyName("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Loan type
    /// </summary>
    [JsonPropertyName("type")]

    public LoanType Type { get; set; }
}
