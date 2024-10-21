using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Enums
{
    /// <summary>
    /// Order kind
    /// </summary>
    public enum TriggerOrderKind
    {
        /// <summary>
        /// Condition
        /// </summary>
        [Map("condition")]
        Condition,
        /// <summary>
        /// Limit
        /// </summary>
        [Map("limit")]
        Limit
    }
}
