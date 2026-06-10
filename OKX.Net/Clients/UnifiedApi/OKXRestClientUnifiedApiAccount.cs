using CryptoExchange.Net.RateLimiting.Guards;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Affiliate;
using OKX.Net.Objects.Funding;
using OKX.Net.Objects.Public;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiAccount : IOKXRestClientUnifiedApiAccount
{
    private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
    private readonly OKXRestClientUnifiedApi _baseClient;

    internal OKXRestClientUnifiedApiAccount(OKXRestClientUnifiedApi baseClient)
    {
        _baseClient = baseClient;
    }

    /// <inheritdoc />
    public async Task<HttpResult<OKXAccountBalance>> GetAccountBalanceAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/balance", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountBalance>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXPosition[]>> GetPositionsAsync(
        InstrumentType? instrumentType = null,
        string? symbol = null,
        string? positionId = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("instId", symbol);
        parameters.Add("posId", positionId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/positions", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXPosition[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXClosingPosition[]>> GetPositionHistoryAsync(
        InstrumentType? instrumentType = null,
        string? symbol = null,
        MarginMode? marginMode = null,
        ClosingPositionType? type = null,
        string? positionId = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("mgnMode", marginMode);
        parameters.Add("type", type);
        parameters.Add("posId", positionId);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/positions-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXClosingPosition[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXPositionRisk[]>> GetPositionRiskAsync(InstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/account-position-risk", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXPositionRisk[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXRiskState[]>> GetAccountRiskStateAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/risk-state", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXRiskState[]>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountBill[]>> GetBillHistoryAsync(
        InstrumentType? instrumentType = null,
        string? asset = null,
        MarginMode? marginMode = null,
        ContractType? contractType = null,
        AccountBillType? billType = null,
        AccountBillSubType? billSubType = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        string? fromId = null,
        string? toId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);
        parameters.Add("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("after", fromId);
        parameters.Add("before", toId);
        parameters.Add("limit", limit.ToString());

        parameters.Add("instType", instrumentType);
        parameters.Add("mgnMode", marginMode);
        parameters.Add("ctType", contractType);
        parameters.Add("type", billType);
        parameters.Add("subType", billSubType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/bills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAccountBill[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountBill[]>> GetBillArchiveAsync(
        InstrumentType? instrumentType = null,
        string? asset = null,
        MarginMode? marginMode = null,
        ContractType? contractType = null,
        AccountBillType? billType = null,
        AccountBillSubType? billSubType = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        string? fromId = null,
        string? toId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);
        parameters.Add("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("after", fromId);
        parameters.Add("before", toId);
        parameters.Add("limit", limit.ToString());

        parameters.Add("instType", instrumentType);
        parameters.Add("mgnMode", marginMode);
        parameters.Add("ctType", contractType);
        parameters.Add("type", billType);
        parameters.Add("subType", billSubType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/bills-archive", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAccountBill[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/config", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountConfiguration>(request, null, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("posMode", positionMode);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/set-position-mode", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountPositionMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXLeverage[]>> GetLeverageAsync(
        string symbols,
        MarginMode marginMode,
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbols }
        };
        parameters.Add("mgnMode", marginMode);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/leverage-info", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXLeverage[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXLeverage[]>> SetLeverageAsync(
        int leverage,
        MarginMode marginMode,
        string? asset = null,
        string? symbol = null,
        PositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        if (leverage < 1)
            throw new ArgumentException("Invalid Leverage");

        if (string.IsNullOrEmpty(asset) && string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"lever", leverage.ToString() }
        };
        parameters.Add("mgnMode", marginMode);
        parameters.Add("ccy", asset);
        parameters.Add("instId", symbol);
        parameters.Add("posSide", positionSide);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/set-leverage", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXLeverage[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXEstimatedLeverageInfo[]>> GetEstimatedLeverageInfoAsync(InstrumentType instrumentType, MarginMode marginMode, int leverage, string? symbol = null, string? asset = null, PositionSide? positionSide = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("mgnMode", marginMode);
        parameters.Add("lever", leverage.ToString(CultureInfo.InvariantCulture));
        parameters.Add("instId", symbol);
        parameters.Add("ccy", asset);
        parameters.Add("posSide", positionSide);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/adjust-leverage-info", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXEstimatedLeverageInfo[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXMaximumAmount[]>> GetMaximumAmountAsync(
        string symbol,
        TradeMode tradeMode,
        string? asset = null,
        decimal? price = null,
        int? leverage = null,
        string? tradeQuoteAsset = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol }
        };
        parameters.Add("tdMode", tradeMode);
        parameters.Add("ccy", asset);
        parameters.Add("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("leverage", leverage?.ToString(CultureInfo.InvariantCulture));
        parameters.Add("tradeQuoteCcy", tradeQuoteAsset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/max-size", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXMaximumAmount[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXMaximumAvailableAmount[]>> GetMaximumAvailableAmountAsync(
        string symbol,
        TradeMode tradeMode,
        string? asset = null,
        bool? reduceOnly = null,
        string? tradeQuoteAsset = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol },
        };
        parameters.Add("tdMode", tradeMode);
        parameters.Add("ccy", asset);
        parameters.Add("reduceOnly", reduceOnly);
        parameters.Add("tradeQuoteCcy", tradeQuoteAsset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/max-avail-size", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXMaximumAvailableAmount[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXMarginAmount[]>> SetMarginAmountAsync(
        string symbol,
        PositionSide positionSide,
        MarginAddReduce marginAddReduce,
        decimal amount,
        string? asset = null,
        bool? auto = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            {"instId", symbol },
            {"amt", amount.ToString(CultureInfo.InvariantCulture) },
        };

        parameters.Add("posSide", positionSide);
        parameters.Add("type", marginAddReduce);
        parameters.Add("ccy", asset);
        parameters.Add("auto", auto);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/position/margin-balance", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXMarginAmount[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXMaximumLoanAmount[]>> GetMaximumLoanAmountAsync(
        MarginMode marginMode,
        string? symbol = null,
        string? asset = null,
        string? marginAsset = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("mgnMode", marginMode);
        parameters.Add("mgnCcy", marginAsset);
        parameters.Add("instId", symbol);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/max-loan", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXMaximumLoanAmount[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXFeeRate>> GetFeeRatesAsync(
        InstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        string? instrumentFamily = null,
        SymbolRuleType? ruleType = null,
        string? groupId = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("instId", symbol);
        parameters.Add("uly", underlying);
        parameters.Add("instFamily", instrumentFamily);
        parameters.Add("groupId", groupId);
        parameters.Add("ruleType", ruleType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/trade-fee", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXFeeRate>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXInterestAccrued[]>> GetInterestAccruedAsync(
        string? symbol = null,
        string? asset = null,
        MarginMode? marginMode = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instId", symbol);
        parameters.Add("ccy", asset);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("limit", limit.ToString());
        parameters.Add("mgnMode", marginMode);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/interest-accrued", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXInterestAccrued[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountInterestRate[]>> GetInterestRateAsync(
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/interest-rate", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAccountInterestRate[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXBorrowInterestLimit[]>> GetBorrowInterestLimitAsync(LoanType? type = null, string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("type", type);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/interest-limits", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXBorrowInterestLimit[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountGreeksType>> SetGreeksAsync(GreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("greeksType", greeksType);
        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/set-greeks", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountGreeksType>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXWithdrawalAmount[]>> GetMaximumWithdrawalsAsync(
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/max-withdrawal", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXWithdrawalAmount[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAsset[]>> GetAssetsAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/currencies", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXAsset[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXFundingBalance[]>> GetFundingBalanceAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/balances", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXFundingBalance[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXTransferResponse>> TransferAsync(
        string asset,
        decimal amount,
        TransferType type,
        AccountType fromAccount,
        AccountType toAccount,
        string? subAccountName = null,
        string? fromSymbol = null,
        string? toSymbol = null,
        bool? loanTransfer = null,
        string? clientId = null,
        bool? ignorePositionRisk = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
        };
        parameters.Add("from", fromAccount);
        parameters.Add("type", type);
        parameters.Add("to", toAccount);
        parameters.Add("subAcct", subAccountName);
        parameters.Add("instId", fromSymbol);
        parameters.Add("toInstId", toSymbol);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/asset/transfer", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXTransferResponse>(request, parameters, ct, rateLimitKeySuffix: asset).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXFundingBill[]>> GetFundingBillDetailsAsync(
        string? asset = null,
        FundingBillType? type = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        string? clientId = null,
        long? startBillId = null,
        long? endBillId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        if ((startTime != null || endTime != null) && (startBillId != null || endBillId != null))
            throw new ArgumentException("Filter can be either on start/end bill id or start/end time");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);
        parameters.Add("type", type);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddAsString("before", endBillId);
        parameters.AddAsString("after", startBillId);
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));
        parameters.Add("clientId", clientId);
        parameters.Add("pagingType", startBillId != null || endBillId != null ? "2" : null);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/bills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXFundingBill[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXFundingBill[]>> GetFundingBillHistoryAsync(
        string? asset = null,
        FundingBillType? type = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        string? clientId = null,
        long? startBillId = null,
        long? endBillId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        if ((startTime != null || endTime != null) && (startBillId != null || endBillId != null))
            throw new ArgumentException("Filter can be either on start/end bill id or start/end time");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);
        parameters.Add("type", type);
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddAsString("before", endBillId);
        parameters.AddAsString("after", startBillId);
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));
        parameters.Add("clientId", clientId);
        parameters.Add("pagingType", startBillId != null || endBillId != null ? "2" : null);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/bills-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXFundingBill[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXDepositAddress[]>> GetDepositAddressAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/deposit-address", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXDepositHistory[]>> GetDepositHistoryAsync(
        string? asset = null,
        string? transactionId = null,
        DepositState? state = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        string? depositId = null,
        string? fromWithdrawalId = null,
        DepositType? type = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);
        parameters.Add("txId", transactionId);
        parameters.Add("state", EnumConverter.GetString(state));
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));
        parameters.Add("depId", depositId);
        parameters.Add("fromWdId", fromWithdrawalId);
        parameters.Add("type", EnumConverter.GetString(type));

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/deposit-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXDepositHistory[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXWithdrawalResponse>> WithdrawAsync(
        string asset,
        decimal amount,
        WithdrawalDestination destination,
        string toAddress,
        decimal fee,
        string? network = null,
        string? areaCode = null,
        string? clientId = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
            { "toAddr",toAddress},
            { "fee",fee.ToString(CultureInfo.InvariantCulture)},
        };
        parameters.Add("dest", destination);
        parameters.Add("chain", network);
        parameters.Add("areaCode", areaCode);
        parameters.Add("clientId", clientId);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/asset/withdrawal", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXWithdrawalResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXWithdrawalId>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            { "wdId",withdrawalId},
        };

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/asset/cancel-withdrawal", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXWithdrawalId>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXWithdrawalHistory[]>> GetWithdrawalHistoryAsync(
        string? asset = null,
        string? transactionId = null,
        WithdrawalState? state = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        string? withdrawalId = null,
        string? clientId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);
        parameters.Add("txId", transactionId);
        parameters.Add("state", EnumConverter.GetString(state));
        parameters.Add("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.Add("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.Add("limit", limit.ToString(CultureInfo.InvariantCulture));
        parameters.Add("wdId", withdrawalId);
        parameters.Add("clientId", clientId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/withdrawal-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXWithdrawalHistory[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSavingBalance[]>> GetSavingBalancesAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/finance/savings/balance", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXSavingBalance[]>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSavingActionResponse>> SavingPurchaseRedemptionAsync(
        string asset,
        decimal amount,
        SavingActionSide side,
        decimal? rate = null,
        CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings) {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
        };

        parameters.Add("side", side);
        parameters.Add("rate", rate?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/finance/savings/purchase-redempt", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSavingActionResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    #region Get Easy Convert Dust Assets

    /// <inheritdoc />
    public async Task<HttpResult<OKXDustAssets>> GetEasyConvertDustAssetsAsync(AccountType? sourceAccount = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        if (sourceAccount != null)
            parameters.Add("source", sourceAccount == AccountType.Funding ? "2" : "1");
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v5/trade/easy-convert-currency-list", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendGetSingleAsync<OKXDustAssets>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXDustConvertEntry[]>> EasyConvertDustAsync(IEnumerable<string> assets, string targetAsset, AccountType? sourceAccount = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.AddParameter("fromCcy", assets.ToArray());
        parameters.AddParameter("toCcy", targetAsset);
        if (sourceAccount != null)
            parameters.AddParameter("source", sourceAccount == AccountType.Funding ? "2" : "1");

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/trade/easy-convert", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXDustConvertEntry[]>(request, parameters, ct).ConfigureAwait(false);
    }

    #region Get Easy Convert Dust History

    /// <inheritdoc />
    public async Task<HttpResult<OKXDustConvertEntry[]>> GetEasyConvertDustHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("after", startTime);
        parameters.Add("before", endTime);
        parameters.Add("limit", limit);
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v5/trade/easy-convert-history", OKXExchange.RateLimiter.EndpointGate, 1, true);
        var result = await _baseClient.SendAsync<OKXDustConvertEntry[]>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountIsolatedMarginMode>> SetIsolatedMarginModeAsync(InstrumentType instumentType, IsolatedMarginMode isolatedMarginMode, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("type", instumentType);
        parameters.Add("isoMode", isolatedMarginMode);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/set-isolated-mode", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountIsolatedMarginMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXTransferInfo>> GetTransferAsync(string? transferId = null, string? clientTransferId = null, TransferType? type = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("transId", transferId);
        parameters.Add("clientId", clientTransferId);
        parameters.Add("type", type);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/transfer-state", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXTransferInfo>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXPresetAccountMode>> PresetAccountModeSwitchAsync(AccountLevel mode, int? leverage = null, RiskOffsetType? riskOffsetType = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("acctLv", mode);
        parameters.Add("lever", leverage);
        parameters.Add("riskOffsetType", riskOffsetType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/account-level-switch-preset", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXPresetAccountMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountSwitchCheckResult>> PrecheckAccountModeSwitchAsync(AccountLevel mode, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("acctLv", mode);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/set-account-switch-precheck", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountSwitchCheckResult>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAccountMode>> SetAccountModeAsync(AccountLevel mode, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("acctLv", mode);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/set-account-level", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXInviteeDetails>> GetAffiliateInviteeDetailsAsync(string userId, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings)
        {
            { "uid", userId }
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/affiliate/invitee/detail", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXInviteeDetails>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXAssetValuation>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/asset/asset-valuation", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAssetValuation>(request, parameters, ct).ConfigureAwait(false);
    }

    #region Manual Borrow Repay

    /// <inheritdoc />
    public async Task<HttpResult<OKXBorrowRepayResult>> ManualBorrowRepay(string asset, BorrowRepaySide BorrowRepaySide, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);
        parameters.Add("side", BorrowRepaySide);
        parameters.Add("amt", quantity);
        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v5/account/spot-manual-borrow-repay", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(3), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendGetSingleAsync<OKXBorrowRepayResult>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    #region Set Auto Repay

    /// <inheritdoc />
    public async Task<HttpResult<OKXAutoRepayStatus>> SetAutoRepayAsync(bool autoRepay, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("autoRepay", autoRepay);
        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v5/account/set-auto-repay", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        var result = await _baseClient.SendGetSingleAsync<OKXAutoRepayStatus>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    #region Get Borrow Repay History

    /// <inheritdoc />
    public async Task<HttpResult<OKXBorrowRepayEntry[]>> GetBorrowRepayHistoryAsync(string? asset = null, BorrowRepayType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);
        parameters.Add("type", type);
        parameters.Add("after", startTime);
        parameters.Add("before", endTime);
        parameters.Add("limit", limit);
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v5/account/spot-borrow-repay-history", OKXExchange.RateLimiter.EndpointGate, 1, true);
        var result = await _baseClient.SendAsync<OKXBorrowRepayEntry[]>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    #region Get Symbols

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXInstrument[]>> GetSymbolsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("instType", instrumentType);
        parameters.Add("uly", underlying);
        parameters.Add("instId", symbol);
        parameters.Add("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/instruments", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXInstrument[]>(request, parameters, ct, rateLimitKeySuffix: instrumentType.ToString()).ConfigureAwait(false);
    }

    #endregion

    #region Set Fee Type

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXFeeType>> SetFeeTypeAsync(FeeType feeType, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("feeType", feeType);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/set-fee-type", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXFeeType>(request, parameters, ct).ConfigureAwait(false);
    }

    #endregion

    #region Set Settle Asset

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXSettleAsset>> SetSettleAssetAsync(string settleAsset, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("settleCcy", settleAsset);

        var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v5/account/set-settle-currency", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSettleAsset>(request, parameters, ct).ConfigureAwait(false);
    }

    #endregion

    #region Get Greeks

    /// <inheritdoc />
    public virtual async Task<HttpResult<OKXGreeks[]>> GetGreeksAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Parameters(OKXExchange._parameterSerializationSettings);
        parameters.Add("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v5/account/greeks", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<OKXGreeks[]>(request, parameters, ct).ConfigureAwait(false);
    }

    #endregion
}
