using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXQuery : Query<OKXSocketResponse>
{
    public OKXQuery(OKXSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        var ids = new List<string> { "error" };
        foreach (var arg in request.Args)
        {
            ids.Add(request.Op + arg.Channel.ToLowerInvariant() + arg.InstrumentType?.ToString().ToLowerInvariant() + arg.InstrumentFamily?.ToString().ToLowerInvariant() + arg.Symbol?.ToLowerInvariant());
            ids.Add("error" + arg.Channel.ToLowerInvariant() + arg.InstrumentType?.ToString().ToLowerInvariant() + arg.InstrumentFamily?.ToString().ToLowerInvariant() + arg.Symbol?.ToLowerInvariant());
        }

        MessageMatcher = MessageMatcher.Create<OKXSocketResponse>(ids, HandleMessage);

        RequiredResponses = request.Args.Count;
    }

    public OKXQuery(OKXSocketAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        MessageMatcher = MessageMatcher.Create<OKXSocketResponse>(["login", "error"], HandleMessage);
    }

    public CallResult<OKXSocketResponse> HandleMessage(SocketConnection connection, DataEvent<OKXSocketResponse> message)
    {
        if (string.Equals(message.Data.Event, "error", StringComparison.Ordinal))
            return new CallResult<OKXSocketResponse>(new ServerError(message.Data.Code ?? 0, message.Data.Message!), message.OriginalData);

        return new CallResult<OKXSocketResponse>(message.Data, message.OriginalData, null);
    }
}
