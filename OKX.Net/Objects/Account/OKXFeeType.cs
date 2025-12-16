using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Fee type
/// </summary>
public record OKXFeeType
{
    /// <summary>
    /// Fee type
    /// </summary>
    [JsonPropertyName("feeType")]
    public FeeType FeeType { get; set; }
}
