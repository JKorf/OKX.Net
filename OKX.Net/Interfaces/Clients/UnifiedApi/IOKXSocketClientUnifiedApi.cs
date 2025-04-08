namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API
/// </summary>
public interface IOKXSocketClientUnifiedApi : ISocketApiClient
{
    /// <summary>
    /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
    /// </summary>
    IOKXSocketClientUnifiedApiShared SharedClient { get; }

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