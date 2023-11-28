using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketUpdate<T>
{
    [JsonProperty("arg")]
    public OKXSocketArgs Arg { get; set; }
    [JsonProperty("data")]
    public T Data { get; set; }
}
