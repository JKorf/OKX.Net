using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<AccountLevel>))]
public enum AccountLevel
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
