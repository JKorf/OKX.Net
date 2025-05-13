using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Status of a transfer
/// </summary>
[JsonConverter(typeof(EnumConverter<TransferStatus>))]
public enum TransferStatus
{
    /// <summary>
    /// Success
    /// </summary>
    [Map("success")]
    Success,
    /// <summary>
    /// Pending transfer
    /// </summary>
    [Map("pending")]
    Pending,
    /// <summary>
    /// Transfer failed
    /// </summary>
    [Map("failed")]
    Failed
}
