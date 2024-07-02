﻿namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order amend response
/// </summary>
public record OKXAlgoOrderAmendResponse
{
    /// <summary>
    /// Order id
    /// </summary>
    [JsonPropertyName("algoId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
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
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Message
    /// </summary>
    [JsonPropertyName("sMsg")]
    public string Message { get; set; } = string.Empty;
}
