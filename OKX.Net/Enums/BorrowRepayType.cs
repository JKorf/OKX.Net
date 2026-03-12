using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Bororw/repay type
/// </summary>
[JsonConverter(typeof(EnumConverter<BorrowRepayType>))]
public enum BorrowRepayType
{
    /// <summary>
    /// ["<c>auto_borrow</c>"] Auto borrow
    /// </summary>
    [Map("auto_borrow")]
    AutoBorrow,
    /// <summary>
    /// ["<c>auto_repay</c>"] Auto repay
    /// </summary>
    [Map("auto_repay")]
    AutoRepay,
    /// <summary>
    /// ["<c>manual_borrow</c>"] Manual borrow
    /// </summary>
    [Map("manual_borrow")]
    ManualBorrow,
    /// <summary>
    /// ["<c>manual_repay</c>"] Manual repay
    /// </summary>
    [Map("manual_repay")]
    ManualRepay
}
