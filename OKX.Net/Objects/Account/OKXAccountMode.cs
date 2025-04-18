using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account mode
/// </summary>
[SerializationModel]
public record OKXAccountMode
{
    /// <summary>
    /// Account mode
    /// </summary>
    [JsonPropertyName("acctLv")]
    public AccountLevel Mode { get; set; }
}
