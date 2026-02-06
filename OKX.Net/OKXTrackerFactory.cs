using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using OKX.Net.Clients;
using OKX.Net.Interfaces;
using OKX.Net.Interfaces.Clients;

namespace OKX.Net
{
    /// <inheritdoc />
    public class OKXTrackerFactory : IOKXTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public OKXTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public OKXTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<IOKXSocketClient>() ?? new OKXSocketClient());
            return client.UnifiedApi.SharedClient.SubscribeKlineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = (_serviceProvider?.GetRequiredService<IOKXRestClient>() ?? new OKXRestClient()).UnifiedApi.SharedClient;
            var socketClient = (_serviceProvider?.GetRequiredService<IOKXSocketClient>() ?? new OKXSocketClient()).UnifiedApi.SharedClient;

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                interval,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = (_serviceProvider?.GetRequiredService<IOKXRestClient>() ?? new OKXRestClient()).UnifiedApi.SharedClient;
            var socketClient = (_serviceProvider?.GetRequiredService<IOKXSocketClient>() ?? new OKXSocketClient()).UnifiedApi.SharedClient;

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                null,
                socketClient,
                symbol,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IOKXRestClient>() ?? new OKXRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IOKXSocketClient>() ?? new OKXSocketClient();
            return new OKXUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<OKXUserSpotDataTracker>>() ?? new NullLogger<OKXUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, OKXEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IOKXUserClientProvider>() ?? new OKXUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new OKXUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<OKXUserSpotDataTracker>>() ?? new NullLogger<OKXUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IOKXRestClient>() ?? new OKXRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IOKXSocketClient>() ?? new OKXSocketClient();
            return new OKXUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<OKXUserFuturesDataTracker>>() ?? new NullLogger<OKXUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, ApiCredentials credentials, FuturesUserDataTrackerConfig? config = null, OKXEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IOKXUserClientProvider>() ?? new OKXUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new OKXUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<OKXUserFuturesDataTracker>>() ?? new NullLogger<OKXUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
