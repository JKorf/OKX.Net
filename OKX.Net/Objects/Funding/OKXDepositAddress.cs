using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Deposit address info
/// </summary>
[SerializationModel]
public record OKXDepositAddress
{
    /// <summary>
    /// ["<c>addr</c>"] Address
    /// </summary>
    [JsonPropertyName("addr")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>tag</c>"] Tag
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>memo</c>"] Memo
    /// </summary>
    [JsonPropertyName("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// ["<c>addrEx</c>"] Deposit address attachment, e.g. TONCOIN attached tag name is comment, the return will be {'comment':'123456'}
    /// </summary>
    [JsonPropertyName("addrEx")]
    public Dictionary<string, string>? Attachment { get; set; }

    /// <summary>
    /// ["<c>pmtId</c>"] Payment id
    /// </summary>
    [JsonPropertyName("pmtId")]
    public string DepositPaymentId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>ccy</c>"] Asset
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>chain</c>"] Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>selected</c>"] Selected
    /// </summary>
    [JsonPropertyName("selected")]
    public bool Selected { get; set; }

    /// <summary>
    /// ["<c>ctAddr</c>"] Contract address
    /// </summary>
    [JsonPropertyName("ctAddr")]
    public string ContractAddr { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>to</c>"] Account
    /// </summary>
    [JsonPropertyName("to")]
    public AccountType? Account { get; set; }

    /// <summary>
    /// ["<c>verifiedName</c>"] Verified name for recipient
    /// </summary>
    [JsonPropertyName("verifiedName")]
    public string RecepientName { get; set; } = string.Empty;
}
