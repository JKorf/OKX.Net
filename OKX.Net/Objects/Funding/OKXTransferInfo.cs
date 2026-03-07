using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Transfer info
/// </summary>
[SerializationModel]
public record OKXTransferInfo
{
    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>transId</c>"] Transfer id
    /// </summary>
    [JsonPropertyName("transId")]
    public long? TransferId { get; set; }

    /// <summary>
    /// ["<c>amt</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// ["<c>from</c>"] From account
    /// </summary>
    [JsonPropertyName("from")]
    public AccountType? From { get; set; }

    /// <summary>
    /// ["<c>to</c>"] To account
    /// </summary>
    [JsonPropertyName("to")]
    public AccountType? To { get; set; }

    /// <summary>
    /// ["<c>clientId</c>"] Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Type of transfer
    /// </summary>
    [JsonPropertyName("type")]

    public TransferType Type { get; set; }

    /// <summary>
    /// ["<c>subAcct</c>"] Name of the sub account
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string? SubAccountName { get; set; }

    /// <summary>
    /// ["<c>state</c>"] Type of transfer
    /// </summary>
    [JsonPropertyName("state")]
    public TransferStatus Status { get; set; }
}
