using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Interest accrued
/// </summary>
[SerializationModel]
public record OKXInterestAccrued
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>mgnMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode? MarginMode { get; set; }

    /// <summary>
    /// ["<c>interest</c>"] Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// ["<c>interestRate</c>"] Interest rate
    /// </summary>
    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// ["<c>liab</c>"] Liabilities
    /// </summary>
    [JsonPropertyName("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Loan type
    /// </summary>
    [JsonPropertyName("type")]

    public LoanType Type { get; set; }
}
