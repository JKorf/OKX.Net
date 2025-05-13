using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Trading;

/// <summary>
/// Supported coins
/// </summary>
[SerializationModel]
public record OKXSupportCoins
{
    /// <summary>
    /// Contracts
    /// </summary>
    [JsonPropertyName("contract")]
    public string[] Contract { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Options
    /// </summary>
    [JsonPropertyName("option")]
    public string[] Option { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Spot
    /// </summary>
    [JsonPropertyName("spot")]
    public string[] Spot { get; set; } = Array.Empty<string>();
}
