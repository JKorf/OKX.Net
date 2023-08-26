using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXPositionSide
{
    [Map("long")]
    Long,
    [Map("short")]
    Short,
    [Map("net")]
    Net,
}