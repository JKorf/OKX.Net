using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<FeeRateCategory>))]
public enum FeeRateCategory
{
    [Map("1")]
    ClassA,
    [Map("2")]
    ClassB,
    [Map("3")]
    ClassC,
    [Map("4")]
    ClassD,
}
