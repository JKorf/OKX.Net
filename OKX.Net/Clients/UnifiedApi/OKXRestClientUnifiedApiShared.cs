using OKX.Net.Interfaces.Clients.UnifiedApi;
using CryptoExchange.Net.SharedApis;
using OKX.Net.Enums;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Trade;
using OKX.Net.Objects.Public;
using System.Drawing;
using CryptoExchange.Net.Objects.Errors;

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXRestClientUnifiedApi : IOKXRestClientUnifiedApiShared
    {
        private const string _topicSpotId = "OKXSpot";
        private const string _topicFuturesId = "OKXFutures";

        public string Exchange => OKXExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryLinear, TradingMode.DeliveryInverse };
        private TradingMode[] FuturesApiTypes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryInverse };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, true, 100, false,
            SharedKlineInterval.OneMinute,
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
            SharedKlineInterval.OneMonth);
        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

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
            WebCallResult<OKXKline[]> result;
            if (endTime > DateTime.UtcNow.AddSeconds(-(2 * (int)request.Interval)))
            {
                // The last Kline is delayed on the history endpoint so when retrieving the most recent klines use the non-history endpoint
                result = await ExchangeData.GetKlinesAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
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
                    request.Symbol!.GetSymbol(FormatSymbol),
                    interval,
                    startTime,
                    endTime,
                    limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }

            if (!result)
                return result.AsExchangeResult<SharedKline[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.Time);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<SharedKline[]>(Exchange, request.Symbol.TradingMode, result.Data.Select(x => new SharedKline(x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)).ToArray(), nextToken);
        }

        #endregion

        #region Spot Symbol client

        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(InstrumentType.Spot, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, null, default);

            var response = result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, TradingMode.Spot, result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.State == InstrumentState.Live)
            {
                MaxTradeQuantity = Math.Min(s.MaxLimitQuantity ?? decimal.MaxValue, s.MaxMarketQuantity ?? decimal.MaxValue),
                MinTradeQuantity = s.MinimumOrderSize,
                QuantityStep = s.LotSize,
                PriceStep = s.TickSize
            }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicSpotId, response.Data);
            return response;
        }

        #endregion

        #region Spot Ticker client

        EndpointOptions<GetTickerRequest> ISpotTickerRestClient.GetSpotTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicSpotId, symbol), symbol, result.Data.LastPrice ?? 0, result.Data.HighPrice ?? 0, result.Data.LowPrice ?? 0, result.Data.Volume, result.Data.OpenPrice == null ? null : Math.Round((result.Data.LastPrice ?? 0) / result.Data.OpenPrice.Value * 100 - 100, 2))
            {
                QuoteVolume = result.Data.QuoteVolume
            });
        }

        EndpointOptions<GetTickersRequest> ISpotTickerRestClient.GetSpotTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(InstrumentType.Spot, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol), x.Symbol, x.LastPrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume, x.OpenPrice == null ? null : Math.Round((x.LastPrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
            {
                QuoteVolume = x.QuoteVolume
            }).ToArray());
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest>(false);
        async Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IBookTickerRestClient)this).GetBookTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedBookTicker>(Exchange, null, default);

            return resultTicker.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(request.Symbol.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, resultTicker.Data.Symbol),
                resultTicker.Data.Symbol,
                resultTicker.Data.BestAskPrice ?? 0,
                resultTicker.Data.BestAskQuantity ?? 0,
                resultTicker.Data.BestBidPrice ?? 0,
                resultTicker.Data.BestBidQuantity ?? 0));
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(100, false);
        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var result = await ExchangeData.GetTradeHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol.TradingMode, result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Time)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Balance client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            var result = await Account.GetAccountBalanceAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedBalance[]>(Exchange, SupportedTradingModes, result.Data.Details.Select(x => new SharedBalance(x.Asset, x.AvailableBalance ?? 0, x.Equity ?? 0)).ToArray());
        }

        #endregion

        #region Spot Order client

        SharedOrderType[] ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.OutputAsset;

        string ISpotOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions();

        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.TradingMode,
                new[] { TradingMode.Spot },
                ((ISpotOrderRestClient)this).SpotSupportedOrderTypes,
                ((ISpotOrderRestClient)this).SpotSupportedTimeInForce,
                ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset ?? 0,
                price: request.Price,
                quantityAsset: request.Quantity?.QuantityInBaseAsset > 0 ? QuantityAsset.BaseAsset : QuantityAsset.QuoteAsset,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()!));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, new[] { TradingMode.Spot });
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest), "Invalid order id"));

            var order = await Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString()!,
                ParseOrderType(order.Data.OrderType),
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderState),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.QuantityType == QuantityAsset.QuoteAsset ? null : order.Data.Quantity, order.Data.QuantityType == QuantityAsset.QuoteAsset ? order.Data.Quantity : null),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.OrderType),
                UpdateTime = order.Data.UpdateTime,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                Fee = Math.Abs(order.Data.Fee ?? 0),
                FeeAsset = order.Data.FeeAsset
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, new[] { TradingMode.Spot });
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var order = await Trading.GetOrdersAsync(symbol: symbol, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, order.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                ParseOrderType(x.OrderType),
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderState),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(x.QuantityType == QuantityAsset.QuoteAsset ? null : x.Quantity, x.QuantityType == QuantityAsset.QuoteAsset ? x.Quantity : null),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                Fee = Math.Abs(x.Fee ?? 0),
                FeeAsset = x.FeeAsset
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true, 100, true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.TradingMode, new[] { TradingMode.Spot });
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            string? fromId = null;
            if (pageToken is FromIdToken token)
                fromId = token.FromToken;

            var limit = request.Limit ?? 100;
            var order = await Trading.GetOrderArchiveAsync(
                InstrumentType.Spot,
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: limit,
                toId: fromId,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            FromIdToken? nextPageToken = null;
            if (order.Data.Count() == limit)
                nextPageToken = new FromIdToken(order.Data.Min(x => x.OrderId).ToString()!);

            return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, order.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                ParseOrderType(x.OrderType),
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderState),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(x.QuantityType == QuantityAsset.QuoteAsset ? null : x.Quantity, x.QuantityType == QuantityAsset.QuoteAsset ? x.Quantity : null),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                Fee = Math.Abs(x.Fee ?? 0),
                FeeAsset = x.FeeAsset
            }).ToArray(), nextPageToken);
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, new[] { TradingMode.Spot });
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest), "Invalid order id"));

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var order = await Trading.GetUserTradesAsync(InstrumentType.Spot, symbol, orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return order.AsExchangeResult<SharedUserTrade[]>(Exchange, TradingMode.Spot, order.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                x.TradeId.ToString()!,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.QuantityFilled ?? 0,
                x.FillPrice ?? 0,
                x.FillTime)
            {
                Fee = x.Fee.HasValue ? Math.Abs(x.Fee.Value) : null,
                FeeAsset = x.FeeAsset,
                Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationSupport.Descending, true, 100, true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, INextPageToken? nextPageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, new[] { TradingMode.Spot });
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            string? fromId = null;
            if (nextPageToken is FromIdToken fromIdToken)
                fromId = fromIdToken.FromToken;

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var order = await Trading.GetUserTradesAsync(
                InstrumentType.Spot,
                symbol,
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 100,
                fromId: fromId,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (order.Data.Count() == (request.Limit ?? 100))
                nextToken = new FromIdToken(order.Data.Max(o => o.TradeId).ToString()!);

            return order.AsExchangeResult<SharedUserTrade[]>(Exchange, TradingMode.Spot, order.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                x.TradeId.ToString()!,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
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
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, new[] { TradingMode.Spot });
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
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
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await Account.GetAssetsAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            if (!assets.Data.Any())
                return assets.AsExchangeError<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return assets.AsExchangeResult<SharedAsset>(Exchange, TradingMode.Spot, new SharedAsset(request.Asset)
            {
                FullName = assets.Data.First().Name,
                Networks = assets.Data.Select(x => new SharedAssetNetwork(x.Network)
                {
                    MinConfirmations = x.MinDepositConfirmations,
                    DepositEnabled = x.AllowDeposit,
                    MinWithdrawQuantity = x.MinimumWithdrawalAmount,
                    WithdrawEnabled = x.AllowWithdrawal,
                    WithdrawFee = x.MinimumWithdrawalFee,
                    ContractAddress = x.ContractAddress,
                }).ToArray()
            });
        }

        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(true);
        async Task<ExchangeWebResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset[]>(Exchange, validationError);

            var assets = await Account.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset[]>(Exchange, null, default);

            return assets.AsExchangeResult<SharedAsset[]>(Exchange, TradingMode.Spot, assets.Data.GroupBy(x => x.Asset).Select(x => new SharedAsset(x.Key)
            {
                FullName = x.First().Name,
                Networks = x.Select(x => new SharedAssetNetwork(x.Network)
                {
                    MinConfirmations = x.MinDepositConfirmations,
                    DepositEnabled = x.AllowDeposit,
                    MinWithdrawQuantity = x.MinimumWithdrawalAmount,
                    WithdrawEnabled = x.AllowWithdrawal,
                    WithdrawFee = x.MinimumWithdrawalFee,
                    ContractAddress = x.ContractAddress,
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 400, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                depth: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, null, default);

            return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, TradingMode.Spot, depositAddresses.Data.Where(x => request.Network == null ? true : x.Network == request.Network).Select(x => new SharedDepositAddress(request.Asset, x.Address)
            {
                TagOrMemo = x.Memo,
                Network = x.Network
            }
            ).ToArray());
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(SharedPaginationSupport.Descending, true, 100);
        async Task<ExchangeWebResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDeposit[]>(Exchange, validationError);

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
                return deposits.AsExchangeResult<SharedDeposit[]>(Exchange, null, default);

            // Determine next token
            DateTimeToken? nextToken = null;
            if (deposits.Data.Count() == (request.Limit ?? 100))
                nextToken = new DateTimeToken(deposits.Data.Min(x => x.Time).AddMilliseconds(-1));

            return deposits.AsExchangeResult<SharedDeposit[]>(Exchange, TradingMode.Spot, deposits.Data.Select(x => new SharedDeposit(x.Asset, x.Quantity, x.State == DepositState.Successful, x.Time)
            {
                Network = x.Network,
                TransactionId = x.TransactionId
            }).ToArray(), nextToken);
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(SharedPaginationSupport.Descending, true, 100);
        async Task<ExchangeWebResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedWithdrawal[]>(Exchange, validationError);

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
                return withdrawals.AsExchangeResult<SharedWithdrawal[]>(Exchange, null, default);

            // Determine next token
            DateTimeToken? nextToken = null;
            if (withdrawals.Data.Count() == (request.Limit ?? 100))
                nextToken = new DateTimeToken(withdrawals.Data.Min(x => x.Time).AddMilliseconds(-1));

            return withdrawals.AsExchangeResult<SharedWithdrawal[]>(Exchange, TradingMode.Spot, withdrawals.Data.Select(x => new SharedWithdrawal(x.Asset, x.To, x.Quantity, x.State == WithdrawalState.Success, x.Time)
            {
                Network = x.Network,
                TransactionId = x.TransactionId,
                Fee = x.Fee
            }).ToArray());
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions()
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("withdrawFee", typeof(decimal), "Fee to use for the withdrawal", 0.001m)
            }
        };

        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var target = request.Address;
            if (request.AddressTag != null)
                target += ":" + request.AddressTag;

            var fee = ExchangeParameters.GetValue<decimal>(request.ExchangeParameters, Exchange, "withdrawFee");

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                request.Asset,
                request.Quantity,
                WithdrawalDestination.DigitalCurrencyAddress,
                target,
                fee,
                network: request.Network,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(withdrawal.Data.WithdrawalId));
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesSymbol[]>(Exchange, validationError);

            var type = (!request.TradingMode.HasValue || request.TradingMode == TradingMode.PerpetualLinear || request.TradingMode == TradingMode.PerpetualInverse) ? InstrumentType.Swap : InstrumentType.Futures;
            var result = await ExchangeData.GetSymbolsAsync(type, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange, null, default);

            IEnumerable<OKXInstrument> data = result.Data;
            if (request.TradingMode.HasValue)
                data = data.Where(x =>
                    request.TradingMode == TradingMode.PerpetualLinear ? (x.ContractType == ContractType.Linear && x.ExpiryTime == null) :
                    request.TradingMode == TradingMode.PerpetualInverse ? (x.ContractType == ContractType.Inverse && x.ExpiryTime == null) :
                    request.TradingMode == TradingMode.DeliveryLinear ? (x.ContractType == ContractType.Linear && x.ExpiryTime != null) :
                    (x.ContractType == ContractType.Inverse && x.ExpiryTime != null));
            
            var response = result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange,
                request.TradingMode == null ? FuturesApiTypes : new[] { request.TradingMode.Value },
                data.Select(x => new SharedFuturesSymbol(
                x.ContractType == ContractType.Linear ? TradingMode.PerpetualLinear : TradingMode.PerpetualInverse,
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
                    QuantityStep = x.LotSize,
                    MaxLongLeverage = x.MaximumLeverage,
                    MaxShortLeverage = x.MaximumLeverage
                }).ToArray());


            ExchangeSymbolCache.UpdateSymbolInfo(_topicSpotId, response.Data);
            return response;
        }

        #endregion

        #region Futures Order Client

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        SharedOrderType[] IFuturesOrderRestClient.FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        SharedTimeInForce[] IFuturesOrderRestClient.FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport IFuturesOrderRestClient.FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        string IFuturesOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.MarginMode), typeof(SharedMarginMode), "Isolated or cross margin", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.TradingMode,
                SupportedTradingModes,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderTypes,
                ((IFuturesOrderRestClient)this).FuturesSupportedTimeInForce,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity?.QuantityInContracts ?? 0,
                price: request.Price,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                reduceOnly: request.ReduceOnly,
                tradeMode: request.MarginMode == SharedMarginMode.Isolated ? Enums.TradeMode.Isolated : Enums.TradeMode.Cross,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId.ToString()!));
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest), "Invalid order id"));

            var order = await Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, order.Data.Symbol), 
                order.Data.Symbol,
                order.Data.OrderId.ToString()!,
                ParseOrderType(order.Data.OrderType),
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderState),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.OrderType),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.PositionSide == PositionSide.Net ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = order.Data.ReduceOnly,
                Fee = order.Data.Fee,
                FeeAsset = order.Data.FeeAsset,
                Leverage = order.Data.Leverage,
                TakeProfitPrice = order.Data.TakeProfitOrderPrice,
                StopLossPrice = order.Data.StopLossOrderPrice
            });
        }

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var tradingMode = request.Symbol?.TradingMode ?? request.TradingMode ?? TradingMode.PerpetualLinear;
            WebCallResult<OKXOrder[]> orders;
            if (tradingMode.IsPerpetual())
                orders = await Trading.GetOrdersAsync(InstrumentType.Swap, symbol: symbol, ct: ct).ConfigureAwait(false);
            else
                orders = await Trading.GetOrdersAsync(InstrumentType.Futures, symbol: symbol, ct: ct).ConfigureAwait(false);

            if (!orders)
                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                ParseOrderType(x.OrderType),
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderState),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                PositionSide = x.PositionSide == PositionSide.Net ? null : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = x.ReduceOnly,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Leverage = x.Leverage,
                TakeProfitPrice = x.TakeProfitOrderPrice,
                StopLossPrice = x.StopLossOrderPrice
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true, 100, true);
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            string? toId = null;
            if (pageToken is FromIdToken token)
                toId = token.FromToken;

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            WebCallResult<OKXOrder[]> orders;
            if (request.Symbol.TradingMode.IsPerpetual())
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
                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 100))
                nextToken = new FromIdToken(orders.Data.Min(o => o.OrderId!.Value).ToString());

            return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, SupportedTradingModes, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                ParseOrderType(x.OrderType),
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderState),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime,
                PositionSide = x.PositionSide == PositionSide.Net ? null : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = x.ReduceOnly,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Leverage = x.Leverage,
                TakeProfitPrice = x.TakeProfitOrderPrice,
                StopLossPrice = x.StopLossOrderPrice
            }).ToArray(), nextToken);
        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest), "Invalid order id"));

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            WebCallResult<OKXTransaction[]> orders;
            if (request.Symbol.TradingMode.IsPerpetual())
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
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol.TradingMode, orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                x.TradeId.ToString()!,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.QuantityFilled ?? 0,
                x.FillPrice ?? 0,
                x.FillTime)
            {
                Fee = x.Fee == null ? null : Math.Abs(x.Fee.Value),
                FeeAsset = x.FeeAsset,
                Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationSupport.Descending, true, 100, true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            string? toId = null;
            if (pageToken is FromIdToken fromIdToken)
                toId = fromIdToken.FromToken;

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            WebCallResult<OKXTransaction[]> orders;
            if (request.Symbol.TradingMode.IsPerpetual())
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
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 100))
                nextToken = new FromIdToken(orders.Data.Where(x => x.BillId != null).Min(o => o.BillId!.Value - 1).ToString());

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol.TradingMode, orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                x.TradeId.ToString()!,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
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
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(order.Data.OrderId.ToString()!));
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true);
        async Task<ExchangeWebResult<SharedPosition[]>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPosition[]>(Exchange, validationError);

            var result = await Account.GetPositionsAsync(symbol: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPosition[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedPosition[]>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, result.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), x.Symbol, Math.Abs(x.PositionsQuantity ?? 0), x.UpdateTime)
            {
                UnrealizedPnl = x.UnrealizedPnl,
                LiquidationPrice = x.LiquidationPrice,
                Leverage = x.Leverage,
                AverageOpenPrice = x.AveragePrice,
                PositionSide = x.PositionSide == PositionSide.Net ? (x.PositionsQuantity >= 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                StopLossPrice = x.CloseOrderAlgo.FirstOrDefault(x => x.StopLossTriggerPrice > 0)?.StopLossTriggerPrice,
                TakeProfitPrice = x.CloseOrderAlgo.FirstOrDefault(x => x.TakeProfitTriggerPrice > 0)?.TakeProfitTriggerPrice
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
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.ClosePositionAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(string.Empty));
        }

        #endregion

        #region Leverage client
        SharedLeverageSettingMode ILeverageRestClient.LeverageSettingType => SharedLeverageSettingMode.PerSide;

        EndpointOptions<GetLeverageRequest> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(GetLeverageRequest.MarginMode), typeof(SharedMarginMode), "Cross or isolated margin", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).GetLeverageOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.GetLeverageAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            var side = request.PositionSide == null ? result.Data.First() : result.Data.FirstOrDefault(d => d.PositionSide == (request.PositionSide == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long));
            if (side == null)
                return result.AsExchangeError<SharedLeverage>(Exchange, new ServerError(new ErrorInfo(ErrorType.NoPosition, "Position not found")));
            
            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedLeverage(side.Leverage ?? 0)
            {
                MarginMode = side.MarginMode == MarginMode.Isolated ? SharedMarginMode.Isolated : SharedMarginMode.Cross,
                Side = side.PositionSide == PositionSide.Net ? null : side.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short
            });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions()
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(GetLeverageRequest.MarginMode), typeof(SharedMarginMode), "Cross or isolated margin", SharedMarginMode.Cross)
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).SetLeverageOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.SetLeverageAsync(
                (int)request.Leverage,
                request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                positionSide: request.Side == null ? null : request.Side == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedLeverage(result.Data.First().Leverage ?? 0)
            {
                Side = request.Side
            });
        }
        #endregion

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, true, 100, false);

        async Task<ExchangeWebResult<SharedFuturesKline[]>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, validationError);

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
                request.Symbol!.GetSymbol(FormatSymbol),
                interval,
                startTime,
                endTime,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.Time);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, request.Symbol.TradingMode, result.Data.Select(x => new SharedFuturesKline(x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice)).ToArray(), nextToken);
        }

        #endregion

        #region Mark Klines client

        GetKlinesOptions IMarkPriceKlineRestClient.GetMarkPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, true, 100, false);

        async Task<ExchangeWebResult<SharedFuturesKline[]>> IMarkPriceKlineRestClient.GetMarkPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IMarkPriceKlineRestClient)this).GetMarkPriceKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, validationError);

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
                request.Symbol!.GetSymbol(FormatSymbol),
                interval,
                startTime,
                endTime,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.Time);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, request.Symbol.TradingMode, result.Data.Select(x => new SharedFuturesKline(x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice)).ToArray(), nextToken);
        }

        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            WebCallResult<OKXOpenInterest[]> result;
            if (request.Symbol!.TradingMode.IsPerpetual())
                result = await ExchangeData.GetOpenInterestsAsync(InstrumentType.Swap, symbol: request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            else
                result = await ExchangeData.GetOpenInterestsAsync(InstrumentType.Futures, symbol: request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOpenInterest(result.Data.First().OpenInterest ?? 0));
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(SharedPaginationSupport.Descending, true, 400, false);

        async Task<ExchangeWebResult<SharedFundingRate[]>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFundingRate[]>(Exchange, validationError);

            DateTime? fromTime = null;
            if (pageToken is DateTimeToken token)
                fromTime = token.LastTime;

            // Get data
            var result = await ExchangeData.GetFundingRateHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: fromTime ?? request.EndTime,
                limit: 400,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFundingRate[]>(Exchange, null, default);

            DateTimeToken? nextToken = null;
            if (result.Data.Count() == 400)
                nextToken = new DateTimeToken(result.Data.Min(x => x.FundingTime));

            // Return
            return result.AsExchangeResult<SharedFundingRate[]>(Exchange, request.Symbol.TradingMode, result.Data.Select(x => new SharedFundingRate(x.FundingRate, x.FundingTime)).ToArray(), nextToken);
        }
        #endregion

        #region Futures Ticker client

        EndpointOptions<GetTickerRequest> IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = ExchangeData.GetTickerAsync(symbol, ct: ct);
            var resultFunding = ExchangeData.GetFundingRatesAsync(symbol: symbol, ct: ct);
            var resultMarkPrice = ExchangeData.GetMarkPricesAsync(request.Symbol.TradingMode.IsPerpetual() ? InstrumentType.Swap : InstrumentType.Futures, symbol: symbol, ct: ct);
            var resultIndexPrice = ExchangeData.GetIndexTickersAsync(symbol: symbol.Split('-')[0] + "-" + symbol.Split('-')[1], ct: ct);
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
                request.Symbol.TradingMode,
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, resultTicker.Result.Data.Symbol),
                    resultTicker.Result.Data.Symbol,
                    resultTicker.Result.Data.LastPrice,
                    resultTicker.Result.Data.HighPrice,
                    resultTicker.Result.Data.LowPrice,
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
        async Task<ExchangeWebResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker[]>(Exchange, validationError);

            var type = !request.TradingMode.HasValue || request.TradingMode == TradingMode.PerpetualLinear || request.TradingMode == TradingMode.PerpetualInverse ? InstrumentType.Swap : InstrumentType.Futures;
            var resultTickers = ExchangeData.GetTickersAsync(type, ct: ct);
            var resultMarkPrice = ExchangeData.GetMarkPricesAsync(type, ct: ct);
            await Task.WhenAll(resultTickers, resultMarkPrice).ConfigureAwait(false);
            if (!resultTickers.Result)
                return resultTickers.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);
            if (!resultMarkPrice.Result)
                return resultMarkPrice.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);

            return resultTickers.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange,
                type == InstrumentType.Swap ? new[] { TradingMode.PerpetualLinear, TradingMode.PerpetualInverse } : new[] { TradingMode.DeliveryLinear, TradingMode.DeliveryInverse },
                resultTickers.Result.Data.Select(x =>
            {
                var markPrice = resultMarkPrice.Result.Data.Single(p => p.Symbol == x.Symbol);
                return new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.OpenPrice == null ? null : Math.Round((x.LastPrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
                {
                    MarkPrice = markPrice.MarkPrice
                };
            }).ToArray());
        }

        #endregion

        #region Position Mode client

        SharedPositionModeSelection IPositionModeRestClient.PositionModeSettingType => SharedPositionModeSelection.PerAccount;
        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions();
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).GetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.GetAccountConfigurationAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, FuturesApiTypes, new SharedPositionModeResult(result.Data.PositionMode == PositionMode.LongShortMode ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions();
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).SetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.SetPositionModeAsync(request.PositionMode == SharedPositionMode.HedgeMode ? PositionMode.LongShortMode : PositionMode.NetMode, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, FuturesApiTypes, new SharedPositionModeResult(request.PositionMode));
        }
        #endregion

        #region Position History client

        GetPositionHistoryOptions IPositionHistoryRestClient.GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(SharedPaginationSupport.Descending, true, 100);
        async Task<ExchangeWebResult<SharedPositionHistory[]>> IPositionHistoryRestClient.GetPositionHistoryAsync(GetPositionHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IPositionHistoryRestClient)this).GetPositionHistoryOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, FuturesApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionHistory[]>(Exchange, validationError);

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
                return orders.AsExchangeResult<SharedPositionHistory[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Count() == pageSize)
                nextToken = new DateTimeToken(orders.Data.Min(x => x.UpdateTime).AddMilliseconds(-1));

            return orders.AsExchangeResult<SharedPositionHistory[]>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol.TradingMode }, orders.Data.Select(x => new SharedPositionHistory(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
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

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var type = request.Symbol!.TradingMode == TradingMode.Spot ? InstrumentType.Spot : request.Symbol.TradingMode == TradingMode.PerpetualLinear || request.Symbol.TradingMode == TradingMode.PerpetualInverse ? InstrumentType.Swap : InstrumentType.Futures;
            var result = await Account.GetFeeRatesAsync(type,
                type == InstrumentType.Spot ? request.Symbol!.GetSymbol(FormatSymbol) : null,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFee(Math.Abs(result.Data.Maker ?? 0) * 100, Math.Abs(result.Data.Taker ?? 0) * 100));
        }
        #endregion

        #region Spot Trigger Order Client

        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(false)
        {
        };
        async Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).PlaceSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes, ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceAlgoOrderAsync(
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
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(result.Data.AlgoOrderId!));
        }


        EndpointOptions<GetOrderRequest> ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
        };
        async Task<ExchangeWebResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).GetSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTriggerOrder>(Exchange, validationError);

            var order = await Trading.GetAlgoOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotTriggerOrder>(Exchange, null, default);

            OKXOrder? orderDetails = null;
            if (order.Data.OrderIdList?.Any() == true)
            {
                var orderDetailsRequest = await Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), order.Data.OrderIdList.First(), ct: ct).ConfigureAwait(false);
                if (!orderDetailsRequest)
                    return orderDetailsRequest.AsExchangeResult<SharedSpotTriggerOrder>(Exchange, null, default);

                orderDetails = orderDetailsRequest.Data;
            }

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, order.Data.Symbol),
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

            return SharedTriggerOrderStatus.Active;
        }

        EndpointOptions<CancelOrderRequest> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).CancelSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelAlgoOrderAsync(
                [new OKXAlgoOrderRequest { Symbol = request.Symbol!.GetSymbol(FormatSymbol), AlgoOrderId = request.OrderId }],
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(request.OrderId));
        }

        #endregion

        #region Futures Trigger Order Client
        PlaceFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.MarginMode), typeof(SharedMarginMode), "Margin mode to use", SharedMarginMode.Cross),
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.PositionMode), typeof(SharedPositionMode), "Position mode the account is in", SharedPositionMode.HedgeMode)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = ((IFuturesTriggerOrderRestClient)this).PlaceFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes, side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceAlgoOrderAsync(
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
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.AlgoOrderId!));
        }

        private OrderSide GetTriggerOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Buy : OrderSide.Sell;
        
            return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Sell : OrderSide.Buy;
        }

        EndpointOptions<GetOrderRequest> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
        };
        async Task<ExchangeWebResult<SharedFuturesTriggerOrder>> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTriggerOrderRestClient)this).GetFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTriggerOrder>(Exchange, validationError);

            var order = await Trading.GetAlgoOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

            OKXOrder? orderDetails = null;
            if (order.Data.OrderIdList?.Any() == true)
            {
                var orderDetailsRequest = await Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), order.Data.OrderIdList.First(), ct: ct).ConfigureAwait(false);
                if (!orderDetailsRequest)
                    return orderDetailsRequest.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

                orderDetails = orderDetailsRequest.Data;
            }

            return order.AsExchangeResult(Exchange, request.TradingMode, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, order.Data.Symbol),
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

        EndpointOptions<CancelOrderRequest> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTriggerOrderRestClient)this).CancelFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelAlgoOrderAsync(
                [new OKXAlgoOrderRequest { Symbol = request.Symbol!.GetSymbol(FormatSymbol), AlgoOrderId = request.OrderId }],
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
        }

        #endregion

        #region Tp/SL Client
        EndpointOptions<SetTpSlRequest> IFuturesTpSlRestClient.SetFuturesTpSlOptions { get; } = new EndpointOptions<SetTpSlRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SetTpSlRequest.MarginMode), typeof(SharedMarginMode), "Margin mode to use", SharedMarginMode.Cross),
                new ParameterDescription(nameof(SetTpSlRequest.PositionMode), typeof(SharedPositionMode), "Position mode the account is in", SharedPositionMode.OneWay)
            }
        };

        async Task<ExchangeWebResult<SharedId>> IFuturesTpSlRestClient.SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTpSlRestClient)this).SetFuturesTpSlOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceAlgoOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Cross ? TradeMode.Cross : TradeMode.Isolated,
                request.PositionSide == SharedPositionSide.Long ? OrderSide.Buy : OrderSide.Sell,
                AlgoOrderType.Conditional,
                triggerPrice: request.TriggerPrice,
                closeFraction: 1,
                tpOrderPrice: request.TpSlSide == SharedTpSlSide.TakeProfit ? -1 : null,
                slOrderPrice: request.TpSlSide == SharedTpSlSide.StopLoss ? -1 : null,
                cancelOnClose: true,
                reduceOnly: true,
                positionSide: request.PositionMode == SharedPositionMode.OneWay ? null : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.AlgoOrderId!));
        }

        EndpointOptions<CancelTpSlRequest> IFuturesTpSlRestClient.CancelFuturesTpSlOptions { get; } = new EndpointOptions<CancelTpSlRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(CancelTpSlRequest.OrderId), typeof(string), "Id of the tp/sl order", "123123")
            }
        };

        async Task<ExchangeWebResult<bool>> IFuturesTpSlRestClient.CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTpSlRestClient)this).CancelFuturesTpSlOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<bool>(Exchange, validationError);

            var result = await Trading.CancelAlgoOrderAsync(
                [new OKXAlgoOrderRequest {
                    Symbol = request.Symbol!.GetSymbol(FormatSymbol),
                    AlgoOrderId = request.OrderId!
                }],
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<bool>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, true);
        }

        #endregion

        #region Futures Client Id Order Client

        EndpointOptions<GetOrderRequest> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            var order = await Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString()!,
                ParseOrderType(order.Data.OrderType),
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderState),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.OrderType),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.PositionSide == PositionSide.Net ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = order.Data.ReduceOnly,
                Fee = order.Data.Fee,
                FeeAsset = order.Data.FeeAsset,
                Leverage = order.Data.Leverage,
                TakeProfitPrice = order.Data.TakeProfitOrderPrice,
                StopLossPrice = order.Data.StopLossOrderPrice
            });
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(order.Data.OrderId.ToString()!));
        }
        #endregion

        #region Spot Client Id Order Client

        EndpointOptions<GetOrderRequest> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString()!,
                ParseOrderType(order.Data.OrderType),
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderState),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.OrderType),
                UpdateTime = order.Data.UpdateTime,
                Fee = order.Data.Fee,
                FeeAsset = order.Data.FeeAsset,
            });
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(order.Data.OrderId.ToString()!));
        }
        #endregion

    }
}
