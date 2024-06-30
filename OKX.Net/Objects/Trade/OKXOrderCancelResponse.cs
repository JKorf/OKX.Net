﻿namespace OKX.Net.Objects.Trade;

/// <summary>
/// Cancel response
/// </summary>
public record OKXOrderCancelResponse
{
    /// <summary>
    /// Order id
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Code
    /// </summary>
    [JsonProperty("sCode")]
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("sMsg")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Whether order cancellation was successful
    /// </summary>
    public bool Success => Code == 0;
}
