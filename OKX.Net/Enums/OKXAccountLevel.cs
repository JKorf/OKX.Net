using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXAccountLevel
{
    [Map("1")]
    Simple,
    [Map("2")]
    SingleCurrencyMargin,
    [Map("3")]
    MultiCurrencyMargin,
    [Map("4")]
    PortfolioMargin,
}