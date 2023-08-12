using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.System;

/// <summary>
/// Status info
/// </summary>
public class OKXStatus
{
    /// <summary>
    /// The title of system maintenance instructions
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// System maintenance status
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(MaintenanceStateConverter))]
    public OKXMaintenanceState Status { get; set; }

    /// <summary>
    /// Begin time of system maintenance
    /// </summary>
    [JsonProperty("begin"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Begin { get; set; }

    /// <summary>
    /// The time of pre_open. Canceling orders, placing Post Only orders, and transferring funds to trading accounts are back after PreOpenBegin
    /// </summary>
    [JsonProperty("preOpenBegin"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? PreOpenBegin { get; set; }

    /// <summary>
    /// End time of system maintenance
    /// </summary>
    [JsonProperty("end"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? End { get; set; }

    /// <summary>
    /// Hyperlink for system maintenance details, if there is no return value, the default value will be empty. e.g. “”
    /// </summary>
    [JsonProperty("href")]
    public string Link { get; set; } = string.Empty;

    /// <summary>
    /// Service type, 0：WebSocket ; 1：Spot/Margin ; 2：Futures ; 3：Perpetual ; 4：Options ; 5：Trading service
    /// </summary>
    [JsonProperty("serviceType"), JsonConverter(typeof(MaintenanceServiceConverter))]
    public OKXMaintenanceService Product { get; set; }

    /// <summary>
    /// Service type, 0：WebSocket ; 1：Spot/Margin ; 2：Futures ; 3：Perpetual ; 4：Options ; 5：Trading service
    /// </summary>
    [JsonProperty("system"), JsonConverter(typeof(MaintenanceSystemConverter))]
    public OKXMaintenanceSystem System { get; set; }

    /// <summary>
    /// Rescheduled description，e.g. Rescheduled from 2021-01-26T16:30:00.000Z to 2021-01-28T16:30:00.000Z
    /// </summary>
    [JsonProperty("scheDesc")]
    public string RescheduledDescription { get; set; } = string.Empty;

    /// <summary>
    /// Maintenance type
    /// </summary>
    [JsonProperty("maintType"), JsonConverter(typeof(EnumConverter))]
    public OKXMaintenanceType MaintenanceType { get; set; }

    /// <summary>
    /// Environment. 1: Production Trading, 2: Demo Trading
    /// </summary>
    [JsonProperty("env")]
    public string Environment { get; set; } = string.Empty;

    /// <summary>
    /// Push timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}
