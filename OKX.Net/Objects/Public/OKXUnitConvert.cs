using OKX.Net.Enums;

namespace OKX.Net.Objects.Public;

/// <summary>
/// Unit conversion info
/// </summary>
[SerializationModel]
public record OKXUnitConvert
{
    /// <summary>
    /// ["<c>instId</c>"] Symbol
    /// </summary>
    [JsonPropertyName("instId")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>px</c>"] Price
    /// </summary>
    [JsonPropertyName("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// ["<c>sz</c>"] Quantity
    /// </summary>
    [JsonPropertyName("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public ConvertType Type { get; set; }

    /// <summary>
    /// ["<c>unit</c>"] Unit
    /// </summary>
    [JsonPropertyName("unit")]
    public ConvertUnit Unit { get; set; }
}
