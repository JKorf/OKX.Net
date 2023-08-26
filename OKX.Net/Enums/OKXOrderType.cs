using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OKXOrderType
{
    [Map("market")]
    MarketOrder,
    [Map("limit")]
    LimitOrder,
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