using CryptoExchange.Net.Converters.SystemTextJson;
using OKX.Net.Converters;
namespace OKX.Net.Objects.Trading;

/// <summary>
/// Interest volume expiry
/// </summary>
[JsonConverter(typeof(ArrayConverter<OKXInterestVolumeExpiry>))]
[SerializationModel]
public record OKXInterestVolumeExpiry
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Expiry time
    /// </summary>
    [ArrayProperty(1)]
    public string ExpiryTime { get; set; } = string.Empty;

    /// <summary>
    /// Call open interest
    /// </summary>
    [ArrayProperty(2)]
    public decimal CallOpenInterest { get; set; }

    /// <summary>
    /// Put open interest
    /// </summary>
    [ArrayProperty(3)]
    public decimal PutOpenInterest { get; set; }

    /// <summary>
    /// Call volume
    /// </summary>
    [ArrayProperty(4)]
    public decimal CallVolume { get; set; }

    /// <summary>
    /// Put volume
    /// </summary>
    [ArrayProperty(5)]
    public decimal PutVolume { get; set; }
}
