using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum WithdrawalDestination
{
    [Map("3")]
    OKX,
    [Map("4")]
    DigitalCurrencyAddress,
}