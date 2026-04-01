namespace OKX.Net.Objects.CopyTrading;

/// <summary>
/// Leading instrument
/// </summary>
[SerializationModel]
public record OKXLeadingInstrument
{
    /// <summary>
    /// ["<c>instId</c>"] Instrument ID
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>enabled</c>"] Whether instrument is a lead instrument
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
}