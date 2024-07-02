using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum InstrumentAlias
{
    [Map("this_week")]
    ThisWeek,
    [Map("next_week")]
    NextWeek,
    [Map("quarter")]
    Quarter,
    [Map("next_quarter")]
    NextQuarter,
}