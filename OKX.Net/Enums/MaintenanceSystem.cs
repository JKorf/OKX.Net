using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<MaintenanceSystem>))]
public enum MaintenanceSystem
{
    [Map("classic")]
    Classic,
    [Map("unified")]
    Unified,
}
