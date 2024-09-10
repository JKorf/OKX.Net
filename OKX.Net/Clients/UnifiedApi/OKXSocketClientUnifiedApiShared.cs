using OKX.Net;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Models.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using OKX.Net.Enums;
using CryptoExchange.Net.SharedApis.Interfaces.Socket.Futures;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXSocketClientUnifiedApi : IOKXSocketClientUnifiedApiShared
    {
        public string Exchange => OKXExchange.ExchangeName;
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.Spot, ApiType.PerpetualLinear, ApiType.PerpetualInverse, ApiType.DeliveryLinear, ApiType.DeliveryInverse };

        #region Ticker client
        SubscriptionOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscriptionOptions<SubscribeTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await ExchangeData.SubscribeToTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(update.Data.Symbol, update.Data.LastPrice ?? 0, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.Volume))), ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        SubscriptionOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscriptionOptions<SubscribeTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedTrade>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await ExchangeData.SubscribeToTradeUpdatesAsync(symbol, update => handler(update.AsExchangeEvent<IEnumerable<SharedTrade>>(Exchange, new[] { new SharedTrade(update.Data.Price, update.Data.Quantity, update.Data.Time) })), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Book Ticker client

        SubscriptionOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscriptionOptions<SubscribeBookTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await ExchangeData.SubscribeToTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(update.Data.BestAskPrice ?? 0, update.Data.BestAskQuantity ?? 0, update.Data.BestBidPrice ?? 0, update.Data.BestBidQuantity ?? 0))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false);
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await ExchangeData.SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.AsExchangeEvent(Exchange, new SharedKline(update.Data.Time, update.Data.ClosePrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.OpenPrice, update.Data.Volume))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 1, 5 });
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var type = request.Limit == 1 ? OrderBookType.BBO_TBT : OrderBookType.OrderBook_5;

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await ExchangeData.SubscribeToOrderBookUpdatesAsync(symbol, type, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        SubscriptionOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscriptionOptions("SubscribeBalanceRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedBalance>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await Account.SubscribeToAccountUpdatesAsync(null, false, 
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Details.Select(x => new SharedBalance(x.Asset, x.AvailableBalance ?? 0, x.Equity ?? 0)))),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Spot Order client

        SubscriptionOptions ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new SubscriptionOptions("SubscribeSpotOrderRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedSpotOrder>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderSocketClient)this).SubscribeSpotOrderOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var result = await Trading.SubscribeToOrderUpdatesAsync(Enums.InstrumentType.Spot, null, null,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedSpotOrder>>(Exchange, new[] {
                    new SharedSpotOrder(
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.Data.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                        update.Data.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        update.Data.OrderState == Enums.OrderStatus.Canceled ? SharedOrderStatus.Canceled : (update.Data.OrderState == Enums.OrderStatus.Live || update.Data.OrderState == Enums.OrderStatus.PartiallyFilled) ? SharedOrderStatus.Open : SharedOrderStatus.Filled,
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId?.ToString(),
                        Quantity = (update.Data.QuantityType == null && update.Data.OrderType == Enums.OrderType.Market && update.Data.OrderSide == Enums.OrderSide.Buy) ? null : update.Data.QuantityType == Enums.QuantityAsset.QuoteAsset ? null : update.Data.Quantity,
                        QuantityFilled = update.Data.AccumulatedFillQuantity,
                        AveragePrice = update.Data.AveragePrice,
                        UpdateTime = update.Data.UpdateTime,
                        Price = update.Data.Price,
                        FeeAsset = update.Data.FeeAsset,
                        Fee = update.Data.Fee == null ? null : Math.Abs(update.Data.Fee.Value),
                        LastTrade = update.Data.TradeId == null ? null : new SharedUserTrade(update.Data.Symbol, update.Data.OrderId.ToString(), update.Data.TradeId.ToString(), update.Data.QuantityFilled!.Value, update.Data.FillPrice!.Value, update.Data.FillTime!.Value)
                        {
                            Fee = update.Data.FillFee,
                            FeeAsset = update.Data.FillFeeAsset,
                            Role = update.Data.ExecutionType == "T" ? SharedRole.Taker : SharedRole.Maker
                        }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Futures Order client

        SubscriptionOptions IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new SubscriptionOptions("SubscribeFuturesOrderRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedFuturesOrder>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderSocketClient)this).SubscribeFuturesOrderOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var result = await Trading.SubscribeToOrderUpdatesAsync(Enums.InstrumentType.Futures, null, null,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedFuturesOrder>>(Exchange, new[] {
                    new SharedFuturesOrder(
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.Data.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                        update.Data.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        update.Data.OrderState == Enums.OrderStatus.Canceled ? SharedOrderStatus.Canceled : (update.Data.OrderState == Enums.OrderStatus.Live || update.Data.OrderState == Enums.OrderStatus.PartiallyFilled) ? SharedOrderStatus.Open : SharedOrderStatus.Filled,
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId?.ToString(),
                        Quantity = (update.Data.QuantityType == null && update.Data.OrderType == Enums.OrderType.Market && update.Data.OrderSide == Enums.OrderSide.Buy) ? null : update.Data.QuantityType == Enums.QuantityAsset.QuoteAsset ? null : update.Data.Quantity,
                        QuantityFilled = update.Data.AccumulatedFillQuantity,
                        AveragePrice = update.Data.AveragePrice,
                        UpdateTime = update.Data.UpdateTime,
                        Price = update.Data.Price,
                        Leverage = update.Data.Leverage,
                        ReduceOnly = update.Data.ReduceOnly,
                        PositionSide = update.Data.PositionSide == PositionSide.Net ? null : update.Data.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                        FeeAsset = update.Data.FeeAsset,
                        Fee = update.Data.Fee == null ? null : Math.Abs(update.Data.Fee.Value),
                        LastTrade = update.Data.TradeId == null ? null : new SharedUserTrade(update.Data.Symbol, update.Data.OrderId.ToString(), update.Data.TradeId.ToString(), update.Data.QuantityFilled!.Value, update.Data.FillPrice!.Value, update.Data.FillTime!.Value)
                        {
                            Fee = update.Data.FillFee,
                            FeeAsset = update.Data.FillFeeAsset,
                            Role = update.Data.ExecutionType == "T" ? SharedRole.Taker : SharedRole.Maker
                        }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region User Trade client
        SubscriptionOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscriptionOptions("SubscribeUserTradeRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedUserTrade>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await Trading.SubscribeToUserTradeUpdatesAsync(
                null,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedUserTrade>>(Exchange, new[] {
                    new SharedUserTrade(
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.TradeId.ToString(),
                        update.Data.Quantity,
                        update.Data.Price,
                        update.Data.Timestamp)
                    {
                        Role = update.Data.Role == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Position client
        SubscriptionOptions IPositionSocketClient.SubscribePositionOptions { get; } = new SubscriptionOptions("SubscribePositionRequest", true);
        async Task<ExchangeResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedPosition>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var instrType = apiType == null ? InstrumentType.Any : apiType.Value.IsPerpetual() ? InstrumentType.Swap : InstrumentType.Futures;
            var result = await Trading.SubscribeToPositionUpdatesAsync(
                instrType,
                null,
                null,
                false,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedPosition>>(Exchange, update.Data.Select(x =>  new SharedPosition(x.Symbol, x.PositionsQuantity ?? 0, x.UpdateTime)
                {
                    AverageEntryPrice = x.AveragePrice,
                    PositionSide = x.PositionSide == PositionSide.Net ? (x.PositionsQuantity < 0 ? SharedPositionSide.Short : SharedPositionSide.Long) : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                    UnrealizedPnl = x.UnrealizedPnl,
                    InitialMargin = x.InitialMarginRequirement,
                    MaintenanceMargin = x.MaintenanceMarginRequirement,
                    Leverage = x.Leverage
                }))),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion
    }
}
