using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Check order unit type
/// </summary>
[JsonConverter(typeof(EnumConverter<CheckUnitType>))]
public enum CheckUnitType
{
    /// <summary>
    /// It is both base currency before and after placing order
    /// </summary>
    [Map("1")]
    BaseBoth,
    /// <summary>
    /// Before plaing order, it is base currency. after placing order, it is quota currency.
    /// </summary>
    [Map("2")]
    BaseBeforeQuoteAfter,
    /// <summary>
    /// Before plaing order, it is quota currency. after placing order, it is base currency
    /// </summary>
    [Map("3")]
    QuoteBeforeBaseAfter,
    /// <summary>
    /// It is both quota currency before and after placing order
    /// </summary>
    [Map("4")]
    QuoteBoth,
}
