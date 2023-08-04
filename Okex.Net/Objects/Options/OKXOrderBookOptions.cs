using CryptoExchange.Net.Objects.Options;

namespace OKX.Net.Objects.Options;

/// <summary>
/// Order book options
/// </summary>
public class OKXOrderBookOptions : OrderBookOptions
{
    /// <summary>
    /// Default options for the OKX SymbolOrderBook
    /// </summary>
    public static OKXOrderBookOptions Default { get; set; } = new OKXOrderBookOptions();

    /// <summary>
    /// The limit of entries in the order book
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
    /// </summary>
    public TimeSpan? InitialDataTimeout { get; set; }

    internal OKXOrderBookOptions Copy()
    {
        var options = Copy<OKXOrderBookOptions>();
        options.Limit = Limit;
        options.InitialDataTimeout = InitialDataTimeout;
        return options;
    }
}
