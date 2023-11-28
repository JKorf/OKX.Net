using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXSubscription<T> : Subscription<OKXSocketResponse, OKXSocketUpdate<T>>
{
    private List<OKXSocketArgs> _args;
    private Action<DataEvent<T>> _handler;

    public override List<string> Identifiers { get; }

    public OKXSubscription(ILogger logger, List<OKXSocketArgs> args, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
    {
        _args = args;
        _handler = handler;

        Identifiers = args.Select(x => x.Channel.ToLowerInvariant() + x.InstrumentType?.ToString().ToLowerInvariant() + x.Symbol?.ToLowerInvariant()).ToList();
    }

    public override BaseQuery? GetSubQuery(SocketConnection connection)
    {
        return new OKXQuery(new OKXSocketRequest
        {
            Op = "subscribe",
            Args = _args
        }, false);
    }

    public override BaseQuery? GetUnsubQuery()
    {
        return new OKXQuery(new OKXSocketRequest
        {
            Op = "unsubscribe",
            Args = _args
        }, false);
    }

    public override Task<CallResult> HandleEventAsync(SocketConnection connection, DataEvent<ParsedMessage<OKXSocketUpdate<T>>> message)
    {
        _handler.Invoke(message.As(message.Data.TypedData.Data, message.Data.TypedData.Arg.Symbol, SocketUpdateType.Update));
        return Task.FromResult(new CallResult(null));
    }
}
