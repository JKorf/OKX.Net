using OKX.Net.Objects.Core;

namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend response
/// </summary>
[SerializationModel]
public record OKXAlgoOrderAmendResponse : OKXOperationResponse
{
    /// <summary>
    /// ["<c>algoId</c>"] Order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// ["<c>algoClOrdId</c>"] Client order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// ["<c>reqId</c>"] Request id
    /// </summary>
    [JsonPropertyName("reqId")]
    public string RequestId { get; set; } = string.Empty;
}
