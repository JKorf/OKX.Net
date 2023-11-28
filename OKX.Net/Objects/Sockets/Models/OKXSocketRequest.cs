using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketRequest
{
    [JsonProperty("op")]
    public string Op { get; set; }
    [JsonProperty("args")]
    public List<OKXSocketArgs> Args { get; set; }
}
