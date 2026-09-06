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
    internal partial class OKXRestClientUnifiedSharedApi
    {
        #region Place Futures Trigger Order

        async Task<ICallResult<SharedId>> IPlaceFuturesTriggerOrder.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
            => await PlaceFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameterRule<PlaceFuturesTriggerOrderRequest>.Required(x => x.MarginMode, "Margin mode to use", SharedMarginMode.Cross),
                RequestParameterRule<PlaceFuturesTriggerOrderRequest>.Required(x => x.PositionMode, "Position mode the account is in", SharedPositionMode.HedgeMode),
            }
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceAlgoOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Isolated ? TradeMode.Isolated: TradeMode.Cross,
                side,
                AlgoOrderType.Trigger,
                quantity: request.Quantity.QuantityInContracts ?? 0,
                triggerPrice: request.TriggerPrice,
                orderPrice: request.OrderPrice ?? -1,
                clientOrderId: request.ClientOrderId,
                positionSide: request.PositionMode == SharedPositionMode.OneWay ? PositionSide.Net : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.AlgoOrderId!));
        }

        #endregion

        private OrderSide GetTriggerOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Buy : OrderSide.Sell;
        
            return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Sell : OrderSide.Buy;
        }

        #region Get Futures Trigger Order

        async Task<ICallResult<SharedFuturesTriggerOrder>> IGetFuturesTriggerOrder.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            var order = await _api.Trading.GetAlgoOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(order);

            OKXOrder? orderDetails = null;
            if (order.Data.OrderIdList?.Any() == true)
            {
                var orderDetailsRequest = await _api.Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), order.Data.OrderIdList.First(), ct: ct).ConfigureAwait(false);
                if (!orderDetailsRequest.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orderDetailsRequest);

                orderDetails = orderDetailsRequest.Data;
            }

            return HttpResult.Ok(order, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol!,
                order.Data.AlgoId!,
                order.Data.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                order.Data.OrderSide == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.State, orderDetails),
                order.Data.TriggerPrice ?? 0,
                order.Data.PositionSide == PositionSide.Short ? SharedPositionSide.Short : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : null,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.OrderIdList?.Any() == true ? order.Data.OrderIdList.First().ToString() : null,
                AveragePrice = orderDetails?.AveragePrice == 0 ? null : orderDetails?.AveragePrice,
                OrderPrice = order.Data.OrderPrice,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: orderDetails?.QuantityFilled),
                TimeInForce = orderDetails == null? null: ParseTimeInForce(orderDetails.OrderType),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.PositionSide == PositionSide.Net ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                Fee = orderDetails?.Fee,
                FeeAsset = orderDetails?.FeeAsset,
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        #endregion

        #region Cancel Futures Trigger Order

        async Task<ICallResult<SharedId>> ICancelFuturesTriggerOrder.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelAlgoOrderAsync(
                [new OKXAlgoOrderRequest { Symbol = request.Symbol!.GetSymbol(FormatSymbol), AlgoOrderId = request.OrderId }],
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

    }
}
