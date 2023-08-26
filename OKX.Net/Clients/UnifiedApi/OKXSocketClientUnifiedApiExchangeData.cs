using CryptoExchange.Net.Sockets;
using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Public;
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
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(OKXInstrumentType instrumentType, Action<OKXInstrument> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXInstrument>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "instruments",
            InstrumentType = instrumentType,
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<OKXTicker> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, 
            new OKXSocketRequestArgument 
            { 
                Channel = "tickers", 
                Symbol = symbol 
            });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestUpdatesAsync(string symbol, Action<OKXOpenInterest> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXOpenInterest>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, 
            new OKXSocketRequestArgument 
            { 
                Channel = "open-interest", 
                Symbol = symbol 
            });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, OKXPeriod period, Action<OKXCandlestick> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Symbol = symbol;
                onData(d);
            }
        });

        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, 
            new OKXSocketRequestArgument 
            { 
                Channel = "candle" + jc, 
                Symbol = symbol 
            });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<OKXTrade> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXTrade>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "trades",
            Symbol = symbol
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPriceUpdatesAsync(OKXInstrumentType instrumentType, string? instrumentFamily, string? symbol, Action<OKXEstimatedPrice> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXEstimatedPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "estimated-price",
            InstrumentFamily = instrumentFamily,
            InstrumentType = instrumentType,
            Symbol = symbol,
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<OKXMarkPrice> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXMarkPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, 
            new OKXSocketRequestArgument 
            { 
                Channel = "mark-price", 
                Symbol = symbol 
            });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, OKXPeriod period, Action<OKXCandlestick> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Symbol = symbol;
                onData(d);
            }
        });

        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, 
            new OKXSocketRequestArgument 
            { 
                Channel = "mark-price-candle" + jc, 
                Symbol = symbol 
            });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitUpdatesAsync(string symbol, Action<OKXLimitPrice> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXLimitPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "price-limit",
            Symbol = symbol
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, OKXOrderBookType orderBookType, Action<DataEvent<OKXOrderBook>> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXOrderBookUpdate>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Symbol = symbol;
                d.Action = data.Data.Action!;
                onData(data.As(d));
            }
        });

        var jc = JsonConvert.SerializeObject(orderBookType, new OrderBookTypeConverter(false));
        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = jc,
            Symbol = symbol
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOptionSummaryUpdatesAsync(string instrumentFamily, Action<OKXOptionSummary> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXOptionSummary>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "opt-summary",
            InstrumentFamily = instrumentFamily
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<OKXFundingRate> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXFundingRate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "funding-rate",
            Symbol = symbol
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string symbol, OKXPeriod period, Action<OKXCandlestick> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Symbol = symbol;
                onData(d);
            }
        });

        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "index-candle" + jc,
            Symbol = symbol
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickerUpdatesAsync(string symbol, Action<OKXIndexTicker> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXIndexTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "index-tickers",
            Symbol = symbol,
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusUpdatesAsync(Action<OKXStatus> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXStatus>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "status"
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }
}
