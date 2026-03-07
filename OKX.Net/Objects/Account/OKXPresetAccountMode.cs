using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Account mode switch preset info
/// </summary>
[SerializationModel]
public record OKXPresetAccountMode
{
    /// <summary>
    /// ["<c>acctLv</c>"] Current account mode
    /// </summary>
    [JsonPropertyName("acctLv")]
    public AccountLevel CurrentAccountMode { get; set; }
    /// <summary>
    /// ["<c>curAcctLv</c>"] New account mode
    /// </summary>
    [JsonPropertyName("curAcctLv")]
    public AccountLevel NewAccountMode { get; set; }

    /// <summary>
    /// ["<c>lever</c>"] Leverage
    /// </summary>
    [JsonPropertyName("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// ["<c>riskOffsetType</c>"] Risk offset type
    /// </summary>
    [JsonPropertyName("riskOffsetType")]
    public RiskOffsetType? RiskOffsetType { get; set; }
}
