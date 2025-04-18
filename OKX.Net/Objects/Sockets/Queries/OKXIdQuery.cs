using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXIdQuery<T> : Query<OKXSocketResponse<T[]>>
{
    public override HashSet<string> ListenerIdentifiers { get; set; }

    public OKXIdQuery(string op, object[] args, bool authenticated, int weight = 1) : base(new OKXSocketIdRequest() { Id = ExchangeHelpers.NextId().ToString(), Op = op, Args = args }, authenticated, weight)
    {
        ListenerIdentifiers = new HashSet<string>() { ((OKXSocketIdRequest)Request).Id };
    }

    public override CallResult<OKXSocketResponse<T[]>> HandleMessage(SocketConnection connection, DataEvent<OKXSocketResponse<T[]>> message)
    {
        if (string.Equals(message.Data.Event, "error", StringComparison.Ordinal))
            return new CallResult<OKXSocketResponse<T[]>>(new ServerError(message.Data.Code ?? 0, message.Data.Message!), message.OriginalData);

        return base.HandleMessage(connection, message);
    }
}
