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
        IPositionSocketClient,
        ISpotOrderManagementSocketClient,
        IFuturesOrderManagementSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IOKXSocketClientUnifiedSharedApi :
        ISubscribeTickerOperation,
        ISubscribeTradesOperation,
        ISubscribeBookTickerOperation,
        ISubscribeKlinesOperation,
        ISubscribeOrderBookOperation,
        ISubscribeBalancesOperation,
        ISubscribeSpotOrdersOperation,
        ISubscribeFuturesOrdersOperation,
        ISubscribeUserTradesOperation,
        ISubscribePositionsOperation,
        IPlaceSpotOrderOperation,
        ICancelSpotOrderOperation,
        IPlaceFuturesOrderOperation,
        ICancelFuturesOrderOperation
    {
    }
}
