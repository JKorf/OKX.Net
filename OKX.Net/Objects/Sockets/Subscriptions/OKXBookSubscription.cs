using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXBookSubscription : Subscription<OKXSocketResponse, OKXSocketResponse>
{
    private readonly SocketApiClient _client;
    private List<OKXSocketArgs> _args;
    private Action<DataEvent<OKXOrderBook>> _handler;

    public OKXBookSubscription(ILogger logger, SocketApiClient client, List<OKXSocketArgs> args, Action<DataEvent<OKXOrderBook>> handler, bool authenticated) : base(logger, authenticated)
    {
        _client = client;
        _args = args;
        _handler = handler;

        MessageMatcher = MessageMatcher.Create<OKXSocketUpdate<OKXOrderBook[]>>(args.Select(x => x.Channel.ToLowerInvariant() + x.InstrumentType?.ToString().ToLowerInvariant() + x.InstrumentFamily?.ToString().ToLowerInvariant() + x.Symbol?.ToLowerInvariant()), DoHandleMessage);
    }

    protected override Query? GetSubQuery(SocketConnection connection)
    {
        return new OKXQuery(_client, new OKXSocketRequest
        {
            Op = "subscribe",
            Args = _args
        }, false);
    }

    protected override Query? GetUnsubQuery(SocketConnection connection)
    {
        return new OKXQuery(_client, new OKXSocketRequest
        {
            Op = "unsubscribe",
            Args = _args
        }, false);
    }

    public CallResult DoHandleMessage(SocketConnection connection, DataEvent<OKXSocketUpdate<OKXOrderBook[]>> message)
    {
        foreach (var item in message.Data.Data)
            item.Action = message.Data.Action!;
        _handler.Invoke(message.As(message.Data.Data.Single(), message.Data.Arg.Channel, message.Data.Arg.Symbol, string.Equals(message.Data.Action, "snapshot", StringComparison.Ordinal) || message.Data.Action == null ? SocketUpdateType.Snapshot : SocketUpdateType.Update));
        return CallResult.SuccessResult;
    }
}
