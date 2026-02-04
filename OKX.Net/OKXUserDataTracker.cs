using OKX.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace OKX.Net
{
    /// <inheritdoc/>
    public class OKXUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public OKXUserSpotDataTracker(
            ILogger<OKXUserSpotDataTracker> logger,
            IOKXRestClient restClient,
            IOKXSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig config) : base(
                logger,
                restClient.UnifiedApi.SharedClient,
                null,
                restClient.UnifiedApi.SharedClient,
                socketClient.UnifiedApi.SharedClient,
                restClient.UnifiedApi.SharedClient,
                socketClient.UnifiedApi.SharedClient,
                socketClient.UnifiedApi.SharedClient,
                userIdentifier,
                config)
        {
        }
    }

    /// <inheritdoc/>
    public class OKXUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public OKXUserFuturesDataTracker(
            ILogger<OKXUserFuturesDataTracker> logger,
            IOKXRestClient restClient,
            IOKXSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig config) : base(logger,
                restClient.UnifiedApi.SharedClient,
                null,
                restClient.UnifiedApi.SharedClient,
                socketClient.UnifiedApi.SharedClient,
                restClient.UnifiedApi.SharedClient,
                socketClient.UnifiedApi.SharedClient,
                socketClient.UnifiedApi.SharedClient,
                socketClient.UnifiedApi.SharedClient,
                userIdentifier,
                config)
        {
        }
    }
}
