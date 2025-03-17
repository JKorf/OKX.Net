using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
    Borrow,
    /// <summary>
    /// Repay
    /// </summary>
    Repay
}
