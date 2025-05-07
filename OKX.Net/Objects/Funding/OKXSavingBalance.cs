using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Funding;

/// <summary>
/// Saving balance
/// </summary>
[SerializationModel]
public record OKXSavingBalance
{
    /// <summary>
    /// Earnings
    /// </summary>
    [JsonPropertyName("earnings")]
    public decimal? Earnings { get; set; }

    /// <summary>
    /// Redemption amount
    /// </summary>
    [JsonPropertyName("redemptAmt")]
    public decimal? RedemptingAmount { get; set; }

    /// <summary>
    /// Lending rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal? LendingRate { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Loan amount
    /// </summary>
    [JsonPropertyName("loanAmt")]
    public decimal? LoanAmount { get; set; }

    /// <summary>
    /// Pending amount
    /// </summary>
    [JsonPropertyName("pendingAmt")]
    public decimal? PendingAmount { get; set; }

}
