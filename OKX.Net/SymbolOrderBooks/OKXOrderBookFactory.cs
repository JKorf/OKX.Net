using Microsoft.Extensions.DependencyInjection;
using OKX.Net.Clients;
using OKX.Net.Interfaces;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Options;

namespace OKX.Net.SymbolOrderBooks
{
    /// <inheritdoc />
    public class OKXOrderBookFactory : IOKXOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public OKXOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(string symbol, Action<OKXOrderBookOptions>? options = null)
            => new OKXSymbolOrderBook(symbol,
                                      options,
                                      _serviceProvider.GetRequiredService<ILogger<OKXSymbolOrderBook>>(),
                                      _serviceProvider.GetRequiredService<IOKXSocketClient>());
    }
}
