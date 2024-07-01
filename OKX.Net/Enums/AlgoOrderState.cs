﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum AlgoOrderState
{
    [Map("live")]
    Live,
    [Map("pause")]
    Pause,
    [Map("effective")]
    Effective,
    [Map("partially_effective")]
    PartiallyEffective,
    [Map("canceled")]
    Canceled,
    [Map("order_failed")]
    Failed,
}