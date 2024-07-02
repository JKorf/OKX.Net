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
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonPropertyName("type"), JsonConverter(typeof(EnumConverter))]
    public ConvertType Type { get; set; }

    /// <summary>
    /// Unit
    /// </summary>
    [JsonPropertyName("unit"), JsonConverter(typeof(EnumConverter))]
    public ConvertUnit Unit { get; set; }
}
