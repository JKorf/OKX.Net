namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order response
/// </summary>
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
    [JsonConverterCtor<ReplaceConverter>($"{OKXExchange.ClientOrderIdPrefix}->")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Algo client order id
    /// </summary>
    [JsonPropertyName("algoClOrdId")]
    [JsonConverterCtor<ReplaceConverter>($"{OKXExchange.ClientOrderIdPrefix}->")]
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
