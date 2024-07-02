namespace OKX.Net.Objects.Account;

/// <summary>
/// Available amount
/// </summary>
public record OKXMaximumAvailableAmount
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Available buy
    /// </summary>
    [JsonPropertyName("availBuy")]
    public decimal? AvailableBuy { get; set; }

    /// <summary>
    /// Available sell
    /// </summary>
    [JsonPropertyName("availSell")]
    public decimal? AvailableSell { get; set; }
}
