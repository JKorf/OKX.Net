using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Self trade prevention mode
/// </summary>
[JsonConverter(typeof(EnumConverter<SelfTradePreventionMode>))]
public enum SelfTradePreventionMode
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
