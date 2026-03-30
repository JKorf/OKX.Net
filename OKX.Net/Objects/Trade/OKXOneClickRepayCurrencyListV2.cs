namespace OKX.Net.Objects.Trade;

/// <summary>
/// One click repay currency list V2
/// </summary>
[SerializationModel]
public record OKXOneClickRepayCurrencyListV2
{
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