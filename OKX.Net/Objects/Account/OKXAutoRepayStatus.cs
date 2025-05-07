using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Auto repay status
/// </summary>
[SerializationModel]
public record OKXAutoRepayStatus
{
    /// <summary>
    /// Auto repay enabled or not
    /// </summary>
    [JsonPropertyName("autoRepay")]
    public bool AutoRepay { get; set; }
}

