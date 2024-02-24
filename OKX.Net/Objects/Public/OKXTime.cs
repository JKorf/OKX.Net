namespace OKX.Net.Objects.Public;

/// <summary>
/// Server time
/// </summary>
public class OKXTime
{
    /// <summary>
    /// System time
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
