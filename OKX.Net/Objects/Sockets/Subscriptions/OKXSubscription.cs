using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.SocketsV2;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Sockets.Subscriptions;
internal class OKXSubscription<T, U> : Subscription<OKXSocketResponse, OKXSocketResponse> where T: OKXSocketUpdate<U>
{
    private List<OKXSocketArgs> _args;
    private Action<DataEvent<U>> _handler;

    public override List<string> StreamIdentifiers { get; set; }

    public OKXSubscription(ILogger logger, List<OKXSocketArgs> args, Action<DataEvent<U>> handler, bool authenticated) : base(logger, authenticated)
    {
        _args = args;
        _handler = handler;

        StreamIdentifiers = args.Select(x => x.Channel.ToLowerInvariant() + x.InstrumentType?.ToString().ToLowerInvariant() + x.Symbol?.ToLowerInvariant()).ToList();
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

    public override Type? GetMessageType(SocketMessage message) => typeof(OKXSocketUpdate<T>);

    public override Task<CallResult> DoHandleMessageAsync(SocketConnection connection, DataEvent<object> message)
    {
        var data = (OKXSocketUpdate<U>)message.Data;
        _handler.Invoke(message.As(data.Data, data.Arg.Symbol, SocketUpdateType.Update));
        return Task.FromResult(new CallResult(null));
    }
}
