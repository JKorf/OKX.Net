using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXSocketClientUnifiedSharedApi
    {
        #region Spot Order client
        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);
            var result = await _api.Trading.SubscribeToOrderUpdatesAsync(InstrumentType.Spot, null, null,
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] {
                    new SharedSpotOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null,update.Data.Symbol),
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
#pragma warning disable CS0618 // Type or member is obsolete
                        FeeAsset = update.Data.FeeAsset,
                        Fee = update.Data.Fee == null ? null : Math.Abs(update.Data.Fee.Value),
#pragma warning restore CS0618 // Type or member is obsolete
                        LastTrade = update.Data.TradeId == null ? null : 
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, update.Data.Symbol),
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

        #region Spot Order client

        public SharedOrderType[] SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        public SharedQuantitySupport SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        public SharedFeeDeductionType SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        public SharedFeeAssetType SpotFeeAssetType => SharedFeeAssetType.OutputAsset;

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        public PlaceSpotOrderSocketOptions PlaceSpotOrderOptions { get; } = new PlaceSpotOrderSocketOptions(_exchangeName)
        {
            RequestNotes = "The OKX WebSocket Order API uses symbol codes instead of symbol names. Make sure the REST GetSpotSymbolsAsync method has been called prior to resolve the symbol code from name"
        };

        public async Task<QueryResult<SharedId>> PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var symbolName = request.Symbol!.GetSymbol(FormatSymbol);
            var symbolCode = OKXUtils.GetSymbolCode(_api.EnvironmentName, symbolName);
            if (symbolCode == null)
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceSpotOrderRequest.Symbol), "Symbol code not resolved, make sure REST GetSpotSymbolsAsync has been called prior"));

            var result = await _api.Trading.PlaceOrderAsync(
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

        public CancelSpotOrderSocketOptions CancelSpotOrderOptions { get; } = new CancelSpotOrderSocketOptions(_exchangeName, true)
        {
            RequestNotes = "The OKX WebSocket Order API uses symbol codes instead of symbol names. Make sure the REST GetSpotSymbolsAsync method has been called prior to resolve the symbol code from name"
        };
        public async Task<QueryResult<SharedId>> CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var symbolName = request.Symbol!.GetSymbol(FormatSymbol);
            var symbolCode = OKXUtils.GetSymbolCode(_api.EnvironmentName, symbolName);
            if (symbolCode == null)
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceFuturesOrderRequest.Symbol), "Symbol code not resolved, make sure REST GetSpotSymbolsAsync has been called prior"));

            var order = await _api.Trading.CancelOrderAsync(symbolCode.Value, request.OrderId, ct: ct).ConfigureAwait(false);
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
    }
}
