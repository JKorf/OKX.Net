﻿using OKX.Net.Interfaces.Clients.UnifiedApi;
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

namespace OKX.Net.Clients.UnifiedApi
{
    internal partial class OKXRestClientUnifiedApi : IOKXRestClientUnifiedApiShared
    {
        public string Exchange => OKXExchange.ExchangeName;

        async Task<WebCallResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval.TotalSeconds;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new WebCallResult<IEnumerable<SharedKline>>(new ArgumentError("Interval not supported"));

            var result = await ExchangeData.GetKlinesAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                interval,
                request.StartTime,
                request.EndTime,
                request.Limit ?? 100,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedKline>>(default);

            return result.As(result.Data.Select(x => new SharedKline(x.Time, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)));
        }

        async Task<WebCallResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSymbolsAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetSymbolsAsync(InstrumentType.Spot, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedSpotSymbol>>(default);

            return result.As(result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol)
            {
                MaxTradeQuantity = Math.Min(s.MaxLimitQuantity ?? decimal.MaxValue, s.MaxMarketQuantity ?? decimal.MaxValue),
                MinTradeQuantity = s.MinimumOrderSize,
                QuantityStep = s.LotSize,
                PriceStep = s.TickSize,
            }));
        }

        async Task<WebCallResult<SharedTicker>> ITickerRestClient.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result)
                return result.As<SharedTicker>(default);

            return result.As(new SharedTicker(symbol, result.Data.LastPrice ?? 0, result.Data.HighPrice ?? 0, result.Data.LowPrice ?? 0));
        }

        async Task<WebCallResult<IEnumerable<SharedTicker>>> ITickerRestClient.GetTickersAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTickersAsync(InstrumentType.Spot, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedTicker>>(default);

            return result.As<IEnumerable<SharedTicker>>(result.Data.Select(x => new SharedTicker(x.Symbol, x.LastPrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0)));
        }

        async Task<WebCallResult<IEnumerable<SharedTrade>>> ITradeRestClient.GetTradesAsync(GetTradesRequest request, CancellationToken ct)
        {
            if (request.StartTime != null || request.EndTime != null)
                return new WebCallResult<IEnumerable<SharedTrade>>(new ArgumentError("Start/EndTime filtering not supported"));

            var result = await ExchangeData.GetTradeHistoryAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedTrade>>(default);

            return result.As(result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Time)));
        }

        async Task<WebCallResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await Account.GetAccountBalanceAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedBalance>>(default);

            return result.As(result.Data.Details.Select(x => new SharedBalance(x.Asset, x.AvailableBalance ?? 0, x.Equity ?? 0)));
        }
    }
}
