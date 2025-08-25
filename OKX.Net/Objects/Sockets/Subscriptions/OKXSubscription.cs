using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXSubscription<T> : Subscription<OKXSocketResponse, OKXSocketResponse>
{
    private readonly SocketApiClient _client;
    private List<OKXSocketArgs> _args;
    private Action<DataEvent<T>> _handler;

    public OKXSubscription(ILogger logger, SocketApiClient client, List<OKXSocketArgs> args, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
    {
        _client = client;
        _args = args;
        _handler = handler;

        MessageMatcher = MessageMatcher.Create<OKXSocketUpdate<T>>(args.Select(x => x.Channel.ToLowerInvariant() + x.InstrumentType?.ToString().ToLowerInvariant() + x.InstrumentFamily?.ToString().ToLowerInvariant() + x.Symbol?.ToLowerInvariant()), DoHandleMessage);
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

    public CallResult DoHandleMessage(SocketConnection connection, DataEvent<OKXSocketUpdate<T>> message)
    {
        _handler.Invoke(message.As(message.Data.Data, message.Data.Arg.Channel, message.Data.Arg.Symbol, message.Data.EventType == "snapshot" ? SocketUpdateType.Snapshot : SocketUpdateType.Update));
        return CallResult.SuccessResult;
    }
}
