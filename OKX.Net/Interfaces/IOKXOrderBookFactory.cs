using CryptoExchange.Net.SharedApis;
using OKX.Net.Objects.Options;

namespace OKX.Net.Interfaces;

/// <summary>
/// Order book factory
/// </summary>
public interface IOKXOrderBookFactory
{
    /// <summary>
    /// Unified API order book factory methods
    /// </summary>
    IOrderBookFactory<OKXOrderBookOptions> Unified { get; }

    /// <summary>
    /// Create a SymbolOrderBook for the symbol
    /// </summary>
    /// <param name="symbol">The symbol</param>
    /// <param name="options">Book options</param>
    /// <returns></returns>
    ISymbolOrderBook Create(SharedSymbol symbol, Action<OKXOrderBookOptions>? options = null);

    /// <summary>
    /// Create a new order book
    /// </summary>
    /// <param name="symbol">The symbol</param>
    /// <param name="options">Order book options</param>
    /// <returns></returns>
    ISymbolOrderBook Create(string symbol, Action<OKXOrderBookOptions>? options = null);
}