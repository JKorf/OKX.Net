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
    [JsonProperty("state"), JsonConverter(typeof(DepositStateConverter))]
    public OKXDepositState State { get; set; }
}