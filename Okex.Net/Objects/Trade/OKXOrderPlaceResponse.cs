namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order placement response
/// </summary>
public class OKXOrderPlaceResponse
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
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Code
    /// </summary>
    [JsonProperty("sCode")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("sMsg")]
    public string Message { get; set; } = string.Empty;
}
