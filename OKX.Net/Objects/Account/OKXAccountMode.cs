using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account mode
/// </summary>
public class OKXAccountMode
{
    /// <summary>
    /// Account mode
    /// </summary>
    [JsonProperty("acctLv"), JsonConverter(typeof(EnumConverter))]
    public OKXAccountLevel Mode { get; set; }
}
