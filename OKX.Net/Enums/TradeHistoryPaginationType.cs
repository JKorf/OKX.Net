﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum TradeHistoryPaginationType
{
    [Map("1")]
    TradeId,
    [Map("2")]
    Timestamp,
}