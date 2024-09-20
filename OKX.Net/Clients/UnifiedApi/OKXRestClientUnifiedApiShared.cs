using OKX.Net.Interfaces.Clients.UnifiedApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Models.Rest;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OKX.Net.Enums;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Models;
using OKX.Net.Objects.Market;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using OKX.Net.Objects.Trade;
using CryptoExchange.Net.SharedApis.Models.EndpointOptions;
using OKX.Net.Objects.Public;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXRestClientUnifiedApi : IOKXRestClientUnifiedApiShared
    {
        public string Exchange => OKXExchange.ExchangeName;
        public CryptoExchange.Net.Objects.TradingMode[] SupportedApiTypes { get; } = new[] { CryptoExchange.Net.Objects.TradingMode.Spot, CryptoExchange.Net.Objects.TradingMode.PerpetualLinear, CryptoExchange.Net.Objects.TradingMode.PerpetualInverse, CryptoExchange.Net.Objects.TradingMode.DeliveryLinear, CryptoExchange.Net.Objects.TradingMode.DeliveryInverse };
        private CryptoExchange.Net.Objects.TradingMode[] FuturesApiTypes { get; } = new[] { CryptoExchange.Net.Objects.TradingMode.PerpetualLinear, CryptoExchange.Net.Objects.TradingMode.DeliveryLinear, CryptoExchange.Net.Objects.TradingMode.PerpetualInverse, CryptoExchange.Net.Objects.TradingMode.DeliveryInverse };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationType.Descending, false)
        {
            MaxRequestDataPoints = 100
        };

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 100;
            if (startTime == null || startTime < endTime)
            {
                var offset = (int)interval * limit;
                startTime = endTime.AddSeconds(-offset);
            }

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            // Get data
            WebCallResult<IEnumerable<OKXKline>> result;
            if (endTime > DateTime.UtcNow.AddSeconds(-(2 * (int)request.Interval)))
            {
                // The last Kline is delayed on the history endpoint so when retrieving the most recent klines use the non-history endpoint
                result = await ExchangeData.GetKlinesAsync(
                    request.Symbol.GetSymbol(FormatSymbol),
                    interval,
                    startTime,
                    endTime,
                    limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }
            else
            {
                result = await ExchangeData.GetKlineHistoryAsync(
                    request.Symbol.GetSymbol(FormatSymbol),
                    interval,
                    startTime,
                    endTime,
                    limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }

            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.Time);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, request.Symbol.ApiType, result.Data.Select(x => new SharedKline(x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)).ToArray(), nextToken);
        }

        #endregion

        #region Spot Symbol client

        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, CryptoExchange.Net.Objects.TradingMode.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(InstrumentType.Spot, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotSymbol>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedSpotSymbol>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.State == InstrumentState.Live)
            {
                MaxTradeQuantity = Math.Min(s.MaxLimitQuantity ?? decimal.MaxValue, s.MaxMarketQuantity ?? decimal.MaxValue),
                MinTradeQuantity = s.MinimumOrderSize,
                QuantityStep = s.LotSize,
                PriceStep = s.TickSize
            }).ToArray());
        }

        #endregion

        #region Spot Ticker client

        EndpointOptions<GetTickerRequest> ISpotTickerRestClient.GetSpotTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, new SharedSpotTicker(symbol, result.Data.LastPrice ?? 0, result.Data.HighPrice ?? 0, result.Data.LowPrice ?? 0, result.Data.Volume, result.Data.OpenPrice == null ? null : Math.Round((result.Data.LastPrice ?? 0) / result.Data.OpenPrice.Value * 100 - 100, 2)));
        }

        EndpointOptions<GetTickersRequest> ISpotTickerRestClient.GetSpotTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotTicker>>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, CryptoExchange.Net.Objects.TradingMode.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotTicker>>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(InstrumentType.Spot, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotTicker>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedSpotTicker>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, result.Data.Select(x => new SharedSpotTicker(x.Symbol, x.LastPrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume, x.OpenPrice == null ? null : Math.Round((x.LastPrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))).ToArray());
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(100, false);
        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            var result = await ExchangeData.GetTradeHistoryAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, request.Symbol.ApiType, result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Time)).ToArray());
        }

        #endregion

        #region Balance client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true);

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, validationError);

            var result = await Account.GetAccountBalanceAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, SupportedApiTypes, result.Data.Details.Select(x => new SharedBalance(x.Asset, x.AvailableBalance ?? 0, x.Equity ?? 0)).ToArray());
        }

        #endregion

        #region Spot Order client

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market,
                SharedOrderType.LimitMaker
            },
            new[]
            {
                SharedTimeInForce.GoodTillCanceled,
                SharedTimeInForce.ImmediateOrCancel,
                SharedTimeInForce.FillOrKill
            },
            new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset));

#warning chekc
        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.OutputAsset;

        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity ?? request.QuoteQuantity ?? 0,
                price: request.Price,
                quantityAsset: request.QuoteQuantity > 0 ? QuantityAsset.QuoteAsset : QuantityAsset.BaseAsset,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedId(result.Data.OrderId.ToString()));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.GetOrderDetailsAsync(request.Symbol.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, new SharedSpotOrder(
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data.OrderType),
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderState),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                Price = order.Data.Price,
                Quantity = order.Data.QuantityType == QuantityAsset.QuoteAsset ? null : order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                QuoteQuantity = order.Data.QuantityType == QuantityAsset.QuoteAsset ? order.Data.Quantity : null,
                TimeInForce = ParseTimeInForce(order.Data.OrderType),
                UpdateTime = order.Data.UpdateTime,
                AveragePrice = order.Data.AveragePrice,
                Fee = Math.Abs(order.Data.Fee ?? 0),
                FeeAsset = order.Data.FeeAsset
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var order = await Trading.GetOrdersAsync(symbol: symbol, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, null, default);

            return order.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, order.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x.OrderType),
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderState),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                Price = x.Price,
                Quantity = x.QuantityType == QuantityAsset.QuoteAsset ? null : x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuantityType == QuantityAsset.QuoteAsset ? x.Quantity : null,
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                AveragePrice = x.AveragePrice,
                Fee = Math.Abs(x.Fee ?? 0),
                FeeAsset = x.FeeAsset
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationType.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, validationError);

            var order = await Trading.GetOrderHistoryAsync(
                InstrumentType.Spot,
                symbol: request.Symbol.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, null, default);

#warning pagination

            return order.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, order.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x.OrderType),
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderState),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                Price = x.Price,
                Quantity = x.QuantityType == QuantityAsset.QuoteAsset ? null : x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuantityType == QuantityAsset.QuoteAsset ? x.Quantity : null,
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                AveragePrice = x.AveragePrice,
                Fee = Math.Abs(x.Fee ?? 0),
                FeeAsset = x.FeeAsset
            }).ToArray());
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, new ArgumentError("Invalid order id"));

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var order = await Trading.GetUserTradesAsync(InstrumentType.Spot, symbol, orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

            return order.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, order.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.TradeId.ToString(),
                x.QuantityFilled ?? 0,
                x.FillPrice ?? 0,
                x.FillTime)
            {
                Fee = x.Fee.HasValue ? Math.Abs(x.Fee.Value) : null,
                FeeAsset = x.FeeAsset,
                Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationType.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, INextPageToken? nextPageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            string? fromId = null;
            if (nextPageToken is FromIdToken fromIdToken)
                fromId = fromIdToken.FromToken;

            // Get data
            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var order = await Trading.GetUserTradesAsync(
                InstrumentType.Spot,
                symbol,
                startTime: request.StartTime, 
                endTime: request.EndTime,
                limit: request.Limit ?? 100,
                fromId: fromId,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (order.Data.Count() == (request.Limit ?? 100))
                nextToken = new FromIdToken(order.Data.Max(o => o.TradeId).ToString());

            return order.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, order.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.TradeId.ToString(),
                x.QuantityFilled ?? 0,
                x.FillPrice ?? 0,
                x.FillTime)
            {
                Fee = x.Fee.HasValue ? Math.Abs(x.Fee.Value) : null,
                FeeAsset = x.FeeAsset,
                Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray(), nextToken);
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedId(request.OrderId));
        }

        private OrderType GetPlaceOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.Market) return OrderType.Market;
            if (type == SharedOrderType.LimitMaker) return OrderType.PostOnly;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return OrderType.FillOrKill;

            return OrderType.Limit;
        }
        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Live || status == OrderStatus.PartiallyFilled) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled) return SharedOrderStatus.Open;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.PostOnly) return SharedOrderType.LimitMaker;
            if (type == OrderType.ImmediateOrCancel) return SharedOrderType.Limit;
            if (type == OrderType.FillOrKill) return SharedOrderType.Limit;
            if (type == OrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(OrderType type)
        {
            if (type == OrderType.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (type == OrderType.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        #endregion

        #region Asset client

        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, CryptoExchange.Net.Objects.TradingMode.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await Account.GetAssetsAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            if (!assets.Data.Any())
                return assets.AsExchangeError<SharedAsset>(Exchange, new ServerError("Asset not found"));

            return assets.AsExchangeResult<SharedAsset>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, new SharedAsset(request.Asset)
            {
                FullName = assets.Data.First().Name,
                Networks = assets.Data.Select(x => new SharedAssetNetwork(x.Network)
                {
                    MinConfirmations = x.MinDepositConfirmations,
                    DepositEnabled = x.AllowDeposit,
                    MinWithdrawQuantity = x.MinimumWithdrawalAmount,
                    WithdrawEnabled = x.AllowWithdrawal,
                    WithdrawFee = x.MinimumWithdrawalFee
                }).ToArray()
            });
        }

        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedAsset>>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, CryptoExchange.Net.Objects.TradingMode.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedAsset>>(Exchange, validationError);

            var assets = await Account.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<IEnumerable<SharedAsset>>(Exchange, null, default);

            return assets.AsExchangeResult<IEnumerable<SharedAsset>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, assets.Data.GroupBy(x => x.Asset).Select(x => new SharedAsset(x.Key)
            {
                FullName = x.First().Name,
                Networks = x.Select(x => new SharedAssetNetwork(x.Network)
                {
                    MinConfirmations = x.MinDepositConfirmations,
                    DepositEnabled = x.AllowDeposit,
                    MinWithdrawQuantity = x.MinimumWithdrawalAmount,
                    WithdrawEnabled = x.AllowWithdrawal,
                    WithdrawFee = x.MinimumWithdrawalFee
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 400, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                depth: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedDepositAddress>>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, CryptoExchange.Net.Objects.TradingMode.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedDepositAddress>>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, null, default);

            return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, depositAddresses.Data.Where(x => request.Network == null ? true : x.Network == request.Network).Select(x => new SharedDepositAddress(request.Asset, x.Address)
            {
                TagOrMemo = x.Memo,
                Network = x.Network
            }
            ).ToArray());
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(SharedPaginationType.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedDeposit>>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, CryptoExchange.Net.Objects.TradingMode.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedDeposit>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTime = null;
            if (pageToken is DateTimeToken token)
                fromTime = token.LastTime;

            // Get data
            var deposits = await Account.GetDepositHistoryAsync(
                request.Asset,
                startTime: request.StartTime,
                endTime: fromTime ?? request.EndTime,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<IEnumerable<SharedDeposit>>(Exchange, null, default);

            // Determine next token
            DateTimeToken? nextToken = null;
            if (deposits.Data.Count() == (request.Limit ?? 100))
                nextToken = new DateTimeToken(deposits.Data.Min(x => x.Time).AddMilliseconds(-1));

            return deposits.AsExchangeResult<IEnumerable<SharedDeposit>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, deposits.Data.Select(x => new SharedDeposit(x.Asset, x.Quantity, x.State == DepositState.Successful, x.Time)
            {
                Network = x.Network,
                TransactionId = x.TransactionId
            }).ToArray(), nextToken);
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(SharedPaginationType.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedWithdrawal>>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, CryptoExchange.Net.Objects.TradingMode.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedWithdrawal>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTime = null;
            if (pageToken is DateTimeToken token)
                fromTime = token.LastTime;

            // Get data
            var withdrawals = await Account.GetWithdrawalHistoryAsync(
                request.Asset,
                startTime: request.StartTime,
                endTime: fromTime ?? request.EndTime,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!withdrawals)
                return withdrawals.AsExchangeResult<IEnumerable<SharedWithdrawal>>(Exchange, null, default);

            // Determine next token
            DateTimeToken? nextToken = null;
            if (withdrawals.Data.Count() == (request.Limit ?? 100))
                nextToken = new DateTimeToken(withdrawals.Data.Min(x => x.Time).AddMilliseconds(-1));

            return withdrawals.AsExchangeResult<IEnumerable<SharedWithdrawal>>(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, withdrawals.Data.Select(x => new SharedWithdrawal(x.Asset, x.To, x.Quantity, x.State == WithdrawalState.Success, x.Time)
            {
                Network = x.Network,
                TransactionId = x.TransactionId,
                Fee = x.Fee
            }).ToArray());
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();

        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, CryptoExchange.Net.Objects.TradingMode.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var target = request.Address;
            if (request.AddressTag != null)
                target += ":" + request.AddressTag;

            var fee = request.ExchangeParameters?.GetValue<decimal?>(Exchange, "fee");
            if (fee == null)
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("OKX requires withdrawal fee parameter. Please pass it as exchangeParameter `fee`"));

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                request.Asset,
                request.Quantity,
                WithdrawalDestination.DigitalCurrencyAddress,
                target,
                fee.Value,
                network: request.Network,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, CryptoExchange.Net.Objects.TradingMode.Spot, new SharedId(withdrawal.Data.WithdrawalId));
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>(Exchange, validationError);

            var type = (!request.ApiType.HasValue || request.ApiType == CryptoExchange.Net.Objects.TradingMode.PerpetualLinear || request.ApiType == CryptoExchange.Net.Objects.TradingMode.PerpetualInverse) ? InstrumentType.Swap : InstrumentType.Futures;
            var result = await ExchangeData.GetSymbolsAsync(type, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange, null, default);

            var data = result.Data;
            if (request.ApiType.HasValue)
                data = data.Where(x =>
                    request.ApiType == CryptoExchange.Net.Objects.TradingMode.PerpetualLinear ? (x.ContractType == ContractType.Linear && x.ExpiryTime == null) :
                    request.ApiType == CryptoExchange.Net.Objects.TradingMode.PerpetualInverse ? (x.ContractType == ContractType.Inverse && x.ExpiryTime == null) :
                    request.ApiType == CryptoExchange.Net.Objects.TradingMode.DeliveryLinear ? (x.ContractType == ContractType.Linear && x.ExpiryTime != null) :
                    (x.ContractType == ContractType.Inverse && x.ExpiryTime != null));
            return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange,
                request.ApiType == null ? FuturesApiTypes : new[] { request.ApiType.Value },
                data.Select(x => new SharedFuturesSymbol(
                x.ContractType == ContractType.Linear ? SharedSymbolType.PerpetualLinear : SharedSymbolType.PerpetualInverse,
                x.Symbol.Split(new[] { '-' })[0],
                x.Symbol.Split(new[] { '-' })[1],
                x.Symbol,
                x.State == InstrumentState.Live)
            {
                ContractSize = x.ContractValue,
                DeliveryTime = x.ExpiryTime,
                MaxTradeQuantity = x.MaxLimitQuantity,
                MinTradeQuantity = x.MinimumOrderSize,
                PriceStep = x.TickSize,
                QuantityStep = x.LotSize
            }).ToArray());
        }

        #endregion

        #region Futures Order Client

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market
            },
            new[]
            {
                SharedTimeInForce.GoodTillCanceled,
                SharedTimeInForce.ImmediateOrCancel,
                SharedTimeInForce.FillOrKill
            },
            new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts))
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.MarginMode), typeof(SharedMarginMode), "Isolated or cross margin", SharedMarginMode.Cross)
            }
        };

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.InputAsset;

        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity ?? 0,
                price: request.Price,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                reduceOnly: request.ReduceOnly,
                tradeMode: request.MarginMode == SharedMarginMode.Isolated ? Enums.TradeMode.Isolated : Enums.TradeMode.Cross,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedId(result.Data.OrderId.ToString()));
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.GetOrderDetailsAsync(request.Symbol.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedFuturesOrder(
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data.OrderType),
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderState),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                TimeInForce = ParseTimeInForce(order.Data.OrderType),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.PositionSide == PositionSide.Net ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = order.Data.ReduceOnly,
                Fee = order.Data.Fee,
                FeeAsset = order.Data.FeeAsset,
                Leverage = order.Data.Leverage
            });
        }

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var apiType = request.Symbol?.ApiType ?? request.ApiType ?? CryptoExchange.Net.Objects.TradingMode.PerpetualLinear;
            WebCallResult<IEnumerable<OKXOrder>> orders;
            if (apiType.IsPerpetual())
                orders = await Trading.GetOrdersAsync(InstrumentType.Swap, symbol: symbol, ct: ct).ConfigureAwait(false);
            else
                orders = await Trading.GetOrdersAsync(InstrumentType.Futures, symbol: symbol, ct: ct).ConfigureAwait(false);

            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, null, default);

            return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, SupportedApiTypes ,orders.Data.Select(x => new SharedFuturesOrder(
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x.OrderType),
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderState),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                PositionSide = x.PositionSide == PositionSide.Net ? null : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = x.ReduceOnly,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Leverage = x.Leverage
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationType.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            // Determine page token
            string? toId = null;
            if (pageToken is FromIdToken token)
                toId = token.FromToken;

            // Get data
            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            WebCallResult<IEnumerable<OKXOrder>> orders;
            if (request.Symbol.ApiType.IsPerpetual())
            {
                orders = await Trading.GetOrderHistoryAsync(
                    InstrumentType.Swap, 
                    symbol: symbol, 
                    startTime: request.StartTime,
                    endTime: request.EndTime,
                    limit: request.Limit ?? 100,
                    toId: toId,
                    ct: ct).ConfigureAwait(false);
            }
            else
            {
                orders = await Trading.GetOrderHistoryAsync(
                    InstrumentType.Futures, 
                    symbol: symbol,
                    startTime: request.StartTime,
                    endTime: request.EndTime,
                    limit: request.Limit ?? 100,
                    toId: toId,
                    ct: ct).ConfigureAwait(false);
            }

            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, null, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 100))
                nextToken = new FromIdToken(orders.Data.Min(o => o.OrderId!.Value).ToString());

            return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, SupportedApiTypes ,orders.Data.Select(x => new SharedFuturesOrder(
               x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x.OrderType),
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderState),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                PositionSide = x.PositionSide == PositionSide.Net ? null : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = x.ReduceOnly,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Leverage = x.Leverage
            }).ToArray(), nextToken);
        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, new ArgumentError("Invalid order id"));

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            WebCallResult<IEnumerable<OKXTransaction>> orders;
            if (request.Symbol.ApiType.IsPerpetual())
            {
                orders = await Trading.GetUserTradesArchiveAsync(
                    InstrumentType.Swap,
                    symbol: symbol,
                    orderId: orderId,
                    ct: ct).ConfigureAwait(false);
            }
            else
            {
                orders = await Trading.GetUserTradesArchiveAsync(
                    InstrumentType.Futures,
                    symbol: symbol,
                    orderId: orderId,
                    ct: ct).ConfigureAwait(false);
            }

            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

            return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, request.Symbol.ApiType,orders.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.TradeId.ToString(),
                x.QuantityFilled ?? 0,
                x.FillPrice ?? 0,
                x.FillTime)
            {
                Fee = x.Fee == null ? null : Math.Abs(x.Fee.Value),
                FeeAsset = x.FeeAsset,
                Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationType.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            string? toId = null;
            if (pageToken is FromIdToken fromIdToken)
                toId = fromIdToken.FromToken;

            // Get data
            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            WebCallResult<IEnumerable<OKXTransaction>> orders;
            if (request.Symbol.ApiType.IsPerpetual())
            {
                orders = await Trading.GetUserTradesAsync(
                    InstrumentType.Swap,
                    request.Symbol.GetSymbol(FormatSymbol),
                    startTime: request.StartTime,
                    endTime: request.EndTime,
                    limit: request.Limit ?? 100,
                    toId: toId,
                    ct: ct
                ).ConfigureAwait(false);
            }
            else
            {
                orders = await Trading.GetUserTradesAsync(
                    InstrumentType.Futures,
                    request.Symbol.GetSymbol(FormatSymbol),
                    startTime: request.StartTime,
                    endTime: request.EndTime,
                    limit: request.Limit ?? 100,
                    toId: toId,
                    ct: ct
                ).ConfigureAwait(false);
            }
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 100))
                nextToken = new FromIdToken(orders.Data.Where(x => x.BillId != null).Min(o => o.BillId!.Value - 1).ToString());

            return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, request.Symbol.ApiType,orders.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.TradeId.ToString(),
                x.QuantityFilled ?? 0,
                x.FillPrice ?? 0,
                x.FillTime)
            {
                Fee = x.Fee == null ? null : Math.Abs(x.Fee.Value),
                FeeAsset = x.FeeAsset,
                Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray(), nextToken);
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol.GetSymbol(FormatSymbol), orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedId(order.Data.OrderId.ToString()));
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedPosition>>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedPosition>>(Exchange, validationError);

            var result = await Account.GetPositionsAsync(symbol: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, request.Symbol == null ? SupportedApiTypes : new[] { request.Symbol.ApiType }, result.Data.Select(x => new SharedPosition(x.Symbol, Math.Abs(x.PositionsQuantity ?? 0), x.UpdateTime)
            {
                UnrealizedPnl = x.UnrealizedPnl,
                LiquidationPrice = x.LiquidationPrice,
                Leverage = x.Leverage,
                AverageEntryPrice = x.AveragePrice,
                PositionSide = x.PositionSide == PositionSide.Net ? (x.PositionsQuantity >= 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
            }).ToArray());
        }

        EndpointOptions<ClosePositionRequest> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest>(true)
        {
            RequestNotes = "No order id returned by the API for this",
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(ClosePositionRequest.MarginMode), typeof(SharedMarginMode), "Cross or isolated margin position", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.ClosePositionAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedId(string.Empty));
        }

        #endregion

        #region Leverage client

        EndpointOptions<GetLeverageRequest> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(GetLeverageRequest.MarginMode), typeof(SharedMarginMode), "Cross or isolated margin", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).GetLeverageOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.GetLeverageAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            var side = request.Side == null ? result.Data.First() : result.Data.FirstOrDefault(d => d.PositionSide == (request.Side == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long));
            return result.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedLeverage(side.Leverage ?? 0)
            {
                MarginMode = side.MarginMode == MarginMode.Isolated ? SharedMarginMode.Isolated : SharedMarginMode.Cross,
                Side = side.PositionSide == PositionSide.Net ? null : side.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short
            });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions(false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(GetLeverageRequest.MarginMode), typeof(SharedMarginMode), "Cross or isolated margin", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).SetLeverageOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.SetLeverageAsync(
                (int)request.Leverage,
                request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
                symbol: request.Symbol.GetSymbol(FormatSymbol),
                positionSide: request.Side == null ? null: request.Side == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedLeverage(result.Data.First().Leverage ?? 0)
                {
                    Side = request.Side
                });
        }
        #endregion

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationType.Descending, false)
        {
            MaxRequestDataPoints = 100
        };

        async Task<ExchangeWebResult<IEnumerable<SharedMarkKline>>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 100;
            if (startTime == null || startTime < endTime)
            {
                var offset = (int)interval * limit;
                startTime = endTime.AddSeconds(-offset);
            }

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                startTime,
                endTime,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.Time);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, request.Symbol.ApiType, result.Data.Select(x => new SharedMarkKline(x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice)).ToArray(), nextToken);
        }

        #endregion

        #region Mark Klines client

        GetKlinesOptions IMarkPriceKlineRestClient.GetMarkPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationType.Descending, false)
        {
            MaxRequestDataPoints = 100
        };

        async Task<ExchangeWebResult<IEnumerable<SharedMarkKline>>> IMarkPriceKlineRestClient.GetMarkPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IMarkPriceKlineRestClient)this).GetMarkPriceKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 100;
            if (startTime == null || startTime < endTime)
            {
                var offset = (int)interval * limit;
                startTime = endTime.AddSeconds(-offset);
            }

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                startTime,
                endTime,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.Time);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, request.Symbol.ApiType, result.Data.Select(x => new SharedMarkKline(x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice)).ToArray(), nextToken);
        }

        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            WebCallResult<IEnumerable<OKXOpenInterest>> result;
            if (request.Symbol.ApiType.IsPerpetual())
                result = await ExchangeData.GetOpenInterestsAsync(InstrumentType.Swap, symbol: request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            else
                result = await ExchangeData.GetOpenInterestsAsync(InstrumentType.Futures, symbol: request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.ApiType, new SharedOpenInterest(result.Data.First().OpenInterest ?? 0));
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(SharedPaginationType.Descending,false);

        async Task<ExchangeWebResult<IEnumerable<SharedFundingRate>>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFundingRate>>(Exchange, validationError);

            DateTime? fromTime = null;
            if (pageToken is DateTimeToken token)
                fromTime = token.LastTime;

            // Get data
            var result = await ExchangeData.GetFundingRateHistoryAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: fromTime ?? request.EndTime,
                limit: 100,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFundingRate>>(Exchange, null, default);

            DateTimeToken? nextToken = null;
            if (result.Data.Count() == 100)
                nextToken = new DateTimeToken(result.Data.Min(x => x.FundingTime));

            // Return
            return result.AsExchangeResult<IEnumerable<SharedFundingRate>>(Exchange, request.Symbol.ApiType,result.Data.Select(x => new SharedFundingRate(x.FundingRate, x.FundingTime)).ToArray(), nextToken);
        }
        #endregion

        #region Futures Ticker client

        EndpointOptions<GetTickerRequest> IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var resultTicker = ExchangeData.GetTickerAsync(symbol, ct: ct);
            var resultFunding = ExchangeData.GetFundingRatesAsync(symbol: symbol, ct: ct);
            var resultMarkPrice = ExchangeData.GetMarkPricesAsync(request.Symbol.ApiType.IsPerpetual() ? InstrumentType.Swap: InstrumentType.Futures, symbol: symbol, ct: ct);
            var resultIndexPrice = ExchangeData.GetIndexTickersAsync(symbol: symbol.Split('-')[0] + "-" +symbol.Split('-')[1], ct: ct);
            await Task.WhenAll(resultTicker, resultFunding).ConfigureAwait(false);
            if (!resultTicker.Result)
                return resultTicker.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);
            if (!resultIndexPrice.Result)
                return resultIndexPrice.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);
            if (!resultMarkPrice.Result)
                return resultMarkPrice.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);

            var funding = resultFunding.Result.Data?.SingleOrDefault();
            var index = resultIndexPrice.Result.Data.Single();
            var mark = resultMarkPrice.Result.Data.Single();
            return resultTicker.Result.AsExchangeResult(Exchange,
                request.Symbol.ApiType,
                new SharedFuturesTicker(
                    resultTicker.Result.Data.Symbol,
                    resultTicker.Result.Data.LastPrice ?? 0,
                    resultTicker.Result.Data.HighPrice ?? 0,
                    resultTicker.Result.Data.LowPrice ?? 0,
                    resultTicker.Result.Data.Volume,
                    resultTicker.Result.Data.OpenPrice == null ? null : Math.Round((resultTicker.Result.Data.LastPrice ?? 0) / resultTicker.Result.Data.OpenPrice.Value * 100 - 100, 2))
                {
                    IndexPrice = index.IndexPrice,
                    MarkPrice = mark.MarkPrice,
                    FundingRate = funding?.FundingRate,
                    NextFundingTime = funding?.NextFundingTime
                });
        }

        EndpointOptions<GetTickersRequest> IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesTicker>>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, request.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesTicker>>(Exchange, validationError);

            var type = !request.ApiType.HasValue || request.ApiType == CryptoExchange.Net.Objects.TradingMode.PerpetualLinear || request.ApiType == CryptoExchange.Net.Objects.TradingMode.PerpetualInverse ? InstrumentType.Swap : InstrumentType.Futures;
            var resultTickers = ExchangeData.GetTickersAsync(type, ct: ct);
            var resultMarkPrice = ExchangeData.GetMarkPricesAsync(type, ct: ct);
            await Task.WhenAll(resultTickers, resultMarkPrice).ConfigureAwait(false);
            if (!resultTickers.Result)
                return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, null, default);
            if (!resultMarkPrice.Result)
                return resultMarkPrice.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, null, default);

            return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange,
                type == InstrumentType.Swap ? new[] { CryptoExchange.Net.Objects.TradingMode.PerpetualLinear, CryptoExchange.Net.Objects.TradingMode.PerpetualInverse } : new[] { CryptoExchange.Net.Objects.TradingMode.DeliveryLinear, CryptoExchange.Net.Objects.TradingMode.DeliveryInverse },
                resultTickers.Result.Data.Select(x =>
            {
                var markPrice = resultMarkPrice.Result.Data.Single(p => p.Symbol == x.Symbol);
                return new SharedFuturesTicker(x.Symbol, x.LastPrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume, x.OpenPrice == null ? null : Math.Round((x.LastPrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
                {
                    MarkPrice = markPrice.MarkPrice
                };
            }).ToArray());
        }

        #endregion

        #region Position Mode client

        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions(false);
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).GetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.GetAccountConfigurationAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, FuturesApiTypes, new SharedPositionModeResult(result.Data.PositionMode == PositionMode.LongShortMode ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions(true, true, false);
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).SetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.SetPositionModeAsync(request.Mode == SharedPositionMode.HedgeMode ? PositionMode.LongShortMode : PositionMode.NetMode, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, FuturesApiTypes, new SharedPositionModeResult(request.Mode));
        }
        #endregion

        #region Position History client

        GetPositionHistoryOptions IPositionHistoryRestClient.GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(false, SharedPaginationType.Descending);
        async Task<ExchangeWebResult<IEnumerable<SharedPositionHistory>>> IPositionHistoryRestClient.GetPositionHistoryAsync(GetPositionHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IPositionHistoryRestClient)this).GetPositionHistoryOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedPositionHistory>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTime = null;
            int pageSize = request.Limit ?? 100;
            if (pageToken is DateTimeToken token)
                fromTime = token.LastTime;

            // Get data
            var orders = await Account.GetPositionHistoryAsync(
                symbol: request.Symbol?.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: fromTime ?? request.EndTime,
                limit: pageSize,
                ct: ct
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedPositionHistory>>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Count() == pageSize)
                nextToken = new DateTimeToken(orders.Data.Min(x => x.UpdateTime).AddMilliseconds(-1));

            return orders.AsExchangeResult<IEnumerable<SharedPositionHistory>>(Exchange, request.Symbol.ApiType, orders.Data.Select(x => new SharedPositionHistory(
                x.Symbol,
                x.Direction == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                x.OpenAveragePrice ?? 0,
                x.CloseAveragePrice ?? 0,
                x.CloseTotalPos ?? 0,
                x.ProfitAndLoss ?? 0,
                x.UpdateTime)
            {
                Leverage = x.Leverage,
                PositionId = x.PositionId.ToString()
            }).ToArray(), nextToken);
        }
        #endregion
    }
}
