using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Greeks type
/// </summary>
public record OKXAccountGreeksType
{
    /// <summary>
    /// Greeks type
    /// </summary>
    [JsonPropertyName("greeksType"), JsonConverter(typeof(EnumConverter))]
    public GreeksType GreeksType { get; set; }
}
