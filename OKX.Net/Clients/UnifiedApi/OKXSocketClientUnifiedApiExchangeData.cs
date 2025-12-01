using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Sockets.Models;
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
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXInstrument[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXInstrument[]>(data.Data, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXInstrument[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "instruments",
                    InstrumentType = instrumentType,
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<OKXTicker>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXTicker[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXTicker>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXTicker[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "tickers",
                    Symbol = symbol
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }


    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXTicker>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXTicker[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXTicker>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var symbolSubs = symbols.Select(symbol =>
            new OKXSocketArgs
            {
                Channel = "tickers",
                Symbol = symbol
            }
        ).ToList();

        var subscription = new OKXSubscription<OKXTicker[]>(_logger, _client, symbolSubs, internalHandler, false);
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestUpdatesAsync(string symbol, Action<DataEvent<OKXOpenInterest>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXOpenInterest[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXOpenInterest>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXOpenInterest[]>(_logger, _client, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "open-interest",
                    Symbol = symbol
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXOpenInterest>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXOpenInterest[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXOpenInterest>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXOpenInterest[]>(_logger,
           _client, 
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = "open-interest",
                  Symbol = s
              }).ToList(),
            internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval klineInterval, Action<DataEvent<OKXKline>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXKline[]>>((receiveTime, originalData, data) =>
        {
            data.Data.First().Symbol = symbol;
            onData(
                new DataEvent<OKXKline>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var jc = EnumConverter.GetString(klineInterval);
        var subscription = new OKXSubscription<OKXKline[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "candle" + jc,
                    Symbol = symbol
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval klineInterval, Action<DataEvent<OKXKline>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXKline[]>>((receiveTime, originalData, data) =>
        {
            data.Data.First().Symbol = data.Arg.Symbol ?? "";
            onData(
                new DataEvent<OKXKline>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var jc = EnumConverter.GetString(klineInterval);
        var subscription = new OKXSubscription<OKXKline[]>(_logger,
            _client,
           symbols.Select(s =>
              new OKXSocketArgs
              {
                 Channel = "candle" + jc,
                 Symbol = s
              }).ToList(),
        internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<OKXTrade>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXTrade[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXTrade>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXTrade[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "trades",
                    Symbol = symbol
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXTrade>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXTrade[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXTrade>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXTrade[]>(_logger,
            _client,
           symbols.Select(s =>
              new OKXSocketArgs
              {
                  Channel = "trades",
                  Symbol = s
              }).ToList(),
            internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPriceUpdatesAsync(InstrumentType instrumentType, string? instrumentFamily, string? symbol, Action<DataEvent<OKXEstimatedPrice>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXEstimatedPrice[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXEstimatedPrice>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXEstimatedPrice[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "estimated-price",
                    InstrumentFamily = instrumentFamily,
                    InstrumentType = instrumentType,
                    Symbol = symbol,
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<OKXMarkPrice>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXMarkPrice[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXMarkPrice>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXMarkPrice[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "mark-price",
                    Symbol = symbol,
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXMarkPrice>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXMarkPrice[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXMarkPrice>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXMarkPrice[]>(_logger,
            _client,
           symbols.Select(s =>
              new OKXSocketArgs
              {
                  Channel = "mark-price",
                  Symbol = s
              }).ToList(),
            internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval klineInterval, Action<DataEvent<OKXMiniKline[]>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXMiniKline[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXMiniKline[]>(data.Data, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var jc = EnumConverter.GetString(klineInterval);
        var subscription = new OKXSubscription<OKXMiniKline[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "mark-price-candle" + jc,
                    Symbol = symbol,
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitUpdatesAsync(string symbol, Action<DataEvent<OKXLimitPrice>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXLimitPrice[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXLimitPrice>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXLimitPrice[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "price-limit",
                    Symbol = symbol,
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXLimitPrice>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXLimitPrice[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXLimitPrice>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXLimitPrice[]>(_logger, _client,
           symbols.Select(s =>
              new OKXSocketArgs
              {
                  Channel = "price-limit",
                  Symbol = s
              }).ToList(),
            internalHandler, false);

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
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXOptionSummary[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXOptionSummary[]>(data.Data, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.Max(x => x.Time))
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXOptionSummary[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "opt-summary",
                    InstrumentFamily = instrumentFamily,
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<OKXFundingRate>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXFundingRate[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXFundingRate>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Timestamp)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXFundingRate[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "funding-rate",
                    Symbol = symbol
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXFundingRate>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXFundingRate[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXFundingRate>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Timestamp)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXFundingRate[]>(_logger,
           _client, 
           symbols.Select(s =>
              new OKXSocketArgs
              {
                  Channel = "funding-rate",
                  Symbol = s
              }).ToList(),
            internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string symbol, KlineInterval klineInterval, Action<DataEvent<OKXMiniKline[]>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXMiniKline[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXMiniKline[]>(data.Data, receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var jc = EnumConverter.GetString(klineInterval);
        var subscription = new OKXSubscription<OKXMiniKline[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "index-candle" + jc,
                    Symbol = symbol
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickerUpdatesAsync(string symbol, Action<DataEvent<OKXIndexTicker>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXIndexTicker[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXIndexTicker>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXIndexTicker[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "index-tickers",
                    Symbol = symbol
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<OKXIndexTicker>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXIndexTicker[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXIndexTicker>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Time)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXIndexTicker[]>(_logger,
           _client, 
           symbols.Select(s =>
              new Objects.Sockets.Models.OKXSocketArgs
              {
                  Channel = "index-tickers",
                  Symbol = s
              }).ToList(),
            internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusUpdatesAsync(Action<DataEvent<OKXStatus>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DateTime, string?, OKXSocketUpdate<OKXStatus[]>>((receiveTime, originalData, data) =>
        {
            onData(
                new DataEvent<OKXStatus>(data.Data.First(), receiveTime, originalData)
                    .WithUpdateType(data.EventType?.Equals("snapshot", StringComparison.Ordinal) == true ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(data.Data.First().Timestamp)
                    .WithStreamId(data.Arg.Channel)
                    .WithSymbol(data.Arg.Symbol)
                );
        });

        var subscription = new OKXSubscription<OKXStatus[]>(_logger, _client, new List<OKXSocketArgs>
            {
                new OKXSocketArgs
                {
                    Channel = "status"
                }
            }, internalHandler, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }
}
