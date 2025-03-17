using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Deposit update
/// </summary>
[SerializationModel]
public record OKXDepositUpdate
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// From
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// To
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// Transaction id
    /// </summary>
    [JsonPropertyName("txId")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Push time
    /// </summary>
    [JsonPropertyName("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime PushTime { get; set; }

    /// <summary>
    /// Deposit id
    /// </summary>
    [JsonPropertyName("depId")]
    public string DepositId { get; set; } = string.Empty;

    /// <summary>
    /// Deposit state
    /// </summary>
    [JsonPropertyName("state")]
    public DepositState State { get; set; }

    /// <summary>
    /// Actual amount of blockchain confirm in a single deposit
    /// </summary>
    [JsonPropertyName("actualDepBlkConfirm")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// If from is a phone number, this parameter return area code of the phone number
    /// </summary>
    [JsonPropertyName("areaCodeFrom")]
    public string? AreaCodeFrom { get; set; }

    /// <summary>
    /// Internal transfer initiator's withdrawal ID
    /// </summary>
    [JsonPropertyName("fromWdId")]
    public string? FromWithdrawalId { get; set; }

    /// <summary>
    /// Sub account name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string? SubAccount { get; set; }

    /// <summary>
    /// User Identifier of the message producer
    /// </summary>
    [JsonPropertyName("uid")]
    public string? Uid { get; set; }
}
