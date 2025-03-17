using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Check result
/// </summary>
[JsonConverter(typeof(EnumConverter<AccountSwitchCheckResult>))]
public enum AccountSwitchCheckResult
{
    /// <summary>
    /// Passed all checks
    /// </summary>
    [Map("0")]
    Passed,
    /// <summary>
    /// Unmatched information
    /// </summary>
    [Map("1")]
    UmatchedInfo,
    /// <summary>
    /// Leverage setting is not finished
    /// </summary>
    [Map("3")]
    LeverageSettingNotFinished,
    /// <summary>
    /// Position tier or margin check is not passed
    /// </summary>
    [Map("4")]
    PositionTierOrMarginNotPassed
}
