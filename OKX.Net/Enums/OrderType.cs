using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<OrderType>))]
public enum OrderType
{
    [Map("market")]
    Market,
    [Map("limit")]
    Limit,
    [Map("post_only")]
    PostOnly,
    [Map("fok")]
    FillOrKill,
    [Map("ioc")]
    ImmediateOrCancel,
    [Map("optimal_limit_ioc")]
    OptimalLimitOrder,
    [Map("mmp")]
    MarketMakerProtection,
    [Map("mmp_and_post_only")]
    MarketMakerProtectionPostOnly,
}
