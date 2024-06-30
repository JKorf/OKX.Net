namespace OKX.Net.Objects.Affiliate;

/// <summary>
/// Affiliate invitee info
/// </summary>
public record OKXInviteeDetails
{
    /// <summary>
    /// Invitee level
    /// </summary>
    [JsonProperty("inviteeLevel")]
    public string InviteeLevel { get; set; } = string.Empty;
    /// <summary>
    /// Time joined
    /// </summary>
    [JsonProperty("joinTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime JoinTime { get; set; }
    /// <summary>
    /// Rebate rate
    /// </summary>
    [JsonProperty("inviteeRebateRate")]
    public decimal InviteeRebateRate { get; set; }
    /// <summary>
    /// Total commission earned
    /// </summary>
    [JsonProperty("totalCommission")]
    public decimal TotalCommission { get; set; }
    /// <summary>
    /// First trade time
    /// </summary>
    [JsonProperty("firstTradeTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FirstTradeTime { get; set; }
    /// <summary>
    /// Trading fee level
    /// </summary>
    [JsonProperty("level")]
    public string? InviteeTradeFeeLevel { get; set; }
    /// <summary>
    /// Accumulated amount of deposit in USDT
    /// </summary>
    [JsonProperty("depAmt")]
    public decimal? DepositAmount { get; set; }
    /// <summary>
    /// Accumulated Trading volume in the current month in USDT
    /// </summary>
    [JsonProperty("volMonth")]
    public decimal? MonthlyTradingVolume { get; set; }
    /// <summary>
    /// Accumulated Amount of trading fee in USDT
    /// </summary>
    [JsonProperty("accFee")]
    public decimal? TotalFee { get; set; }
    /// <summary>
    /// Timestamp of KYC completion
    /// </summary>
    [JsonProperty("kycTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? KycTime { get; set; }
    /// <summary>
    /// Region
    /// </summary>
    [JsonProperty("region")]
    public string? Region { get; set; }
    /// <summary>
    /// Affiliate code
    /// </summary>
    [JsonProperty("affiliateCode")]
    public string? AffiliateCode { get; set; }
}
