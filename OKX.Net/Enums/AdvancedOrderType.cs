using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<AdvancedOrderType>))]
public enum AdvancedOrderType
{
    [Map("fok")]
    FillOrKill,
    [Map("ioc")]
    ImmediateOrCancel,
}
