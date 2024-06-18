namespace OKX.Net.Objects.Account;

/// <summary>
/// Available amount
/// </summary>
public record OKXMaximumAvailableAmount
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Available buy
    /// </summary>
    [JsonProperty("availBuy")]
    public decimal? AvailableBuy { get; set; }

    /// <summary>
    /// Available sell
    /// </summary>
    [JsonProperty("availSell")]
    public decimal? AvailableSell { get; set; }
}
