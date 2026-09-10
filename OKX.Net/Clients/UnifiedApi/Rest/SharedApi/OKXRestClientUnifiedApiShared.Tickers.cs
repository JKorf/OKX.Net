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

        #region Get Ticker

        async Task<ICallResult<SharedTicker>> IGetTicker.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await ((IGetTickerRest)this).GetTickerAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker>> IGetTickerRest.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            if (request.Symbol!.TradingMode == TradingMode.Spot)
            {
                var result = await GetSpotTickerAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker>(result);

                return HttpResult.Ok<SharedTicker>(result, result.Data);
            }
            else
            {
                var result = await GetFuturesTickerAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker>(result);

                return HttpResult.Ok<SharedTicker>(result, result.Data);
            }
        }

        GetTickerOptions ISpotTickerRestClient.GetSpotTickerOptions => GetTickerOptions;
        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions => GetTickerOptions;

        public GetTickerOptions GetTickerOptions { get; } = new GetTickerOptions(_exchangeName);

        public async Task<HttpResult<SharedSpotTicker>> GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
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

        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
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

        #endregion

        #region Get All Tickers

        async Task<ICallResult<SharedTicker[]>> IGetAllTickers.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await ((IGetAllTickersRest)this).GetAllTickersAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker[]>> IGetAllTickersRest.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTicker[]>(Exchange, validationError);

            if (request.TradingMode == TradingMode.Spot)
            {
                var result = await GetAllSpotTickersAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker[]>(result);

                return HttpResult.Ok<SharedTicker[]>(result, result.Data);
            }
            else
            {
                var result = await GetAllFuturesTickersAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker[]>(result);

                return HttpResult.Ok<SharedTicker[]>(result, result.Data);
            }
        }

        Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllSpotTickersAsync(
                request.TradingMode == null ? request with { TradingMode = TradingMode.Spot } : request,
                ct);
        GetAllTickersOptions ISpotTickerRestClient.GetSpotTickersOptions => GetAllTickersOptions;

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(
                request.TradingMode == null ? request with { TradingMode = TradingMode.PerpetualLinear } : request,
                ct);
        GetAllTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllTickersOptions;

        public GetAllTickersOptions GetAllTickersOptions { get; } = new GetAllTickersOptions(_exchangeName)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<GetTickersRequest>.Required(x => x.TradingMode)
                ]
        };

        public async Task<HttpResult<SharedSpotTicker[]>> GetAllSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
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

        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var type = request.TradingMode == TradingMode.PerpetualLinear || request.TradingMode == TradingMode.PerpetualInverse ? InstrumentType.Swap : InstrumentType.Futures;
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
            if (europeXPerps && request.TradingMode!.Value.IsPerpetual())
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

    }
}
