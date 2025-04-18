using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<AlgoOrderState>))]
public enum AlgoOrderState
{
    [Map("live")]
    Live,
    [Map("pause")]
    Pause,
    [Map("effective")]
    Effective,
    [Map("partially_effective")]
    PartiallyEffective,
    [Map("canceled")]
    Canceled,
    [Map("order_failed")]
    Failed,
}
