using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal response
/// </summary>
[SerializationModel]
public record OKXWithdrawalResponse
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonPropertyName("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;

    /// <summary>
    /// Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
}
