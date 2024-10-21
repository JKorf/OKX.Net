using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Enums;

/// <summary>
/// Bororw/repay type
/// </summary>
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
