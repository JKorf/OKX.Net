using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position mode
/// </summary>
public record OKXAccountPositionMode
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode"), JsonConverter(typeof(EnumConverter))]
    public PositionMode PositionMode { get; set; }
}
