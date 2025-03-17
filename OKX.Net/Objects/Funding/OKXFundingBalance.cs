using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Funding;

/// <summary>
/// Funding balance
/// </summary>
[SerializationModel]
public record OKXFundingBalance
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Available balance
    /// </summary>
    [JsonPropertyName("availBal")]
    public decimal Available { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonPropertyName("bal")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonPropertyName("frozenBal")]
    public decimal Frozen { get; set; }
}
