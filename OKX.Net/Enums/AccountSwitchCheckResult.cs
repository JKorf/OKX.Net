using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Check result
/// </summary>
[JsonConverter(typeof(EnumConverter<AccountSwitchCheckResult>))]
public enum AccountSwitchCheckResult
{
    /// <summary>
    /// ["<c>0</c>"] Passed all checks
    /// </summary>
    [Map("0")]
    Passed,
    /// <summary>
    /// ["<c>1</c>"] Unmatched information
    /// </summary>
    [Map("1")]
    UmatchedInfo,
    /// <summary>
    /// ["<c>3</c>"] Leverage setting is not finished
    /// </summary>
    [Map("3")]
    LeverageSettingNotFinished,
    /// <summary>
    /// ["<c>4</c>"] Position tier or margin check is not passed
    /// </summary>
    [Map("4")]
    PositionTierOrMarginNotPassed
}
