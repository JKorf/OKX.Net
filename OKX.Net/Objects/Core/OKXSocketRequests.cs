using OKX.Net.Converters;
using OKX.Net.Enums;

namespace OKX.Net.Objects.Core;

internal class OKXSocketRequest
{
    [JsonProperty("op"), JsonConverter(typeof(OKXSocketOperationConverter))]
    public OKXSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<OKXSocketRequestArgument> Arguments { get; set; }

    public OKXSocketRequest(OKXSocketOperation op, params OKXSocketRequestArgument[] args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OKXSocketRequest(OKXSocketOperation op, IEnumerable<OKXSocketRequestArgument> args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OKXSocketRequest(OKXSocketOperation op, string channel)
    {
        Operation = op;
        Arguments = new List<OKXSocketRequestArgument>();
        Arguments.Add(new OKXSocketRequestArgument(channel));
    }

    public OKXSocketRequest(OKXSocketOperation op, string channel, string instrumentId)
    {
        Operation = op;
        Arguments = new List<OKXSocketRequestArgument>();
        Arguments.Add(new OKXSocketRequestArgument(channel, instrumentId));
    }

    public OKXSocketRequest(OKXSocketOperation op, string channel, string underlying, string instrumentId)
    {
        Operation = op;
        Arguments = new List<OKXSocketRequestArgument>();
        Arguments.Add(new OKXSocketRequestArgument(channel, underlying, instrumentId));
    }

    public OKXSocketRequest(OKXSocketOperation op, string channel, OKXInstrumentType instrumentType)
    {
        Operation = op;
        Arguments = new List<OKXSocketRequestArgument>();
        Arguments.Add(new OKXSocketRequestArgument(channel, instrumentType));
    }

    public OKXSocketRequest(OKXSocketOperation op, string channel, OKXInstrumentType instrumentType, string underlying)
    {
        Operation = op;
        Arguments = new List<OKXSocketRequestArgument>();
        Arguments.Add(new OKXSocketRequestArgument(channel, instrumentType, underlying));
    }

}

internal class OKXSocketRequestArgument
{
    [JsonProperty("channel")]
    public string Channel { get; set; } = string.Empty;

    [JsonProperty("uly", NullValueHandling = NullValueHandling.Ignore)]
    public string Underlying { get; set; } = string.Empty;

    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentId { get; set; } = string.Empty;

    [JsonProperty("instType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(InstrumentTypeConverter))]
    public OKXInstrumentType? InstrumentType { get; set; }

    public OKXSocketRequestArgument()
    {
    }

    public OKXSocketRequestArgument(string channel)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
    }

    public OKXSocketRequestArgument(string channel, string instrumentId)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(instrumentId)) InstrumentId = instrumentId;
    }

    public OKXSocketRequestArgument(string channel, string underlying, string instrumentId)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(underlying)) Underlying = underlying;
        if (!string.IsNullOrEmpty(instrumentId)) InstrumentId = instrumentId;
    }

    public OKXSocketRequestArgument(string channel, OKXInstrumentType? instrumentType)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (instrumentType != null) InstrumentType = instrumentType;
    }

    public OKXSocketRequestArgument(string channel, OKXInstrumentType? instrumentType, string underlying)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(underlying)) Underlying = underlying;
        if (instrumentType != null) InstrumentType = instrumentType;
    }
}

internal class OKXSocketAuthRequest
{
    [JsonProperty("op"), JsonConverter(typeof(OKXSocketOperationConverter))]
    public OKXSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<OKXSocketAuthRequestArgument> Arguments { get; set; }

    public OKXSocketAuthRequest(OKXSocketOperation op, params OKXSocketAuthRequestArgument[] args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OKXSocketAuthRequest(OKXSocketOperation op, IEnumerable<OKXSocketAuthRequestArgument> args)
    {
        Operation = op;
        Arguments = args.ToList();
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
