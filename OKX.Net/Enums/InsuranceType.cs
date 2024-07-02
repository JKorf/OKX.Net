﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum InsuranceType
{
    [Map("all")]
    All,
    [Map("liquidation_balance_deposit")]
    LiquidationBalanceDeposit,
    [Map("bankruptcy_loss")]
    BankruptcyLoss,
    [Map("platform_revenue")]
    PlatformRevenue,
    [Map("adl")]
    AutoDeleverage,
    [Map("regular_update")]
    RegularUpdate
}