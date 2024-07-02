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
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Transfer id
    /// </summary>
    [JsonPropertyName("transId")]
    public long? TransferId { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// From account
    /// </summary>
    [JsonPropertyName("from"), JsonConverter(typeof(EnumConverter))]
    public AccountType? From { get; set; }

    /// <summary>
    /// To account
    /// </summary>
    [JsonPropertyName("to"), JsonConverter(typeof(EnumConverter))]
    public AccountType? To { get; set; }

    /// <summary>
    /// Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Type of transfer
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumConverter))]
    public TransferType Type { get; set; }

    /// <summary>
    /// Name of the sub account
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string? SubAccountName { get; set; }

    /// <summary>
    /// Type of transfer
    /// </summary>
    [JsonPropertyName("state")]
    public TransferStatus Status { get; set; }
}