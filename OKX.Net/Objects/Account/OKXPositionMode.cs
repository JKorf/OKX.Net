using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position mode
/// </summary>
[SerializationModel]
public record OKXAccountPositionMode
{
    /// <summary>
    /// ["<c>posMode</c>"] Position mode
    /// </summary>
    [JsonPropertyName("posMode")]
    public PositionMode PositionMode { get; set; }
}
