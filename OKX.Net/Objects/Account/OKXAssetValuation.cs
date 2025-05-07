using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Account;

/// <summary>
/// Account asset valuation
/// </summary>
[SerializationModel]
public record OKXAssetValuation
{
    /// <summary>
    /// Valuation details
    /// </summary>
    [JsonPropertyName("details")]
    public OKXAssetValuationDetails Valuations { get; set; } = null!;
    /// <summary>
    /// Total balance
    /// </summary>
    [JsonPropertyName("totalBal")]
    public decimal TotalBalance { get; set; }
    /// <summary>
    /// Data timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// Valuation details
/// </summary>
[SerializationModel]
public record OKXAssetValuationDetails
{
    /// <summary>
    /// Earn balance
    /// </summary>
    [JsonPropertyName("earn")]
    public decimal Earn { get; set; }
    /// <summary>
    /// Funding balance
    /// </summary>
    [JsonPropertyName("funding")]
    public decimal Funding { get; set; }
    /// <summary>
    /// Trading balance
    /// </summary>
    [JsonPropertyName("trading")]
    public decimal Trading { get; set; }
}
