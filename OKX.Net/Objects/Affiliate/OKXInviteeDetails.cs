using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Affiliate;

/// <summary>
/// Affiliate invitee info
/// </summary>
[SerializationModel]
public record OKXInviteeDetails
{
    /// <summary>
    /// Invitee level
    /// </summary>
    [JsonPropertyName("inviteeLevel")]
    public string InviteeLevel { get; set; } = string.Empty;
    /// <summary>
    /// Time joined
    /// </summary>
    [JsonPropertyName("joinTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime JoinTime { get; set; }
    /// <summary>
    /// Rebate rate
    /// </summary>
    [JsonPropertyName("inviteeRebateRate")]
    public decimal InviteeRebateRate { get; set; }
    /// <summary>
    /// Total commission earned
    /// </summary>
    [JsonPropertyName("totalCommission")]
    public decimal TotalCommission { get; set; }
    /// <summary>
    /// First trade time
    /// </summary>
    [JsonPropertyName("firstTradeTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FirstTradeTime { get; set; }
    /// <summary>
    /// Trading fee level
    /// </summary>
    [JsonPropertyName("level")]
    public string? InviteeTradeFeeLevel { get; set; }
    /// <summary>
    /// Accumulated amount of deposit in USDT
    /// </summary>
    [JsonPropertyName("depAmt")]
    public decimal? DepositAmount { get; set; }
    /// <summary>
    /// Accumulated Trading volume in the current month in USDT
    /// </summary>
    [JsonPropertyName("volMonth")]
    public decimal? MonthlyTradingVolume { get; set; }
    /// <summary>
    /// Accumulated Amount of trading fee in USDT
    /// </summary>
    [JsonPropertyName("accFee")]
    public decimal? TotalFee { get; set; }
    /// <summary>
    /// Timestamp of KYC completion
    /// </summary>
    [JsonPropertyName("kycTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? KycTime { get; set; }
    /// <summary>
    /// Region
    /// </summary>
    [JsonPropertyName("region")]
    public string? Region { get; set; }
    /// <summary>
    /// Affiliate code
    /// </summary>
    [JsonPropertyName("affiliateCode")]
    public string? AffiliateCode { get; set; }
}
