using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal update
/// </summary>
public record OKXWithdrawalUpdate
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(EnumConverter))]
    public OKXWithdrawalState State { get; set; }

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
    /// Transaction id
    /// </summary>
    [JsonProperty("txId")]
    public string TransactionId { get; set; } = string.Empty;

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
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Client id
    /// </summary>
    [JsonProperty("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Area code for the phone number
    /// </summary>
    [JsonProperty("areaCodeFrom")]
    public string? AreaCodeFrom { get; set; }

    /// <summary>
    /// Area code for the phone number
    /// </summary>
    [JsonProperty("areaCodeTo")]
    public string? AreaCodeTo { get; set; }

    /// <summary>
    /// Sub account name
    /// </summary>
    [JsonProperty("subAcct")]
    public string? SubAccount { get; set; }

    /// <summary>
    /// User Identifier of the message producer
    /// </summary>
    [JsonProperty("uid")]
    public string? Uid { get; set; }

    /// <summary>
    /// Fee asset
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeAsset { get; set; } = string.Empty;

    /// <summary>
    /// Withdrawal memo
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Payment id
    /// </summary>
    [JsonProperty("pmtId")]
    public string? PaymentId { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; }

    /// <summary>
    /// Whether it is a non-tradable asset or not
    /// </summary>
    [JsonProperty("nonTradableAsset")]
    public bool? IsNonTradableAsset { get; set; }

    /// <summary>
    /// Push time
    /// </summary>
    [JsonProperty("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime PushTime { get; set; }
}