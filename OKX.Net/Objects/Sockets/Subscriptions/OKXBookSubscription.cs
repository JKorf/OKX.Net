using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXBookSubscription : Subscription<OKXSocketResponse, OKXSocketResponse>
{
    private List<OKXSocketArgs> _args;
    private Action<DataEvent<OKXOrderBook>> _handler;

    public override HashSet<string> ListenerIdentifiers { get; set; }

    public OKXBookSubscription(ILogger logger, List<OKXSocketArgs> args, Action<DataEvent<OKXOrderBook>> handler, bool authenticated) : base(logger, authenticated)
    {
        _args = args;
        _handler = handler;

        ListenerIdentifiers = new HashSet<string>(args.Select(x => x.Channel.ToLowerInvariant() + x.InstrumentType?.ToString().ToLowerInvariant() +  x.InstrumentFamily?.ToString().ToLowerInvariant() + x.Symbol?.ToLowerInvariant()));
    }

    public override Query? GetSubQuery(SocketConnection connection)
    {
        return new OKXQuery(new OKXSocketRequest
        {
            Op = "subscribe",
            Args = _args
        }, false);
    }

    public override Query? GetUnsubQuery()
    {
        return new OKXQuery(new OKXSocketRequest
        {
            Op = "unsubscribe",
            Args = _args
        }, false);
    }

    public override Type? GetMessageType(IMessageAccessor message) => typeof(OKXSocketUpdate<IEnumerable<OKXOrderBook>>);

    public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
    {
        var data = (OKXSocketUpdate<IEnumerable<OKXOrderBook>>)message.Data;
        foreach (var item in data.Data)
            item.Action = data.Action!;
        _handler.Invoke(message.As(data.Data.Single(), data.Arg.Symbol, data.Action == "snapshot" || data.Action == null ? SocketUpdateType.Snapshot: SocketUpdateType.Update));
        return new CallResult(null);
    }
}
