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
    [JsonProperty("isoMode"), JsonConverter(typeof(EnumConverter))]
    public OKXIsolatedMarginMode PositionMode { get; set; }
}
