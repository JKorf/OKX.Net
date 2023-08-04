namespace OKX.Net.Objects.Account;

/// <summary>
/// Maximum buy sell info
/// </summary>
public class OKXMaximumAmount
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Maximum buy
    /// </summary>
    [JsonProperty("maxBuy")]
    public decimal? MaximumBuy { get; set; }

    /// <summary>
    /// Maximum sell
    /// </summary>
    [JsonProperty("maxSell")]
    public decimal? MaximumSell { get; set; }
}
