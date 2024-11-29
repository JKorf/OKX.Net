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
            var symbolName = symbol.GetSymbol(OKXExchange.FormatSymbol);
            return Create(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(string symbol, Action<OKXOrderBookOptions>? options = null)
            => new OKXSymbolOrderBook(symbol,
                                      options,
                                      _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                      _serviceProvider.GetRequiredService<IOKXSocketClient>());
    }
}
