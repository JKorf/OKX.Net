﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Transfer info
/// </summary>
public record OKXTransferInfo
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Transfer id
    /// </summary>
    [JsonProperty("transId")]
    public long? TransferId { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// From account
    /// </summary>
    [JsonProperty("from"), JsonConverter(typeof(EnumConverter))]
    public AccountType? From { get; set; }

    /// <summary>
    /// To account
    /// </summary>
    [JsonProperty("to"), JsonConverter(typeof(EnumConverter))]
    public AccountType? To { get; set; }

    /// <summary>
    /// Client id
    /// </summary>
    [JsonProperty("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Type of transfer
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(EnumConverter))]
    public TransferType Type { get; set; }

    /// <summary>
    /// Name of the sub account
    /// </summary>
    [JsonProperty("subAcct")]
    public string? SubAccountName { get; set; }

    /// <summary>
    /// Type of transfer
    /// </summary>
    [JsonProperty("state")]
    public TransferStatus Status { get; set; }
}