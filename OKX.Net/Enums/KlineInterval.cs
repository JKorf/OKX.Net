using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Period
/// </summary>
[JsonConverter(typeof(EnumConverter<KlineInterval>))]
public enum KlineInterval
{
    /// <summary>
    /// 1s
    /// </summary>
    [Map("1s")]
    OneSecond = 1,

    /// <summary>
    /// 1m
    /// </summary>
    [Map("1m")]
    OneMinute = 60,

    /// <summary>
    /// 3m
    /// </summary>
    [Map("3m")]
    ThreeMinutes = 60 * 3,

    /// <summary>
    /// 5m
    /// </summary>
    [Map("5m")]
    FiveMinutes = 60 * 5,

    /// <summary>
    /// 15m
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 60 * 15,

    /// <summary>
    /// 30m
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 60 * 30,

    /// <summary>
    /// 1H
    /// </summary>
    [Map("1H")]
    OneHour = 60 * 60,

    /// <summary>
    /// 2H
    /// </summary>
    [Map("2H")]
    TwoHours = 60 * 60 * 2,

    /// <summary>
    /// 4H
    /// </summary>
    [Map("4H")]
    FourHours = 60 * 60 * 4,

    /// <summary>
    /// 6H
    /// </summary>
    [Map("6H")]
    SixHours = 60 * 60 * 6,

    /// <summary>
    /// 12H
    /// </summary>
    [Map("12H")]
    TwelveHours = 60 * 60 * 12,

    /// <summary>
    /// 1D
    /// </summary>
    [Map("1D")]
    OneDay = 60 * 60 * 24,

    /// <summary>
    /// 1W
    /// </summary>
    [Map("1W")]
    OneWeek = 60 * 60 * 24 * 7,

    /// <summary>
    /// 1M
    /// </summary>
    [Map("1M")]
    OneMonth = 60 * 60 * 24 * 30,

    /// <summary>
    /// 3M
    /// </summary>
    [Map("3M")]
    ThreeMonths = 60 * 60 * 24 * 90,

    /// <summary>
    /// 6M
    /// </summary>
    [Map("6M")]
    SixMonths = 60 * 60 * 24 * 180,

    /// <summary>
    /// 1Y
    /// </summary>
    [Map("1Y")]
    OneYear = 60 * 60 * 24 * 365,
}
