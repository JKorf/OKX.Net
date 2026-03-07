using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Fee type
/// </summary>
public record OKXFeeType
{
    /// <summary>
    /// ["<c>feeType</c>"] Fee type
    /// </summary>
    [JsonPropertyName("feeType")]
    public FeeType FeeType { get; set; }
}
