namespace OKX.Net.Objects.Trading;

/// <summary>
/// Supported coins
/// </summary>
[SerializationModel]
public record OKXSupportCoins
{
    /// <summary>
    /// ["<c>contract</c>"] Contracts
    /// </summary>
    [JsonPropertyName("contract")]
    public string[] Contract { get; set; } = Array.Empty<string>();

    /// <summary>
    /// ["<c>option</c>"] Options
    /// </summary>
    [JsonPropertyName("option")]
    public string[] Option { get; set; } = Array.Empty<string>();

    /// <summary>
    /// ["<c>spot</c>"] Spot
    /// </summary>
    [JsonPropertyName("spot")]
    public string[] Spot { get; set; } = Array.Empty<string>();
}
