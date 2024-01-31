using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketArgs
{
    [JsonProperty("channel")]
    public string Channel { get; set; } = string.Empty;
    [JsonProperty("instType", DefaultValueHandling = DefaultValueHandling.Ignore), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType? InstrumentType { get; set; }
    [JsonProperty("instFamily", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? InstrumentFamily { get; set; }
    [JsonProperty("instId", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Symbol { get; set; }
    [JsonProperty("ccy", NullValueHandling = NullValueHandling.Ignore)]
    public string? Asset { get; set; }

    [JsonProperty("algoId", NullValueHandling = NullValueHandling.Ignore)]
    public string? AlgoId { get; set; }

    [JsonProperty("extraParams", NullValueHandling = NullValueHandling.Ignore)]
    public string? ExtraParams { get; set; }
}
