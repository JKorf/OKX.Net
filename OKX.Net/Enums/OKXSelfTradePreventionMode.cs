using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Self trade prevention mode
/// </summary>
public enum OKXSelfTradePreventionMode
{
    /// <summary>
    /// Cancel maker
    /// </summary>
    [Map("cancel_maker")]
    CancelMaker,
    /// <summary>
    /// Cancel taker
    /// </summary>
    [Map("cancel_taker")]
    CancelTaker,
    /// <summary>
    /// Cancel both
    /// </summary>
    [Map("cancel_both")]
    CancelBoth
}
