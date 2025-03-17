using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Deposit address info
/// </summary>
[SerializationModel]
public record OKXDepositAddress
{
    /// <summary>
    /// Address
    /// </summary>
    [JsonPropertyName("addr")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; } = string.Empty;

    /// <summary>
    /// Memo
    /// </summary>
    [JsonPropertyName("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Deposit address attachment, e.g. TONCOIN attached tag name is comment, the return will be {'comment':'123456'}
    /// </summary>
    [JsonPropertyName("addrEx")]
    public Dictionary<string, string>? Attachment { get; set; }

    /// <summary>
    /// Payment id
    /// </summary>
    [JsonPropertyName("pmtId")]
    public string DepositPaymentId { get; set; } = string.Empty;

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
    /// Selected
    /// </summary>
    [JsonPropertyName("selected")]
    public bool Selected { get; set; }

    /// <summary>
    /// Contract address
    /// </summary>
    [JsonPropertyName("ctAddr")]
    public string ContractAddr { get; set; } = string.Empty;

    /// <summary>
    /// Account
    /// </summary>
    [JsonPropertyName("to")]
    public AccountType? Account { get; set; }

    /// <summary>
    /// Verified name for recipient
    /// </summary>
    [JsonPropertyName("verifiedName")]
    public string RecepientName { get; set; } = string.Empty;
}
