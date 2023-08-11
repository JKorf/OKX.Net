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

    /// <summary>
    /// Max withdrawal (including borrowed assets under Multi-currency margin/Portfolio margin)
    /// </summary>
    [JsonProperty("maxWdEx")]
    public decimal? MaximumWithdrawalIncl { get; set; }

    /// <summary>
    /// Max withdrawal under Spot-Derivatives risk offset mode (excluding borrowed assets under Portfolio margin)
    /// </summary>
    [JsonProperty("spotOffsetMaxWd")]
    public decimal? MaximumWithdrawalSpotOffset { get; set; }

    /// <summary>
    ///	Max withdrawal under Spot-Derivatives risk offset mode (including borrowed assets under Portfolio margin)
    /// </summary>
    [JsonProperty("spotOffsetMaxWdEx")]
    public decimal? MaximumWithdrawalSpotOffsetIncl { get; set; }
}
