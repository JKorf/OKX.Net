using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Trading;

/// <summary>
/// Taker volume
/// </summary>
[JsonConverter(typeof(ArrayConverter<OKXTakerVolume>))]
[SerializationModel]
public record OKXTakerVolume
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonIgnore]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Sell volume
    /// </summary>
    [ArrayProperty(1)]
    public decimal SellVolume { get; set; }

    /// <summary>
    /// Buy volume
    /// </summary>
    [ArrayProperty(2)]
    public decimal BuyVolume { get; set; }
}
