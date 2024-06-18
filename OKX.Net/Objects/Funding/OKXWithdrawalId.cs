namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal id
/// </summary>
public record OKXWithdrawalId
{
    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonProperty("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;
}