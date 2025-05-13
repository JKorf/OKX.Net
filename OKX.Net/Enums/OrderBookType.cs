using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<OrderBookType>))]
public enum OrderBookType
{
    [Map("books")]
    OrderBook,
    [Map("books5")]
    OrderBook_5,
    [Map("books50-l2-tbt")]
    OrderBook_50_l2_TBT,
    [Map("books-l2-tbt")]
    OrderBook_l2_TBT,
    [Map("bbo-tbt")]
    BBO_TBT
}
