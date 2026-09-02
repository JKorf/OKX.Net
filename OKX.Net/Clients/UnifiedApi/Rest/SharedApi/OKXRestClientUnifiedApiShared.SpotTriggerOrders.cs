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
        #region Spot Trigger Order Client

        public PlaceSpotTriggerOrderOptions PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, false);
        public async Task<HttpResult<SharedId>> PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceAlgoOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                TradeMode.Cash,
                request.OrderSide == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                AlgoOrderType.Trigger,
                quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset ?? 0,
                quantityType: request.Quantity?.QuantityInBaseAsset > 0 ? QuantityAsset.BaseAsset : QuantityAsset.QuoteAsset,
                triggerPrice: request.TriggerPrice,
                orderPrice: request.OrderPrice ?? -1,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.AlgoOrderId!));
        }


        public GetSpotTriggerOrderOptions GetSpotTriggerOrderOptions { get; } = new GetSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotTriggerOrder>> GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            var order = await _api.Trading.GetAlgoOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(order);

            OKXOrder? orderDetails = null;
            if (order.Data.OrderIdList?.Any() == true)
            {
                var orderDetailsRequest = await _api.Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), order.Data.OrderIdList.First(), ct: ct).ConfigureAwait(false);
                if (!orderDetailsRequest.Success)
                    return HttpResult.Fail<SharedSpotTriggerOrder>(orderDetailsRequest);

                orderDetails = orderDetailsRequest.Data;
            }

            return HttpResult.Ok(order, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol!,
                order.Data.AlgoId!,
                order.Data.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                order.Data.OrderSide == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.State, orderDetails),
                order.Data.TriggerPrice ?? 0,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.OrderIdList?.Any() == true ? order.Data.OrderIdList.First().ToString() : null,
                OrderPrice = order.Data.OrderPrice,
                OrderQuantity = new SharedOrderQuantity(order.Data.QuantityType == QuantityAsset.QuoteAsset ? null : order.Data.Quantity, order.Data.QuantityType == QuantityAsset.QuoteAsset ? order.Data.Quantity : null),
                QuantityFilled = new SharedOrderQuantity(orderDetails?.QuantityFilled),
                TimeInForce = orderDetails == null ? null : ParseTimeInForce(orderDetails.OrderType),
                UpdateTime = order.Data.UpdateTime,
                AveragePrice = orderDetails?.AveragePrice == 0 ? null : orderDetails?.AveragePrice,
                Fee = Math.Abs(orderDetails?.Fee ?? 0),
                FeeAsset = orderDetails?.FeeAsset,
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(AlgoOrderState state, OKXOrder? orderInfo)
        {
            if (state == AlgoOrderState.Canceled || state == AlgoOrderState.Failed || orderInfo?.OrderState == OrderStatus.Canceled)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (orderInfo == null)
                return SharedTriggerOrderStatus.Active;

            if (orderInfo.OrderState == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (orderInfo.OrderState == OrderStatus.Live || orderInfo.OrderState == OrderStatus.PartiallyFilled)
                return SharedTriggerOrderStatus.Active;

            return SharedTriggerOrderStatus.Unknown;
        }

        public CancelSpotTriggerOrderOptions CancelSpotTriggerOrderOptions { get; } = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
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
