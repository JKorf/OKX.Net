using OKX.Net.Enums;
using OKX.Net.Objects.Account;
using OKX.Net.Objects.Affiliate;
using OKX.Net.Objects.Funding;

namespace OKX.Net.Interfaces.Clients.UnifiedApi;

/// <summary>
/// Unified account endpoints
/// </summary>
public interface IOKXRestClientUnifiedApiAccount
{
    /// <summary>
    /// Cancel withdrawal. You can cancel normal withdrawal requests, but you cannot cancel withdrawal requests on Lightning.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-cancel-withdrawal" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/asset/cancel-withdrawal
    /// </para>
    /// </summary>
    /// <param name="withdrawalId">["<c>wdId</c>"] Withdrawal ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXWithdrawalId>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);

    /// <summary>
    /// Get a list of assets (with non-zero balance), remaining balance, and available amount in the account.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-balance" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/balance
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountBalance>> GetAccountBalanceAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get current account configuration.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-account-configuration" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/config
    /// </para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default);

    /// <summary>
    /// Get Leverage
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-leverage" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/leverage-info
    /// </para>
    /// </summary>
    /// <param name="symbols">["<c>instId</c>"] Single symbol or multiple symbols (no more than 20) separated with comma, for example `BTC-USD-SWAP`</param>
    /// <param name="marginMode">["<c>mgnMode</c>"] Margin Mode</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, only applicable to Quick Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLeverage[]>> GetLeverageAsync(string symbols, MarginMode marginMode, string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the updated position data for the last 3 months. Return in reverse chronological order using utime.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-positions-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/positions-history
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument type</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="marginMode">["<c>mgnMode</c>"] Margin mode</param>
    /// <param name="type">["<c>type</c>"] The type of closing position. It is the latest type if there are several types for the same position.</param>
    /// <param name="positionId">["<c>posId</c>"] Position ID</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXClosingPosition[]>> GetPositionHistoryAsync(InstrumentType? instrumentType = null, string? symbol = null, MarginMode? marginMode = null, ClosingPositionType? type = null, string? positionId = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get account and position risk
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-account-and-position-risk" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/account-position-risk
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPositionRisk[]>> GetPositionRiskAsync(InstrumentType? instrumentType = null, CancellationToken ct = default);


    /// <summary>
    /// Get position info. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-positions" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/positions
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="positionId">["<c>posId</c>"] Position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPosition[]>> GetPositionsAsync(InstrumentType? instrumentType = null, string? symbol = null, string? positionId = null, CancellationToken ct = default);

    /// <summary>
    /// Get the account’s bills. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with most recent first. This endpoint can retrieve data from the last 3 months.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-bills-details-last-3-months" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/bills-archive
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="marginMode">["<c>mgnMode</c>"] Margin Mode</param>
    /// <param name="contractType">["<c>ctType</c>"] Contract Type</param>
    /// <param name="billType">["<c>type</c>"] Bill Type</param>
    /// <param name="billSubType">["<c>subType</c>"] Bill Sub Type</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="fromId">["<c>after</c>"] Pagination of data to return records earlier than the requested id</param>
    /// <param name="toId">["<c>before</c>"] Pagination of data to return records newer than the requested id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountBill[]>> GetBillArchiveAsync(InstrumentType? instrumentType = null, string? asset = null, MarginMode? marginMode = null, ContractType? contractType = null, AccountBillType? billType = null, AccountBillSubType? billSubType = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? fromId = null, string? toId = null, CancellationToken ct = default);

    /// <summary>
    /// Get the bills of the account. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with the most recent first. This endpoint can retrieve data from the last 7 days.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-bills-details-last-7-days" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/bills
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="marginMode">["<c>mgnMode</c>"] Margin Mode</param>
    /// <param name="contractType">["<c>ctType</c>"] Contract Type</param>
    /// <param name="billType">["<c>type</c>"] Bill Type</param>
    /// <param name="billSubType">["<c>subType</c>"] Bill Sub Type</param>
    /// <param name="startTime">["<c>begin</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>end</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="fromId">["<c>after</c>"] Pagination of data to return records earlier than the requested id</param>
    /// <param name="toId">["<c>before</c>"] Pagination of data to return records newer than the requested id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountBill[]>> GetBillHistoryAsync(InstrumentType? instrumentType = null, string? asset = null, MarginMode? marginMode = null, ContractType? contractType = null, AccountBillType? billType = null, AccountBillSubType? billSubType = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? fromId = null, string? toId = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of all assets. Not all assets can be traded. Assets that have not been defined in ISO 4217 may use a custom symbol.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-currencies" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/currencies
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Single asset or multiple assets (no more than 20) separated with comma, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAsset[]>> GetAssetsAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the deposit addresses of assets, including previously-used addresses.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-deposit-address" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/deposit-address
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDepositAddress[]>> GetDepositAddressAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the deposit history of all assets, up to 100 recent records in a year.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-deposit-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/deposit-history
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="transactionId">["<c>txId</c>"] Transaction ID</param>
    /// <param name="state">["<c>state</c>"] State</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="depositId">["<c>depId</c>"] Deposit ID</param>
    /// <param name="fromWithdrawalId">["<c>fromWdId</c>"] Internal transfer initiator's withdrawal ID</param>
    /// <param name="type">["<c>type</c>"] Deposit Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDepositHistory[]>> GetDepositHistoryAsync(string? asset = null, string? transactionId = null, DepositState? state = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? depositId = null, string? fromWithdrawalId = null, DepositType? type = null, CancellationToken ct = default);

    /// <summary>
    /// Get Fee Rates
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-fee-rates" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/trade-fee
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ruleType">["<c>ruleType</c>"] Rule type</param>
    /// <param name="groupId">["<c>groupId</c>"] Instrument trading fee group id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFeeRate>> GetFeeRatesAsync(
        InstrumentType instrumentType,
        string? symbol = null,
        string? underlying = null,
        string? instrumentFamily = null,
        SymbolRuleType? ruleType = null,
        string? groupId = null,
        CancellationToken ct = default);

    /// <summary>
    /// Get the balances of all the assets, and the amount that is available or on hold.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-balance" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/balances
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFundingBalance[]>> GetFundingBalanceAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get billing records, you can get the latest 1 month historical data
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-asset-bills-details" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/bills
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="type">["<c>type</c>"] Bill type</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="startBillId">["<c>after</c>"] Filter by start bill id</param>
    /// <param name="endBillId">["<c>before</c>"] Filter by end bill id</param>
    /// <param name="clientId">["<c>clientId</c>"] Client id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFundingBill[]>> GetFundingBillDetailsAsync(string? asset = null, FundingBillType? type = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? clientId = null, long? startBillId = null, long? endBillId = null, CancellationToken ct = default);

    /// <summary>
    /// Get interest-accrued
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-interest-accrued-data" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/interest-accrued
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="asset">["<c>ccy</c>"] Asset</param>
    /// <param name="marginMode">["<c>mgnMode</c>"] Margin Mode</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestAccrued[]>> GetInterestAccruedAsync(string? symbol = null, string? asset = null, MarginMode? marginMode = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get the user's current leveraged currency borrowing interest rate
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-interest-rate" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/interest-rate
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountInterestRate[]>> GetInterestRateAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get lightning deposits. Users can create up to 10,000 different invoices within 24 hours.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-lightning-deposits" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/deposit-lightning
    /// </para>
    /// </summary>
    /// <param name="currency">["<c>ccy</c>"] Currency</param>
    /// <param name="amount">["<c>amt</c>"] deposit amount between 0.000001 - 0.1</param>
    /// <param name="account">["<c>to</c>"] Receiving account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLightningDeposit[]>> GetLightningDepositsAsync(string currency, decimal amount, LightningDepositAccount? account = null, CancellationToken ct = default);

    /// <summary>
    /// Get a lightning withdrawal. The maximum withdrawal amount is 0.1 BTC per request, and 1 BTC in 24 hours. The minimum withdrawal amount is approximately 0.000001 BTC. Sub-account does not support withdrawal.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-lightning-withdrawals" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/withdrawal-lightning
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset. Currently only BTC is supported.</param>
    /// <param name="invoice">["<c>invoice</c>"] Invoice text</param>
    /// <param name="memo">["<c>memo</c>"] Lightning withdrawal memo</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLightningWithdrawal>> GetLightningWithdrawalAsync(string asset, string invoice, string? memo = null, CancellationToken ct = default);

    /// <summary>
    /// Get maximum buy/sell amount or open amount
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-buy-sell-amount-or-open-amount" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/max-size
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example 'ETH-USDT'</param>
    /// <param name="tradeMode">["<c>tdMode</c>"] Trade Mode</param>
    /// <param name="asset">["<c>ccy</c>"] Asset</param>
    /// <param name="price">["<c>px</c>"] Price</param>
    /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
    /// <param name="tradeQuoteAsset">["<c>tradeQuoteCcy</c>"] The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXMaximumAmount[]>> GetMaximumAmountAsync(
        string symbol,
        Enums.TradeMode tradeMode,
        string? asset = null,
        decimal? price = null, 
        int? leverage = null,
        string? tradeQuoteAsset = null,
        CancellationToken ct = default);

    /// <summary>
    /// Get Maximum Available Tradable Amount
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-available-tradable-amount" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/max-avail-size
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example 'ETH-USDT'</param>
    /// <param name="tradeMode">["<c>tdMode</c>"] Trade Mode</param>
    /// <param name="asset">["<c>ccy</c>"] Currency</param>
    /// <param name="reduceOnly">["<c>reduceOnly</c>"] Reduce Only</param>
    /// <param name="tradeQuoteAsset">["<c>tradeQuoteCcy</c>"] The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXMaximumAvailableAmount[]>> GetMaximumAvailableAmountAsync(
        string symbol,
        Enums.TradeMode tradeMode,
        string? asset = null,
        bool? reduceOnly = null, 
        string? tradeQuoteAsset = null,
        CancellationToken ct = default);

    /// <summary>
    /// Get the maximum loan of a instrument
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-the-maximum-loan-of-instrument" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/max-loan
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example 'ETH-USDT'</param>
    /// <param name="asset">["<c>ccy</c>"] Applicable to get Max loan of manual borrow for the currency in Spot mode (enabled borrowing)</param>
    /// <param name="marginMode">["<c>mgnMode</c>"] Margin Mode</param>
    /// <param name="marginAsset">["<c>mgnCcy</c>"] Margin asset</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXMaximumLoanAmount[]>> GetMaximumLoanAmountAsync(MarginMode marginMode, string? symbol = null, string? asset = null, string? marginAsset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the maximum transferable amount.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-withdrawals" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/max-withdrawal
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example 'ETH'</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXWithdrawalAmount[]>> GetMaximumWithdrawalsAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get saving balances. Only the assets in the funding account can be used for saving.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#financial-product-savings-get-saving-balance" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/finance/savings/balance
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example 'ETH'</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSavingBalance[]>> GetSavingBalancesAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-withdrawal-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/withdrawal-history
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example 'ETH'</param>
    /// <param name="transactionId">["<c>txId</c>"] Transaction ID</param>
    /// <param name="state">["<c>state</c>"] State</param>
    /// <param name="startTime">["<c>before</c>"] Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">["<c>after</c>"] Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">["<c>limit</c>"] Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="withdrawalId">["<c>wdId</c>"] Client-supplied ID</param>
    /// <param name="clientId">["<c>clientId</c>"] Client id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXWithdrawalHistory[]>> GetWithdrawalHistoryAsync(string? asset = null, string? transactionId = null, WithdrawalState? state = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? withdrawalId = null, string? clientId = null, CancellationToken ct = default);

    /// <summary>
    /// Purchase or redeem saving shares
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#financial-product-simple-earn-flexible-post-savings-purchase-redemption" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/finance/savings/purchase-redempt
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="amount">["<c>amt</c>"] Amount</param>
    /// <param name="side">["<c>side</c>"] Side</param>
    /// <param name="rate">["<c>rate</c>"] Rate</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSavingActionResponse>> SavingPurchaseRedemptionAsync(string asset, decimal amount, SavingActionSide side, decimal? rate = null, CancellationToken ct = default);

    /// <summary>
    /// Set leverage. The following are the setting leverage cases for an instrument:<br />
    /// Set leverage for isolated MARGIN at pairs level.<br />
    /// Set leverage for cross MARGIN in Single-currency margin at pairs level.<br />
    /// Set leverage for cross MARGIN in Multi-currency margin at currency level.<br />
    /// Set leverage for cross/isolated FUTURES/SWAP at underlying/contract level.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-leverage" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/set-leverage
    /// </para>
    /// </summary>
    /// <param name="leverage">["<c>lever</c>"] Leverage</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="marginMode">["<c>mgnMode</c>"] Margin Mode</param>
    /// <param name="positionSide">["<c>posSide</c>"] Position Side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLeverage[]>> SetLeverageAsync(int leverage, MarginMode marginMode, string? asset = null, string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default);

    /// <summary>
    /// Set position mode. FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-position-mode" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/set-position-mode
    /// </para>
    /// </summary>
    /// <param name="positionMode">["<c>posMode</c>"] Position mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default);

    /// <summary>
    /// Set the display type of Greeks.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-greeks-pa-bs" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/set-greeks
    /// </para>
    /// </summary>
    /// <param name="greeksType">["<c>greeksType</c>"] Display type of Greeks.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountGreeksType>> SetGreeksAsync(GreeksType greeksType, CancellationToken ct = default);

    /// <summary>
    /// Increase or decrease the margin of the isolated position.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-increase-decrease-margin" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/position/margin-balance
    /// </para>
    /// </summary>
    /// <param name="symbol">["<c>instId</c>"] Symbol, for example `ETH-USDT`</param>
    /// <param name="positionSide">["<c>posSide</c>"] Position Side</param>
    /// <param name="marginAddReduce">["<c>type</c>"] Type</param>
    /// <param name="amount">["<c>amt</c>"] Amount</param>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="auto">["<c>auto</c>"] Automatic loan transfer out</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXMarginAmount[]>> SetMarginAmountAsync(string symbol, PositionSide positionSide, MarginAddReduce marginAddReduce, decimal amount, string? asset = null, bool? auto = null, CancellationToken ct = default);

    /// <summary>
    /// Transfer asset. This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts. Direct transfers between sub-accounts are not allowed.
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-funds-transfer" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/asset/transfer
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="amount">["<c>amt</c>"] Amount</param>
    /// <param name="type">["<c>type</c>"] Transfer type</param>
    /// <param name="fromAccount">["<c>from</c>"] The remitting account</param>
    /// <param name="toAccount">["<c>to</c>"] The beneficiary account</param>
    /// <param name="subAccountName">["<c>subAcct</c>"] Sub Account Name</param>
    /// <param name="fromSymbol">["<c>instId</c>"] MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred out.</param>
    /// <param name="toSymbol">["<c>toInstId</c>"] MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred in.</param>
    /// <param name="loanTransfer">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin</param>
    /// <param name="clientId">Client id</param>
    /// <param name="ignorePositionRisk">Ignore position risk</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTransferResponse>> TransferAsync(string asset, decimal amount, TransferType type, AccountType fromAccount, AccountType toAccount, string? subAccountName = null, string? fromSymbol = null, string? toSymbol = null, bool? loanTransfer = null, string? clientId = null, bool? ignorePositionRisk = null, CancellationToken ct = default);

    /// <summary>
    /// Withdraw an asset
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-withdrawal" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/asset/withdrawal
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset, for example `ETH`</param>
    /// <param name="amount">["<c>amt</c>"] Amount</param>
    /// <param name="destination">["<c>dest</c>"] Withdrawal destination address</param>
    /// <param name="toAddress">["<c>toAddr</c>"] Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'</param>
    /// <param name="fee">["<c>fee</c>"] Transaction fee</param>
    /// <param name="network">["<c>chain</c>"] Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni. If this parameter is not filled in because it is not available, it will default to the main chain.</param>
    /// <param name="areaCode">["<c>areaCode</c>"] 	Area code for the phone number, e.g. 86. If toAddr is a phone number, this parameter is required.</param>
    /// <param name="clientId">["<c>clientId</c>"] Client-supplied ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXWithdrawalResponse>> WithdrawAsync(string asset, decimal amount, WithdrawalDestination destination, string toAddress, decimal fee, string? network = null, string? areaCode = null, string? clientId = null, CancellationToken ct = default);

    /// <summary>
    /// Get assets available for dust conversion
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-easy-convert-currency-list" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/easy-convert-currency-list
    /// </para>
    /// </summary>
    /// <param name="sourceAccount">["<c>source</c>"] Source account type</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXDustAssets>> GetEasyConvertDustAssetsAsync(AccountType? sourceAccount = null, CancellationToken ct = default);

    /// <summary>
    /// Convert small assets in funding account to a target asset
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-easy-convert" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/trade/easy-convert
    /// </para>
    /// </summary>
    /// <param name="assets">["<c>fromCcy</c>"] Assets to convert, for example `ETH`</param>
    /// <param name="targetAsset">["<c>toCcy</c>"] Target asset</param>
    /// <param name="sourceAccount">["<c>source</c>"] Convert from account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDustConvertEntry[]>> EasyConvertDustAsync(IEnumerable<string> assets, string targetAsset, AccountType? sourceAccount = null, CancellationToken ct = default);

    /// <summary>
    /// Get easy dust convert history
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-easy-convert-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/trade/easy-convert-history
    /// </para>
    /// </summary>
    /// <param name="startTime">["<c>after</c>"] Filter by start time</param>
    /// <param name="endTime">["<c>before</c>"] Filter by end time</param>
    /// <param name="limit">["<c>limit</c>"] Max number of results</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXDustConvertEntry[]>> GetEasyConvertDustHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    /// <summary>
    /// Set isolated margin mode for the Margin or Contracts instrument type
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-isolated-margin-trading-settings" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/set-isolated-mode
    /// </para>
    /// </summary>
    /// <param name="instumentType">["<c>type</c>"] Instrument type, only Margin and Contracts supported</param>
    /// <param name="isolatedMarginMode">["<c>isoMode</c>"] Isolated margin mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountIsolatedMarginMode>> SetIsolatedMarginModeAsync(InstrumentType instumentType, IsolatedMarginMode isolatedMarginMode, CancellationToken ct = default);

    /// <summary>
    /// Get info on a transfer
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-funds-transfer-state" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/transfer-state
    /// </para>
    /// </summary>
    /// <param name="transferId">["<c>transId</c>"] Transfer id, either this or clientTransferId needs to be provided</param>
    /// <param name="clientTransferId">["<c>clientId</c>"] Client transfer id, either this or transferId needs to be provided</param>
    /// <param name="type">["<c>type</c>"] The type of transfer</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTransferInfo>> GetTransferAsync(string? transferId = null, string? clientTransferId = null, TransferType? type = null, CancellationToken ct = default);

    /// <summary>
    /// Preset info for switching account mode
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-preset-account-mode-switch" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/account-level-switch-preset
    /// </para>
    /// </summary>
    /// <param name="mode">["<c>acctLv</c>"] Account mode</param>
    /// <param name="leverage">["<c>lever</c>"] Leverage, required when switching from Portfolio margin mode to Spot and futures mode or Multi-currency margin mode, and the user holds cross-margin positions.</param>
    /// <param name="riskOffsetType">["<c>riskOffsetType</c>"] Risk offset type, applicable when switching from Spot and futures mode or Multi-currency margin mode to Portfolio margin mode.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPresetAccountMode>> PresetAccountModeSwitchAsync(AccountLevel mode, int? leverage = null, RiskOffsetType? riskOffsetType = null, CancellationToken ct = default);

    /// <summary>
    /// Run a pre-check for account mode switching
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-precheck-account-mode-switch" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/set-account-switch-precheck
    /// </para>
    /// </summary>
    /// <param name="mode">["<c>acctLv</c>"] Account mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountSwitchCheckResult>> PrecheckAccountModeSwitchAsync(AccountLevel mode, CancellationToken ct = default);

    /// <summary>
    /// Set the account mode
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-account-mode" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/set-account-level
    /// </para>
    /// </summary>
    /// <param name="mode">["<c>acctLv</c>"] New account mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountMode>> SetAccountModeAsync(AccountLevel mode, CancellationToken ct = default);

    /// <summary>
    /// Get details of an affiliate invitee
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#affiliate-rest-api-get-the-invitee-39-s-detail" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/affiliate/invitee/detail
    /// </para>
    /// </summary>
    /// <param name="userId">["<c>uid</c>"] User id</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInviteeDetails>> GetAffiliateInviteeDetailsAsync(string userId, CancellationToken ct = default);

    /// <summary>
    /// Get asset valuation
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-account-asset-valuation" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/asset/asset-valuation
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] The asset to denote the value in, defaults to BTC</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<WebCallResult<OKXAssetValuation>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Manually borrow / repay. Only applicable to Spot mode
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-manual-borrow-repay" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/spot-manual-borrow-repay
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Asset name</param>
    /// <param name="BorrowRepaySide">["<c>side</c>"] Borrow or repay</param>
    /// <param name="quantity">["<c>amt</c>"] Quantity</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXBorrowRepayResult>> ManualBorrowRepay(string asset, BorrowRepaySide BorrowRepaySide, decimal quantity, CancellationToken ct = default);

    /// <summary>
    /// Set auto repay. Only applicable to Spot mode
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-auto-repay" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/set-auto-repay
    /// </para>
    /// </summary>
    /// <param name="autoRepay">["<c>autoRepay</c>"] Auto repay enabled or not</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXAutoRepayStatus>> SetAutoRepayAsync(bool autoRepay, CancellationToken ct = default);

    /// <summary>
    /// Get borrow/repay history
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-borrow-repay-history" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/spot-borrow-repay-history
    /// </para>
    /// </summary>
    /// <param name="asset">["<c>ccy</c>"] Filter by asset</param>
    /// <param name="type">["<c>type</c>"] Filter by type</param>
    /// <param name="startTime">["<c>after</c>"] Filter by start time</param>
    /// <param name="endTime">["<c>before</c>"] Filter by end time</param>
    /// <param name="limit">["<c>limit</c>"] Max number of results</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXBorrowRepayEntry[]>> GetBorrowRepayHistoryAsync(string? asset = null, BorrowRepayType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of instruments that are available to the user
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-instruments" /><br />
    /// Endpoint:<br />
    /// GET /api/v5/account/instruments
    /// </para>
    /// </summary>
    /// <param name="instrumentType">["<c>instType</c>"] Instrument Type</param>
    /// <param name="underlying">["<c>uly</c>"] Underlying</param>
    /// <param name="symbol">["<c>instId</c>"] Filter by symbol, for example `ETH-USDT`</param>
    /// <param name="instrumentFamily">["<c>instFamily</c>"] Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    Task<WebCallResult<Objects.Public.OKXInstrument[]>> GetSymbolsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Set fee charge type for spot trading
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-fee-type" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/set-fee-type
    /// </para>
    /// </summary>
    /// <param name="feeType">["<c>feeType</c>"] Fee type</param>
    /// <param name="ct">Cancellation Token</param>
    Task<WebCallResult<OKXFeeType>> SetFeeTypeAsync(FeeType feeType, CancellationToken ct = default);

    /// <summary>
    /// Set settlement asset for USD contracts
    /// <para>
    /// Docs:<br />
    /// <a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-settle-currency" /><br />
    /// Endpoint:<br />
    /// POST /api/v5/account/set-settle-currency
    /// </para>
    /// </summary>
    /// <param name="settleAsset">["<c>settleCcy</c>"] The settlement asset</param>
    /// <param name="ct">Cancellation Token</param>
    Task<WebCallResult<OKXSettleAsset>> SetSettleAssetAsync(string settleAsset, CancellationToken ct = default);
}

