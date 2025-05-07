using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Dust convert status
/// </summary>
[JsonConverter(typeof(EnumConverter<DustConvertStatus>))]
public enum DustConvertStatus
{
    /// <summary>
    /// Running
    /// </summary>
    [Map("running")]
    Running,
    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,
    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed,
}
