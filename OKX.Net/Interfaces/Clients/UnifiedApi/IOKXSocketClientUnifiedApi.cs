namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified API
/// </summary>
public interface IOKXSocketClientUnifiedApi : ISocketApiClient
{
    /// <summary>
    /// Get the shared socket subscription client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
    /// </summary>
    IOKXSocketClientUnifiedApiShared SharedClient { get; }

    /// <summary>
    /// Account streams and queries
    /// </summary>
    IOKXSocketClientUnifiedApiAccount Account { get; }
    /// <summary>
    /// Exchange data streams and queries
    /// </summary>
    IOKXSocketClientUnifiedApiExchangeData ExchangeData { get; }
    /// <summary>
    /// Trading data and queries
    /// </summary>
    IOKXSocketClientUnifiedApiTrading Trading { get; }
}