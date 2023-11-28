using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketResponse
{
    [JsonProperty("op")]
    public string Op { get; set; }
    [JsonProperty("arg")]
    public OKXSocketArgs Arg { get; set; }
    [JsonProperty("connId")]
    public string ConnectionId { get; set; }
}
