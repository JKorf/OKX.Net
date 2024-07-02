namespace OKX.Net.Objects.Trading;

/// <summary>
/// Supported coins
/// </summary>
public record OKXSupportCoins
{
    /// <summary>
    /// Contracts
    /// </summary>
    [JsonPropertyName("contract")]
    public IEnumerable<string> Contract { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Options
    /// </summary>
    [JsonPropertyName("option")]
    public IEnumerable<string> Option { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Spot
    /// </summary>
    [JsonPropertyName("spot")]
    public IEnumerable<string> Spot { get; set; } = Array.Empty<string>();
}
