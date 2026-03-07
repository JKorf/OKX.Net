namespace OKX.Net.Objects.Trade;

/// <summary>
/// Order info update
/// </summary>
[SerializationModel]
public record OKXOrderUpdate : OKXOrder
{
    /// <summary>
    /// ["<c>amendResult</c>"] The result of amending the order. -1: failure, 0: success, 1: Automatic cancel(due to failed amendment)
    /// </summary>
    [JsonPropertyName("amendResult")]
    public string? AmendResult { get; set; }

    /// <summary>
    /// ["<c>amendSource</c>"] Source of the order amendation. 1: Order amended by user, 2: Order amended by user, but the order quantity is overriden by system due to reduce-only, 3: New order placed by user, but the order quantity is overriden by system due to reduce-only, 4: Order amended by system due to other pending orders
    /// </summary>
    [JsonPropertyName("amendSource")]
    public string? AmendSource { get; set; }

    /// <summary>
    /// ["<c>execType</c>"] Liquidity taker or maker of the last filled, T: taker M: maker
    /// </summary>
    [JsonPropertyName("execType")]
    public string? ExecutionType { get; set; }

    /// <summary>
    /// ["<c>fillFee</c>"] Last filled fee amount or rebate amount:
    /// Negative number represents the user transaction fee charged by the platform;
    /// Positive number represents rebate
    /// </summary>
    [JsonPropertyName("fillFee")]
    public decimal FillFee { get; set; }

    /// <summary>
    /// ["<c>fillFeeCcy</c>"] Last filled fee currency or rebate currency.
    /// </summary>
    [JsonPropertyName("fillFeeCcy")]
    public string FillFeeAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>fillNotionalUsd</c>"] Filled notional value in USD of order
    /// </summary>
    [JsonPropertyName("fillNotionalUsd")]
    public decimal? FillNotionalUsd { get; set; }

    /// <summary>
    /// ["<c>fillPnl</c>"] Last filled profit and loss
    /// </summary>
    [JsonPropertyName("fillPnl")]
    public decimal FillPnl { get; set; }

    /// <summary>
    /// ["<c>notionalUsd</c>"] Estimated national value in USD of order
    /// </summary>
    [JsonPropertyName("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// ["<c>reqId</c>"] Client Request ID as assigned by the client for order amendment
    /// </summary>
    [JsonPropertyName("reqId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// ["<c>fillPxVol</c>"] Implied volitality for options
    /// </summary>
    [JsonPropertyName("fillPxVol")]
    public decimal? LastTradeImpliedVolatility { get; set; }

    /// <summary>
    /// ["<c>fillPxUsd</c>"] Usd fill price for options
    /// </summary>
    [JsonPropertyName("fillPxUsd")]
    public decimal? LastTradeUsdPrice { get; set; }

    /// <summary>
    /// ["<c>fillMarkVol</c>"] Mark volatility when filled for options
    /// </summary>
    [JsonPropertyName("fillMarkVol")]
    public decimal? LastTradeMarkVolatility { get; set; }

    /// <summary>
    /// ["<c>fillFwdPx</c>"] Forward price when filled for options
    /// </summary>
    [JsonPropertyName("fillFwdPx")]
    public decimal? LastTradeForwardPrice { get; set; }

    /// <summary>
    /// ["<c>fillMarkPx</c>"] Mark price when filled
    /// </summary>
    [JsonPropertyName("fillMarkPx")]
    public decimal? LastTradeMarkPrice { get; set; }
}
