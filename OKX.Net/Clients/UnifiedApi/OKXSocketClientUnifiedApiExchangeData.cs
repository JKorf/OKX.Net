using CryptoExchange.Net.Objects.Sockets;
using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.Sockets.Subscriptions;
using OKX.Net.Objects.System;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
public class OKXSocketClientUnifiedApiExchangeData : IOKXSocketClientUnifiedApiExchangeData
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
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(OKXInstrumentType instrumentType, Action<DataEvent<IEnumerable<OKXInstrument>>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXInstrument>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "instruments",
                    InstrumentType = instrumentType,
                }
            }, null, onData, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<OKXTicker>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXTicker>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "tickers",
                    Symbol = symbol
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestUpdatesAsync(string symbol, Action<DataEvent<OKXOpenInterest>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXOpenInterest>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "open-interest",
                    Symbol = symbol
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, OKXPeriod period, Action<DataEvent<OKXCandlestick>> onData, CancellationToken ct = default)
    {
        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var subscription = new OKXSubscription<OKXCandlestick>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "candle" + jc,
                    Symbol = symbol
                }
            },
            data => 
            {
                data.Data.Symbol = symbol;
                onData(data);
            }, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<OKXTrade>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXTrade>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "trades",
                    Symbol = symbol
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPriceUpdatesAsync(OKXInstrumentType instrumentType, string? instrumentFamily, string? symbol, Action<DataEvent<OKXEstimatedPrice>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXEstimatedPrice>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "estimated-price",
                    InstrumentFamily = instrumentFamily,
                    InstrumentType = instrumentType,
                    Symbol = symbol,
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<OKXMarkPrice>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXMarkPrice>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "mark-price",
                    Symbol = symbol,
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, OKXPeriod period, Action<DataEvent<OKXCandlestick>> onData, CancellationToken ct = default)
    {
        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var subscription = new OKXSubscription<OKXCandlestick>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "mark-price-candle" + jc,
                    Symbol = symbol,
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitUpdatesAsync(string symbol, Action<DataEvent<OKXLimitPrice>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXLimitPrice>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "price-limit",
                    Symbol = symbol,
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, OKXOrderBookType orderBookType, Action<DataEvent<OKXOrderBook>> onData, CancellationToken ct = default)
    {
        var jc = JsonConvert.SerializeObject(orderBookType, new OrderBookTypeConverter(false));
        var subscription = new OKXBookSubscription(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = jc,
                    Symbol = symbol,
                }
            }, onData, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOptionSummaryUpdatesAsync(string instrumentFamily, Action<DataEvent<IEnumerable<OKXOptionSummary>>> onData, CancellationToken ct = default)
    {
        //TEST
        var subscription = new OKXSubscription<OKXOptionSummary>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "opt-summary",
                    InstrumentFamily = instrumentFamily,
                }
            }, null, onData, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<OKXFundingRate>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXFundingRate>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "funding-rate",
                    Symbol = symbol
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string symbol, OKXPeriod period, Action<DataEvent<OKXCandlestick>> onData, CancellationToken ct = default)
    {
        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var subscription = new OKXSubscription<OKXCandlestick>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "index-candle" + jc,
                    Symbol = symbol
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickerUpdatesAsync(string symbol, Action<DataEvent<OKXIndexTicker>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXIndexTicker>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "index-tickers",
                    Symbol = symbol
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusUpdatesAsync(Action<DataEvent<OKXStatus>> onData, CancellationToken ct = default)
    {
        var subscription = new OKXSubscription<OKXStatus>(_logger, new List<Objects.Sockets.Models.OKXSocketArgs>
            {
                new Objects.Sockets.Models.OKXSocketArgs
                {
                    Channel = "status"
                }
            }, onData, null, false);

        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), subscription, ct).ConfigureAwait(false);
    }
}
