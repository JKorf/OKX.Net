using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Isolated margin mode
/// </summary>
[SerializationModel]
public record OKXAccountIsolatedMarginMode
{
    /// <summary>
    /// ["<c>isoMode</c>"] Isolated margin mode
    /// </summary>
    [JsonPropertyName("isoMode")]
    public IsolatedMarginMode PositionMode { get; set; }
}
