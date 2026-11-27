using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using OKX.Net.Interfaces;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Options;

namespace OKX.Net.SymbolOrderBooks
{
    /// <inheritdoc />
    public class OKXOrderBookFactory : IOKXOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public string ExchangeName => OKXExchange.ExchangeName;

        /// <inheritdoc />
        public IOrderBookFactory<OKXOrderBookOptions> Unified { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public OKXOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Unified = new OrderBookFactory<OKXOrderBookOptions>(Create, Create);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<OKXOrderBookOptions>? options = null)
        {
            var client = _serviceProvider.GetRequiredService<IOKXSocketClient>();
            var symbolName = symbol.GetSymbol(client.UnifiedApi.FormatSymbol);
            return new OKXSymbolOrderBook(symbolName,
                                    options,
                                    _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                    _serviceProvider.GetRequiredService<IOKXSocketClient>());
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(string symbol, Action<OKXOrderBookOptions>? options = null)
            => new OKXSymbolOrderBook(symbol,
                                      options,
                                      _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                      _serviceProvider.GetRequiredService<IOKXSocketClient>());
    }
}
