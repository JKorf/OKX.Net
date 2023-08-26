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
        bool? reduceOnly = null,
        CancellationToken ct = default)
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
        parameters.AddOptionalParameter("clOrdId", asset); // TODO
        parameters.AddOptionalParameter("tag", asset); // TODO
        parameters.AddOptionalParameter("posSide", EnumConverter.GetString(positionSide));
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.AddOptionalParameter("quickMgnType", EnumConverter.GetString(quickMarginType));
        parameters.AddOptionalParameter("stpId", selfTradePreventionId);
        parameters.AddOptionalParameter("stpMode", EnumConverter.GetString(selfTradePreventionMode));

        var result = await _client.QueryInternalAsync<OKXOrderPlaceResponse>(_client.GetUri("/ws/v5/private"), "order", parameters, true, 1);
        if (result.Data.Code != "0")
            return result.AsError<OKXOrderPlaceResponse>(new OKXRestApiError(int.Parse(result.Data.Code), result.Data.Message, null));

        return result;
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
}
