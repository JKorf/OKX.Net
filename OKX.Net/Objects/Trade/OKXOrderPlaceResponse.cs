using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order placement response
/// </summary>
[SerializationModel]
public record OKXOrderPlaceResponse
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
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

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
    /// Whether order placement was successful
    /// </summary>
    public bool Success => Code == 0;
}
