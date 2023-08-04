using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Unit conversion info
/// </summary>
public class OKXUnitConvert
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(ConvertTypeConverter))]
    public OKXConvertType Type { get; set; }

    /// <summary>
    /// Unit
    /// </summary>
    [JsonProperty("unit"), JsonConverter(typeof(ConvertUnitConverter))]
    public OKXConvertUnit Unit { get; set; }
}
