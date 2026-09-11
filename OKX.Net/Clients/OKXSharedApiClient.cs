using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Options;

namespace OKX.Net.Clients
{
    /// <inheritdoc />
    public class OKXSharedApiClient : SharedApiClientBase, IOKXSharedApiClient
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
            IOKXSocketClient socketClient,
            IOptions<OKXOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                restClient.UnifiedApi.SharedApi,
                socketClient.UnifiedApi.SharedApi
                )
        {
            Rest = restClient.UnifiedApi.SharedApi;
            Socket = socketClient.UnifiedApi.SharedApi;
        }
    }
}
