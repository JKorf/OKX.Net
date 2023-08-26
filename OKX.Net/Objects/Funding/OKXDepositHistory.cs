using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Deposit history
/// </summary>
public class OKXDepositHistory
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Network
    /// </summary>
    [JsonProperty("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// From
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// To
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// Transaction id
    /// </summary>
    [JsonProperty("txId")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Deposit id
    /// </summary>
    [JsonProperty("depId")]
    public string DepositId { get; set; } = string.Empty;

    /// <summary>
    /// Deposit state
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(EnumConverter))]
    public OKXDepositState State { get; set; }

    /// <summary>
    /// Actual amount of blockchain confirm in a single deposit
    /// </summary>
    [JsonProperty("actualDepBlkConfirm")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// If from is a phone number, this parameter return area code of the phone number
    /// </summary>
    [JsonProperty("areaCodeFrom")]
    public string? AreaCodeFrom { get; set; } = string.Empty;

    /// <summary>
    /// Internal transfer initiator's withdrawal ID
    /// </summary>
    [JsonProperty("fromWdId")]
    public string? FromWithdrawalId { get; set; } = string.Empty;
}