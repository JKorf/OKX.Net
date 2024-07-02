﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum PositionMode
{
    [Map("long_short_mode")]
    LongShortMode,
    [Map("net_mode")]
    NetMode,
}