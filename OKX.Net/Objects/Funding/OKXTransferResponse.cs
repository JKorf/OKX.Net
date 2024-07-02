﻿using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Transfer response
/// </summary>
public record OKXTransferResponse
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
}