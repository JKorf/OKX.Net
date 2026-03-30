using OKX.Net.Enums;

namespace OKX.Net.Objects.CopyTrading;

/// <summary>
/// Subposition history
/// </summary>
[SerializationModel]
public record OKXSubpositionHistory
{
    /// <summary>
    /// ["<c>ccy</c>"] Currency
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instId</c>"] Instrument ID
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>instType</c>"] Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// ["<c>margin</c>"] Margin
    /// </summary>
    [JsonPropertyName("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// ["<c>mgnMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("mgnMode")]
    public MarginMode MarginMode { get; set; }

    /// <summary>
    /// ["<c>openAvgPx</c>"] Average open price
    /// </summary>
    [JsonPropertyName("openAvgPx")]
    public decimal OpenAveragePrice { get; set; }

    /// <summary>
    /// ["<c>openTime</c>"] Open time
    /// </summary>
    [JsonPropertyName("openTime")]
    public long OpenTimestamp { get; set; }

    /// <summary>
    /// ["<c>posSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("posSide")]
    public PositionSide PositionSide { get; set; }

    /// <summary>
    /// ["<c>subPos</c>"] Quantity of positions
    /// </summary>
    [JsonPropertyName("subPos")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// ["<c>subPosId</c>"] Lead position ID
    /// </summary>
    [JsonPropertyName("subPosId")]
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>closeTime</c>"] Time of closing position
    /// </summary>
    [JsonPropertyName("closeTime")]
    public long CloseTimestamp { get; set; }

    /// <summary>
    /// ["<c>closeAvgPx</c>"] Average price of closing position
    /// </summary>
    [JsonPropertyName("closeAvgPx")]
    public decimal CloseAveragePrice { get; set; }

    /// <summary>
    /// ["<c>pnl</c>"] Profit and loss
    /// </summary>
    [JsonPropertyName("pnl")]
    public decimal ProfitAndLoss { get; set; }

    /// <summary>
    /// ["<c>pnlRatio</c>"] PnL ratio
    /// </summary>
    [JsonPropertyName("pnlRatio")]
    public decimal ProfitAndLossRatio { get; set; }

    /// <summary>
    /// ["<c>markPx</c>"] Latest mark price
    /// </summary>
    [JsonPropertyName("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// ["<c>uniqueCode</c>"] Lead trader unique code
    /// </summary>
    [JsonPropertyName("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>profitSharingAmt</c>"] Shared profit amount
    /// </summary>
    [JsonPropertyName("profitSharingAmt")]
    public decimal? ProfitSharingAmount { get; set; }

    /// <summary>
    /// ["<c>closeSubPos</c>"] Quantity of closed positions
    /// </summary>
    [JsonPropertyName("closeSubPos")]
    public decimal? CloseQuantity { get; set; }
}