namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Sub account transfer
/// </summary>
public class OKXSubAccountTransfer
{
    /// <summary>
    /// Transfer id
    /// </summary>
    [JsonProperty("transId")]
    public long? TransferId { get; set; }
}
