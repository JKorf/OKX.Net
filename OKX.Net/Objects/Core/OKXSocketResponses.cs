using OKX.Net.Objects.Market;

namespace OKX.Net.Objects.Core;

internal class OKXSocketResponse
{
    public bool Success
    {
        get
        {
            return
                string.IsNullOrEmpty(ErrorCode)
                || ErrorCode?.Trim() == "0";
        }
    }

    [JsonProperty("event")]
    public string? Event { get; set; }

    [JsonProperty("code")]
    public string? ErrorCode { get; set; }

    [JsonProperty("msg")]
    public string? ErrorMessage { get; set; }
}

internal class OKXSocketUpdateResponse<T> : OKXSocketResponse
{
    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}

internal class OKXOrderBookUpdate
{
    [JsonProperty("action")]
    public string? Action { get; set; }

    [JsonProperty("data")]
    public IEnumerable<OKXOrderBook> Data { get; set; } = default!;
}
