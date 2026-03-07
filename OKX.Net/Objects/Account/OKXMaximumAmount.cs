namespace OKX.Net.Objects.Account;

/// <summary>
/// Maximum buy sell info
/// </summary>
[SerializationModel]
public record OKXMaximumAmount
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>maxBuy</c>"] Maximum buy
    /// </summary>
    [JsonPropertyName("maxBuy")]
    public decimal? MaximumBuy { get; set; }

    /// <summary>
    /// ["<c>maxSell</c>"] Maximum sell
    /// </summary>
    [JsonPropertyName("maxSell")]
    public decimal? MaximumSell { get; set; }
}
