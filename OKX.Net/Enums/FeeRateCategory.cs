﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum FeeRateCategory
{
    [Map("1")]
    ClassA,
    [Map("2")]
    ClassB,
    [Map("3")]
    ClassC,
    [Map("4")]
    ClassD,
}