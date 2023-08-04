namespace OKX.Net.Objects.Trading;

/// <summary>
/// Supported coins
/// </summary>
public class OKXSupportCoins
{
    /// <summary>
    /// Contracts
    /// </summary>
    [JsonProperty("contract")]
    public IEnumerable<string> Contract { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Options
    /// </summary>
    [JsonProperty("option")]
    public IEnumerable<string> Option { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Spot
    /// </summary>
    [JsonProperty("spot")]
    public IEnumerable<string> Spot { get; set; } = Array.Empty<string>();
}
