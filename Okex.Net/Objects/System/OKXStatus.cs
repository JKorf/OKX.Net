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
    /// Begin time of system maintenance, Unix timestamp format in milliseconds, e.g. 1617788463867
    /// </summary>
    [JsonProperty("begin"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Begin { get; set; }

    /// <summary>
    /// End time of system maintenance, Unix timestamp format in milliseconds, e.g. 1617788463867
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
}
