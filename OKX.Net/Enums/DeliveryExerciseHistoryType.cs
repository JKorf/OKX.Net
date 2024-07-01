using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum DeliveryExerciseHistoryType
{
    [Map("delivery")]
    Delivery,
    [Map("exercised")]
    Exercised,
    [Map("expired_otm")]
    ExpiredOtm,
}