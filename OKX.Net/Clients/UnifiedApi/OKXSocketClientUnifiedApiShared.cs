using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXSocketClientUnifiedApi : IOKXSocketClientUnifiedApiShared
    {
        private const string _topicSpotId = "OKXSpot";
        private const string _topicFuturesId = "OKXFutures";
        private const string _exchangeName = "OKX";

        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryLinear, TradingMode.DeliveryInverse };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(OKXExchange.Metadata, this);

        #region Ticker client
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName) {
            SupportsMultipleSymbols = true
        };
        async Task<WebSocketResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(request.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, EnvironmentName, null, update.Data.Symbol), 
                    update.Data.Symbol,
                    update.Data.LastPrice ?? 0,
                    update.Data.HighPrice ?? 0,
                    update.Data.LowPrice ?? 0,
                    new SharedOrderQuantity(
                                request.TradingMode == TradingMode.Spot ? update.Data.Volume : update.Data.QuoteVolume,
                                request.TradingMode == TradingMode.Spot ? update.Data.QuoteVolume : null,
                                request.TradingMode != TradingMode.Spot ? update.Data.Volume : null),
                    update.Data.OpenPrice == null ? null : Math.Round((update.Data.LastPrice ?? 0) / update.Data.OpenPrice.Value * 100 - 100, 2))
                {
#pragma warning disable CS0618 // Type or member is obsolete | Temporary to maintain previous behavior
                    Volume = update.Data.Volume,
#pragma warning restore
                })), ct: ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Trade client

        SubscribeTradeOptions ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToTradeUpdatesAsync(symbols, update => handler(update.ToType<SharedTrade[]>(new[] {
                new SharedTrade(
                    ExchangeSymbolCache.ParseSymbol(request.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, EnvironmentName, null, update.Data.Symbol),
                    update.Data.Symbol,
                    new SharedOrderQuantity(
                        request.TradingMode == TradingMode.Spot ? update.Data.Quantity : null,
                        null,
                        request.TradingMode != TradingMode.Spot ? update.Data.Quantity : null),
                    update.Data.Price,
                    update.Data.Time){
                Side = update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            } })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Book Ticker client

        SubscribeBookTickerOptions IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        async Task<WebSocketResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedBookTicker(
                    ExchangeSymbolCache.ParseSymbol(request.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, EnvironmentName, null, update.Data.Symbol),
                    update.Data.Symbol,
                    update.Data.BestAskPrice ?? 0,
                    new SharedOrderQuantity(
                    request.TradingMode == TradingMode.Spot ? update.Data.BestAskQuantity : null,
                    null,
                    request.TradingMode != TradingMode.Spot ? update.Data.BestAskQuantity : null),
                    update.Data.BestBidPrice ?? 0,
                    new SharedOrderQuantity(
                        request.TradingMode == TradingMode.Spot ? update.Data.BestBidQuantity : null,
                        null,
                        request.TradingMode != TradingMode.Spot ? update.Data.BestBidQuantity : null)))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.ThreeMinutes,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.SixHours,
            SharedKlineInterval.TwelveHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth)
        {
            SupportsMultipleSymbols = true
        };
        async Task<WebSocketResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (KlineInterval)request.Interval;
            if ((int)request.Interval >= 60 * 60 * 6)
                // For 6hours and up the correct int value for UTC is the value + 1
                interval = (KlineInterval)((int)request.Interval + 1);

            var validationError = SharedClient.SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToKlineUpdatesAsync(symbols, interval, update => handler(update.ToType(
                new SharedKline(
                    ExchangeSymbolCache.ParseSymbol(request.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, EnvironmentName, null, update.Data.Symbol), 
                    update.Data.Symbol,
                    update.Data.Time,
                    update.Data.ClosePrice,
                    update.Data.HighPrice,
                    update.Data.LowPrice,
                    update.Data.OpenPrice, 
                    new SharedOrderQuantity(
                                request.TradingMode == TradingMode.Spot ? update.Data.Volume : update.Data.VolumeCurrency,
                                update.Data.VolumeCurrencyQuote,
                                request.TradingMode != TradingMode.Spot ? update.Data.Volume : null
                                )))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 1, 5 })
        {
            SupportsMultipleSymbols = true
        };
        async Task<WebSocketResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var type = request.Limit == 1 ? OrderBookType.BBO_TBT : OrderBookType.OrderBook_5;

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToOrderBookUpdatesAsync(symbols, type, update => handler(
                update.ToType(
                    new SharedOrderBook(request.TradingMode == TradingMode.Spot ? SharedQuantityType.BaseAsset : SharedQuantityType.Contracts, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Balance client
        SubscribeBalanceOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await Account.SubscribeToAccountUpdatesAsync(null, false,
                update =>
                {
                    if (update.UpdateType == SocketUpdateType.Snapshot)
                        return;

                    handler(update.ToType<SharedBalance[]>(update.Data.Details.Select(x => 
                        new SharedBalance(SupportedTradingModes, x.Asset, x.AvailableBalance ?? 0, x.Equity ?? 0)).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Spot Order client

        SubscribeSpotOrderOptions ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);
            var result = await Trading.SubscribeToOrderUpdatesAsync(InstrumentType.Spot, null, null,
                update => handler(update.ToType<SharedSpotOrder[]>(new[] {
                    new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null,update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString()!,
                        update.Data.OrderType == OrderType.Limit ? SharedOrderType.Limit : update.Data.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                        update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.OrderState),
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId?.ToString(),
                        OrderQuantity = new SharedOrderQuantity(ParseQuantity(update.Data), ParseQuoteQuantity(update.Data)),
                        QuantityFilled = new SharedOrderQuantity(update.Data.AccumulatedFillQuantity),
                        AveragePrice = update.Data.AveragePrice == 0 ? null : update.Data.AveragePrice,
                        UpdateTime = update.Data.UpdateTime,
                        OrderPrice = update.Data.Price,
                        FeeAsset = update.Data.FeeAsset,
                        Fee = update.Data.Fee == null ? null : Math.Abs(update.Data.Fee.Value),
                        LastTrade = update.Data.TradeId == null ? null : 
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, update.Data.Symbol),
                            update.Data.Symbol, 
                            update.Data.OrderId.ToString()!,
                            update.Data.TradeId.ToString()!,
                            update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(update.Data.QuantityFilled!.Value), 
                            update.Data.FillPrice!.Value,
                            update.Data.FillTime!.Value)
                            {
                                ClientOrderId = update.Data.ClientOrderId,
                                Fee = Math.Abs(update.Data.FillFee),
                                FeeAsset = update.Data.FillFeeAsset,
                                Role = update.Data.ExecutionType == "T" ? SharedRole.Taker : SharedRole.Maker
                            }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus orderState)
        {
            if (orderState == OrderStatus.Canceled)
                return SharedOrderStatus.Canceled;
            if (orderState == OrderStatus.Live || orderState == OrderStatus.PartiallyFilled)
                return SharedOrderStatus.Open;
            if (orderState == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }
        #endregion

        #region Futures Order client

        SubscribeFuturesOrderOptions IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);
            var result = await Trading.SubscribeToOrderUpdatesAsync(InstrumentType.Futures, null, null,
                update => handler(update.ToType<SharedFuturesOrder[]>(new[] {
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString()!,
                        update.Data.OrderType == OrderType.Limit ? SharedOrderType.Limit : update.Data.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                        update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.OrderState),
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: update.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity: update.Data.AccumulatedFillQuantity),
                        AveragePrice = update.Data.AveragePrice == 0 ? null : update.Data.AveragePrice,
                        UpdateTime = update.Data.UpdateTime,
                        OrderPrice = update.Data.Price,
                        Leverage = update.Data.Leverage,
                        ReduceOnly = update.Data.ReduceOnly,
                        PositionSide = update.Data.PositionSide == PositionSide.Net ? null : update.Data.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                        FeeAsset = update.Data.FeeAsset,
                        Fee = update.Data.Fee == null ? null : Math.Abs(update.Data.Fee.Value),
                        StopLossPrice = update.Data.StopLossTriggerPrice,
                        TakeProfitPrice = update.Data.TakeProfitTriggerPrice,
                        LastTrade = update.Data.TradeId == null ? null : 
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, update.Data.Symbol),
                            update.Data.Symbol,
                            update.Data.OrderId.ToString()!,
                            update.Data.TradeId.ToString()!,
                            update.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(contractQuantity: update.Data.QuantityFilled!.Value), 
                            update.Data.FillPrice!.Value,
                            update.Data.FillTime!.Value)
                        {
                            Fee = Math.Abs(update.Data.FillFee),
                            FeeAsset = update.Data.FillFeeAsset,
                            Role = update.Data.ExecutionType == "T" ? SharedRole.Taker : SharedRole.Maker
                        }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private decimal? ParseQuantity(OKXOrderUpdate data)
        {
            if (data.QuantityType == QuantityAsset.QuoteAsset)
                return null;

            return data.Quantity;
        }

        private decimal? ParseQuoteQuantity(OKXOrderUpdate data)
        {
            if (data.QuantityType == QuantityAsset.QuoteAsset)
                return data.Quantity;

            return null;
        }
        #endregion

        #region User Trade client
        SubscribeUserTradeOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await Trading.SubscribeToUserTradeUpdatesAsync(
                null,
                update => handler(update.ToType<SharedUserTrade[]>(new[] {
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, update.Data.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.TradeId.ToString(),
                        update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(
                            request.TradingMode == TradingMode.Spot ? update.Data.Quantity : null,
                            null,
                            request.TradingMode != TradingMode.Spot ? update.Data.Quantity : null),
                        update.Data.Price,
                        update.Data.Timestamp)
                    {
                        Role = update.Data.Role == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Position client
        SubscribePositionOptions IPositionSocketClient.SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true);
        async Task<WebSocketResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var instrType = request.TradingMode == null ? InstrumentType.Any : request.TradingMode.Value.IsPerpetual() ? InstrumentType.Swap : InstrumentType.Futures;
            var result = await Trading.SubscribeToPositionUpdatesAsync(
                instrType,
                null,
                null,
                true,
                update =>
                {
                    if (update.UpdateType == SocketUpdateType.Snapshot)
                        return;

                    handler(update.ToType<SharedPosition[]>(update.Data.Select(x => 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, x.Symbol), 
                        x.Symbol, 
                        new SharedOrderQuantity(contractQuantity: x.PositionsQuantity),
                        x.UpdateTime)
                    {
                        AverageOpenPrice = x.AveragePrice,
                        PositionMode = x.PositionSide == PositionSide.Net ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.PositionSide == PositionSide.Net ? (x.PositionsQuantity < 0 ? SharedPositionSide.Short : SharedPositionSide.Long) : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                        UnrealizedPnl = x.UnrealizedPnl,
                        Leverage = x.Leverage
                    }).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Spot Order client

        SharedOrderType[] ISpotOrderManagementSocketClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderManagementSocketClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        SharedQuantitySupport ISpotOrderManagementSocketClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        SharedFeeDeductionType ISpotOrderManagementSocketClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderManagementSocketClient.SpotFeeAssetType => SharedFeeAssetType.OutputAsset;

        string ISpotOrderManagementSocketClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceSpotOrderSocketOptions ISpotOrderManagementSocketClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderSocketOptions(_exchangeName)
        {
            RequestNotes = "The OKX WebSocket Order API uses symbol codes instead of symbol names. Make sure the REST GetSpotSymbolsAsync method has been called prior to resolve the symbol code from name"
        };

        async Task<QueryResult<SharedId>> ISpotOrderManagementSocketClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var symbolName = request.Symbol!.GetSymbol(FormatSymbol);
            var symbolCode = OKXUtils.GetSymbolCode(EnvironmentName, symbolName);
            if (symbolCode == null)
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceSpotOrderRequest.Symbol), "Symbol code not resolved, make sure REST GetSpotSymbolsAsync has been called prior"));

            var result = await Trading.PlaceOrderAsync(
                symbolCode.Value,
                request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                TradeMode.Cash,
                quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset ?? 0,
                price: request.Price,
                quantityAsset: request.Quantity?.QuantityInBaseAsset > 0 ? QuantityAsset.BaseAsset : QuantityAsset.QuoteAsset,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok(result, new SharedId(result.Data.OrderId.ToString()!));
        }

        CancelSpotOrderSocketOptions ISpotOrderManagementSocketClient.CancelSpotOrderOptions { get; } = new CancelSpotOrderSocketOptions(_exchangeName, true)
        {
            RequestNotes = "The OKX WebSocket Order API uses symbol codes instead of symbol names. Make sure the REST GetSpotSymbolsAsync method has been called prior to resolve the symbol code from name"
        };
        async Task<QueryResult<SharedId>> ISpotOrderManagementSocketClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var symbolName = request.Symbol!.GetSymbol(FormatSymbol);
            var symbolCode = OKXUtils.GetSymbolCode(EnvironmentName, symbolName);
            if (symbolCode == null)
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceFuturesOrderRequest.Symbol), "Symbol code not resolved, make sure REST GetSpotSymbolsAsync has been called prior"));

            var order = await Trading.CancelOrderAsync(symbolCode.Value, request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(request.OrderId));
        }

        private OrderType GetPlaceOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.Market) return OrderType.Market;
            if (type == SharedOrderType.LimitMaker) return OrderType.PostOnly;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return OrderType.FillOrKill;

            return OrderType.Limit;
        }
        #endregion


        #region Futures Order Client

        SharedFeeDeductionType IFuturesOrderManagementSocketClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderManagementSocketClient.FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        SharedOrderType[] IFuturesOrderManagementSocketClient.FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        SharedTimeInForce[] IFuturesOrderManagementSocketClient.FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport IFuturesOrderManagementSocketClient.FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        string IFuturesOrderManagementSocketClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceFuturesOrderSocketOptions IFuturesOrderManagementSocketClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderSocketOptions(_exchangeName, false)
        {
            RequestNotes = "The OKX WebSocket Order API uses symbol codes instead of symbol names. Make sure the REST GetFuturesSymbolsAsync method has been called prior to resolve the symbol code from name",
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.MarginMode), typeof(SharedMarginMode), "Isolated or cross margin", SharedMarginMode.Cross)
            }
        };
        async Task<QueryResult<SharedId>> IFuturesOrderManagementSocketClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var symbolName = request.Symbol!.GetSymbol(FormatSymbol);
            var symbolCode = OKXUtils.GetSymbolCode(EnvironmentName, symbolName);
            if (symbolCode == null)
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceFuturesOrderRequest.Symbol), "Symbol code not resolved, make sure REST GetFuturesSymbolsAsync has been called prior"));

            var result = await Trading.PlaceOrderAsync(
                symbolCode.Value,
                request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity?.QuantityInContracts ?? 0,
                price: request.Price,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                reduceOnly: request.ReduceOnly,
                tradeMode: request.MarginMode == SharedMarginMode.Isolated ? TradeMode.Isolated : TradeMode.Cross,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok(result, new SharedId(result.Data.OrderId.ToString()!));
        }


        CancelFuturesOrderSocketOptions IFuturesOrderManagementSocketClient.CancelFuturesOrderOptions { get; } = new CancelFuturesOrderSocketOptions(_exchangeName, true)
        {
            RequestNotes = "The OKX WebSocket Order API uses symbol codes instead of symbol names. Make sure the REST GetFuturesSymbolsAsync method has been called prior to resolve the symbol code from name"
        };
        async Task<QueryResult<SharedId>> IFuturesOrderManagementSocketClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var symbolName = request.Symbol!.GetSymbol(FormatSymbol);
            var symbolCode = OKXUtils.GetSymbolCode(EnvironmentName, symbolName);
            if (symbolCode == null)
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceFuturesOrderRequest.Symbol), "Symbol code not resolved, make sure REST GetFuturesSymbolsAsync has been called prior"));

            var order = await Trading.CancelOrderAsync(symbolCode.Value, request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(order.Data.OrderId.ToString()!));
        }

        #endregion
    }
}
