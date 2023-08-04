using OKX.Net.Converters;
using OKX.Net.Enums;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Funding;

namespace OKX.Net.Clients.UnifiedApi;
internal class OKXRestClientUnifiedApiAccount : IOKXRestClientUnifiedApiAccount
{
    private readonly OKXRestClientUnifiedApi _baseClient;

    #region Account Endpoints
    private const string Endpoints_V5_Account_Balance = "api/v5/account/balance";
    private const string Endpoints_V5_Account_Positions = "api/v5/account/positions";
    private const string Endpoints_V5_Account_PositionsHistory = "api/v5/account/positions-history";
    private const string Endpoints_V5_Account_PositionRisk = "api/v5/account/account-position-risk";
    private const string Endpoints_V5_Account_Bills = "api/v5/account/bills";
    private const string Endpoints_V5_Account_BillsArchive = "api/v5/account/bills-archive";
    private const string Endpoints_V5_Account_Config = "api/v5/account/config";
    private const string Endpoints_V5_Account_SetPositionMode = "api/v5/account/set-position-mode";
    private const string Endpoints_V5_Account_SetLeverage = "api/v5/account/set-leverage";
    private const string Endpoints_V5_Account_MaxSize = "api/v5/account/max-size";
    private const string Endpoints_V5_Account_MaxAvailSize = "api/v5/account/max-avail-size";
    private const string Endpoints_V5_Account_PositionMarginBalance = "api/v5/account/position/margin-balance";
    private const string Endpoints_V5_Account_LeverageInfo = "api/v5/account/leverage-info";
    private const string Endpoints_V5_Account_MaxLoan = "api/v5/account/max-loan";
    private const string Endpoints_V5_Account_TradeFee = "api/v5/account/trade-fee";
    private const string Endpoints_V5_Account_InterestAccrued = "api/v5/account/interest-accrued";
    private const string Endpoints_V5_Account_InterestRate = "api/v5/account/interest-rate";
    private const string Endpoints_V5_Account_SetGreeks = "api/v5/account/set-greeks";
    private const string Endpoints_V5_Account_MaxWithdrawal = "api/v5/account/max-withdrawal";
    #endregion

    #region Funding Endpoints
    private const string Endpoints_V5_Asset_Currencies = "api/v5/asset/currencies";
    private const string Endpoints_V5_Asset_Balances = "api/v5/asset/balances";
    private const string Endpoints_V5_Asset_Transfer = "api/v5/asset/transfer";
    private const string Endpoints_V5_Asset_Bills = "api/v5/asset/bills";
    private const string Endpoints_V5_Asset_DepositLightning = "api/v5/asset/deposit-lightning";
    private const string Endpoints_V5_Asset_DepositAddress = "api/v5/asset/deposit-address";
    private const string Endpoints_V5_Asset_DepositHistory = "api/v5/asset/deposit-history";
    private const string Endpoints_V5_Asset_Withdrawal = "api/v5/asset/withdrawal";
    private const string Endpoints_V5_Asset_WithdrawalLightning = "api/v5/asset/withdrawal-lightning";
    private const string Endpoints_V5_Asset_WithdrawalCancel = "api/v5/asset/cancel-withdrawal";
    private const string Endpoints_V5_Asset_WithdrawalHistory = "api/v5/asset/withdrawal-history";
    private const string Endpoints_V5_Asset_SavingBalance = "api/v5/asset/saving-balance";
    private const string Endpoints_V5_Asset_SavingPurchaseRedempt = "api/v5/asset/purchase_redempt";
    #endregion

    internal OKXRestClientUnifiedApiAccount(OKXRestClientUnifiedApi baseClient)
    {
        _baseClient = baseClient;
    }

    /// <inheritdoc />
    public async Task<WebCallResult<OKXAccountBalance>> GetAccountBalanceAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAccountBalance>>>(_baseClient.GetUri(Endpoints_V5_Account_Balance), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXAccountBalance>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXAccountBalance>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPosition>>> GetAccountPositionsAsync(
        OKXInstrumentType? instrumentType = null,
        string? symbol = null,
        string? positionId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("posId", positionId);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXPosition>>>(_baseClient.GetUri(Endpoints_V5_Account_Positions), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXPosition>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXPosition>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXClosingPosition>>> GetAccountPositionHistoryAsync(
        OKXInstrumentType? instrumentType = null,
        string? symbol = null,
        OKXMarginMode? marginMode = null,
        OKXClosingPositionType? type = null,
        string? positionId = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", symbol);
        if (marginMode != null)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        if (type != null)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ClosingPositionTypeConverter(false)));
        parameters.AddOptionalParameter("posId", positionId);
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXClosingPosition>>>(_baseClient.GetUri(Endpoints_V5_Account_PositionsHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXClosingPosition>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXClosingPosition>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXPositionRisk>>> GetAccountPositionRiskAsync(OKXInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXPositionRisk>>>(_baseClient.GetUri(Endpoints_V5_Account_PositionRisk), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXPositionRisk>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXPositionRisk>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAccountBill>>> GetBillHistoryAsync(
        OKXInstrumentType? instrumentType = null,
        string? asset = null,
        OKXMarginMode? marginMode = null,
        OKXContractType? contractType = null,
        OKXAccountBillType? billType = null,
        OKXAccountBillSubType? billSubType = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        if (contractType.HasValue)
            parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)));
        if (billType.HasValue)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new AccountBillTypeConverter(false)));
        if (billSubType.HasValue)
            parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new AccountBillSubTypeConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAccountBill>>>(_baseClient.GetUri(Endpoints_V5_Account_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXAccountBill>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXAccountBill>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }
    
    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAccountBill>>> GetBillArchiveAsync(
        OKXInstrumentType? instrumentType = null,
        string? asset = null,
        OKXMarginMode? marginMode = null,
        OKXContractType? contractType = null,
        OKXAccountBillType? billType = null,
        OKXAccountBillSubType? billSubType = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        if (contractType.HasValue)
            parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)));
        if (billType.HasValue)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new AccountBillTypeConverter(false)));
        if (billSubType.HasValue)
            parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new AccountBillSubTypeConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAccountBill>>>(_baseClient.GetUri(Endpoints_V5_Account_BillsArchive), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXAccountBill>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXAccountBill>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAccountConfiguration>>>(_baseClient.GetUri(Endpoints_V5_Account_Config), HttpMethod.Get, ct, null, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXAccountConfiguration>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXAccountConfiguration>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }
        
    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountPositionMode>> SetAccountPositionModeAsync(OKXPositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"posMode", JsonConvert.SerializeObject(positionMode, new PositionModeConverter(false)) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAccountPositionMode>>>(_baseClient.GetUri(Endpoints_V5_Account_SetPositionMode), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXAccountPositionMode>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXAccountPositionMode>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLeverage>>> GetAccountLeverageAsync(
        string symbols,
        OKXMarginMode marginMode,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbols },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXLeverage>>>(_baseClient.GetUri(Endpoints_V5_Account_LeverageInfo), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXLeverage>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXLeverage>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLeverage>>> SetAccountLeverageAsync(
        int leverage,
        string? asset = null,
        string? symbol = null,
        OKXMarginMode? marginMode = null,
        OKXPositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        if (leverage < 1)
            throw new ArgumentException("Invalid Leverage");

        if (string.IsNullOrEmpty(asset) && string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

        if (marginMode == null)
            throw new ArgumentException("marginMode is required");

        var parameters = new Dictionary<string, object> {
            {"lever", leverage.ToString() },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("instId", symbol);
        if (positionSide.HasValue)
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXLeverage>>>(_baseClient.GetUri(Endpoints_V5_Account_SetLeverage), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXLeverage>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXLeverage>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumAmount>>> GetMaximumAmountAsync(
        string symbol,
        OKXTradeMode tradeMode,
        string? asset = null,
        decimal? price = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbol },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXMaximumAmount>>>(_baseClient.GetUri(Endpoints_V5_Account_MaxSize), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXMaximumAmount>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXMaximumAmount>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumAvailableAmount>>> GetMaximumAvailableAmountAsync(
        string symbol,
        OKXTradeMode tradeMode,
        string? asset = null,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbol },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXMaximumAvailableAmount>>>(_baseClient.GetUri(Endpoints_V5_Account_MaxAvailSize), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXMaximumAvailableAmount>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXMaximumAvailableAmount>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMarginAmount>>> SetMarginAmountAsync(
        string symbol,
        OKXPositionSide positionSide,
        OKXMarginAddReduce marginAddReduce,
        decimal amount,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", symbol },
            {"posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)) },
            {"type", JsonConvert.SerializeObject(marginAddReduce, new MarginAddReduceConverter(false)) },
            {"amt", amount.ToString(CultureInfo.InvariantCulture) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXMarginAmount>>>(_baseClient.GetUri(Endpoints_V5_Account_PositionMarginBalance), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXMarginAmount>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXMarginAmount>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXMaximumLoanAmount>>> GetMaximumLoanAmountAsync(
        string instrumentId,
        OKXMarginMode marginMode,
        string? marginAsset = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("mgnCcy", marginAsset);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXMaximumLoanAmount>>>(_baseClient.GetUri(Endpoints_V5_Account_MaxLoan), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXMaximumLoanAmount>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXMaximumLoanAmount>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXFeeRate>> GetFeeRatesAsync(
        OKXInstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        OKXFeeRateCategory? category = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new FeeRateCategoryConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXFeeRate>>>(_baseClient.GetUri(Endpoints_V5_Account_TradeFee), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXFeeRate>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXFeeRate>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
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

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", symbol);
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInterestAccrued>>>(_baseClient.GetUri(Endpoints_V5_Account_InterestAccrued), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXInterestAccrued>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXInterestAccrued>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXInterestRate>>> GetInterestRateAsync(
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXInterestRate>>>(_baseClient.GetUri(Endpoints_V5_Account_InterestRate), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXInterestRate>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXInterestRate>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXAccountGreeksType>> SetGreeksAsync(OKXGreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"greeksType", JsonConvert.SerializeObject(greeksType, new GreeksTypeConverter(false)) },
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAccountGreeksType>>>(_baseClient.GetUri(Endpoints_V5_Account_SetGreeks), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXAccountGreeksType>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXAccountGreeksType>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXWithdrawalAmount>>> GetMaximumWithdrawalsAsync(
        string? asset = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXWithdrawalAmount>>>(_baseClient.GetUri(Endpoints_V5_Account_MaxWithdrawal), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXWithdrawalAmount>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXWithdrawalAmount>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXAsset>>> GetAssetsAsync(CancellationToken ct = default)
    {
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXAsset>>>(_baseClient.GetUri(Endpoints_V5_Asset_Currencies), HttpMethod.Get, ct, null, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXAsset>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXAsset>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingBalance>>> GetFundingBalanceAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXFundingBalance>>>(_baseClient.GetUri(Endpoints_V5_Asset_Balances), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXFundingBalance>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXFundingBalance>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXTransferResponse>> TransferAsync(
        string asset,
        decimal amount,
        OKXTransferType type,
        OKXAccount fromAccount,
        OKXAccount toAccount,
        string? subAccountName = null,
        string? fromSymbol = null,
        string? toSymbol = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
            { "type", JsonConvert.SerializeObject(type, new TransferTypeConverter(false)) },
            { "from", JsonConvert.SerializeObject(fromAccount, new AccountConverter(false)) },
            { "to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
        };
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("instId", fromSymbol);
        parameters.AddOptionalParameter("toInstId", toSymbol);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXTransferResponse>>>(_baseClient.GetUri(Endpoints_V5_Asset_Transfer), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXTransferResponse>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXTransferResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXFundingBill>>> GetFundingBillDetailsAsync(
        string? asset = null,
        OKXFundingBillType? type = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);
        if (type.HasValue)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FundingBillTypeConverter(false)));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXFundingBill>>>(_baseClient.GetUri(Endpoints_V5_Asset_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXFundingBill>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXFundingBill>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXLightningDeposit>>> GetLightningDepositsAsync(
        string asset,
        decimal amount,
        OKXLightningDepositAccount? account = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", asset },
            { "amt", amount.ToString(CultureInfo.InvariantCulture) },
        };
        if (account.HasValue)
            parameters.AddOptionalParameter("to", JsonConvert.SerializeObject(account, new LightningDepositAccountConverter(false)));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXLightningDeposit>>>(_baseClient.GetUri(Endpoints_V5_Asset_DepositLightning), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXLightningDeposit>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXLightningDeposit>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDepositAddress>>> GetDepositAddressAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);
        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXDepositAddress>>>(_baseClient.GetUri(Endpoints_V5_Asset_DepositAddress), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXDepositAddress>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXDepositAddress>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXDepositHistory>>> GetDepositHistoryAsync(
        string? asset = null,
        string? transactionId = null,
        OKXDepositState? state = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("txId", transactionId);
        if (state.HasValue)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new DepositStateConverter(false)));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXDepositHistory>>>(_baseClient.GetUri(Endpoints_V5_Asset_DepositHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXDepositHistory>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXDepositHistory>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXWithdrawalResponse>> WithdrawAsync(
        string asset,
        decimal amount,
        OKXWithdrawalDestination destination,
        string toAddress,
        string password,
        decimal fee,
        string? network = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
            { "dest", JsonConvert.SerializeObject(destination, new WithdrawalDestinationConverter(false)) },
            { "toAddr",toAddress},
            { "pwd",password},
            { "fee",fee.ToString(CultureInfo.InvariantCulture)},
        };
        parameters.AddOptionalParameter("chain", network);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXWithdrawalResponse>>>(_baseClient.GetUri(Endpoints_V5_Asset_Withdrawal), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXWithdrawalResponse>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXWithdrawalResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXLightningWithdrawal>> GetLightningWithdrawalsAsync(
        string asset,
        string invoice,
        string? memo = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", asset },
            { "invoice", invoice },
        };
        if (!string.IsNullOrEmpty(memo))
            parameters.AddOptionalParameter("memo", memo);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXLightningWithdrawal>>>(_baseClient.GetUri(Endpoints_V5_Asset_WithdrawalLightning), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXLightningWithdrawal>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXLightningWithdrawal>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXWithdrawalId>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "wdId",withdrawalId},
        };

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXWithdrawalId>>>(_baseClient.GetUri(Endpoints_V5_Asset_WithdrawalCancel), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXWithdrawalId>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXWithdrawalId>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXWithdrawalHistory>>> GetWithdrawalHistoryAsync(
        string? asset = null,
        string? transactionId = null,
        OKXWithdrawalState? state = null,
        DateTime? endTime = null,
        DateTime? startTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("txId", transactionId);
        if (state.HasValue)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new WithdrawalStateConverter(false)));
        parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(startTime)?.ToString());
        parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(endTime)?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXWithdrawalHistory>>>(_baseClient.GetUri(Endpoints_V5_Asset_WithdrawalHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXWithdrawalHistory>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXWithdrawalHistory>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<IEnumerable<OKXSavingBalance>>> GetSavingBalancesAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", asset);

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXSavingBalance>>>(_baseClient.GetUri(Endpoints_V5_Asset_SavingBalance), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OKXSavingBalance>>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OKXSavingBalance>>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data!);
    }

    /// <inheritdoc />
    public virtual async Task<WebCallResult<OKXSavingActionResponse>> SavingPurchaseRedemptionAsync(
        string asset,
        decimal amount,
        OKXSavingActionSide side,
        decimal? rate = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",asset},
            { "amt",amount.ToString(CultureInfo.InvariantCulture)},
            { "side", JsonConvert.SerializeObject(side, new SavingActionSideConverter(false)) },
        };
        parameters.AddOptionalParameter("rate", rate?.ToString(CultureInfo.InvariantCulture));

        var result = await _baseClient.ExecuteAsync<OKXRestApiResponse<IEnumerable<OKXSavingActionResponse>>>(_baseClient.GetUri(Endpoints_V5_Asset_SavingPurchaseRedempt), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OKXSavingActionResponse>(result.Error!);
        if (result.Data.ErrorCode > 0) return result.AsError<OKXSavingActionResponse>(new OKXRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage!, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }
}
