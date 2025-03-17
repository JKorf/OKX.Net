using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Type of maintenance
/// </summary>
[JsonConverter(typeof(EnumConverter<MaintenanceType>))]
public enum MaintenanceType
{
    /// <summary>
    /// Scheduled
    /// </summary>
    [Map("1")]
    ScheduledMaintenance,
    /// <summary>
    /// Unscheduled
    /// </summary>
    [Map("2")]
    UnscheduledMaintenance,
    /// <summary>
    /// Disruption
    /// </summary>
    [Map("3")]
    SystemDisruption
}
