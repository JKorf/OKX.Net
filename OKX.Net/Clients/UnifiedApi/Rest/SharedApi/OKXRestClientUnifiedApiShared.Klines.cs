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
    }
}
