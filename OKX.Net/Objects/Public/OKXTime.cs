using CryptoExchange.Net.Converters.SystemTextJson;
namespace OKX.Net.Objects.Public;

/// <summary>
/// Server time
/// </summary>
[SerializationModel]
public record OKXTime
{
    /// <summary>
    /// System time
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
