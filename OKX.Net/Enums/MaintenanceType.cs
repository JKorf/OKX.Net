using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums;

/// <summary>
/// Type of maintenance
/// </summary>
[JsonConverter(typeof(EnumConverter<MaintenanceType>))]
public enum MaintenanceType
{
    /// <summary>
    /// ["<c>1</c>"] Scheduled
    /// </summary>
    [Map("1")]
    ScheduledMaintenance,
    /// <summary>
    /// ["<c>2</c>"] Unscheduled
    /// </summary>
    [Map("2")]
    UnscheduledMaintenance,
    /// <summary>
    /// ["<c>3</c>"] Disruption
    /// </summary>
    [Map("3")]
    SystemDisruption
}
