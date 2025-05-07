using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Bororw/repay type
/// </summary>
[JsonConverter(typeof(EnumConverter<BorrowRepayType>))]
public enum BorrowRepayType
{
    /// <summary>
    /// Auto borrow
    /// </summary>
    [Map("auto_borrow")]
    AutoBorrow,
    /// <summary>
    /// Auto repay
    /// </summary>
    [Map("auto_repay")]
    AutoRepay,
    /// <summary>
    /// Manual borrow
    /// </summary>
    [Map("manual_borrow")]
    ManualBorrow,
    /// <summary>
    /// Manual repay
    /// </summary>
    [Map("manual_repay")]
    ManualRepay
}
