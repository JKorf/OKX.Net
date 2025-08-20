using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Collateral restriction status
/// </summary>
[JsonConverter(typeof(EnumConverter<CollateralRestrictionStatus>))]
public enum CollateralRestrictionStatus
{
    /// <summary>
    /// The restriction is not enabled
    /// </summary>
    [Map("0")]
    NoRestriction,
    /// <summary>
    /// The restriction is not enabled. But the crypto is close to the platform's collateral limit
    /// </summary>
    [Map("1")]
    NoRestrictionButCloseToLimit,
    /// <summary>
    /// The restriction is enabled. This crypto can't be used as margin for your new orders
    /// </summary>
    [Map("2")]
    Restricted,
}
