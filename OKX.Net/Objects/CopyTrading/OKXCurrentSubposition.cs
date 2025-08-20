using OKX.Net.Enums;

namespace OKX.Net.Objects.CopyTrading;

/// <summary>
/// Lead trader current lead position
/// </summary>
[SerializationModel]
public record OKXCurrentSubposition
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonPropertyName("instId")]
    public string? Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Margin
    /// </summary>
    [JsonPropertyName("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// Latest mark price, only applicable to contract
    /// </summary>
    [JsonPropertyName("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Margin mode. cross isolated
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonPropertyName("openAvgPx")]
    public decimal? OpenAveragePrice { get; set; }

    /// <summary>
    /// Open time
    /// </summary>
    [JsonPropertyName("openTime")]
    public long? OpenTimestamp { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// Quantity of positions
    /// </summary>
    [JsonPropertyName("subPos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Lead position ID
    /// </summary>
    [JsonPropertyName("subPosId")]
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// Lead trader unique code
    /// </summary>
    [JsonPropertyName("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// Unrealized profit and loss
    /// </summary>
    [JsonPropertyName("upl")]
    public decimal UnrealizedPnl { get; set; }

    /// <summary>
    /// Unrealized profit and loss ratio
    /// </summary>
    [JsonPropertyName("uplRatio")]
    public decimal UnrealizedProfitAndLossRatio { get; set; }
}
