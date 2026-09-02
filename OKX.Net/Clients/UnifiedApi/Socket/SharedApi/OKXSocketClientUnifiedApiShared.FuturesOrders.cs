using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXSocketClientUnifiedSharedApi
    {
        #region Futures Order client

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);
            var result = await _api.Trading.SubscribeToOrderUpdatesAsync(InstrumentType.Futures, null, null,
                update => handler(update.ToType<SharedFuturesOrderUpdate[]>(new[] {
                    new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, update.Data.Symbol),
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
#pragma warning disable CS0618 // Type or member is obsolete
                        FeeAsset = update.Data.FeeAsset,
                        Fee = update.Data.Fee == null ? null : Math.Abs(update.Data.Fee.Value),
#pragma warning restore CS0618 // Type or member is obsolete
                        StopLossPrice = update.Data.StopLossTriggerPrice,
                        TakeProfitPrice = update.Data.TakeProfitTriggerPrice,
                        LastTrade = update.Data.TradeId == null ? null : 
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, update.Data.Symbol),
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

        #region Futures Order Client

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        public PlaceFuturesOrderSocketOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderSocketOptions(_exchangeName, false)
        {
            RequestNotes = "The OKX WebSocket Order API uses symbol codes instead of symbol names. Make sure the REST GetFuturesSymbolsAsync method has been called prior to resolve the symbol code from name",
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.MarginMode), typeof(SharedMarginMode), "Isolated or cross margin", SharedMarginMode.Cross)
            }
        };
        public async Task<QueryResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var symbolName = request.Symbol!.GetSymbol(FormatSymbol);
            var symbolCode = OKXUtils.GetSymbolCode(_api.EnvironmentName, symbolName);
            if (symbolCode == null)
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceFuturesOrderRequest.Symbol), "Symbol code not resolved, make sure REST GetFuturesSymbolsAsync has been called prior"));

            var result = await _api.Trading.PlaceOrderAsync(
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


        public CancelFuturesOrderSocketOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderSocketOptions(_exchangeName, true)
        {
            RequestNotes = "The OKX WebSocket Order API uses symbol codes instead of symbol names. Make sure the REST GetFuturesSymbolsAsync method has been called prior to resolve the symbol code from name"
        };
        public async Task<QueryResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var symbolName = request.Symbol!.GetSymbol(FormatSymbol);
            var symbolCode = OKXUtils.GetSymbolCode(_api.EnvironmentName, symbolName);
            if (symbolCode == null)
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceFuturesOrderRequest.Symbol), "Symbol code not resolved, make sure REST GetFuturesSymbolsAsync has been called prior"));

            var order = await _api.Trading.CancelOrderAsync(symbolCode.Value, request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(order.Data.OrderId.ToString()!));
        }

        #endregion
    }
}
