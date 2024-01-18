using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXQuery : Query<OKXSocketResponse>
{
    public override List<string> StreamIdentifiers { get; set;  }

    public OKXQuery(OKXSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        StreamIdentifiers = request.Args.Select(a => request.Op + a.Channel.ToLowerInvariant() + a.InstrumentType?.ToString().ToLowerInvariant() + a.Symbol?.ToLowerInvariant()).ToList();
    }

    public OKXQuery(OKXSocketAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
    {
        StreamIdentifiers = new List<string> { "login" };
    }

    public override Task<CallResult<OKXSocketResponse>> HandleMessageAsync(SocketConnection connection, DataEvent<OKXSocketResponse> message)
    {
        // TODO
        return base.HandleMessageAsync(connection, message);
    }
}
