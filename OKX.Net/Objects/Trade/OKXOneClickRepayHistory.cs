namespace OKX.Net.Objects.Trade;

/// <summary>
/// One click repay history
/// </summary>
[SerializationModel]
public record OKXOneClickRepayHistory
{
    /// <summary>
    /// ["<c>debtCcy</c>"] Debt currency type
    /// </summary>
    [JsonPropertyName("debtCcy")]
    public string DebtAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>fillDebtSz</c>"] Amount of debt currency transacted
    /// </summary>
    [JsonPropertyName("fillDebtSz")]
    public decimal FillDebtQuantity { get; set; }

    /// <summary>
    /// ["<c>repayCcy</c>"] Repay currency type
    /// </summary>
    [JsonPropertyName("repayCcy")]
    public string RepayAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>fillRepaySz</c>"] Amount of repay currency transacted
    /// </summary>
    [JsonPropertyName("fillRepaySz")]
    public decimal FillRepayQuantity { get; set; }

    /// <summary>
    /// ["<c>status</c>"] Current status of one-click repay
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>uTime</c>"] Trade time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TradeTime { get; set; }
}