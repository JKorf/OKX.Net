using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Quick margin type
/// </summary>
[JsonConverter(typeof(EnumConverter<QuickMarginType>))]
public enum QuickMarginType
{
    /// <summary>
    /// Manual
    /// </summary>
    [Map("manual")]
    Manual,
    /// <summary>
    /// Auto borrow
    /// </summary>
    [Map("auto_borrow")]
    AutoBorrow,
    /// <summary>
    /// Auto repay
    /// </summary>
    [Map("auto_repay")]
    AutoRepay
}
