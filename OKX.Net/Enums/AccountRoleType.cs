﻿using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Role type
/// </summary>
public enum AccountRoleType
{
    /// <summary>
    /// Normal user
    /// </summary>
    [Map("0")]
    GeneralUser,
    /// <summary>
    /// Leading trader
    /// </summary>
    [Map("1")]
    LeadingTrader,
    /// <summary>
    /// Copy trader
    /// </summary>
    [Map("2")]
    CopyTrader
}
