namespace OKX.Net.Objects.Affiliate;

/// <summary>
/// Affiliate invitee info
/// </summary>
[SerializationModel]
public record OKXInviteeDetails
{
    /// <summary>
    /// ["<c>inviteeLevel</c>"] Invitee level
    /// </summary>
    [JsonPropertyName("inviteeLevel")]
    public string InviteeLevel { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>joinTime</c>"] Time joined
    /// </summary>
    [JsonPropertyName("joinTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime JoinTime { get; set; }
    /// <summary>
    /// ["<c>inviteeRebateRate</c>"] Rebate rate
    /// </summary>
    [JsonPropertyName("inviteeRebateRate")]
    public decimal InviteeRebateRate { get; set; }
    /// <summary>
    /// ["<c>totalCommission</c>"] Total commission earned
    /// </summary>
    [JsonPropertyName("totalCommission")]
    public decimal TotalCommission { get; set; }
    /// <summary>
    /// ["<c>firstTradeTime</c>"] First trade time
    /// </summary>
    [JsonPropertyName("firstTradeTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FirstTradeTime { get; set; }
    /// <summary>
    /// ["<c>level</c>"] Trading fee level
    /// </summary>
    [JsonPropertyName("level")]
    public string? InviteeTradeFeeLevel { get; set; }
    /// <summary>
    /// ["<c>depAmt</c>"] Accumulated amount of deposit in USDT
    /// </summary>
    [JsonPropertyName("depAmt")]
    public decimal? DepositAmount { get; set; }
    /// <summary>
    /// ["<c>volMonth</c>"] Accumulated Trading volume in the current month in USDT
    /// </summary>
    [JsonPropertyName("volMonth")]
    public decimal? MonthlyTradingVolume { get; set; }
    /// <summary>
    /// ["<c>accFee</c>"] Accumulated Amount of trading fee in USDT
    /// </summary>
    [JsonPropertyName("accFee")]
    public decimal? TotalFee { get; set; }
    /// <summary>
    /// ["<c>kycTime</c>"] Timestamp of KYC completion
    /// </summary>
    [JsonPropertyName("kycTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? KycTime { get; set; }
    /// <summary>
    /// ["<c>region</c>"] Region
    /// </summary>
    [JsonPropertyName("region")]
    public string? Region { get; set; }
    /// <summary>
    /// ["<c>affiliateCode</c>"] Affiliate code
    /// </summary>
    [JsonPropertyName("affiliateCode")]
    public string? AffiliateCode { get; set; }
}
