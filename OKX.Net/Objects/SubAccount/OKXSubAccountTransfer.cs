using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Sub account transfer
/// </summary>
[SerializationModel]
public record OKXSubAccountTransfer
{
    /// <summary>
    /// Transfer id
    /// </summary>
    [JsonPropertyName("transId")]
    public long? TransferId { get; set; }
}
