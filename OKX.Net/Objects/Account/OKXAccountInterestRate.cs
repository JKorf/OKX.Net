using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Account;

/// <summary>
/// Interest rate
/// </summary>
[SerializationModel]
public record OKXAccountInterestRate
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Interest rate
    /// </summary>
    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; set; }
}
