using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Borrow/repay side
/// </summary>
[JsonConverter(typeof(EnumConverter<BorrowRepaySide>))]
public enum BorrowRepaySide
{
    /// <summary>
    /// Borrow
    /// </summary>
    [Map("borrow")]
    Borrow,
    /// <summary>
    /// Repay
    /// </summary>
    [Map("repay")]
    Repay
}
