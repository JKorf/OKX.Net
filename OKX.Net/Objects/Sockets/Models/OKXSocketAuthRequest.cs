namespace OKX.Net.Objects.Sockets.Models;
internal class OKXSocketAuthRequest
{
    [JsonProperty("op")]
    public string Op { get; set; } = string.Empty;
    [JsonProperty("args")]
    public List<OKXSocketAuthArgs> Args { get; set; } = new List<OKXSocketAuthArgs>();
}

internal class OKXSocketAuthArgs
{
    [JsonProperty("apiKey")]
    public string ApiKey { get; set; } = string.Empty;
    [JsonProperty("passphrase")]
    public string Passphrase { get; set; } = string.Empty;
    [JsonProperty("timestamp")]
    public string Timestamp { get; set; } = string.Empty;
    [JsonProperty("sign")]
    public string Sign { get; set; } = string.Empty;
}
