using CryptoExchange.Net.SharedApis;

namespace OKX.Net.Interfaces.Clients.UnifiedApi
{
    /// <summary>
    /// Shared interface for rest API usage
    /// </summary>
    public interface IOKXRestClientUnifiedApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        //ITradeHistoryRestClient
        IWithdrawalRestClient,
        IWithdrawRestClient,
        IFuturesSymbolRestClient,
        IFuturesOrderRestClient,
        ILeverageRestClient,
        IMarkPriceKlineRestClient,
        IIndexPriceKlineRestClient,
        IOpenInterestRestClient,
        IFuturesTickerRestClient,
        IFundingRateRestClient,
        IPositionModeRestClient,
        IPositionHistoryRestClient,
        IFeeRestClient,
        ISpotTriggerOrderRestClient,
        IFuturesTriggerOrderRestClient,
        IFuturesTpSlRestClient,
        ISpotOrderClientIdRestClient,
        IFuturesOrderClientIdRestClient,
        IBookTickerRestClient
    {
    }
}
