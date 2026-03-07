using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Transfer response
/// </summary>
[SerializationModel]
public record OKXTransferResponse
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
}
