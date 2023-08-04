using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Withdrawal history
/// </summary>
public class OKXWithdrawalHistory
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
    [JsonProperty("state"), JsonConverter(typeof(WithdrawalStateConverter))]
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
}