using OKX.Net.Enums;

namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketArgs
{
    [JsonPropertyName("channel")]
    public string Channel { get; set; } = string.Empty;
    [JsonPropertyName("instType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public InstrumentType? InstrumentType { get; set; }
    [JsonPropertyName("instFamily"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? InstrumentFamily { get; set; }
    [JsonPropertyName("instId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Symbol { get; set; }
    [JsonPropertyName("ccy"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Asset { get; set; }

    [JsonPropertyName("algoId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? AlgoId { get; set; }

    [JsonPropertyName("extraParams"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? ExtraParams { get; set; }
}
