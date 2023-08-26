using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Core;

internal class OKXSocketMessage
{
    [JsonProperty("op")]
    public string Operation { get; set; } = string.Empty;

    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("args")]
    public IEnumerable<object> Args { get; set; } = Array.Empty<object>();
}

internal class OKXSocketRequest
{
    [JsonProperty("op"), JsonConverter(typeof(OKXSocketOperationConverter))]
    public OKXSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<OKXSocketRequestArgument> Arguments { get; set; } = new List<OKXSocketRequestArgument>();

    public OKXSocketRequest(OKXSocketOperation op, OKXSocketRequestArgument argument)
    {
        Operation = op;
        Arguments.Add(argument);
    }
}

internal class OKXSocketRequestArgument
{
    [JsonProperty("channel")]
    public string Channel { get; set; } = string.Empty;

    [JsonProperty("instFamily", NullValueHandling = NullValueHandling.Ignore)]
    public string? InstrumentFamily { get; set; }

    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string? Symbol { get; set; }

    [JsonProperty("instType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType? InstrumentType { get; set; }

    [JsonProperty("ccy", NullValueHandling = NullValueHandling.Ignore)]
    public string? Asset { get; set; }

    [JsonProperty("algoId", NullValueHandling = NullValueHandling.Ignore)]
    public string? AlgoId { get; set; }

    [JsonProperty("extraParams", NullValueHandling = NullValueHandling.Ignore)]
    public string? ExtraParams { get; set; }
}

internal class OKXSocketAuthRequest
{
    [JsonProperty("op"), JsonConverter(typeof(OKXSocketOperationConverter))]
    public OKXSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<OKXSocketAuthRequestArgument> Arguments { get; set; } = new List<OKXSocketAuthRequestArgument>();

    public OKXSocketAuthRequest(OKXSocketOperation op, OKXSocketAuthRequestArgument argument)
    {
        Operation = op;
        Arguments.Add(argument);
    }
}

internal class OKXSocketAuthRequestArgument
{
    [JsonProperty("apiKey", NullValueHandling = NullValueHandling.Ignore)]
    public string? ApiKey { get; set; }

    [JsonProperty("passphrase", NullValueHandling = NullValueHandling.Ignore)]
    public string? Passphrase { get; set; }

    [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
    public string? Timestamp { get; set; }

    [JsonProperty("sign", NullValueHandling = NullValueHandling.Ignore)]
    public string? Signature { get; set; }
}

internal enum OKXSocketOperation
{
    Subscribe,
    Unsubscribe,
    Login,
}

internal class OKXSocketOperationConverter : BaseConverter<OKXSocketOperation>
{
    public OKXSocketOperationConverter() : this(true) { }
    public OKXSocketOperationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXSocketOperation, string>> Mapping => new List<KeyValuePair<OKXSocketOperation, string>>
    {
        new KeyValuePair<OKXSocketOperation, string>(OKXSocketOperation.Subscribe, "subscribe"),
        new KeyValuePair<OKXSocketOperation, string>(OKXSocketOperation.Unsubscribe, "unsubscribe"),
        new KeyValuePair<OKXSocketOperation, string>(OKXSocketOperation.Login, "login"),
    };
}
