using CryptoExchange.Net.RateLimiting.Guards;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Affiliate;
using OKX.Net.Objects.Funding;

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
    public async Task<WebCallResult<OKXAccountBalance>> GetAccountBalanceAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/balance", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountBalance>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPosition>>> GetPositionsAsync(
        InstrumentType? instrumentType = null,
        string? symbol = null,
        string? positionId = null,
        CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("posId", positionId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/positions", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXPosition>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXClosingPosition>>> GetPositionHistoryAsync(
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
       var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("mgnMode", marginMode);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalParameter("posId", positionId);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/positions-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXClosingPosition>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPositionRisk>>> GetPositionRiskAsync(InstrumentType? instrumentType = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/account-position-risk", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXPositionRisk>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAccountBill>>> GetBillHistoryAsync(
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

       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("after", fromId);
        parameters.AddOptionalParameter("before", toId);
        parameters.AddOptionalParameter("limit", limit.ToString());

        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("mgnMode", marginMode);
        parameters.AddOptionalEnum("ctType", contractType);
        parameters.AddOptionalEnum("type", billType);
        parameters.AddOptionalEnum("subType", billSubType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/bills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXAccountBill>>(request, parameters, ct).ConfigureAwait(false);
    }
    
    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAccountBill>>> GetBillArchiveAsync(
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

       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("begin", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("end", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("after", fromId);
        parameters.AddOptionalParameter("before", toId);
        parameters.AddOptionalParameter("limit", limit.ToString());

        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("mgnMode", marginMode);
        parameters.AddOptionalEnum("ctType", contractType);
        parameters.AddOptionalEnum("type", billType);
        parameters.AddOptionalEnum("subType", billSubType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/bills-archive", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXAccountBill>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/config", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey)); 
        return await _baseClient.SendGetSingleAsync<OKXAccountConfiguration>(request, null, ct).ConfigureAwait(false);
    }
        
    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("posMode", positionMode);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-position-mode", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountPositionMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLeverage>>> GetLeverageAsync(
        string symbols,
        MarginMode marginMode,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", symbols }
        };
        parameters.AddEnum("mgnMode", marginMode);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/leverage-info", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXLeverage>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLeverage>>> SetLeverageAsync(
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

        var parameters = new ParameterCollection {
            {"lever", leverage.ToString() }
        };
        parameters.AddEnum("mgnMode", marginMode);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalEnum("posSide", positionSide);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-leverage", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXLeverage>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumAmount>>> GetMaximumAmountAsync(
        string symbol,
        TradeMode tradeMode,
        string? asset = null,
        decimal? price = null,
        int? leverage = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", symbol }
        };
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("leverage", leverage?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/max-size", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXMaximumAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumAvailableAmount>>> GetMaximumAvailableAmountAsync(
        string symbol,
        TradeMode tradeMode,
        string? asset = null,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", symbol },
        };
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/max-avail-size", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXMaximumAvailableAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMarginAmount>>> SetMarginAmountAsync(
        string symbol,
        PositionSide positionSide,
        MarginAddReduce marginAddReduce,
        decimal amount,
        string? asset = null,
        bool? auto = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", symbol },
            {"amt", amount.ToString(CultureInfo.InvariantCulture) },
        };

        parameters.AddEnum("posSide", positionSide);
        parameters.AddEnum("type", marginAddReduce);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("auto", auto);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/position/margin-balance", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXMarginAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumLoanAmount>>> GetMaximumLoanAmountAsync(
        string instrumentId,
        MarginMode marginMode,
        string? marginAsset = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", instrumentId }
        };
        parameters.AddEnum("mgnMode", marginMode);
        parameters.AddOptionalParameter("mgnCcy", marginAsset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/max-loan", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXMaximumLoanAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXFeeRate>> GetFeeRatesAsync(
        InstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/trade-fee", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXFeeRate>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestAccrued>>> GetInterestAccruedAsync(
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

       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalEnum("mgnMode", marginMode);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/interest-accrued", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXInterestAccrued>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestRate>>> GetInterestRateAsync(
        string? asset = null,
        CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/interest-rate", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXInterestRate>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountGreeksType>> SetGreeksAsync(GreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("greeksType", greeksType);
        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-greeks", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountGreeksType>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXWithdrawalAmount>>> GetMaximumWithdrawalsAsync(
        string? asset = null,
        CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/max-withdrawal", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXWithdrawalAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAsset>>> GetAssetsAsync(string? asset = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/currencies", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXAsset>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingBalance>>> GetFundingBalanceAsync(string? asset = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/balances", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXFundingBalance>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTransferResponse>> TransferAsync(
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
        var parameters = new ParameterCollection {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
        };
        parameters.AddEnum("from", fromAccount);
        parameters.AddEnum("type", type);
        parameters.AddEnum("to", toAccount);
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("instId", fromSymbol);
        parameters.AddOptionalParameter("toInstId", toSymbol);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/transfer", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXTransferResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingBill>>> GetFundingBillDetailsAsync(
        string? asset = null,
        FundingBillType? type = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        string? clientId = null,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("clientId", clientId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/bills", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXFundingBill>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLightningDeposit>>> GetLightningDepositsAsync(
        string asset,
        decimal amount,
        LightningDepositAccount? account = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection 
        {
            { "ccy", asset },
            { "amt", amount.ToString(CultureInfo.InvariantCulture) },
        };
        parameters.AddOptionalEnum("to", account);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/deposit-lightning", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXLightningDeposit>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDepositAddress>>> GetDepositAddressAsync(string? asset = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/deposit-address", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXDepositAddress>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDepositHistory>>> GetDepositHistoryAsync(
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

       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("txId", transactionId);
        parameters.AddOptionalParameter("state", EnumConverter.GetString(state));
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("depId", depositId);
        parameters.AddOptionalParameter("fromWdId", fromWithdrawalId);
        parameters.AddOptionalParameter("type", EnumConverter.GetString(type));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/deposit-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXDepositHistory>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXWithdrawalResponse>> WithdrawAsync(
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
        var parameters = new ParameterCollection {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
            { "toAddr",toAddress},
            { "fee",fee.ToString(CultureInfo.InvariantCulture)},
        };
        parameters.AddEnum("dest", destination);
        parameters.AddOptionalParameter("chain", network);
        parameters.AddOptionalParameter("areaCode", areaCode);
        parameters.AddOptionalParameter("clientId", clientId);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/withdrawal", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXWithdrawalResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXLightningWithdrawal>> GetLightningWithdrawalAsync(
        string asset,
        string invoice,
        string? memo = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection 
        {
            { "ccy", asset },
            { "invoice", invoice },
        };
        if (!string.IsNullOrEmpty(memo))
            parameters.AddOptionalParameter("memo", memo);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/withdrawal-lightning", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(2, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXLightningWithdrawal>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXWithdrawalId>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "wdId",withdrawalId},
        };

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/cancel-withdrawal", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXWithdrawalId>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXWithdrawalHistory>>> GetWithdrawalHistoryAsync(
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

       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("txId", transactionId);
        parameters.AddOptionalParameter("state", EnumConverter.GetString(state));
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("wdId", withdrawalId);
        parameters.AddOptionalParameter("clientId", clientId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/withdrawal-history", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXWithdrawalHistory>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXSavingBalance>>> GetSavingBalancesAsync(string? asset = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/finance/savings/balance", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendAsync<IEnumerable<OKXSavingBalance>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSavingActionResponse>> SavingPurchaseRedemptionAsync(
        string asset,
        decimal amount,
        SavingActionSide side,
        decimal? rate = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
        };

        parameters.AddEnum("side", side);
        parameters.AddOptionalParameter("rate", rate?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/finance/savings/purchase-redempt", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(6, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXSavingActionResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXDustConvertResult>> ConvertDustAsync(IEnumerable<string> assets, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddParameter("ccy", assets);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/convert-dust-assets", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXDustConvertResult>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountIsolatedMarginMode>> SetIsolatedMarginModeAsync(InstrumentType instumentType, IsolatedMarginMode isolatedMarginMode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("type", instumentType);
        parameters.AddEnum("isoMode", isolatedMarginMode);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-isolated-mode", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountIsolatedMarginMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTransferInfo>> GetTransferAsync(string? transferId = null, string? clientTransferId = null, TransferType? type = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("transId", transferId);
        parameters.AddOptional("clientId", clientTransferId);
        parameters.AddOptionalEnum("type", type);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/transfer-state", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXTransferInfo>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountMode>> SetAccountModeAsync(AccountLevel mode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("acctLv", mode);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-account-level", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(5, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAccountMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInviteeDetails>> GetAffiliateInviteeDetailsAsync(string userId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection ()
        {
            { "uid", userId }
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/affiliate/invitee/detail", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXInviteeDetails>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAssetValuation>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/asset-valuation", OKXExchange.RateLimiter.EndpointGate, 1, true,
            limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
        return await _baseClient.SendGetSingleAsync<OKXAssetValuation>(request, parameters, ct).ConfigureAwait(false);
    }
}
