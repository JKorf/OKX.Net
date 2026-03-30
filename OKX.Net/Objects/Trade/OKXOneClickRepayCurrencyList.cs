namespace OKX.Net.Objects.Trade;

/// <summary>
/// One click repay currency list
/// </summary>
[SerializationModel]
public record OKXOneClickRepayCurrencyList
{
    /// <summary>
    /// ["<c>debtType</c>"] Debt type
    /// </summary>
    [JsonPropertyName("debtType")]
    public string DebtType { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>debtData</c>"] Debt data
    /// </summary>
    [JsonPropertyName("debtData")]
    public OKXOneClickRepayDebtData[] DebtData { get; set; } = Array.Empty<OKXOneClickRepayDebtData>();

    /// <summary>
    /// ["<c>repayData</c>"] Repay data
    /// </summary>
    [JsonPropertyName("repayData")]
    public OKXOneClickRepayRepayData[] RepayData { get; set; } = Array.Empty<OKXOneClickRepayRepayData>();
}

/// <summary>
/// Debt data
/// </summary>
[SerializationModel]
public record OKXOneClickRepayDebtData
{
    /// <summary>
    /// ["<c>debtCcy</c>"] Debt currency
    /// </summary>
    [JsonPropertyName("debtCcy")]
    public string DebtAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>debtAmt</c>"] Debt amount
    /// </summary>
    [JsonPropertyName("debtAmt")]
    public decimal DebtAmount { get; set; }
}

/// <summary>
/// Repay data
/// </summary>
[SerializationModel]
public record OKXOneClickRepayRepayData
{
    /// <summary>
    /// ["<c>repayCcy</c>"] Repay currency
    /// </summary>
    [JsonPropertyName("repayCcy")]
    public string RepayAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>repayAmt</c>"] Repay amount
    /// </summary>
    [JsonPropertyName("repayAmt")]
    public decimal RepayAmount { get; set; }
}