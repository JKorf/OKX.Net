using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Funding;

/// <summary>
/// Transfer response
/// </summary>
public record OKXTransferResponse
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
    [JsonProperty("from"), JsonConverter(typeof(EnumConverter))]
    public AccountType? From { get; set; }

    /// <summary>
    /// To account
    /// </summary>
    [JsonProperty("to"), JsonConverter(typeof(EnumConverter))]
    public AccountType? To { get; set; }

    /// <summary>
    /// Client id
    /// </summary>
    [JsonProperty("clientId")]
    public string? ClientId { get; set; }
}