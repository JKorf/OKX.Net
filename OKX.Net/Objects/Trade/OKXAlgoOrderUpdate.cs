using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order update
/// </summary>
[SerializationModel]
public record OKXAlgoOrderUpdate : OKXAlgoOrder
{
    /// <summary>
    /// The result of amending the order. -1: failure, 0: success, 1: Automatic cancel(due to failed amendment)
    /// </summary>
    [JsonPropertyName("amendResult")]
    public string? AmendResult { get; set; }

    /// <summary>
    /// Source of the order amendation. 1: Order amended by user, 2: Order amended by user, but the order quantity is overriden by system due to reduce-only, 3: New order placed by user, but the order quantity is overriden by system due to reduce-only, 4: Order amended by system due to other pending orders
    /// </summary>
    [JsonPropertyName("amendSource")]
    public string? AmendSource { get; set; }

    /// <summary>
    /// Estimated national value in USD of order
    /// </summary>
    [JsonPropertyName("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Client Request ID as assigned by the client for order amendment
    /// </summary>
    [JsonPropertyName("reqId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Algo Order count, only applicable to iceberg order or twap order
    /// </summary>
    [JsonPropertyName("count")]
    public int? AlgoOrderCount { get; set; }

    /// <summary>
    /// Push time
    /// </summary>
    [JsonPropertyName("pTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? PushTime { get; set; }
}
