using OKX.Net.Objects.Core;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel response
/// </summary>
[SerializationModel]
public record OKXOrderCancelResponse : OKXOperationResponse
{
    /// <summary>
    /// ["<c>ordId</c>"] Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Whether order cancellation was successful
    /// </summary>
    public bool Success => Code == SuccessCode;
}
