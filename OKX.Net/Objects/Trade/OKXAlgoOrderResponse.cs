using OKX.Net.Objects.Core;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order response
/// </summary>
[SerializationModel]
public record OKXAlgoOrderResponse : OKXOperationResponse
{
    /// <summary>
    /// ["<c>algoId</c>"] Algo order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public string? AlgoOrderId { get; set; }

    /// <summary>
    /// ["<c>clOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Algo client order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    public string? AgloClientOrderId { get; set; }

    /// <summary>
    /// Whether order placement was successful
    /// </summary>
    public bool Success => Code == SuccessCode;
}
