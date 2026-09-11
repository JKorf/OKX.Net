using CryptoExchange.Net.SharedApis;
using OKX.Net.Interfaces.Clients.UnifiedApi;

namespace OKX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of OKX
    /// </summary>
    public interface IOKXSharedApiClient : ISharedApiClientBase
    {
        /// <summary>
        /// REST shared API implementations
        /// </summary>
        IOKXRestClientUnifiedSharedApi Rest { get; }

        /// <summary>
        /// WebSocket shared API implementations
        /// </summary>
        IOKXSocketClientUnifiedSharedApi Socket { get; }
    }
}
