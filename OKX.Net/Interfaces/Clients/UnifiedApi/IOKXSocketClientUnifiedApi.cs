using CryptoExchange.Net.Interfaces.Clients;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API
/// </summary>
public interface IOKXSocketClientUnifiedApi : ISocketApiClient<OKXCredentials>
{
    /// <summary>
    /// Get the shared socket subscription client. For new implementations prefer <see cref="SharedApi"/>
    /// </summary>
    IOKXSocketClientUnifiedApiShared SharedClient { get; }
    /// <summary>
    /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    IOKXSocketClientUnifiedSharedApi SharedApi { get; }

    /// <summary>
    /// Account streams and queries
    /// </summary>
    /// <see cref="IOKXSocketClientUnifiedApiAccount"/>
    IOKXSocketClientUnifiedApiAccount Account { get; }
    /// <summary>
    /// Exchange data streams and queries
    /// </summary>
    /// <see cref="IOKXSocketClientUnifiedApiExchangeData"/>
    IOKXSocketClientUnifiedApiExchangeData ExchangeData { get; }
    /// <summary>
    /// Trading data and queries
    /// </summary>
    /// <see cref="IOKXSocketClientUnifiedApiTrading"/>
    IOKXSocketClientUnifiedApiTrading Trading { get; }
}