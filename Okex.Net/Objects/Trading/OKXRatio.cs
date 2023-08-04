using OKX.Net.Converters;

namespace OKX.Net.Objects.Trading;

/// <summary>
/// Ratio
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OKXRatio
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Ratio
    /// </summary>
    [ArrayProperty(1)]
    public decimal Ratio { get; set; }
}
