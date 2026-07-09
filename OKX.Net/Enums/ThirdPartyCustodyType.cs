using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<ThirdPartyCustodyType>))]
public enum ThirdPartyCustodyType
{
    [Map("1")]
    Copper,
    [Map("2")]
    Komainu,
    [Map("3")]
    SCB,
}
