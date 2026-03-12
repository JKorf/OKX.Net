using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Self trade prevention mode
/// </summary>
[JsonConverter(typeof(EnumConverter<SelfTradePreventionMode>))]
public enum SelfTradePreventionMode
{
    /// <summary>
    /// ["<c>cancel_maker</c>"] Cancel maker
    /// </summary>
    [Map("cancel_maker")]
    CancelMaker,
    /// <summary>
    /// ["<c>cancel_taker</c>"] Cancel taker
    /// </summary>
    [Map("cancel_taker")]
    CancelTaker,
    /// <summary>
    /// ["<c>cancel_both</c>"] Cancel both
    /// </summary>
    [Map("cancel_both")]
    CancelBoth
}
