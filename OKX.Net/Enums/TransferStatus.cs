using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Status of a transfer
/// </summary>
[JsonConverter(typeof(EnumConverter<TransferStatus>))]
public enum TransferStatus
{
    /// <summary>
    /// ["<c>success</c>"] Success
    /// </summary>
    [Map("success")]
    Success,
    /// <summary>
    /// ["<c>pending</c>"] Pending transfer
    /// </summary>
    [Map("pending")]
    Pending,
    /// <summary>
    /// ["<c>failed</c>"] Transfer failed
    /// </summary>
    [Map("failed")]
    Failed
}
