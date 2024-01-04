using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Status of a transfer
/// </summary>
public enum OKXTransferStatus
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
