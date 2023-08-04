using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Greeks type
/// </summary>
public class OKXAccountGreeksType
{
    /// <summary>
    /// Greeks type
    /// </summary>
    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OKXGreeksType GreeksType { get; set; }
}
