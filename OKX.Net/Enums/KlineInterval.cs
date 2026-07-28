using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Period
/// </summary>
[JsonConverter(typeof(EnumConverter<KlineInterval>))]
public enum KlineInterval
{
    /// <summary>
    /// ["<c>1s</c>"] 1s
    /// </summary>
    [Map("1s")]
    OneSecond = 1,

    /// <summary>
    /// ["<c>1m</c>"] 1m
    /// </summary>
    [Map("1m")]
    OneMinute = 60,

    /// <summary>
    /// ["<c>3m</c>"] 3m
    /// </summary>
    [Map("3m")]
    ThreeMinutes = 60 * 3,

    /// <summary>
    /// ["<c>5m</c>"] 5m
    /// </summary>
    [Map("5m")]
    FiveMinutes = 60 * 5,

    /// <summary>
    /// ["<c>15m</c>"] 15m
    /// </summary>
    [Map("15m")]
    FifteenMinutes = 60 * 15,

    /// <summary>
    /// ["<c>30m</c>"] 30m
    /// </summary>
    [Map("30m")]
    ThirtyMinutes = 60 * 30,

    /// <summary>
    /// ["<c>1H</c>"] 1H
    /// </summary>
    [Map("1H")]
    OneHour = 60 * 60,

    /// <summary>
    /// ["<c>2H</c>"] 2H
    /// </summary>
    [Map("2H")]
    TwoHours = 60 * 60 * 2,

    /// <summary>
    /// ["<c>4H</c>"] 4H
    /// </summary>
    [Map("4H")]
    FourHours = 60 * 60 * 4,

    /// <summary>
    /// ["<c>6H</c>"] 6H
    /// </summary>
    [Map("6H")]
    SixHours = 60 * 60 * 6,

    /// <summary>
    /// ["<c>6Hutc</c>"] 6H in UTC
    /// </summary>
    [Map("6Hutc")]
    SixHoursUtc = 60 * 60 * 6 + 1,

    /// <summary>
    /// ["<c>12H</c>"] 12H
    /// </summary>
    [Map("12H")]
    TwelveHours = 60 * 60 * 12,

    /// <summary>
    /// ["<c>12Hutc</c>"] 12H in UTC
    /// </summary>
    [Map("12Hutc")]
    TwelveHoursUtc = 60 * 60 * 12 + 1,

    /// <summary>
    /// ["<c>1D</c>"] 1D
    /// </summary>
    [Map("1D")]
    OneDay = 60 * 60 * 24,

    /// <summary>
    /// ["<c>1Dutc</c>"] 1D in UTC
    /// </summary>
    [Map("1Dutc")]
    OneDayUtc = 60 * 60 * 24 + 1,

    /// <summary>
    /// ["<c>2D</c>"] 2D
    /// </summary>
    [Map("2D")]
    TwoDays = 60 * 60 * 24 * 2,

    /// <summary>
    /// ["<c>2Dutc</c>"] 2D in UTC
    /// </summary>
    [Map("2Dutc")]
    TwoDaysUtc = 60 * 60 * 24 * 2 + 1,

    /// <summary>
    /// ["<c>3D</c>"] 3D
    /// </summary>
    [Map("3D")]
    ThreeDays = 60 * 60 * 24 * 3,

    /// <summary>
    /// ["<c>3Dutc</c>"] 3D in UTC
    /// </summary>
    [Map("3Dutc")]
    ThreeDaysUtc = 60 * 60 * 24 * 3 + 1,

    /// <summary>
    /// ["<c>1W</c>"] 1W
    /// </summary>
    [Map("1W")]
    OneWeek = 60 * 60 * 24 * 7,

    /// <summary>
    /// ["<c>1Wutc</c>"] 1W in UTC
    /// </summary>
    [Map("1Wutc")]
    OneWeekUtc = 60 * 60 * 24 * 7 + 1,

    /// <summary>
    /// ["<c>1M</c>"] 1M
    /// </summary>
    [Map("1M")]
    OneMonth = 60 * 60 * 24 * 30,

    /// <summary>
    /// ["<c>1Mutc</c>"] 1M in UTC
    /// </summary>
    [Map("1Mutc")]
    OneMonthUtc = 60 * 60 * 24 * 30 + 1,

    /// <summary>
    /// ["<c>3M</c>"] 3M
    /// </summary>
    [Map("3M")]
    ThreeMonths = 60 * 60 * 24 * 90,

    /// <summary>
    /// ["<c>3Mutc</c>"] 3M in UTC
    /// </summary>
    [Map("3Mutc")]
    ThreeMonthsUtc = 60 * 60 * 24 * 90 + 1,

    /// <summary>
    /// ["<c>6M</c>"] 6M
    /// </summary>
    [Map("6M")]
    SixMonths = 60 * 60 * 24 * 180,

    /// <summary>
    /// ["<c>1Y</c>"] 1Y
    /// </summary>
    [Map("1Y")]
    OneYear = 60 * 60 * 24 * 365,
}
