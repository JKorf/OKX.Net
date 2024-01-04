using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXInstrumentType
{
    [Map("ANY")]
    Any,
    [Map("SPOT")]
    Spot,
    [Map("MARGIN")]
    Margin,
    [Map("SWAP")]
    Swap,
    [Map("FUTURES")]
    Futures,
    [Map("OPTION")]
    Option,
    [Map("CONTRACTS")]
    Contracts
}