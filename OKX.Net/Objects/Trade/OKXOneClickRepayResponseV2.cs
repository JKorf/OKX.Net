namespace OKX.Net.Objects.Trade;

/// <summary>
/// One click repay response V2
/// </summary>
[SerializationModel]
public record OKXOneClickRepayResponseV2
{
    /// <summary>
    /// ["<c>debtCcy</c>"] Debt currency
    /// </summary>
    [JsonPropertyName("debtCcy")]
    public string DebtAsset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>repayCcyList</c>"] Repay currency list
    /// </summary>
    [JsonPropertyName("repayCcyList")]
    public string[] RepayAssetList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// ["<c>ts</c>"] Request time
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? RequestTime { get; set; }
}