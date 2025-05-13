using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Enums;

namespace OKX.Net.Objects.SubAccount;

/// <summary>
/// Subaccount info
/// </summary>
[SerializationModel]
public record OKXSubAccount
{
    /// <summary>
    /// Enabled
    /// </summary>
    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

    /// <summary>
    /// Google auth enabled
    /// </summary>
    [JsonPropertyName("gAuth")]
    public bool GoogleAuth { get; set; }

    /// <summary>
    /// Subaccount name
    /// </summary>
    [JsonPropertyName("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Label
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Mobile
    /// </summary>
    [JsonPropertyName("mobile")]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// Can transfer out
    /// </summary>
    [JsonPropertyName("canTransOut")]
    public bool CanTransOut { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonPropertyName("type")]
    public SubAccountType Type { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    /// <summary>
    /// Frozen functions
    /// </summary>
    [JsonPropertyName("frozenFunc")]
    public string[] FrozenFunctions { get; set; } = Array.Empty<string>();
}
