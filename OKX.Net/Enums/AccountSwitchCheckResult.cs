using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Enums;

/// <summary>
/// Check result
/// </summary>
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
