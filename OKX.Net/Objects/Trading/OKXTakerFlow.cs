using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Trading;

/// <summary>
/// Take flow
/// </summary>
[JsonConverter(typeof(ArrayConverter<OKXTakerFlow>))]
[SerializationModel]
public record OKXTakerFlow
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Call option buy volume
    /// </summary>
    [ArrayProperty(1)]
    public string CallOptionBuyVolume { get; set; } = string.Empty;

    /// <summary>
    /// Call option sell volume
    /// </summary>
    [ArrayProperty(2)]
    public string CallOptionSellVolume { get; set; } = string.Empty;

    /// <summary>
    /// Put option buy volume
    /// </summary>
    [ArrayProperty(3)]
    public string PutOptionBuyVolume { get; set; } = string.Empty;

    /// <summary>
    /// Put option sell volume
    /// </summary>
    [ArrayProperty(4)]
    public string PutOptionSellVolume { get; set; } = string.Empty;

    /// <summary>
    /// Call block volume
    /// </summary>
    [ArrayProperty(5)]
    public decimal CallBlockVolume { get; set; }

    /// <summary>
    /// Put block volume
    /// </summary>
    [ArrayProperty(6)]
    public decimal PutBlockVolume { get; set; }
}
