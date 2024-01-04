using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXTransferType
{
    [Map("0")]
    TransferWithinAccount,
    [Map("1")]
    MasterAccountToSubAccount,
    [Map("2")]
    SubAccountToMasterAccount,
}