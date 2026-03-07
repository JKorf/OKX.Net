namespace OKX.Net.Objects.Funding;

/// <summary>
/// Saving balance
/// </summary>
[SerializationModel]
public record OKXSavingBalance
{
    /// <summary>
    /// ["<c>earnings</c>"] Earnings
    /// </summary>
    [JsonPropertyName("earnings")]
    public decimal? Earnings { get; set; }

    /// <summary>
    /// ["<c>redemptAmt</c>"] Redemption amount
    /// </summary>
    [JsonPropertyName("redemptAmt")]
    public decimal? RedemptingAmount { get; set; }

    /// <summary>
    /// ["<c>rate</c>"] Lending rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal? LendingRate { get; set; }

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// ["<c>loanAmt</c>"] Loan amount
    /// </summary>
    [JsonPropertyName("loanAmt")]
    public decimal? LoanAmount { get; set; }

    /// <summary>
    /// ["<c>pendingAmt</c>"] Pending amount
    /// </summary>
    [JsonPropertyName("pendingAmt")]
    public decimal? PendingAmount { get; set; }

}
