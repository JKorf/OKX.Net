using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Check order unit type
/// </summary>
[JsonConverter(typeof(EnumConverter<CheckUnitType>))]
public enum CheckUnitType
{
    /// <summary>
    /// ["<c>1</c>"] It is both base currency before and after placing order
    /// </summary>
    [Map("1")]
    BaseBoth,
    /// <summary>
    /// ["<c>2</c>"] Before plaing order, it is base currency. after placing order, it is quota currency.
    /// </summary>
    [Map("2")]
    BaseBeforeQuoteAfter,
    /// <summary>
    /// ["<c>3</c>"] Before plaing order, it is quota currency. after placing order, it is base currency
    /// </summary>
    [Map("3")]
    QuoteBeforeBaseAfter,
    /// <summary>
    /// ["<c>4</c>"] It is both quota currency before and after placing order
    /// </summary>
    [Map("4")]
    QuoteBoth,
}
