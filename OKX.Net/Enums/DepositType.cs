using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Deposit type
/// </summary>
[JsonConverter(typeof(EnumConverter<DepositType>))]
public enum DepositType
{
    /// <summary>
    /// ["<c>3</c>"] Internal transfer
    /// </summary>
    [Map("3")]
    InternalTransfer,
    /// <summary>
    /// ["<c>4</c>"] Deposit
    /// </summary>
    [Map("4")]
    NetworkDeposit
}
