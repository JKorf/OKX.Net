using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel response
/// </summary>
[SerializationModel]
public record OKXOrderCancelResponse
{
    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    [JsonConverter(typeof(OKXClientIdConverter))]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Code
    /// </summary>
    [JsonPropertyName("sCode")]
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [JsonPropertyName("sMsg")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Whether order cancellation was successful
    /// </summary>
    public bool Success => Code == 0;
}
