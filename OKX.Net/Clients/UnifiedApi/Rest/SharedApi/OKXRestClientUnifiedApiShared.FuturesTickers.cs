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

        #region Get Futures Ticker

        async Task<ICallResult<SharedFuturesTicker>> IGetFuturesTicker.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await GetFuturesTickerAsync(request, ct).ConfigureAwait(false);

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

        #endregion

        #region Get All Futures Tickers

        async Task<ICallResult<SharedFuturesTicker[]>> IGetAllFuturesTickers.GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await GetAllFuturesTickersAsync(request, ct).ConfigureAwait(false);

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

    }
}
