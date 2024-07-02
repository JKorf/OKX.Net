namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Sub account transfer
/// </summary>
public record OKXSubAccountTransfer
{
    /// <summary>
    /// Transfer id
    /// </summary>
    [JsonPropertyName("transId")]
    public long? TransferId { get; set; }
}
