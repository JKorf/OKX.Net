using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[JsonConverter(typeof(EnumConverter<MaintenanceState>))]
public enum MaintenanceState
{
    [Map("scheduled")]
    Scheduled,
    [Map("ongoing")]
    Ongoing,
    [Map("completed")]
    Completed,
    [Map("canceled")]
    Canceled,
}
