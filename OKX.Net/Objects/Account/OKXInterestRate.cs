namespace OKX.Net.Objects.Account;

/// <summary>
/// Interest rate
/// </summary>
public class OKXInterestRate
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Interest rate
    /// </summary>
    [JsonProperty("interestRate")]
    public decimal? InterestRate { get; set; }
}
