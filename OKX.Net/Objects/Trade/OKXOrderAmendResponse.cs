using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend response
/// </summary>
[SerializationModel]
public record OKXOrderAmendResponse
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
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Request id
    /// </summary>
    [JsonPropertyName("reqId")]
    public string RequestId { get; set; } = string.Empty;

    /// <summary>
    /// Code
    /// </summary>
    [JsonPropertyName("sCode")]
    public int Code { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [JsonPropertyName("sMsg")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Whether order edit was successful
    /// </summary>
    public bool Success => Code == 0;
}
