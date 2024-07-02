using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Position info
/// </summary>
public record OKXClosingPosition
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(EnumConverter))]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(EnumConverter))]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// Closing type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(EnumConverter))]
    public ClosingPositionType Type { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Last update time
    /// </summary>
    [JsonProperty("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal? OpenAveragePrice { get; set; }

    /// <summary>
    /// Average closing price
    /// </summary>
    [JsonProperty("closeAvgPx")]
    public decimal? CloseAveragePrice { get; set; }

    /// <summary>
    /// Position id
    /// </summary>
    [JsonProperty("posId")]
    public long? PositionId { get; set; }

    /// <summary>
    /// Max open position
    /// </summary>
    [JsonProperty("openMaxPos")]
    public decimal? OpenMaxPos { get; set; }

    /// <summary>
    /// Total close position
    /// </summary>
    [JsonProperty("closeTotalPos")]
    public decimal? CloseTotalPos { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    [JsonProperty("direction"), JsonConverter(typeof(EnumConverter))]
    public PositionSide Direction { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonProperty("triggerPx")]
    public decimal? TriggerMarkPrice { get; set; }

    /// <summary>
    /// Liquidation price
    /// </summary>
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string? Asset { get; set; }

    /// <summary>
    /// Profit and loss ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal? PnlRatio { get; set; }

    /// <summary>
    /// Realized profit and loss
    /// </summary>
    [JsonProperty("realizedPnl")]
    public decimal? RealizedPnl { get; set; }

    /// <summary>
    /// Accumulated fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Funding fee
    /// </summary>
    [JsonProperty("fundingFee")]
    public decimal? FundingFee { get; set; }

    /// <summary>
    /// Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonProperty("liqPenalty")]
    public decimal? LiquidationPenalty { get; set; }
}