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
    internal class OKXRestClientUnifiedSharedApi : 
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

        #region Kline client

        public GetKlinesOptions GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, false, true, true, 100, false,
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
        public async Task<HttpResult<SharedKline[]>> GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (KlineInterval)request.Interval;
            if ((int)request.Interval >= 60 * 60 * 6)
                // For 6hours and up the correct int value for UTC is the value + 1
                interval = (KlineInterval)((int)request.Interval + 1);

            var validationError = GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            HttpResult<OKXKline[]> result;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            if (pageParams.EndTime > DateTime.UtcNow.AddSeconds(-(2 * (int)request.Interval)))
            {
                // The last Kline is delayed on the history endpoint so when retrieving the most recent klines use the non-history endpoint
                result = await _api.ExchangeData.GetKlinesAsync(
                    symbol,
                    interval,
                    pageParams.StartTime,
                    pageParams.EndTime,
                    pageParams.Limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }
            else
            {
                result = await _api.ExchangeData.GetKlineHistoryAsync(
                    symbol,
                    interval,
                    pageParams.StartTime,
                    pageParams.EndTime,
                    pageParams.Limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }

            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Time)),
                     result.Data.Length,
                     result.Data.Select(x => x.Time),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Time, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedKline(
                            request.Symbol,
                            symbol, 
                            x.Time, 
                            x.ClosePrice,
                            x.HighPrice, 
                            x.LowPrice, 
                            x.OpenPrice,
                            new SharedOrderQuantity(
                                request.Symbol.TradingMode == TradingMode.Spot ? x.Volume : x.VolumeCurrency,
                                x.VolumeCurrencyQuote,
                                request.Symbol.TradingMode != TradingMode.Spot ? x.Volume: null
                                )))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Spot Symbol client

        public SharedSymbolCatalog? SpotSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicSpotId, _api.EnvironmentName, null);
        public GetSpotSymbolsOptions GetSpotSymbolsOptions { get; } = new GetSpotSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedSpotSymbol[]>> GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetSymbolsAsync(InstrumentType.Spot, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(result);

            var resultData =
                 result.Data
                .Select(x => ParseSpotSymbol(x))
                .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicSpotId, _api.EnvironmentName, null, resultData);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(resultData, request));
        }

        private SharedSpotSymbol ParseSpotSymbol(OKXInstrument s)
        {
            var result = new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.State == InstrumentState.Live)
            {
                MaxTradeQuantity = Math.Min(s.MaxLimitQuantity ?? decimal.MaxValue, s.MaxMarketQuantity ?? decimal.MaxValue),
                MinTradeQuantity = s.MinimumOrderSize,
                QuantityStep = s.LotSize,
                PriceStep = s.TickSize,
                DisplayName = s.Symbol,
                BaseAssetType = SharedAssetType.Crypto,
                UpperPriceLimitPercentage = s.PriceLimitPercentage * 100,
                LowerPriceLimitPercentage = -s.PriceLimitPercentage * 100
            };

            if (LibraryHelpers.IsStableCoin(result.BaseAsset))
                result.BaseAssetSubType = SharedAssetSubType.StableCoin;

            if (_fiatCurrencies.Contains(result.QuoteAsset))
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                if (LibraryHelpers.IsStableCoin(result.QuoteAsset))
                    result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicSpotId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicSpotId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicSpotId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicSpotId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicSpotId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicSpotId, _api.EnvironmentName, null, symbolName));
        }
        #endregion

        #region Spot Ticker client

        public GetSpotTickerOptions GetSpotTickerOptions { get; } = new GetSpotTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedSpotTicker>> GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            return HttpResult.Ok(result, new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, symbol),
                symbol,
                result.Data.LastPrice ?? 0, 
                result.Data.HighPrice ?? 0, 
                result.Data.LowPrice ?? 0,
                new SharedOrderQuantity(result.Data.Volume, result.Data.QuoteVolume),
                result.Data.OpenPrice == null ? null : Math.Round((result.Data.LastPrice ?? 0) / result.Data.OpenPrice.Value * 100 - 100, 2))
            {
            });
        }

        Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllSpotTickersAsync(request, ct);
        GetAllSpotTickersOptions ISpotTickerRestClient.GetSpotTickersOptions => GetAllSpotTickersOptions;

        public GetAllSpotTickersOptions GetAllSpotTickersOptions { get; } = new GetAllSpotTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedSpotTicker[]>> GetAllSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllSpotTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(InstrumentType.Spot, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x => 
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastPrice ?? 0,
                    x.HighPrice ?? 0,
                    x.LowPrice ?? 0,
                    new SharedOrderQuantity(x.Volume, x.QuoteVolume),
                    x.OpenPrice == null ? null : Math.Round((x.LastPrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
                {
                }).ToArray());
        }

        #endregion

        #region Book Ticker client

        public GetBookTickerOptions GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await _api.ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(request.Symbol.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, _api.EnvironmentName, null, resultTicker.Data.Symbol),
                resultTicker.Data.Symbol,
                resultTicker.Data.BestAskPrice ?? 0,
                new SharedOrderQuantity(
                    request.TradingMode == TradingMode.Spot ? resultTicker.Data.BestAskQuantity : null,
                    null,
                    request.TradingMode != TradingMode.Spot ? resultTicker.Data.BestAskQuantity : null),
                resultTicker.Data.BestBidPrice ?? 0,
               new SharedOrderQuantity(
                    request.TradingMode == TradingMode.Spot ? resultTicker.Data.BestBidQuantity : null,
                    null,
                    request.TradingMode != TradingMode.Spot ? resultTicker.Data.BestBidQuantity : null)));
        }

        #endregion

        #region Recent Trade client

        public GetRecentTradesOptions GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 100, false);
        public async Task<HttpResult<SharedTrade[]>> GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetTradeHistoryAsync(
                symbol,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x => 
            new SharedTrade(
                request.Symbol, 
                symbol, 
                new SharedOrderQuantity(
                    request.TradingMode == TradingMode.Spot ? x.Quantity : null,
                    null,
                    request.TradingMode != TradingMode.Spot ? x.Quantity : null), 
                x.Price,
                x.Time)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Balance client
        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, 
            AccountTypeFilter.Funding, AccountTypeFilter.Spot, AccountTypeFilter.Futures, AccountTypeFilter.Margin, AccountTypeFilter.Option);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType != SharedAccountType.Funding)
            {
                var result = await _api.Account.GetAccountBalanceAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data.Details.Select(x => 
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset, 
                        x.AvailableBalance ?? 0, 
                        x.Equity ?? 0)).ToArray());
            }
            else
            {
                var result = await _api.Account.GetFundingBalanceAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data.Select(x => 
                    new SharedBalance(
                        [], 
                        x.Asset,
                        x.Available,
                        x.Balance)).ToArray());
            }
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

        public PlaceSpotOrderOptions PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(_exchangeName);

        public async Task<HttpResult<SharedId>> PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset ?? 0,
                price: request.Price,
                quantityAsset: request.Quantity?.QuantityInBaseAsset > 0 ? QuantityAsset.BaseAsset : QuantityAsset.QuoteAsset,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()!));
        }

        public GetSpotOrderOptions GetSpotOrderOptions { get; } = new GetSpotOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder>> GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedSpotOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest), "Invalid order id"));

            var order = await _api.Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, order.Data.Symbol),
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
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = Math.Abs(order.Data.Fee ?? 0),
                FeeAsset = order.Data.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
            });
        }

        public GetOpenSpotOrdersOptions GetOpenSpotOrdersOptions { get; } = new GetOpenSpotOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder[]>> GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var order = await _api.Trading.GetOrdersAsync(symbol: symbol, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(order);

            return HttpResult.Ok(order, order.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol), 
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
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = Math.Abs(x.Fee ?? 0),
                FeeAsset = x.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
            }).ToArray());
        }

        public GetSpotClosedOrdersOptions GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, false, true, true, 100)
        {
            MaxAge = TimeSpan.FromDays(90)
        };
        public async Task<HttpResult<SharedSpotOrder[]>> GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            var result = await _api.Trading.GetOrderArchiveAsync(
                InstrumentType.Spot,
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                toId: pageParams.FromId,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromId(result.Data.Min(x => x.OrderId!.Value)),
                         result.Data.Length,
                         result.Data.Select(x => x.CreateTime),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         maxAge: TimeSpan.FromDays(90));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol), 
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
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = Math.Abs(x.Fee ?? 0),
                        FeeAsset = x.FeeAsset
#pragma warning restore CS0618 // Type or member is obsolete
                    })
                .ToArray(), nextPageRequest);
        }

        public GetSpotOrderTradesOptions GetSpotOrderTradesOptions { get; } = new GetSpotOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest), "Invalid order id"));

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var order = await _api.Trading.GetUserTradesAsync(InstrumentType.Spot, symbol, orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedUserTrade[]>(order);

            return HttpResult.Ok(order, order.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                x.TradeId.ToString()!,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(x.QuantityFilled),
                x.FillPrice ?? 0,
                x.FillTime)
            {
                ClientOrderId = x.ClientOrderId,
                Fee = x.Fee.HasValue ? Math.Abs(x.Fee.Value) : null,
                FeeAsset = x.FeeAsset,
                Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }


        Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetSpotUserTradeHistoryAsync(request, pageRequest, ct);
        GetSpotUserTradeHistoryOptions ISpotOrderRestClient.GetSpotUserTradesOptions => GetSpotUserTradeHistoryOptions;


        public GetSpotUserTradeHistoryOptions GetSpotUserTradeHistoryOptions { get; } = new GetSpotUserTradeHistoryOptions(_exchangeName, false, true, true, 100)
        {
            MaxAge = TimeSpan.FromDays(3)
        };
        public async Task<HttpResult<SharedUserTrade[]>> GetSpotUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetSpotUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.Trading.GetUserTradesAsync(
                InstrumentType.Spot,
                symbol,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                fromId: pageParams.FromId,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromId(result.Data.Min(x => x.OrderId!.Value)),
                         result.Data.Length,
                         result.Data.Select(x => x.Time),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         maxAge: TimeSpan.FromDays(3));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Time, request.StartTime, request.EndTime, direction)
                .Select(x =>
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol), 
                        x.Symbol,
                        x.OrderId.ToString()!,
                        x.TradeId.ToString()!,
                        x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(x.QuantityFilled),
                        x.FillPrice ?? 0,
                        x.FillTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        Fee = x.Fee.HasValue ? Math.Abs(x.Fee.Value) : null,
                        FeeAsset = x.FeeAsset,
                        Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
                    }).ToArray(), nextPageRequest);
        }

        public CancelSpotOrderOptions CancelSpotOrderOptions { get; } = new CancelSpotOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest), "Invalid order id"));

            var order = await _api.Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        private OrderType GetPlaceOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.Market) return OrderType.Market;
            if (type == SharedOrderType.LimitMaker) return OrderType.PostOnly;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return OrderType.FillOrKill;

            return OrderType.Limit;
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

        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await _api.Account.GetAssetsAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            if (!assets.Data.Any())
                return HttpResult.Fail<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return HttpResult.Ok(assets, new SharedAsset(request.Asset)
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

        Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => GetAllAssetsAsync(request, ct);
        GetAllAssetsOptions IAssetsRestClient.GetAssetsOptions => GetAllAssetsOptions;

        public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await _api.Account.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data.GroupBy(x => x.Asset).Select(x => new SharedAsset(x.Key)
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
        public GetOrderBookOptions GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, 1, 400, false);
        public async Task<HttpResult<SharedOrderBook>> GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await _api.ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                depth: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(
                request.TradingMode == TradingMode.Spot ? SharedQuantityType.BaseAsset: SharedQuantityType.Contracts, 
                result.Data.SequenceId,
                result.Data.Asks, 
                result.Data.Bids));
        }

        #endregion

        #region Deposit client

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await _api.Account.GetDepositAddressAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.Where(x => request.Network == null ? true : x.Network == request.Network).Select(x => new SharedDepositAddress(request.Asset, x.Address)
            {
                TagOrMemo = x.Memo,
                Network = x.Network
            }
            ).ToArray());
        }

        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetDepositHistoryAsync(request, pageRequest, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;

        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetDepositHistoryAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Time)),
                         result.Data.Length,
                         result.Data.Select(x => x.Time),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Time, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedDeposit(
                        x.Asset,
                        x.Quantity,
                        x.State == DepositState.Successful,
                        x.Time,
                        ParseTransferStatus(x.State))
                    {
                        Network = x.Network,
                        TransactionId = x.TransactionId
                    }).ToArray(), nextPageRequest);
        }

        private SharedTransferStatus ParseTransferStatus(DepositState state)
        {
            if (state == DepositState.Successful)
                return SharedTransferStatus.Completed;

            if (state == DepositState.Pending || state == DepositState.Credited || state == DepositState.WaitingForConfirmation)
                return SharedTransferStatus.InProgress;

            if (state == DepositState.KycLimit || state == DepositState.Frozen || state == DepositState.AddressBlacklisted || state == DepositState.SubAccountIntercepted)
                return SharedTransferStatus.Failed;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdrawal client

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetWithdrawalHistoryAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Time)),
                         result.Data.Length,
                         result.Data.Select(x => x.Time),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Time, request.StartTime, request.EndTime, direction)
                .Select(x =>
                    new SharedWithdrawal(
                        x.Asset,
                        x.To,
                        x.Quantity, 
                        x.State == WithdrawalState.Success, 
                        x.Time,
                        GetWithdrawalStatus(x))
                    {
                        Network = x.Network,
                        TransactionId = x.TransactionId,
                        Fee = x.Fee
                    })
                .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus GetWithdrawalStatus(OKXWithdrawalHistory x)
        {
            if (x.State == WithdrawalState.Canceled
                || x.State == WithdrawalState.Failed
                || x.State == WithdrawalState.InsufficientHotWalletBalance)
            {
                return SharedTransferStatus.Failed;
            }

            if (x.State == WithdrawalState.Success)
                return SharedTransferStatus.Completed;

            if (x.State == WithdrawalState.Approved
                || x.State == WithdrawalState.AwaitingManualReview
                || x.State == WithdrawalState.AwaitingTransfer
                || x.State == WithdrawalState.Canceling
                || x.State == WithdrawalState.Pending
                || x.State == WithdrawalState.PendingTransactionValidation
                || x.State == WithdrawalState.PendingTravelRule
                || x.State == WithdrawalState.Withdrawing)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }
        #endregion

        #region Withdraw client

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName) {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                ExchangeParameterDescription.Required(
                    "withdrawFee",
                    aliases: ["fee"],
                    description: "Fee to use for the withdrawal",
                    exampleValue: 0.001m)
            }
        };

        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var target = request.Address;
            if (request.AddressTag != null)
                target += ":" + request.AddressTag;

            var fee = request.GetParamValue<decimal>(Exchange, "withdrawFee", "fee");

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
                request.Asset,
                request.Quantity,
                WithdrawalDestination.DigitalCurrencyAddress,
                target,
                fee,
                network: request.Network,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.WithdrawalId));
        }

        #endregion

        #region Futures Symbol client

        public SharedSymbolCatalog? FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicFuturesId, _api.EnvironmentName, null);
        public GetFuturesSymbolsOptions GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            // For Europe environment there are 2 different types of perp contracts
            //   1. Swaps, do not appear to be tradable, but still returned by the server
            //   2. XPerps, which are returned under InstrumentType.Futures with a delivery time of 5 years
            // Which one we return is determined by the configuration

            var europeXPerps = _api.EnvironmentName == OKXEnvironment.Europe.Name && _api.ClientOptions.SharedApiEuropeUseXPerps;

            IEnumerable<OKXInstrument> data;
            HttpResult<OKXInstrument[]> result;
            if (request.TradingMode == null)
            {
                // No trading mode filter, request both Swap (perps) and Futures (delivery)
                var request1 = _api.ExchangeData.GetSymbolsAsync(InstrumentType.Swap, ct: ct);
                var request2 = _api.ExchangeData.GetSymbolsAsync(InstrumentType.Futures, ct: ct);
                await Task.WhenAll(request1, request2).ConfigureAwait(false);
                if (!request1.Result.Success)
                    return HttpResult.Fail<SharedFuturesSymbol[]>(request1.Result);
                if (!request2.Result.Success)
                    return HttpResult.Fail<SharedFuturesSymbol[]>(request2.Result);

                result = request1.Result;
                data = request1.Result.Data.Concat(request2.Result.Data);
            }
            else
            {
                InstrumentType requestType;
                if (europeXPerps)
                    requestType = InstrumentType.Futures; // Europe XPerps are returned as futures with 5y delivery
                else
                    requestType = (!request.TradingMode.HasValue || request.TradingMode == TradingMode.PerpetualLinear || request.TradingMode == TradingMode.PerpetualInverse) ? InstrumentType.Swap : InstrumentType.Futures;

                result = await _api.ExchangeData.GetSymbolsAsync(requestType, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedFuturesSymbol[]>(result);

                data = result.Data;
            }

            var resultData =
                 data
                .Select(x => ParseFuturesSymbol(x)!)
                .Where(x => x != null)
                .ToArray();

            foreach (var distinctMode in resultData.GroupBy(x => x.TradingMode))
                ExchangeSymbolCache.UpdateSymbolInfo(_topicFuturesId, _api.EnvironmentName, distinctMode.Key.ToString(), distinctMode!.ToArray());
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(resultData, request));
        }

        private SharedFuturesSymbol? ParseFuturesSymbol(OKXInstrument x)
        {
            var underlyingParts = x.Underlying.Split('-');
            if (underlyingParts.Length != 2)
                return null!;

            var result = new SharedFuturesSymbol(
                x.InstrumentType == InstrumentType.Swap
                ? (x.ContractType == ContractType.Linear ? TradingMode.PerpetualLinear : TradingMode.PerpetualInverse)
                : (x.ContractType == ContractType.Linear ? (x.RuleType == SymbolRuleType.Perp ? TradingMode.PerpetualLinear : TradingMode.DeliveryLinear) : (x.RuleType == SymbolRuleType.Perp ? TradingMode.PerpetualInverse : TradingMode.DeliveryInverse)),
                    underlyingParts[0],
                    underlyingParts[1],
                    x.Symbol,
                    x.State == InstrumentState.Live)
            {
                ContractSize = x.ContractValue,
                DeliveryTime = x.ExpiryTime,
                MaxTradeQuantity = x.MaxLimitQuantity,
                MinTradeQuantity = x.MinimumOrderSize,
                PriceStep = x.TickSize,
                QuantityStep = x.LotSize,
                DisplayName = x.Symbol,
                MaxLongLeverage = x.MaximumLeverage,
                MaxShortLeverage = x.MaximumLeverage,
                BaseAssetType = SharedAssetType.Crypto,
                UpperPriceLimitPercentage = x.PriceLimitPercentage * 100,
                LowerPriceLimitPercentage = -x.PriceLimitPercentage * 100
            };

            if (x.SymbolCategory == SymbolCategory.Stocks
                || x.SymbolCategory == SymbolCategory.Bonds)
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Equity;
            }
            else if (x.SymbolCategory == SymbolCategory.Commodities)
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Commodity;
            }
            else if (x.SymbolCategory == SymbolCategory.Forex)
            {
                result.BaseAssetType = SharedAssetType.Fiat;
            }
            else if (x.SymbolCategory == SymbolCategory.Crypto)
            {
                result.BaseAssetType = SharedAssetType.Crypto;
            }

            if (LibraryHelpers.IsStableCoin(result.QuoteAsset))
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicFuturesId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, _api.EnvironmentName, null, symbolName));
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

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameter<PlaceFuturesOrderRequest>.Required(x => x.MarginMode, "Cross or isolated margin", SharedMarginMode.Cross),
            }
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
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
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()!));
        }

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest), "Invalid order id"));

            var order = await _api.Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, order.Data.Symbol), 
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
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = order.Data.Fee,
                FeeAsset = order.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                Leverage = order.Data.Leverage,
                TakeProfitPrice = order.Data.TakeProfitOrderPrice,
                StopLossPrice = order.Data.StopLossOrderPrice
            });
        }

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var tradingMode = request.Symbol?.TradingMode ?? request.TradingMode ?? TradingMode.PerpetualLinear;
            HttpResult<OKXOrder[]> orders;
            if (tradingMode.IsPerpetual())
                orders = await _api.Trading.GetOrdersAsync(InstrumentType.Swap, symbol: symbol, ct: ct).ConfigureAwait(false);
            else
                orders = await _api.Trading.GetOrdersAsync(InstrumentType.Futures, symbol: symbol, ct: ct).ConfigureAwait(false);

            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
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
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                Leverage = x.Leverage,
                TakeProfitPrice = x.TakeProfitOrderPrice,
                StopLossPrice = x.StopLossOrderPrice
            }).ToArray());
        }

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 100)
        {
            MaxAge = TimeSpan.FromDays(90)
        };
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            HttpResult<OKXOrder[]> result;
            if (request.Symbol.TradingMode.IsPerpetual())
            {
                result = await _api.Trading.GetOrderArchiveAsync(
                    InstrumentType.Swap,
                    symbol: symbol,
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    limit: pageParams.Limit,
                    toId: pageParams.FromId,
                    ct: ct).ConfigureAwait(false);
            }
            else
            {
                result = await _api.Trading.GetOrderArchiveAsync(
                    InstrumentType.Futures,
                    symbol: symbol,
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    limit: pageParams.Limit,
                    toId: pageParams.FromId,
                    ct: ct).ConfigureAwait(false);
            }

            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromId(result.Data.Min(x => x.OrderId!.Value)),
                         result.Data.Length,
                         result.Data.Select(x => x.CreateTime),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         TimeSpan.FromDays(90));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
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
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        Leverage = x.Leverage,
                        TakeProfitPrice = x.TakeProfitOrderPrice,
                        StopLossPrice = x.StopLossOrderPrice
                    })
                .ToArray(), nextPageRequest);
        }

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest), "Invalid order id"));

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            HttpResult<OKXTransaction[]> orders;
            if (request.Symbol.TradingMode.IsPerpetual())
            {
                orders = await _api.Trading.GetUserTradesArchiveAsync(
                    InstrumentType.Swap,
                    symbol: symbol,
                    orderId: orderId,
                    ct: ct).ConfigureAwait(false);
            }
            else
            {
                orders = await _api.Trading.GetUserTradesArchiveAsync(
                    InstrumentType.Futures,
                    symbol: symbol,
                    orderId: orderId,
                    ct: ct).ConfigureAwait(false);
            }

            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString()!,
                x.TradeId.ToString()!,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(contractQuantity: x.QuantityFilled),
                x.FillPrice ?? 0,
                x.FillTime)
            {
                ClientOrderId = x.ClientOrderId,
                Fee = x.Fee == null ? null : Math.Abs(x.Fee.Value),
                FeeAsset = x.FeeAsset,
                Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetFuturesUserTradeHistoryAsync(request, pageRequest, ct);
        GetFuturesUserTradeHistoryOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions => GetFuturesUserTradeHistoryOptions;

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 100)
        {
            MaxAge = TimeSpan.FromDays(3)
        };
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            HttpResult<OKXTransaction[]> result;
            if (request.Symbol.TradingMode.IsPerpetual())
            {
                result = await _api.Trading.GetUserTradesAsync(
                    InstrumentType.Swap,
                    request.Symbol.GetSymbol(FormatSymbol),
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    limit: pageParams.Limit,
                    toId: pageParams.FromId,
                    ct: ct
                ).ConfigureAwait(false);
            }
            else
            {
                result = await _api.Trading.GetUserTradesAsync(
                    InstrumentType.Futures,
                    request.Symbol.GetSymbol(FormatSymbol),
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    limit: pageParams.Limit,
                    toId: pageParams.FromId,
                    ct: ct
                ).ConfigureAwait(false);
            }
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromId(result.Data.Min(x => x.OrderId!.Value)),
                         result.Data.Length,
                         result.Data.Select(x => x.Time),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         maxAge: TimeSpan.FromDays(3));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Time, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                        x.Symbol,
                        x.OrderId.ToString()!,
                        x.TradeId.ToString()!,
                        x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(contractQuantity: x.QuantityFilled),
                        x.FillPrice ?? 0,
                        x.FillTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        Fee = x.Fee == null ? null : Math.Abs(x.Fee.Value),
                        FeeAsset = x.FeeAsset,
                        Role = x.OrderFlowType == OrderFlowType.Maker ? SharedRole.Maker : SharedRole.Taker
                    })
                .ToArray(), nextPageRequest);
        }

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest), "Invalid order id"));

            var order = await _api.Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.OrderId.ToString()!));
        }

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var result = await _api.Account.GetPositionsAsync(symbol: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPosition[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x => 
                new SharedPosition(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol, 
                    new SharedOrderQuantity(contractQuantity: Math.Abs(x.PositionsQuantity ?? 0)),
                    x.UpdateTime)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    LiquidationPrice = x.LiquidationPrice,
                    Leverage = x.Leverage,
                    AverageOpenPrice = x.AveragePrice,
                    PositionMode = x.PositionSide == PositionSide.Net ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                    PositionSide = x.PositionSide == PositionSide.Net ? (x.PositionsQuantity >= 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                    StopLossPrice = x.CloseOrderAlgo.FirstOrDefault(x => x.StopLossTriggerPrice > 0)?.StopLossTriggerPrice,
                    TakeProfitPrice = x.CloseOrderAlgo.FirstOrDefault(x => x.TakeProfitTriggerPrice > 0)?.TakeProfitTriggerPrice
                }).ToArray());
        }

        public ClosePositionOptions ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true)
        {
            RequestNotes = "No order id returned by the API for this",
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameter<ClosePositionRequest>.Required(x => x.MarginMode, "Cross or isolated margin", SharedMarginMode.Cross),
            }
        };
        public async Task<HttpResult<SharedId>> ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.ClosePositionAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(string.Empty));
        }

        #endregion

        #region Leverage client
        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSide;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameter<GetLeverageRequest>.Required(x => x.MarginMode, "Cross or isolated margin", SharedMarginMode.Cross),
            }
        };
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.GetLeverageAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.MarginMode == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            var side = request.PositionSide == null ? result.Data.First() : result.Data.FirstOrDefault(d => d.PositionSide == (request.PositionSide == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long));
            if (side == null)
                return HttpResult.Fail<SharedLeverage>(Exchange, new ServerError(new ErrorInfo(ErrorType.NoPosition, "Position not found")));
            
            return HttpResult.Ok(result, new SharedLeverage(side.Leverage ?? 0)
            {
                MarginMode = side.MarginMode == MarginMode.Isolated ? SharedMarginMode.Isolated : SharedMarginMode.Cross,
                Side = side.PositionSide == PositionSide.Net ? null : side.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short
            });
        }

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName) {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameter<SetLeverageRequest>.Required(x => x.MarginMode, "Cross or isolated margin", SharedMarginMode.Cross),
            }
        };
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.SetLeverageAsync(
                (int)request.Leverage,
                request.MarginMode == SharedMarginMode.Isolated ? MarginMode.Isolated : MarginMode.Cross,
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                positionSide: request.Side == null ? null : request.Side == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(result.Data.First().Leverage ?? 0)
            {
                Side = request.Side
            });
        }
        #endregion

        #region Index Klines client

        public GetIndexPriceKlinesOptions GetIndexPriceKlinesOptions { get; } = new GetIndexPriceKlinesOptions(_exchangeName, false, true, true, 100, false);

        public async Task<HttpResult<SharedFuturesKline[]>> GetIndexPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (KlineInterval)request.Interval;

            var validationError = GetIndexPriceKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetIndexKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime,
                pageParams.EndTime,
                pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Time)),
                     result.Data.Length,
                     result.Data.Select(x => x.Time),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Time, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedFuturesKline(request.Symbol, symbol, x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Mark Klines client

        public GetMarkPriceKlinesOptions GetMarkPriceKlinesOptions { get; } = new GetMarkPriceKlinesOptions(_exchangeName, false, true, true, 100, false);

        public async Task<HttpResult<SharedFuturesKline[]>> GetMarkPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (KlineInterval)request.Interval;

            var validationError = GetMarkPriceKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetMarkPriceKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime,
                pageParams.EndTime,
                pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Time)),
                     result.Data.Length,
                     result.Data.Select(x => x.Time),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Time, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedFuturesKline(request.Symbol, symbol, x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Open Interest client

        public GetOpenInterestOptions GetOpenInterestOptions { get; } = new GetOpenInterestOptions(_exchangeName, false);
        public async Task<HttpResult<SharedOpenInterest>> GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = GetOpenInterestOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOpenInterest>(Exchange, validationError);

            var instrumentType = request.Symbol!.TradingMode.IsPerpetual() ? InstrumentType.Swap : InstrumentType.Futures;
            var europeXPerps = _api.EnvironmentName == OKXEnvironment.Europe.Name && _api.ClientOptions.SharedApiEuropeUseXPerps;
            if (europeXPerps && request.Symbol.TradingMode.IsPerpetual())
                // XPerps are categorized under futures
                instrumentType = InstrumentType.Futures;

            var result = await _api.ExchangeData.GetOpenInterestsAsync(instrumentType, symbol: request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOpenInterest>(result);

            return HttpResult.Ok(result, new SharedOpenInterest(new SharedOrderQuantity(contractQuantity: result.Data.First().OpenInterest ?? 0)));
        }

        #endregion

        #region Funding Rate client
        public GetFundingRateHistoryOptions GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(_exchangeName, false, true, true, 400, false);

        public async Task<HttpResult<SharedFundingRate[]>> GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFundingRateHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingRate[]>(Exchange, validationError);

            int limit = request.Limit ?? 400;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.ExchangeData.GetFundingRateHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingRate[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.FundingTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.FundingTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.FundingTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedFundingRate(x.FundingRate, x.FundingTime))
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Futures Ticker client

        public GetFuturesTickerOptions GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var instrumentType = request.Symbol!.TradingMode.IsPerpetual() ? InstrumentType.Swap : InstrumentType.Futures;
            var europeXPerps = _api.EnvironmentName == OKXEnvironment.Europe.Name && _api.ClientOptions.SharedApiEuropeUseXPerps;
            if (europeXPerps && request.Symbol.TradingMode.IsPerpetual())
                // XPerps are categorized under futures
                instrumentType = InstrumentType.Futures;

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = _api.ExchangeData.GetTickerAsync(symbol, ct: ct);
            var resultFunding = _api.ExchangeData.GetFundingRatesAsync(symbol: symbol, ct: ct);
            var resultMarkPrice = _api.ExchangeData.GetMarkPricesAsync(instrumentType, symbol: symbol, ct: ct);
            var resultIndexPrice = _api.ExchangeData.GetIndexTickersAsync(symbol: symbol.Split('-')[0] + "-" + symbol.Split('-')[1].Split('_')[0], ct: ct);
            await Task.WhenAll(resultTicker, resultFunding).ConfigureAwait(false);
            if (!resultTicker.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker.Result);
            if (!resultIndexPrice.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultIndexPrice.Result);
            if (!resultMarkPrice.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultMarkPrice.Result);

            var funding = resultFunding.Result.Data?.SingleOrDefault();
            var index = resultIndexPrice.Result.Data.Single();
            var mark = resultMarkPrice.Result.Data.Single();
            return HttpResult.Ok(resultTicker.Result, 
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, resultTicker.Result.Data.Symbol),
                    resultTicker.Result.Data.Symbol,
                    resultTicker.Result.Data.LastPrice,
                    resultTicker.Result.Data.HighPrice,
                    resultTicker.Result.Data.LowPrice,
                    new SharedOrderQuantity(resultTicker.Result.Data.QuoteVolume, null, resultTicker.Result.Data.Volume),
                    resultTicker.Result.Data.OpenPrice == null ? null : Math.Round((resultTicker.Result.Data.LastPrice ?? 0) / resultTicker.Result.Data.OpenPrice.Value * 100 - 100, 2))
                {
#pragma warning disable CS0618 // Type or member is obsolete | Temporary to maintain previous behavior
                    Volume = resultTicker.Result.Data.Volume,
#pragma warning restore
                    IndexPrice = index.IndexPrice,
                    MarkPrice = mark.MarkPrice,
                    FundingRate = funding?.FundingRate,
                    NextFundingTime = funding?.NextFundingTime
                });
        }

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllFuturesTickersOptions;

        public GetAllFuturesTickersOptions GetAllFuturesTickersOptions { get; } = new GetAllFuturesTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllFuturesTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var type = !request.TradingMode.HasValue || request.TradingMode == TradingMode.PerpetualLinear || request.TradingMode == TradingMode.PerpetualInverse ? InstrumentType.Swap : InstrumentType.Futures;
            var europeXPerps = _api.EnvironmentName == OKXEnvironment.Europe.Name && _api.ClientOptions.SharedApiEuropeUseXPerps;
            if (europeXPerps && type == InstrumentType.Swap)
                // XPerps are categorized under futures
                type = InstrumentType.Futures;

            var resultTickers = _api.ExchangeData.GetTickersAsync(type, ct: ct);
            var resultMarkPrice = _api.ExchangeData.GetMarkPricesAsync(type, ct: ct);
            await Task.WhenAll(resultTickers, resultMarkPrice).ConfigureAwait(false);
            if (!resultTickers.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultTickers.Result);
            if (!resultMarkPrice.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultMarkPrice.Result);

            var resultData = resultTickers.Result.Data.AsEnumerable();
            if (europeXPerps && (!request.TradingMode.HasValue || request.TradingMode!.Value.IsPerpetual()))
                resultData = resultTickers.Result.Data.Where(x => x.Symbol.Contains("XPERP"));

            return HttpResult.Ok(resultTickers.Result, resultData.Select(x =>
            {
                var markPrice = resultMarkPrice.Result.Data.Single(p => p.Symbol == x.Symbol);
                return new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                    x.Symbol,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.QuoteVolume, null, x.Volume),
                    x.OpenPrice == null ? null : Math.Round((x.LastPrice ?? 0) / x.OpenPrice.Value * 100 - 100, 2))
                {
                    MarkPrice = markPrice.MarkPrice
                };
            }).ToArray());
        }

        #endregion

        #region Position Mode client

        public SharedPositionModeSelection PositionModeSettingType => SharedPositionModeSelection.PerAccount;
        public GetPositionModeOptions GetPositionModeOptions { get; } = new GetPositionModeOptions(_exchangeName);
        public async Task<HttpResult<SharedPositionModeResult>> GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = GetPositionModeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionModeResult>(Exchange, validationError);

            var result = await _api.Account.GetAccountConfigurationAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionModeResult>(result);

            return HttpResult.Ok(result, new SharedPositionModeResult(result.Data.PositionMode == PositionMode.LongShortMode ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
        }

        public SetPositionModeOptions SetPositionModeOptions { get; } = new SetPositionModeOptions(_exchangeName);
        public async Task<HttpResult<SharedPositionModeResult>> SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = SetPositionModeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionModeResult>(Exchange, validationError);

            var result = await _api.Account.SetPositionModeAsync(request.PositionMode == SharedPositionMode.HedgeMode ? PositionMode.LongShortMode : PositionMode.NetMode, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionModeResult>(result);

            return HttpResult.Ok(result, new SharedPositionModeResult(request.PositionMode));
        }
        #endregion

        #region Position History client

        public GetPositionHistoryOptions GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedPositionHistory[]>> GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetPositionHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionHistory[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.Account.GetPositionHistoryAsync(
                symbol: request.Symbol?.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionHistory[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedPositionHistory(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                            x.Symbol,
                            x.Direction == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                            x.OpenAveragePrice ?? 0,
                            x.CloseAveragePrice ?? 0,
                            new SharedOrderQuantity(contractQuantity: x.CloseTotalPos ?? 0),
                            x.ProfitAndLoss ?? 0,
                            x.UpdateTime)
                        {
                            Leverage = x.Leverage,
                            PositionId = x.PositionId.ToString()
                        })
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Fee Client
        public GetFeeOptions GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        public async Task<HttpResult<SharedFee>> GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var type = request.Symbol!.TradingMode == TradingMode.Spot ? InstrumentType.Spot : request.Symbol.TradingMode == TradingMode.PerpetualLinear || request.Symbol.TradingMode == TradingMode.PerpetualInverse ? InstrumentType.Swap : InstrumentType.Futures;
            var result = await _api.Account.GetFeeRatesAsync(type,
                type == InstrumentType.Spot ? request.Symbol!.GetSymbol(FormatSymbol) : null,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            // Return
            return HttpResult.Ok(result, new SharedFee(Math.Abs(result.Data.Maker ?? 0) * 100, Math.Abs(result.Data.Taker ?? 0) * 100));
        }
        #endregion

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

        #region Futures Trigger Order Client
        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameter<PlaceFuturesTriggerOrderRequest>.Required(x => x.MarginMode, "Margin mode to use", SharedMarginMode.Cross),
                RequestParameter<PlaceFuturesTriggerOrderRequest>.Required(x => x.PositionMode, "Position mode the account is in", SharedPositionMode.HedgeMode),
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

        private OrderSide GetTriggerOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Buy : OrderSide.Sell;
        
            return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Sell : OrderSide.Buy;
        }

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

        #region Tp/SL Client
        public SetFuturesTpSlOptions SetFuturesTpSlOptions { get; } = new SetFuturesTpSlOptions(_exchangeName, true)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameter<SetTpSlRequest>.Required(x => x.MarginMode, "Margin mode to use", SharedMarginMode.Cross),
                RequestParameter<SetTpSlRequest>.Required(x => x.PositionMode, "Position mode the account is in", SharedPositionMode.OneWay)
            }
        };

        public async Task<HttpResult<SharedId>> SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
        {
            var validationError = SetFuturesTpSlOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceAlgoOrderAsync(
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

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.AlgoOrderId!));
        }

        public CancelFuturesTpSlOptions CancelFuturesTpSlOptions { get; } = new CancelFuturesTpSlOptions(_exchangeName, true)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameter<CancelTpSlRequest>.Required(x => x.OrderId, "Id of the tp/sl order", "123123")
            }
        };

        public async Task<HttpResult<bool>> CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTpSlOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<bool>(Exchange, validationError);

            var result = await _api.Trading.CancelAlgoOrderAsync(
                [new OKXAlgoOrderRequest {
                    Symbol = request.Symbol!.GetSymbol(FormatSymbol),
                    AlgoOrderId = request.OrderId!
                }],
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<bool>(result);

            // Return
            return HttpResult.Ok(result, true);
        }

        #endregion

        #region Futures Client Id Order Client

        public GetFuturesOrderByClientOrderIdOptions GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, order.Data.Symbol),
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
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = order.Data.Fee,
                FeeAsset = order.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                Leverage = order.Data.Leverage,
                TakeProfitPrice = order.Data.TakeProfitOrderPrice,
                StopLossPrice = order.Data.StopLossOrderPrice
            });
        }

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.OrderId.ToString()!));
        }
        #endregion

        #region Spot Client Id Order Client

        public GetSpotOrderByClientOrderIdOptions GetSpotOrderByClientOrderIdOptions { get; } = new GetSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder>> GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderDetailsAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, order.Data.Symbol),
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
#pragma warning disable CS0618 // Type or member is obsolete
                Fee = order.Data.Fee,
                FeeAsset = order.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
            });
        }

        public CancelSpotOrderByClientOrderIdOptions CancelSpotOrderByClientOrderIdOptions { get; } = new CancelSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.OrderId.ToString()!));
        }
        #endregion

        #region Transfer client

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Funding,
            SharedAccountType.Unified
            ]);
        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await _api.Account.TransferAsync(
                request.Asset,
                request.Quantity,
                TransferType.TransferWithinAccount,
                fromType.Value,
                toType.Value,
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(transfer.Data.TransferId?.ToString() ?? ""));
        }

        private AccountType? GetTransferType(SharedAccountType type)
        {
            if (type == SharedAccountType.Funding) return AccountType.Funding;
            if (type == SharedAccountType.Unified) return AccountType.Trading;
            return null;
        }

        #endregion
    }
}
