using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;
using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account mode switch preset info
/// </summary>
[SerializationModel]
public record OKXPresetAccountMode
{
    /// <summary>
    /// Current account mode
    /// </summary>
    [JsonPropertyName("acctLv")]
    public AccountLevel CurrentAccountMode { get; set; }
    /// <summary>
    /// New account mode
    /// </summary>
    [JsonPropertyName("curAcctLv")]
    public AccountLevel NewAccountMode { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Risk offset type
    /// </summary>
    [JsonPropertyName("riskOffsetType")]
    public RiskOffsetType? RiskOffsetType { get; set; }
}
