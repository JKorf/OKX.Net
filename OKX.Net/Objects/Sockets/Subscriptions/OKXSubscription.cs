using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXSubscription<T> : Subscription
{
    private readonly SocketApiClient _client;
    private List<OKXSocketArgs> _args;
    private Action<DateTime, string?, OKXSocketUpdate<T>> _handler;

    public OKXSubscription(ILogger logger, SocketApiClient client, List<OKXSocketArgs> args, Action<DateTime, string?, OKXSocketUpdate<T>> handler, bool authenticated) : base(logger, authenticated)
    {
        _client = client;
        _args = args;
        _handler = handler;

        MessageMatcher = MessageMatcher.Create<OKXSocketUpdate<T>>(args.Select(x => x.Channel.ToLowerInvariant() + x.InstrumentType?.ToString().ToLowerInvariant() + x.InstrumentFamily?.ToString().ToLowerInvariant() + x.Symbol?.ToLowerInvariant()), DoHandleMessage);
        MessageRouter = MessageRouter.CreateWithTopicFilters<OKXSocketUpdate<T>>(args.First().Channel, args.Select(x => x.InstrumentType + x.InstrumentFamily + x.Symbol), DoHandleMessage);
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

    public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, OKXSocketUpdate<T> message)
    {
        _handler.Invoke(receiveTime, originalData, message);
        //_handler.Invoke(message.As(message.Data.Data, message.Data.Arg.Channel, message.Data.Arg.Symbol, message.Data.EventType == "snapshot" ? SocketUpdateType.Snapshot : SocketUpdateType.Update));
        return CallResult.SuccessResult;
    }
}
