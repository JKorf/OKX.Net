using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketAuthRequest
{
    [JsonProperty("op")]
    public string Op { get; set; }
    [JsonProperty("args")]
    public List<OKXSocketAuthArgs> Args { get; set; }
}

internal class OKXSocketAuthArgs
{
    [JsonProperty("apiKey")]
    public string ApiKey { get; set; }
    [JsonProperty("passphrase")]
    public string Passphrase { get; set; }
    [JsonProperty("timestamp")]
    public string Timestamp { get; set; }
    [JsonProperty("sign")]
    public string Sign { get; set; }
}
