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
        IGetAssetRest,
        IGetAllAssetsRest,
        IGetBalancesRest,
        IGetDepositAddressesRest,
        IGetDepositHistoryRest,
        IGetOrderBookRest,
        IGetKlinesRest,
        IGetRecentTradesRest,
        IPlaceSpotOrderRest,
        IGetSpotOrderRest,
        IGetOpenSpotOrdersRest,
        IGetClosedSpotOrdersRest,
        IGetSpotOrderTradesRest,
        IGetSpotUserTradeHistoryRest,
        ICancelSpotOrderRest,
        IGetSpotSymbolsRest,
        IGetTickerRest,
        IGetAllTickersRest,
        IGetWithdrawalHistoryRest,
        IWithdrawRest,
        IGetFuturesSymbolsRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        IGetFuturesOrderTradesRest,
        IGetFuturesUserTradeHistoryRest,
        ICancelFuturesOrderRest,
        IGetPositionsRest,
        ICloseFullPositionRest,
        IGetLeverageRest,
        ISetLeverageRest,
        IGetMarkPriceKlinesRest,
        IGetIndexPriceKlinesRest,
        IGetOpenInterestRest,
        IGetFundingRateHistoryRest,
        IGetPositionModeRest,
        ISetPositionModeRest,
        IGetPositionHistoryRest,
        IGetFeesRest,
        IPlaceSpotTriggerOrderRest,
        IGetSpotTriggerOrderRest,
        ICancelSpotTriggerOrderRest,
        IPlaceFuturesTriggerOrderRest,
        IGetFuturesTriggerOrderRest,
        ICancelFuturesTriggerOrderRest,
        ISetFuturesTpSlRest,
        ICancelFuturesTpSlRest,
        IGetSpotOrderByClientOrderIdRest,
        ICancelSpotOrderByClientOrderIdRest,
        IGetFuturesOrderByClientOrderIdRest,
        ICancelFuturesOrderByClientOrderIdRest,
        IGetBookTickerRest,
        ITransferRest
    {
    }
}
