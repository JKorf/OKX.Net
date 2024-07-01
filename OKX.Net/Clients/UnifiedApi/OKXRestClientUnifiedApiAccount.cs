using OKX.Net.Converters;
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/balance", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXAccountBalance>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPosition>>> GetAccountPositionsAsync(
        OKXInstrumentType? instrumentType = null,
        string? symbol = null,
        string? positionId = null,
        CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("posId", positionId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/positions", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXPosition>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXClosingPosition>>> GetAccountPositionHistoryAsync(
        OKXInstrumentType? instrumentType = null,
        string? symbol = null,
        OKXMarginMode? marginMode = null,
        ClosingPositionType? type = null,
        string? positionId = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", symbol);
        if (marginMode != null)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalParameter("posId", positionId);
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/positions-history", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXClosingPosition>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPositionRisk>>> GetAccountPositionRiskAsync(OKXInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/account-position-risk", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXPositionRisk>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAccountBill>>> GetBillHistoryAsync(
        OKXInstrumentType? instrumentType = null,
        string? asset = null,
        OKXMarginMode? marginMode = null,
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

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        parameters.AddOptionalEnum("ctType", contractType);
        parameters.AddOptionalEnum("type", billType);
        parameters.AddOptionalEnum("subType", billSubType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/bills", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXAccountBill>>(request, parameters, ct).ConfigureAwait(false);
    }
    
    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAccountBill>>> GetBillArchiveAsync(
        OKXInstrumentType? instrumentType = null,
        string? asset = null,
        OKXMarginMode? marginMode = null,
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

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));

        parameters.AddOptionalEnum("ctType", contractType);
        parameters.AddOptionalEnum("type", billType);
        parameters.AddOptionalEnum("subType", billSubType);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/bills-archive", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXAccountBill>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/config", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXAccountConfiguration>(request, null, ct).ConfigureAwait(false);
    }
        
    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountPositionMode>> SetAccountPositionModeAsync(OKXPositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"posMode", JsonConvert.SerializeObject(positionMode, new PositionModeConverter(false)) },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-position-mode", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXAccountPositionMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLeverage>>> GetAccountLeverageAsync(
        string symbols,
        OKXMarginMode marginMode,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", symbols },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/leverage-info", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXLeverage>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLeverage>>> SetAccountLeverageAsync(
        int leverage,
        OKXMarginMode marginMode,
        string? asset = null,
        string? symbol = null,
        OKXPositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        if (leverage < 1)
            throw new ArgumentException("Invalid Leverage");

        if (string.IsNullOrEmpty(asset) && string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

        var parameters = new ParameterCollection {
            {"lever", leverage.ToString() },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("instId", symbol);
        if (positionSide.HasValue)
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-leverage", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXLeverage>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumAmount>>> GetMaximumAmountAsync(
        string symbol,
        OKXTradeMode tradeMode,
        string? asset = null,
        decimal? price = null,
        int? leverage = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", symbol },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("leverage", leverage?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/max-size", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXMaximumAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumAvailableAmount>>> GetMaximumAvailableAmountAsync(
        string symbol,
        OKXTradeMode tradeMode,
        string? asset = null,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", symbol },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/max-avail-size", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXMaximumAvailableAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMarginAmount>>> SetMarginAmountAsync(
        string symbol,
        OKXPositionSide positionSide,
        OKXMarginAddReduce marginAddReduce,
        decimal amount,
        string? asset = null,
        bool? auto = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", symbol },
            {"posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)) },
            {"type", JsonConvert.SerializeObject(marginAddReduce, new MarginAddReduceConverter(false)) },
            {"amt", amount.ToString(CultureInfo.InvariantCulture) },
        };

        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("auto", auto);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/position/margin-balance", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXMarginAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumLoanAmount>>> GetMaximumLoanAmountAsync(
        string instrumentId,
        OKXMarginMode marginMode,
        string? marginAsset = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("mgnCcy", marginAsset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/max-loan", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXMaximumLoanAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXFeeRate>> GetFeeRatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/trade-fee", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXFeeRate>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestAccrued>>> GetInterestAccruedAsync(
        string? symbol = null,
        string? asset = null,
        OKXMarginMode? marginMode = null,
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

        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/interest-accrued", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXInterestAccrued>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestRate>>> GetInterestRateAsync(
        string? asset = null,
        CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/interest-rate", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXInterestRate>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountGreeksType>> SetGreeksAsync(OKXGreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"greeksType", JsonConvert.SerializeObject(greeksType, new GreeksTypeConverter(false)) },
        };

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-greeks", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXAccountGreeksType>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXWithdrawalAmount>>> GetMaximumWithdrawalsAsync(
        string? asset = null,
        CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/account/max-withdrawal", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXWithdrawalAmount>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAsset>>> GetAssetsAsync(string? asset = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/currencies", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXAsset>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingBalance>>> GetFundingBalanceAsync(string? asset = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/balances", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXFundingBalance>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTransferResponse>> TransferAsync(
        string asset,
        decimal amount,
        OKXTransferType type,
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
            { "type", JsonConvert.SerializeObject(type, new TransferTypeConverter(false)) },
        };
        parameters.AddEnum("from", fromAccount);
        parameters.AddEnum("to", toAccount);
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("instId", fromSymbol);
        parameters.AddOptionalParameter("toInstId", toSymbol);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/transfer", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXTransferResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingBill>>> GetFundingBillDetailsAsync(
        string? asset = null,
        OKXFundingBillType? type = null,
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
        if (type.HasValue)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FundingBillTypeConverter(false)));
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("clientId", clientId);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/bills", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXFundingBill>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLightningDeposit>>> GetLightningDepositsAsync(
        string asset,
        decimal amount,
        OKXLightningDepositAccount? account = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection 
        {
            { "ccy", asset },
            { "amt", amount.ToString(CultureInfo.InvariantCulture) },
        };
        if (account.HasValue)
            parameters.AddOptionalParameter("to", JsonConvert.SerializeObject(account, new LightningDepositAccountConverter(false)));

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/deposit-lightning", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXLightningDeposit>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDepositAddress>>> GetDepositAddressAsync(string? asset = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/deposit-address", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXDepositAddress>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDepositHistory>>> GetDepositHistoryAsync(
        string? asset = null,
        string? transactionId = null,
        OKXDepositState? state = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        string? depositId = null,
        string? fromWithdrawalId = null,
        OKXDepositType? type = null,
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/deposit-history", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXDepositHistory>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXWithdrawalResponse>> WithdrawAsync(
        string asset,
        decimal amount,
        OKXWithdrawalDestination destination,
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
            { "dest", JsonConvert.SerializeObject(destination, new WithdrawalDestinationConverter(false)) },
            { "toAddr",toAddress},
            { "fee",fee.ToString(CultureInfo.InvariantCulture)},
        };
        parameters.AddOptionalParameter("chain", network);
        parameters.AddOptionalParameter("areaCode", areaCode);
        parameters.AddOptionalParameter("clientId", clientId);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/withdrawal", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXWithdrawalResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXLightningWithdrawal>> GetLightningWithdrawalsAsync(
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/withdrawal-lightning", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXLightningWithdrawal>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXWithdrawalId>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "wdId",withdrawalId},
        };

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/cancel-withdrawal", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXWithdrawalId>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXWithdrawalHistory>>> GetWithdrawalHistoryAsync(
        string? asset = null,
        string? transactionId = null,
        OKXWithdrawalState? state = null,
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

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/withdrawal-history", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXWithdrawalHistory>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXSavingBalance>>> GetSavingBalancesAsync(string? asset = null, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/finance/savings/balance", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendAsync<IEnumerable<OKXSavingBalance>>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSavingActionResponse>> SavingPurchaseRedemptionAsync(
        string asset,
        decimal amount,
        OKXSavingActionSide side,
        decimal? rate = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
            { "side", JsonConvert.SerializeObject(side, new SavingActionSideConverter(false)) },
        };
        parameters.AddOptionalParameter("rate", rate?.ToString(CultureInfo.InvariantCulture));

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/finance/savings/purchase-redempt", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXSavingActionResponse>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXDustConvertResult>> ConvertDustAsync(IEnumerable<string> assets, CancellationToken ct = default)
    {
       var parameters = new ParameterCollection();
        parameters.AddParameter("ccy", assets);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/asset/convert-dust-assets", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXDustConvertResult>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountIsolatedMarginMode>> SetIsolatedMarginModeAsync(OKXInstrumentType instumentType, OKXIsolatedMarginMode isolatedMarginMode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("type", instumentType);
        parameters.AddEnum("isoMode", isolatedMarginMode);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-isolated-mode", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXAccountIsolatedMarginMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTransferInfo>> GetTransferAsync(string? transferId = null, string? clientTransferId = null, OKXTransferType? type = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("transId", transferId);
        parameters.AddOptional("clientId", clientTransferId);
        parameters.AddOptionalEnum("type", type);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/transfer-state", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXTransferInfo>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountMode>> SetAccountModeAsync(AccountLevel mode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("acctLv", mode);

        var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v5/account/set-account-level", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXAccountMode>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXInviteeDetails>> GetAffiliateInviteeDetailsAsync(string userId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection ()
        {
            { "uid", userId }
        };

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/affiliate/invitee/detail", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXInviteeDetails>(request, parameters, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAssetValuation>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", asset);

        var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v5/asset/asset-valuation", OKXExchange.RateLimiter.Public, 1, true);
        return await _baseClient.SendGetSingleAsync<OKXAssetValuation>(request, parameters, ct).ConfigureAwait(false);
    }
}
