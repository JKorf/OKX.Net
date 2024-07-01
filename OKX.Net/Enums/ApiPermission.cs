﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum ApiPermission
{
    [Map("read_only")]
    ReadOnly,
    [Map("trade")]
    Trade
}