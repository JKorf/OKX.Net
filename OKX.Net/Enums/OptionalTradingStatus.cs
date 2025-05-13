using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Status of optional trading
/// </summary>
[JsonConverter(typeof(EnumConverter<OptionalTradingStatus>))]
public enum OptionalTradingStatus
{
    /// <summary>
    /// Not activated
    /// </summary>
    [Map("0")]
    NotActivated,
    /// <summary>
    /// Activated
    /// </summary>
    [Map("1")]
    Activated
}
