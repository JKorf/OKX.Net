using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXDepositState
{
    [Map("0")]
    WaitingForConfirmation,
    [Map("1")]
    Credited,
    [Map("2")]
    Successful,
    [Map("8")]
    Pending,
    [Map("11")]
    AddressBlacklisted,
    [Map("12")]
    Frozen,
    [Map("13")]
    SubAccountIntercepted,
    [Map("14")]
    KycLimit
}