using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Unit conversion info
/// </summary>
public record OKXUnitConvert
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
    [JsonProperty("type"), JsonConverter(typeof(EnumConverter))]
    public ConvertType Type { get; set; }

    /// <summary>
    /// Unit
    /// </summary>
    [JsonProperty("unit"), JsonConverter(typeof(EnumConverter))]
    public ConvertUnit Unit { get; set; }
}
