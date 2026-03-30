namespace OKX.Net.Objects.Trade;

/// <summary>
/// One click repay response
/// </summary>
[SerializationModel]
public record OKXOneClickRepayResponse
{
    /// <summary>
    /// ["<c>status</c>"] Status
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>debtCcy</c>"] Debt currency
    /// </summary>
    [JsonPropertyName("debtCcy")]
    public string DebtAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>repayCcy</c>"] Repay currency
    /// </summary>
    [JsonPropertyName("repayCcy")]
    public string RepayAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>fillDebtSz</c>"] Filled amount of debt currency
    /// </summary>
    [JsonPropertyName("fillDebtSz")]
    public decimal? FillDebtQuantity { get; set; }

    /// <summary>
    /// ["<c>fillRepaySz</c>"] Filled amount of repay currency
    /// </summary>
    [JsonPropertyName("fillRepaySz")]
    public decimal? FillRepayQuantity { get; set; }

    /// <summary>
    /// ["<c>uTime</c>"] Trade time
    /// </summary>
    [JsonPropertyName("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? TradeTime { get; set; }
}