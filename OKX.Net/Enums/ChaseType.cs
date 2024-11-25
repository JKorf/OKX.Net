using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Enums;

/// <summary>
/// Chase type
/// </summary>
public enum ChaseType
{
    /// <summary>
    /// Distance from best bid/ask price. Default
    /// </summary>
    [Map("distance")]
    Distance,
    /// <summary>
    /// Ratio
    /// </summary>
    [Map("ratio")]
    Ratio
}
