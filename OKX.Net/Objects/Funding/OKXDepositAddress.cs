using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Deposit address info
/// </summary>
public class OKXDepositAddress
{
    /// <summary>
    /// Address
    /// </summary>
    [JsonProperty("addr")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Tag
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; } = string.Empty;

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Payment id
    /// </summary>
    [JsonProperty("pmtId")]
    public string DepositPaymentId { get; set; } = string.Empty;

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
    /// Selected
    /// </summary>
    [JsonProperty("selected")]
    public bool Selected { get; set; }

    /// <summary>
    /// Contract address
    /// </summary>
    [JsonProperty("ctAddr")]
    public string ContractAddr { get; set; } = string.Empty;

    /// <summary>
    /// Account
    /// </summary>
    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OKXAccount? Account { get; set; }
}