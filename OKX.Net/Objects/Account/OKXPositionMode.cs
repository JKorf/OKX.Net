using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position mode
/// </summary>
public class OKXAccountPositionMode
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OKXPositionMode PositionMode { get; set; }
}
