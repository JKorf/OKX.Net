using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal id
/// </summary>
[SerializationModel]
public record OKXWithdrawalId
{
    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonPropertyName("wdId")]
    public string WithdrawalId { get; set; } = string.Empty;
}
