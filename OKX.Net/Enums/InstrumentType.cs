using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<InstrumentType>))]
public enum InstrumentType
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
