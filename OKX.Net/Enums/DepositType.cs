﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Deposit type
/// </summary>
public enum DepositType
{
    /// <summary>
    /// Internal transfer
    /// </summary>
    [Map("3")]
    InternalTransfer,
    /// <summary>
    /// Deposit
    /// </summary>
    [Map("4")]
    NetworkDeposit
}
