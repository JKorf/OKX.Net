using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Sub account funding balance
/// </summary>
[SerializationModel]
public record OKXSubAccountFundingBalance
{
    /// <summary>
    /// Available balance
    /// </summary>
    [JsonPropertyName("availBal")]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonPropertyName("bal")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonPropertyName("frozenBal")]
    public decimal? FrozenBalance { get; set; }
}
