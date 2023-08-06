using CryptoExchange.Net.Sockets;
using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Market;
using OKX.Net.Objects.Options;
using OKX.Net.Objects.Public;
using OKX.Net.Objects.System;
using OKX.Net.Objects.Trade;
using System.IO;
using System.IO.Compression;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
public class OKXSocketClientUnifiedApi : SocketApiClient, IOKXSocketClientUnifiedApi
{
    #region ctor

    internal OKXSocketClientUnifiedApi(ILogger logger, OKXSocketOptions options) :
        base(logger, options.Environment.SocketAddress, options, options.UnifiedOptions)
    {
        SetDataInterpreter(DecompressData, HandlePongData);
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => "ping");
        AddGenericHandler("Ping", (a) => { });
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/business"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/business"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/business"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
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
        return await SubscribeAsync(GetUri("/ws/v5/public"), request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(
        string? asset,
        bool regularUpdates,
        Action<OKXAccountBalance> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXAccountBalance>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "account",
            Asset = asset,
            ExtraParams = "{ \"updateInterval\": " + (regularUpdates ? 1 : 0) + " }"
        });
        return await SubscribeAsync(GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        bool regularUpdates,
        Action<OKXPosition> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXPosition>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "positions",
            Symbol = symbol,
            InstrumentType = instrumentType,
            InstrumentFamily = instrumentFamily,
            ExtraParams = "{ \"updateInterval\": " + (regularUpdates ? 1 : 0) + " }"
        });
        return await SubscribeAsync(GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(
        Action<OKXPositionRisk> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXPositionRisk>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "balance_and_position"
        });
        return await SubscribeAsync(GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<OKXOrder> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "orders",
            Symbol = symbol,
            InstrumentType = instrumentType,
            InstrumentFamily = instrumentFamily,
        });
        return await SubscribeAsync(GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<OKXAlgoOrder> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXAlgoOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });


        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "orders-algo",
            Symbol = symbol,
            InstrumentType = instrumentType,
            InstrumentFamily = instrumentFamily,
        });
        return await SubscribeAsync(GetUri("/ws/v5/business"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? algoId,
        Action<OKXAlgoOrder> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXAlgoOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });


        var request = new OKXSocketRequest(OKXSocketOperation.Subscribe, new OKXSocketRequestArgument
        {
            Channel = "algo-advance",
            Symbol = symbol,
            InstrumentType = instrumentType,
            AlgoId = algoId,
        });
        return await SubscribeAsync(GetUri("/ws/v5/business"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    internal string GetUri(string path) => BaseAddress.Trim(new[] { '/' }) + path;

    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OKXAuthenticationProvider((OKXApiCredentials)credentials);

    internal static string DecompressData(byte[] byteData)
    {
        using var decompressedStream = new MemoryStream();
        using var compressedStream = new MemoryStream(byteData);
        using var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress);
        deflateStream.CopyTo(decompressedStream);
        decompressedStream.Position = 0;

        using var streamReader = new StreamReader(decompressedStream);
        return streamReader.ReadToEnd();
    }

    internal static string HandlePongData(string data)
    {
        if (data == "pong")
            return "{ \"op\": \"pong\" }";

        return data;
    }


    /// <inheritdoc />
    protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
    {
        // Check Point
        if (AuthenticationProvider == null)
            return new CallResult<bool>(new NoApiCredentialsError());

        var okxAuthProvider = (OKXAuthenticationProvider)AuthenticationProvider;

        var timestamp = (DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow) / 1000).Value.ToString(CultureInfo.InvariantCulture);
        var signature = okxAuthProvider.SignWebsocket(timestamp);
        var request = new OKXSocketAuthRequest(OKXSocketOperation.Login, new OKXSocketAuthRequestArgument
        {
            ApiKey = okxAuthProvider.ApiKey,
            Passphrase = okxAuthProvider.Passphrase,
            Timestamp = timestamp,
            Signature = signature,
        });

        // Try to Login
        var result = new CallResult<bool>(new ServerError("No response from server"));
        await s.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), null, data =>
        {
            if ((string?)data["event"] != "login")
                return false;

            var authResponse = Deserialize<OKXSocketResponse>(data);
            if (!authResponse)
            {
                _logger.Log(LogLevel.Warning, "Authorization failed: " + authResponse.Error);
                result = new CallResult<bool>(authResponse.Error!);
                return true;
            }
            if (!authResponse.Data.Success)
            {
                _logger.Log(LogLevel.Warning, "Authorization failed: " + authResponse.Error!.Message);
                result = new CallResult<bool>(new ServerError(authResponse.Error.Code!.Value, authResponse.Error.Message));
                return true;
            }

            _logger.Log(LogLevel.Debug, "Authorization completed");
            result = new CallResult<bool>(true);
            return true;
        });

        return result;
    }

    /// <inheritdoc />
    protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = null!;

        // Check for Error
        if (data is JObject && data["event"] != null && (string)data["event"]! == "error" && data["code"] != null && data["msg"] != null)
        {
            _logger.Log(LogLevel.Warning, "Query failed: " + (string)data["msg"]!);
            callResult = new CallResult<T>(new ServerError($"{(string)data["code"]!}, {(string)data["msg"]!}"));
            return true;
        }

        // Login Request
        if (data is JObject && data["event"] != null && (string)data["event"]! == "login")
        {
            var desResult = Deserialize<T>(data);
            if (!desResult)
            {
                _logger.Log(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            callResult = new CallResult<T>(desResult.Data);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
    {
        callResult = null!;

        // Check for Error
        // 30040: {0} Channel : {1} doesn't exist
        if (message.HasValues && message["event"] != null && (string)message["event"]! == "error" && message["code"] != null)
        {
            _logger.Log(LogLevel.Warning, "Subscription failed: " + (string)message["msg"]!);
            callResult = new CallResult<object>(new ServerError($"{(string)message["code"]!}, {(string)message["msg"]!}"));
            return true;
        }

        // Check for Success
        if (message.HasValues && message["event"] != null && (string)message["event"]! == "subscribe" && message["arg"]?["channel"] != null)
        {
            if (request is OKXSocketRequest socRequest)
            {
                if (socRequest.Arguments.FirstOrDefault().Channel == (string)message["arg"]?["channel"]!)
                {
                    _logger.Log(LogLevel.Debug, "Subscription completed");
                    callResult = new CallResult<object>(true);
                    return true;
                }
            }
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool MessageMatchesHandler(SocketConnection s, JToken message, string identifier)
    {
        if (identifier == "Ping" && message["op"]?.ToString() == "pong")
        {
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool MessageMatchesHandler(SocketConnection s, JToken message, object request)
    {
        // Check Point
        if (message.Type != JTokenType.Object)
            return false;

        if (message["op"]?.ToString() == "pong")
            return false;

        // Socket Request
        if (request is OKXSocketRequest hRequest)
        {
            // Check for Error
            if (message is JObject && message["event"] != null && (string)message["event"]! == "error" && message["code"] != null && message["msg"] != null)
                return false;

            // Check for Channel
            if (hRequest.Operation != OKXSocketOperation.Subscribe || message["arg"]?["channel"] == null)
                return false;

            // Compare Request and Response Arguments
            var reqArg = hRequest.Arguments.FirstOrDefault();
            var resArg = JsonConvert.DeserializeObject<OKXSocketRequestArgument>(message["arg"]!.ToString());

            // Check Data
            if (reqArg.Channel == resArg!.Channel &&
                reqArg.InstrumentFamily == resArg.InstrumentFamily &&
                reqArg.Symbol == resArg.Symbol &&
                reqArg.InstrumentType == resArg.InstrumentType)
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
    {
        if (s == null || s.Request == null)
            return false;

        var request = new OKXSocketRequest(OKXSocketOperation.Unsubscribe, ((OKXSocketRequest)s.Request).Arguments[0]);
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), s, data =>
        {
            if (data.Type != JTokenType.Object)
                return false;

            if ((string?)data["event"] == "unsubscribe")
            {
                return (string?)data["arg"]?["channel"] == request.Arguments.FirstOrDefault().Channel;
            }

            return false;
        });
        return false;
    }
}
