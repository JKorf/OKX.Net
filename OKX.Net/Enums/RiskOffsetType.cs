using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<RiskOffsetType>))]
public enum RiskOffsetType
{
    [Map("1")]
    SpotDerivativesUsdtOffset,
    [Map("2")]
    SpotDerivativesCryptoOffset,
    [Map("4")]
    SpotDerivativesUsdcOffset,
    [Map("3")]
    DerivativesOnlyMode
}
