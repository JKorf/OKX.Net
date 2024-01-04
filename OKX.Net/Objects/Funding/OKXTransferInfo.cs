using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Transfer info
/// </summary>
public class OKXTransferInfo
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Transfer id
    /// </summary>
    [JsonProperty("transId")]
    public long? TransferId { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amt")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// From account
    /// </summary>
    [JsonProperty("from"), JsonConverter(typeof(AccountConverter))]
    public OKXAccount? From { get; set; }

    /// <summary>
    /// To account
    /// </summary>
    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OKXAccount? To { get; set; }

    /// <summary>
    /// Client id
    /// </summary>
    [JsonProperty("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Type of transfer
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(EnumConverter))]
    public OKXTransferType Type { get; set; }

    /// <summary>
    /// Name of the sub account
    /// </summary>
    [JsonProperty("subAcct")]
    public string? SubAccountName { get; set; }

    /// <summary>
    /// Type of transfer
    /// </summary>
    [JsonProperty("state")]
    public OKXTransferStatus Status { get; set; }
}