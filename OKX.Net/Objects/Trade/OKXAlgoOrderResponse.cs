using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order response
/// </summary>
[SerializationModel]
public record OKXAlgoOrderResponse
{
    /// <summary>
    /// Algo order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string? AlgoOrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    [JsonConverter(typeof(OKXClientIdConverter))]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Algo client order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    [JsonConverter(typeof(OKXClientIdConverter))]
    public string? AgloClientOrderId { get; set; }

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
    /// Whether order placement was successful
    /// </summary>
    public bool Success => Code == 0;
}
