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
        IBookTickerRestClient,
        ITransferRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IOKXRestClientUnifiedSharedApi :
        IGetAssetEndpoint,
        IGetAllAssetsEndpoint,
        IGetBalancesEndpoint,
        IGetDepositAddressesEndpoint,
        IGetDepositHistoryEndpoint,
        IGetOrderBookEndpoint,
        IGetKlinesEndpoint,
        IGetRecentTradesEndpoint,
        IPlaceSpotOrderEndpoint,
        IGetSpotOrderEndpoint,
        IGetOpenSpotOrdersEndpoint,
        IGetClosedSpotOrdersEndpoint,
        IGetSpotOrderTradesEndpoint,
        IGetSpotUserTradeHistoryEndpoint,
        ICancelSpotOrderEndpoint,
        IGetSpotSymbolsEndpoint,
        IGetSpotTickerEndpoint,
        IGetAllSpotTickersEndpoint,
        IGetWithdrawalHistoryEndpoint,
        IWithdrawEndpoint,
        IGetFuturesSymbolsEndpoint,
        IPlaceFuturesOrderEndpoint,
        IGetFuturesOrderEndpoint,
        IGetOpenFuturesOrdersEndpoint,
        IGetClosedFuturesOrdersEndpoint,
        IGetFuturesOrderTradesEndpoint,
        IGetFuturesUserTradeHistoryEndpoint,
        ICancelFuturesOrderEndpoint,
        IGetPositionsEndpoint,
        IClosePositionEndpoint,
        IGetLeverageEndpoint,
        ISetLeverageEndpoint,
        IGetMarkPriceKlinesEndpoint,
        IGetIndexPriceKlinesEndpoint,
        IGetOpenInterestEndpoint,
        IGetFuturesTickerEndpoint,
        IGetAllFuturesTickersEndpoint,
        IGetFundingRateHistoryEndpoint,
        IGetPositionModeEndpoint,
        ISetPositionModeEndpoint,
        IGetPositionHistoryEndpoint,
        IGetFeesEndpoint,
        IPlaceSpotTriggerOrderEndpoint,
        IGetSpotTriggerOrderEndpoint,
        ICancelSpotTriggerOrderEndpoint,
        IPlaceFuturesTriggerOrderEndpoint,
        IGetFuturesTriggerOrderEndpoint,
        ICancelFuturesTriggerOrderEndpoint,
        ISetFuturesTpSlEndpoint,
        ICancelFuturesTpSlEndpoint,
        IGetSpotOrderByClientOrderIdEndpoint,
        ICancelSpotOrderByClientOrderIdEndpoint,
        IGetFuturesOrderByClientOrderIdEndpoint,
        ICancelFuturesOrderByClientOrderIdEndpoint,
        IGetBookTickerEndpoint,
        ITransferEndpoint
    {
    }
}
