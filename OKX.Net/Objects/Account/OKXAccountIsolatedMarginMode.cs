using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Isolated margin mode
/// </summary>
public record OKXAccountIsolatedMarginMode
{
    /// <summary>
    /// Isolated margin mode
    /// </summary>
    [JsonPropertyName("isoMode"), JsonConverter(typeof(EnumConverter))]
    public IsolatedMarginMode PositionMode { get; set; }
}
