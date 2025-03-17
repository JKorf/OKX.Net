using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Account;

/// <summary>
/// Maximum buy sell info
/// </summary>
[SerializationModel]
public record OKXMaximumAmount
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Maximum buy
    /// </summary>
    [JsonPropertyName("maxBuy")]
    public decimal? MaximumBuy { get; set; }

    /// <summary>
    /// Maximum sell
    /// </summary>
    [JsonPropertyName("maxSell")]
    public decimal? MaximumSell { get; set; }
}
