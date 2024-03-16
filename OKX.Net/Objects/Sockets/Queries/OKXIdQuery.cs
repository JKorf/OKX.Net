using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using OKX.Net.Objects.Sockets.Models;

namespace OKX.Net.Objects.Sockets.Queries;
internal class OKXIdQuery<T> : Query<OKXSocketResponse<IEnumerable<T>>>
{
    public override HashSet<string> ListenerIdentifiers { get; set; }

    public OKXIdQuery(string op, IEnumerable<object> args, bool authenticated, int weight = 1) : base(new OKXSocketIdRequest() { Id = ExchangeHelpers.NextId().ToString(), Op = op, Args = args }, authenticated, weight)
    {
        ListenerIdentifiers = new HashSet<string>() { ((OKXSocketIdRequest)Request).Id };
    }

    public override CallResult<OKXSocketResponse<IEnumerable<T>>> HandleMessage(SocketConnection connection, DataEvent<OKXSocketResponse<IEnumerable<T>>> message)
    {
        if (message.Data.Event == "error")
            return new CallResult<OKXSocketResponse<IEnumerable<T>>>(new ServerError(message.Data.Code ?? 0, message.Data.Message!), message.OriginalData);

        return base.HandleMessage(connection, message);
    }
}
