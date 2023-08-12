namespace OKX.Net.Objects.Trade;

/// <summary>
/// Algo order update
/// </summary>
public class OKXAlgoOrderUpdate : OKXAlgoOrder
{
    /// <summary>
    /// The result of amending the order. -1: failure, 0: success, 1: Automatic cancel(due to failed amendment)
    /// </summary>
    [JsonProperty("amendResult")]
    public string? AmendResult { get; set; }

    /// <summary>
    /// Source of the order amendation. 1: Order amended by user, 2: Order amended by user, but the order quantity is overriden by system due to reduce-only, 3: New order placed by user, but the order quantity is overriden by system due to reduce-only, 4: Order amended by system due to other pending orders
    /// </summary>
    [JsonProperty("amendSource")]
    public string? AmendSource { get; set; }

    /// <summary>
    /// Estimated national value in USD of order
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Client Request ID as assigned by the client for order amendment
    /// </summary>
    [JsonProperty("reqId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Algo Order count, only applicable to iceberg order or twap order
    /// </summary>
    [JsonProperty("count")]
    public int? AlgoOrderCount { get; set; }
}
