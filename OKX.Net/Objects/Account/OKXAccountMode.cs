using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account mode
/// </summary>
public record OKXAccountMode
{
    /// <summary>
    /// Account mode
    /// </summary>
    [JsonPropertyName("acctLv"), JsonConverter(typeof(EnumConverter))]
    public AccountLevel Mode { get; set; }
}
