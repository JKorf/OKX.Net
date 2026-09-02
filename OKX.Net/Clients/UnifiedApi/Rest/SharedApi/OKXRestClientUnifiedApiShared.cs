using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Funding;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXRestClientUnifiedSharedApi :
        SharedApiBase,
        IOKXRestClientUnifiedApiShared,
        IOKXRestClientUnifiedSharedApi
    {
        private readonly OKXRestClientUnifiedApi _api;

        private const string _topicSpotId = "OKXSpot";
        private const string _topicFuturesId = "OKXFutures";
        private const string _exchangeName = "OKX";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(OKXExchange.Metadata, this);

        private static readonly HashSet<string> _fiatCurrencies = ["USD", "EUR", "TRY", "SGD", "AED", "AUD", "BRL"];

        public OKXRestClientUnifiedSharedApi(OKXRestClientUnifiedApi api)
            : base(
                  api.Exchange,
                  [TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryLinear, TradingMode.DeliveryInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities( 
                GetKlinesOptions,
                GetSpotSymbolsOptions,
                GetSpotTickerOptions,
                GetAllSpotTickersOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                GetBalancesOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotOrderTradesOptions,
                GetSpotUserTradeHistoryOptions,
                CancelSpotOrderOptions,
                GetAssetOptions,
                GetAllAssetsOptions,
                GetOrderBookOptions,
                GetDepositAddressesOptions,
                GetDepositHistoryOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                GetFuturesSymbolsOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetIndexPriceKlinesOptions,
                GetMarkPriceKlinesOptions,
                GetOpenInterestOptions,
                GetFundingRateHistoryOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetPositionModeOptions,
                SetPositionModeOptions,
                GetPositionHistoryOptions,
                PlaceSpotTriggerOrderOptions,
                GetSpotTriggerOrderOptions,
                CancelSpotTriggerOrderOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                SetFuturesTpSlOptions,
                CancelFuturesTpSlOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                GetSpotOrderByClientOrderIdOptions,
                CancelSpotOrderByClientOrderIdOptions,
                TransferOptions,
                GetFeeOptions
                );
        }

    }
}
