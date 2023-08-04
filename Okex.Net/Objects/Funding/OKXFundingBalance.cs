namespace OKX.Net.Objects.Funding;

/// <summary>
/// Funding balance
/// </summary>
public class OKXFundingBalance
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Available balance
    /// </summary>
    [JsonProperty("availBal")]
    public decimal Available { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("bal")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonProperty("frozenBal")]
    public decimal Frozen { get; set; }
}
