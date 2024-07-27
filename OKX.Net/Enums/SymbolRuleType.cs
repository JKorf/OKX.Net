using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Enums;
/// <summary>
/// Rule type
/// </summary>
public enum SymbolRuleType
{
    /// <summary>
    /// Normal trading
    /// </summary>
    [Map("normal")]
    Normal,
    /// <summary>
    /// Pre-market trading
    /// </summary>
    [Map("pre_market")]
    PreMarket,
}
