namespace OKX.Net.Objects.Affiliate;

/// <summary>
/// Affiliate invitee info
/// </summary>
public class OKXInviteeDetails
{
    /// <summary>
    /// Invitee level
    /// </summary>
    [JsonProperty("inviteeLv")]
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
}
