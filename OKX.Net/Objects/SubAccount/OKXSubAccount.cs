using OKX.Net.Enums;

namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount info
/// </summary>
[SerializationModel]
public record OKXSubAccount
{
    /// <summary>
    /// ["<c>enable</c>"] Enabled
    /// </summary>
    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

    /// <summary>
    /// ["<c>gAuth</c>"] Google auth enabled
    /// </summary>
    [JsonPropertyName("gAuth")]
    public bool GoogleAuth { get; set; }

    /// <summary>
    /// ["<c>subAcct</c>"] Subaccount name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>label</c>"] Label
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>mobile</c>"] Mobile
    /// </summary>
    [JsonPropertyName("mobile")]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>canTransOut</c>"] Can transfer out
    /// </summary>
    [JsonPropertyName("canTransOut")]
    public bool CanTransOut { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public SubAccountType Type { get; set; }

    /// <summary>
    /// ["<c>uid</c>"] Id
    /// </summary>
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>frozenFunc</c>"] Frozen functions
    /// </summary>
    [JsonPropertyName("frozenFunc")]
    public string[] FrozenFunctions { get; set; } = Array.Empty<string>();
}
