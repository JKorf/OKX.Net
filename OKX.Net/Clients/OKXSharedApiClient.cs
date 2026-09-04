using OKX.Net.Interfaces.Clients;
using OKX.Net.Interfaces.Clients.UnifiedApi;

namespace OKX.Net.Clients
{
    /// <inheritdoc />
    public class OKXSharedApiClient : IOKXSharedApiClient
    {
        /// <inheritdoc />
        public IOKXRestClientUnifiedSharedApi Rest { get; }
        /// <inheritdoc />
        public IOKXSocketClientUnifiedSharedApi Socket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public OKXSharedApiClient(
            IOKXRestClient restClient,
            IOKXSocketClient socketClient)
        {
            Rest = restClient.UnifiedApi.SharedApi;
            Socket = socketClient.UnifiedApi.SharedApi;
        }
    }
}
