using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel request
/// </summary>
[SerializationModel]
public record OKXOrderCancelRequest
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("ordId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? ClientOrderId { get; set; }
}
