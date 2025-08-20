using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Sockets.Subscriptions;
using OKX.Net.Objects.System;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
internal class OKXSocketClientUnifiedApiExchangeData : IOKXSocketClientUnifiedApiExchangeData
{
    private readonly OKXSocketClientUnifiedApi _client;

    private readonly ILogger _logger;

    #region ctor

    internal OKXSocketClientUnifiedApiExchangeData(ILogger logger, OKXSocketClientUnifiedApi client)
    {
        _client = client;
        _logger = logger;
    }
    #endregion

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(InstrumentType instrumentType, Action<DataEvent<OKXInstrument[]>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXInstrument[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "instruments",
                    InstrumentType = instrumentType,
                }
            }, onData, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<OKXTicker>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXTicker[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "tickers",
                    Symbol = symbol
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }


    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXTicker>> onData, CancellationToken ct = default)
    {
        var symbolSubs = symbols.Select(symbol =>
            new Objects.Sockets.Models.OKXSocketArgs
            {
                Channel = "tickers",
                Symbol = symbol
            }
        ).ToList();

        var subscription = new OKXSubscription<OKXTicker[]>(_logger, _client, symbolSubs, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestUpdatesAsync(string symbol, Action<DataEvent<OKXOpenInterest>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXOpenInterest[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "open-interest",
                    Symbol = symbol
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXOpenInterest>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXOpenInterest[]>(_logger,
           _client, 
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = "open-interest",
                  Symbol = s
              }).ToList(),
            x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval klineInterval, Action<DataEvent<OKXKline>> onData, CancellationToken ct = default)
    {
        var jc = EnumConverter.GetString(klineInterval);
        var subscription = new OKXSubscription<OKXKline[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "candle" + jc,
                    Symbol = symbol
                }
            },
            data =>
            {
                data.Data.First().Symbol = symbol;
                onData(data.As(data.Data.First()).WithDataTimestamp(data.Data.First().Time));
            }, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval klineInterval, Action<DataEvent<OKXKline>> onData, CancellationToken ct = default)
    {
        var jc = EnumConverter.GetString(klineInterval);
        var subscription = new OKXSubscription<OKXKline[]>(_logger,
            _client,
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                 Channel = "candle" + jc,
                 Symbol = s
              }).ToList(),
        data =>
        {
            data.Data.First().Symbol = data.Symbol ?? "";
            onData(data.As(data.Data.First()).WithDataTimestamp(data.Data.First().Time));
        }, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<OKXTrade>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXTrade[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "trades",
                    Symbol = symbol
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXTrade>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXTrade[]>(_logger,
            _client,
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = "trades",
                  Symbol = s
              }).ToList(),
            x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPriceUpdatesAsync(InstrumentType instrumentType, string? instrumentFamily, string? symbol, Action<DataEvent<OKXEstimatedPrice>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXEstimatedPrice[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "estimated-price",
                    InstrumentFamily = instrumentFamily,
                    InstrumentType = instrumentType,
                    Symbol = symbol,
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<OKXMarkPrice>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXMarkPrice[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "mark-price",
                    Symbol = symbol,
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXMarkPrice>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXMarkPrice[]>(_logger,
            _client,
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = "mark-price",
                  Symbol = s
              }).ToList(),
            x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval klineInterval, Action<DataEvent<OKXMiniKline[]>> onData, CancellationToken ct = default)
    {
        var jc = EnumConverter.GetString(klineInterval);
        var subscription = new OKXSubscription<OKXMiniKline[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "mark-price-candle" + jc,
                    Symbol = symbol,
                }
            }, onData, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitUpdatesAsync(string symbol, Action<DataEvent<OKXLimitPrice>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXLimitPrice[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "price-limit",
                    Symbol = symbol,
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXLimitPrice>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXLimitPrice[]>(_logger, _client,
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = "price-limit",
                  Symbol = s
              }).ToList(),
            x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, OrderBookType orderBookType, Action<DataEvent<OKXOrderBook>> onData, CancellationToken ct = default)
    {
        var jc = EnumConverter.GetString(orderBookType);
        var subscription = new OKXBookSubscription(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = jc,
                    Symbol = symbol,
                }
            }, x => onData(x.WithDataTimestamp(x.Data.Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, OrderBookType orderBookType, Action<DataEvent<OKXOrderBook>> onData, CancellationToken ct = default)
    {
        var jc = EnumConverter.GetString(orderBookType);
        var subscription = new OKXBookSubscription(_logger,
           _client, 
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = jc,
                  Symbol = s
              }).ToList(),
            data =>
            {
                data.Data.Symbol = data.Symbol??"";
                onData(data.WithDataTimestamp(data.Data.Time));
            }, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOptionSummaryUpdatesAsync(string instrumentFamily, Action<DataEvent<OKXOptionSummary[]>> onData, CancellationToken ct = default)
    {
        //TEST
        var subscription = new OKXSubscription<OKXOptionSummary[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "opt-summary",
                    InstrumentFamily = instrumentFamily,
                }
            }, x => onData(x.WithDataTimestamp(x.Data.Max(x => x.Time))), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<OKXFundingRate>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXFundingRate[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "funding-rate",
                    Symbol = symbol
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Timestamp)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXFundingRate>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXFundingRate[]>(_logger,
           _client, 
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = "funding-rate",
                  Symbol = s
              }).ToList(),
            x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Timestamp)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string symbol, KlineInterval klineInterval, Action<DataEvent<OKXMiniKline[]>> onData, CancellationToken ct = default)
    {
        var jc = EnumConverter.GetString(klineInterval);
        var subscription = new OKXSubscription<OKXMiniKline[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "index-candle" + jc,
                    Symbol = symbol
                }
            }, onData, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickerUpdatesAsync(string symbol, Action<DataEvent<OKXIndexTicker>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXIndexTicker[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "index-tickers",
                    Symbol = symbol
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXIndexTicker>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXIndexTicker[]>(_logger,
           _client, 
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = "index-tickers",
                  Symbol = s
              }).ToList(),
            x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Time)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusUpdatesAsync(Action<DataEvent<OKXStatus>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXStatus[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "status"
                }
            }, x => onData(x.As(x.Data.First()).WithDataTimestamp(x.Data.First().Timestamp)), false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }
}
