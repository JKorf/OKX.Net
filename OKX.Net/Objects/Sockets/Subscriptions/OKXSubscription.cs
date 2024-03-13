using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXSubscription<T> : Subscription<OKXSocketResponse, OKXSocketResponse>
{
    private List<OKXSocketArgs> _args;
    private Action<DataEvent<T>>? _singleHandler;
    private Action<DataEvent<IEnumerable<T>>>? _arrayHandler;

    public override HashSet<string> ListenerIdentifiers { get; set; }

    public OKXSubscription(ILogger logger, List<OKXSocketArgs> args, Action<DataEvent<T>>? singleHandler, Action<DataEvent<IEnumerable<T>>>? arrayHandler, bool authenticated) : base(logger, authenticated)
    {
        _args = args;
        _singleHandler = singleHandler;
        _arrayHandler = arrayHandler;

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

    public override Type? GetMessageType(IMessageAccessor message) => typeof(OKXSocketUpdate<IEnumerable<T>>);

    public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
    {
        var data = (OKXSocketUpdate<IEnumerable<T>>)message.Data;
        if (_singleHandler != null && data.Data.Any())
            _singleHandler.Invoke(message.As(data.Data.Single(), data.Arg.Symbol, SocketUpdateType.Update));
        else if (_arrayHandler != null)
            _arrayHandler!.Invoke(message.As(data.Data, data.Arg.Symbol, SocketUpdateType.Update));
        return new CallResult(null);
    }
}
