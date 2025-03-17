using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal history
/// </summary>
[SerializationModel]
public record OKXWithdrawalHistory
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Withdrawal id
    /// </summary>
    [JsonPropertyName("wdId")]
    public long WithdrawalId { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonPropertyName("state")]
    public WithdrawalState State { get; set; }

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
    /// Transaction id
    /// </summary>
    [JsonPropertyName("txId")]
    public string TransactionId { get; set; } = string.Empty;

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
    /// Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Client id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Area code for the phone number
    /// </summary>
    [JsonPropertyName("areaCodeFrom")]
    public string? AreaCodeFrom { get; set; }

    /// <summary>
    /// Area code for the phone number
    /// </summary>
    [JsonPropertyName("areaCodeTo")]
    public string? AreaCodeTo { get; set; }
}
