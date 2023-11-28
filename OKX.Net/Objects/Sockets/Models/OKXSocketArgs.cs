using OKX.Net.Converters;
using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketArgs
{
    [JsonProperty("channel")]
    public string Channel { get; set; }
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType? InstrumentType { get; set; }
    [JsonProperty("instId")]
    public string? Symbol { get; set; }
}
