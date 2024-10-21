using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Enums;
/// <summary>
/// Dust convert status
/// </summary>
public enum DustConvertStatus
{
    /// <summary>
    /// Running
    /// </summary>
    [Map("running")]
    Running,
    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,
    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed,
}
