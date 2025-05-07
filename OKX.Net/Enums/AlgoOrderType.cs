using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<AlgoOrderType>))]
public enum AlgoOrderType
{
    [Map("conditional")]
    Conditional,
    [Map("oco")]
    OCO,
    [Map("trigger")]
    Trigger,
    [Map("move_order_stop")]
    TrailingOrder,
    [Map("iceberg")]
    Iceberg,
    [Map("twap")]
    TWAP,
    [Map("chase")]
    Chase
}
