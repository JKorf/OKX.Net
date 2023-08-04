namespace OKX.Net.Objects.Account;

/// <summary>
/// Withdrawal info
/// </summary>
public class OKXWithdrawalAmount
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Maximum withdrawal amount
    /// </summary>
    [JsonProperty("maxWd")]
    public decimal? MaximumWithdrawal { get; set; }
}
