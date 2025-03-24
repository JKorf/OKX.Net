using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum InstrumentAlias
{
    [Map("this_week")]
    ThisWeek,
    [Map("next_week")]
    NextWeek,
    [Map("this_month")]
    ThisMonth,
    [Map("next_month")]
    NextMonth,
    [Map("quarter")]
    Quarter,
    [Map("next_quarter")]
    NextQuarter,
    [Map("third_quarter")]
    ThirdQuarter,
}