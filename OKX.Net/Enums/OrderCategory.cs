using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<OrderCategory>))]
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
