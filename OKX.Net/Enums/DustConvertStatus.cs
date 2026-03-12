using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
/// <summary>
/// Dust convert status
/// </summary>
[JsonConverter(typeof(EnumConverter<DustConvertStatus>))]
public enum DustConvertStatus
{
    /// <summary>
    /// ["<c>running</c>"] Running
    /// </summary>
    [Map("running")]
    Running,
    /// <summary>
    /// ["<c>filled</c>"] Filled
    /// </summary>
    [Map("filled")]
    Filled,
    /// <summary>
    /// ["<c>failed</c>"] Failed
    /// </summary>
    [Map("failed")]
    Failed,
}
