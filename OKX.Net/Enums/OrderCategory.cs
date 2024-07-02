using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum OrderCategory
{
    [Map("twap")]
    TWAP,
    [Map("adl")]
    ADL,
    [Map("full_liquidation")]
    FullLiquidation,
    [Map("partial_liquidation")]
    PartialLiquidation,
    [Map("delivery")]
    Delivery,
}