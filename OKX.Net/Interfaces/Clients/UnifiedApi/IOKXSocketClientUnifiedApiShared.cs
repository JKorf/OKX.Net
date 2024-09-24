using CryptoExchange.Net.SharedApis;

namespace OKX.Net.Interfaces.Clients.UnifiedApi
{
    /// <summary>
    /// Shared interface for socket API usage
    /// </summary>
    public interface IOKXSocketClientUnifiedApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        ISpotOrderSocketClient,
        IFuturesOrderSocketClient,
        IUserTradeSocketClient,
        IPositionSocketClient
    {
    }
}
