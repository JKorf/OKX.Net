using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Premium history
/// </summary>
[SerializationModel]
public record OKXPremiumHistory
{
    /// <summary>
    /// ["<c>instId</c>"] Instrument ID
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>premium</c>"] Premium
    /// </summary>
    [JsonPropertyName("premium")]
    public decimal Premium { get; set; }

    /// <summary>
    /// ["<c>ts</c>"] Data generation time
    /// </summary>
    [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
