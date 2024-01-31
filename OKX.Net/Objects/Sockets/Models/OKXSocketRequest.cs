namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketRequest
{
    [JsonProperty("op")]
    public string Op { get; set; } = string.Empty;
    [JsonProperty("args")]
    public List<OKXSocketArgs> Args { get; set; } = new List<OKXSocketArgs>();
}

internal class OKXSocketIdRequest
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
    [JsonProperty("op")]
    public string Op { get; set; } = string.Empty;
    [JsonProperty("args")]
    public IEnumerable<object> Args { get; set; } = Array.Empty<object>();
}
