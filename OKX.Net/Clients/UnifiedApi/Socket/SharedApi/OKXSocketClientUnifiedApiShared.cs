using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXSocketClientUnifiedSharedApi :
        SharedApiBase,
        IOKXSocketClientUnifiedApiShared,
        IOKXSocketClientUnifiedSharedApi
    {
        private readonly OKXSocketClientUnifiedApi _api;

        private const string _topicSpotId = "OKXSpot";
        private const string _topicFuturesId = "OKXFutures";
        private const string _exchangeName = "OKX";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(OKXExchange.Metadata, this);

        public OKXSocketClientUnifiedSharedApi(OKXSocketClientUnifiedApi api)
            : base(
                  api.Exchange,
                  [TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryLinear, TradingMode.DeliveryInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeBalanceOptions,
                SubscribeSpotOrderOptions,
                SubscribeFuturesOrderOptions,
                SubscribeUserTradeOptions,
                SubscribePositionOptions,
                PlaceSpotOrderOptions,
                CancelSpotOrderOptions,
                PlaceFuturesOrderOptions,
                CancelFuturesOrderOptions
                );
        }

    }
}
