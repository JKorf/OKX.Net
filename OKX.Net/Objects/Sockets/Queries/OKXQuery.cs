using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXQuery : Query<OKXSocketResponse>
{
    private readonly SocketApiClient _client;

    public OKXQuery(SocketApiClient client, OKXSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        _client = client;

        var ids = new List<string> { "error" };
        foreach (var arg in request.Args)
        {
            ids.Add(request.Op + arg.Channel.ToLowerInvariant() + arg.InstrumentType?.ToString().ToLowerInvariant() + arg.InstrumentFamily?.ToString().ToLowerInvariant() + arg.Symbol?.ToLowerInvariant());
            ids.Add("error" + arg.Channel.ToLowerInvariant() + arg.InstrumentType?.ToString().ToLowerInvariant() + arg.InstrumentFamily?.ToString().ToLowerInvariant() + arg.Symbol?.ToLowerInvariant());
        }

        MessageMatcher = MessageMatcher.Create<OKXSocketResponse>(ids, HandleMessage);
        MessageRouter = MessageRouter.CreateWithTopicFilters<OKXSocketResponse>(request.Op + request.Args.First().Channel, request.Args.Select(x => x.InstrumentType + x.InstrumentFamily + x.Symbol), HandleMessage);

        RequiredResponses = request.Args.Count;
    }

    public OKXQuery(SocketApiClient client, OKXSocketAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        _client = client;
        MessageMatcher = MessageMatcher.Create<OKXSocketResponse>(["login", "error"], HandleMessage);
        MessageRouter = MessageRouter.CreateWithoutTopicFilter<OKXSocketResponse>(["login", "error"], HandleMessage);
    }

    public CallResult<OKXSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, OKXSocketResponse message)
    {
        if (string.Equals(message.Event, "error", StringComparison.Ordinal))
            return new CallResult<OKXSocketResponse>(new ServerError(message.Code!.Value, _client.GetErrorInfo(message.Code.Value, message.Message!)), originalData);

        return new CallResult<OKXSocketResponse>(message, originalData, null);
    }
}
