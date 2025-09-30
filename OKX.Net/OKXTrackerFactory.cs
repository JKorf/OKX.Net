using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using OKX.Net.Interfaces;
using OKX.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using OKX.Net.Clients;

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
    }
}
