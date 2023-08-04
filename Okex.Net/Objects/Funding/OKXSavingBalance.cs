namespace OKX.Net.Objects.Funding;

/// <summary>
/// Saving balance
/// </summary>
public class OKXSavingBalance
{
    /// <summary>
    /// Earnings
    /// </summary>
    [JsonProperty("earnings")]
    public decimal? Earnings { get; set; }

    /// <summary>
    /// Redemption amount
    /// </summary>
    [JsonProperty("redemptAmt")]
    public decimal? RedemptingAmount { get; set; }

    /// <summary>
    /// Lending rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? LendingRate { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Loan amount
    /// </summary>
    [JsonProperty("loanAmt")]
    public decimal? LoanAmount { get; set; }

    /// <summary>
    /// Pending amount
    /// </summary>
    [JsonProperty("pendingAmt")]
    public decimal? PendingAmount { get; set; }

}