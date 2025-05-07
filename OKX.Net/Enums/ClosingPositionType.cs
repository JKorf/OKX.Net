using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<ClosingPositionType>))]
public enum ClosingPositionType
{
    [Map("1")]
    ClosePartially,
    [Map("2")]
    CloseAll,
    [Map("3")]
    Liquidation,
    [Map("4")]
    PartialLiquidation,
    [Map("5")]
    ADL
}
