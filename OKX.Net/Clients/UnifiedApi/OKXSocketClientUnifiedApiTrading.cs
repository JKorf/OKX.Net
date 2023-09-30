using CryptoExchange.Net.Sockets;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Trade;

namespace OKX.Net.Clients.UnifiedApi;

/// <inheritdoc />
public class OKXSocketClientUnifiedApiTrading : IOKXSocketClientUnifiedApiTrading
{
    private readonly OKXSocketClientUnifiedApi _client;
    private static Random _random = new Random();

    private readonly ILogger _logger;

    #region ctor

    internal OKXSocketClientUnifiedApiTrading(ILogger logger, OKXSocketClientUnifiedApi client)
    {
        _client = client;
        _logger = logger;
    }
    #endregion

    /// <inheritdoc />
    public async Task<CallResult<OKXOrderPlaceResponse>> PlaceOrderAsync(string symbol,
        OKXOrderSide side,
        OKXOrderType type,
        OKXTradeMode tradeMode,
        decimal quantity,
        decimal? price = null,
        OKXPositionSide? positionSide = null,

        OKXQuickMarginType? quickMarginType = null,
        int? selfTradePreventionId = null,
        OKXSelfTradePreventionMode? selfTradePreventionMode = null,

        string? asset = null,
        OKXQuantityAsset? quantityAsset = null,
        string? clientOrderId = null,
        bool? reduceOnly = null)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "instId", symbol },
            { "tdMode", EnumConverter.GetString(tradeMode) },
            { "side", EnumConverter.GetString(side) },
            { "ordType", EnumConverter.GetString(type) },
            { "sz", quantity.ToString(CultureInfo.InvariantCulture) },
        };

        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("clOrdId", _client._ref + (clientOrderId ?? RandomString(15)));
        parameters.AddOptionalParameter("tag", _client._ref);
        parameters.AddOptionalParameter("posSide", EnumConverter.GetString(positionSide));
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.AddOptionalParameter("quickMgnType", EnumConverter.GetString(quickMarginType));
        parameters.AddOptionalParameter("stpId", selfTradePreventionId);
        parameters.AddOptionalParameter("stpMode", EnumConverter.GetString(selfTradePreventionMode));

        var result = await _client.QueryInternalAsync<OKXOrderPlaceResponse>(_client.GetUri("/ws/v5/private"), "order", parameters, true, 1).ConfigureAwait(false);
        if (result.Data.Code != "0")
            return result.AsError<OKXOrderPlaceResponse>(new ServerError(int.Parse(result.Data.Code), result.Data.Message, null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<IEnumerable<OKXOrderPlaceResponse>>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders)
    {
        foreach (var order in orders)
        {
            order.Tag = _client._ref;
            order.ClientOrderId = _client._ref + (order.ClientOrderId ?? RandomString(15));
        }

        return await _client.QueryInternalAsync<IEnumerable<OKXOrderPlaceResponse>>(_client.GetUri("/ws/v5/private"), "batch-orders", orders, true, 1).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<CallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "instId", symbol }
        };

        parameters.AddOptionalParameter("ordId", orderId);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        var result = await _client.QueryInternalAsync<OKXOrderCancelResponse>(_client.GetUri("/ws/v5/private"), "cancel-order", parameters, true, 1).ConfigureAwait(false);
        if (result.Data.Code != "0")
            return result.AsError<OKXOrderCancelResponse>(new ServerError(int.Parse(result.Data.Code), result.Data.Message, null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<IEnumerable<OKXOrderCancelResponse>>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> ordersToCancel)
    {
        return await _client.QueryInternalAsync<IEnumerable<OKXOrderCancelResponse>>(_client.GetUri("/ws/v5/private"), "batch-cancel-orders", ordersToCancel, true, 1).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<OKXOrderAmendResponse>> AmendOrderAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        string? requestId = null,
        decimal? newQuantity = null,
        decimal? newPrice = null)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", symbol },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("reqId", requestId);
        parameters.AddOptionalParameter("newSz", newQuantity?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("newPx", newPrice?.ToString(CultureInfo.InvariantCulture));

        var result = await _client.QueryInternalAsync<OKXOrderAmendResponse>(_client.GetUri("/ws/v5/private"), "amend-order", parameters, true, 1).ConfigureAwait(false);
        if (result.Data.Code != "0")
            return result.AsError<OKXOrderAmendResponse>(new ServerError(int.Parse(result.Data.Code), result.Data.Message, null));

        return result;
    }

    /// <inheritdoc />
    public async Task<CallResult<IEnumerable<OKXOrderAmendResponse>>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> ordersToCancel)
    {
        return await _client.QueryInternalAsync<IEnumerable<OKXOrderAmendResponse>>(_client.GetUri("/ws/v5/private"), "batch-amend-orders", ordersToCancel, true, 1).ConfigureAwait(false);
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
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(OKXInstrumentType instrumentType,
        string? instrumentFamily,
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
            Channel = "liquidation-warning",
            InstrumentType = instrumentType,
            InstrumentFamily = instrumentFamily
        });
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<OKXOrderUpdate> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXOrderUpdate>>>>(data =>
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
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/private"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? instrumentFamily,
        Action<OKXAlgoOrderUpdate> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXAlgoOrderUpdate>>>>(data =>
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
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol,
        string? algoId,
        Action<OKXAlgoOrderUpdate> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<DataEvent<OKXSocketUpdateResponse<IEnumerable<OKXAlgoOrderUpdate>>>>(data =>
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
        return await _client.SubscribeInternalAsync(_client.GetUri("/ws/v5/business"), request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    private string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
