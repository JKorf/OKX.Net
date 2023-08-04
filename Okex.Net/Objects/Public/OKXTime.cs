using OKX.Net.Converters;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Server time
/// </summary>
public class OKXTime
{
    /// <summary>
    /// System time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
