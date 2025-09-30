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
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-cancel-withdrawal" /></para>
    /// </summary>
    /// <param name="withdrawalId">Withdrawal ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXWithdrawalId>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);

    /// <summary>
    /// Get a list of assets (with non-zero balance), remaining balance, and available amount in the account.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-balance" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountBalance>> GetAccountBalanceAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get current account configuration.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-account-configuration" /></para>
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default);

    /// <summary>
    /// Get Leverage
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-leverage" /></para>
    /// </summary>
    /// <param name="symbols">Single symbol or multiple symbols (no more than 20) separated with comma, for example `BTC-USD-SWAP`</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="asset">Asset, only applicable to Quick Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLeverage[]>> GetLeverageAsync(string symbols, MarginMode marginMode, string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the updated position data for the last 3 months. Return in reverse chronological order using utime.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-positions-history" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="type">The type of closing position. It is the latest type if there are several types for the same position.</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXClosingPosition[]>> GetPositionHistoryAsync(InstrumentType? instrumentType = null, string? symbol = null, MarginMode? marginMode = null, ClosingPositionType? type = null, string? positionId = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get account and position risk
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-account-and-position-risk" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPositionRisk[]>> GetPositionRiskAsync(InstrumentType? instrumentType = null, CancellationToken ct = default);


    /// <summary>
    /// Get position info. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-positions" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPosition[]>> GetPositionsAsync(InstrumentType? instrumentType = null, string? symbol = null, string? positionId = null, CancellationToken ct = default);

    /// <summary>
    /// Get the account’s bills. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with most recent first. This endpoint can retrieve data from the last 3 months.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-bills-details-last-3-months" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="fromId">Pagination of data to return records earlier than the requested id</param>
    /// <param name="toId">Pagination of data to return records newer than the requested id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountBill[]>> GetBillArchiveAsync(InstrumentType? instrumentType = null, string? asset = null, MarginMode? marginMode = null, ContractType? contractType = null, AccountBillType? billType = null, AccountBillSubType? billSubType = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? fromId = null, string? toId = null, CancellationToken ct = default);

    /// <summary>
    /// Get the bills of the account. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with the most recent first. This endpoint can retrieve data from the last 7 days.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-bills-details-last-7-days" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="fromId">Pagination of data to return records earlier than the requested id</param>
    /// <param name="toId">Pagination of data to return records newer than the requested id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountBill[]>> GetBillHistoryAsync(InstrumentType? instrumentType = null, string? asset = null, MarginMode? marginMode = null, ContractType? contractType = null, AccountBillType? billType = null, AccountBillSubType? billSubType = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? fromId = null, string? toId = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of all assets. Not all assets can be traded. Assets that have not been defined in ISO 4217 may use a custom symbol.
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-currencies" /></para>
    /// </summary>
    /// <param name="asset">Single asset or multiple assets (no more than 20) separated with comma, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAsset[]>> GetAssetsAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the deposit addresses of assets, including previously-used addresses.
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-deposit-address" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDepositAddress[]>> GetDepositAddressAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Retrieve the deposit history of all assets, up to 100 recent records in a year.
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-deposit-history" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="state">State</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="depositId">Deposit ID</param>
    /// <param name="fromWithdrawalId">Internal transfer initiator's withdrawal ID</param>
    /// <param name="type">Deposit Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDepositHistory[]>> GetDepositHistoryAsync(string? asset = null, string? transactionId = null, DepositState? state = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? depositId = null, string? fromWithdrawalId = null, DepositType? type = null, CancellationToken ct = default);

    /// <summary>
    /// Get Fee Rates
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-fee-rates" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ruleType">Rule type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFeeRate>> GetFeeRatesAsync(InstrumentType instrumentType, string? symbol = null, string? underlying = null, string? instrumentFamily = null, SymbolRuleType? ruleType = null, CancellationToken ct = default);

    /// <summary>
    /// Get the balances of all the assets, and the amount that is available or on hold.
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-balance" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFundingBalance[]>> GetFundingBalanceAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get billing records, you can get the latest 1 month historical data
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-asset-bills-details" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="type">Bill type</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="startBillId">Filter by start bill id</param>
    /// <param name="endBillId">Filter by end bill id</param>
    /// <param name="clientId">Client id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXFundingBill[]>> GetFundingBillDetailsAsync(string? asset = null, FundingBillType? type = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? clientId = null, long? startBillId = null, long? endBillId = null, CancellationToken ct = default);

    /// <summary>
    /// Get interest-accrued
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-interest-accrued-data" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="asset">Asset</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInterestAccrued[]>> GetInterestAccruedAsync(string? symbol = null, string? asset = null, MarginMode? marginMode = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Get the user's current leveraged currency borrowing interest rate
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-interest-rate" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountInterestRate[]>> GetInterestRateAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get lightning deposits. Users can create up to 10,000 different invoices within 24 hours.
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-lightning-deposits" /></para>
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">deposit amount between 0.000001 - 0.1</param>
    /// <param name="account">Receiving account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLightningDeposit[]>> GetLightningDepositsAsync(string currency, decimal amount, LightningDepositAccount? account = null, CancellationToken ct = default);

    /// <summary>
    /// Get a lightning withdrawal. The maximum withdrawal amount is 0.1 BTC per request, and 1 BTC in 24 hours. The minimum withdrawal amount is approximately 0.000001 BTC. Sub-account does not support withdrawal.
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-lightning-withdrawals" /></para>
    /// </summary>
    /// <param name="asset">Asset. Currently only BTC is supported.</param>
    /// <param name="invoice">Invoice text</param>
    /// <param name="memo">Lightning withdrawal memo</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLightningWithdrawal>> GetLightningWithdrawalAsync(string asset, string invoice, string? memo = null, CancellationToken ct = default);

    /// <summary>
    /// Get maximum buy/sell amount or open amount
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-buy-sell-amount-or-open-amount" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example 'ETH-USDT'</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="asset">Asset</param>
    /// <param name="price">Price</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="tradeQuoteAsset">The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
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
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-available-tradable-amount" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example 'ETH-USDT'</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="asset">Currency</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="tradeQuoteAsset">The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the symbol, for example: for BTC-USD, the default is USD.</param>
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
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-the-maximum-loan-of-instrument" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example 'ETH-USDT'</param>
    /// <param name="asset">Applicable to get Max loan of manual borrow for the currency in Spot mode (enabled borrowing)</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="marginAsset">Margin asset</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXMaximumLoanAmount[]>> GetMaximumLoanAmountAsync(MarginMode marginMode, string? symbol = null, string? asset = null, string? marginAsset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the maximum transferable amount.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-withdrawals" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example 'ETH'</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXWithdrawalAmount[]>> GetMaximumWithdrawalsAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get saving balances. Only the assets in the funding account can be used for saving.
    /// <para><a href="https://www.okx.com/docs-v5/en/#financial-product-savings-get-saving-balance" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example 'ETH'</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSavingBalance[]>> GetSavingBalancesAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Get the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-withdrawal-history" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example 'ETH'</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="state">State</param>
    /// <param name="startTime">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="endTime">Pagination of data to return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="withdrawalId">Client-supplied ID</param>
    /// <param name="clientId">Client id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXWithdrawalHistory[]>> GetWithdrawalHistoryAsync(string? asset = null, string? transactionId = null, WithdrawalState? state = null, DateTime? endTime = null, DateTime? startTime = null, int limit = 100, string? withdrawalId = null, string? clientId = null, CancellationToken ct = default);

    /// <summary>
    /// Purchase or redeem saving shares
    /// <para><a href="https://www.okx.com/docs-v5/en/#financial-product-simple-earn-flexible-post-savings-purchase-redemption" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="amount">Amount</param>
    /// <param name="side">Side</param>
    /// <param name="rate">Rate</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXSavingActionResponse>> SavingPurchaseRedemptionAsync(string asset, decimal amount, SavingActionSide side, decimal? rate = null, CancellationToken ct = default);

    /// <summary>
    /// Set leverage. The following are the setting leverage cases for an instrument:<br />
    /// Set leverage for isolated MARGIN at pairs level.<br />
    /// Set leverage for cross MARGIN in Single-currency margin at pairs level.<br />
    /// Set leverage for cross MARGIN in Multi-currency margin at currency level.<br />
    /// Set leverage for cross/isolated FUTURES/SWAP at underlying/contract level.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-leverage" /></para>
    /// </summary>
    /// <param name="leverage">Leverage</param>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="symbol">Symbol, for example `BTC-USD-SWAP`</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXLeverage[]>> SetLeverageAsync(int leverage, MarginMode marginMode, string? asset = null, string? symbol = null, PositionSide? positionSide = null, CancellationToken ct = default);

    /// <summary>
    /// Set position mode. FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-position-mode" /></para>
    /// </summary>
    /// <param name="positionMode">Position mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default);

    /// <summary>
    /// Set the display type of Greeks.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-greeks-pa-bs" /></para>
    /// </summary>
    /// <param name="greeksType">Display type of Greeks.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountGreeksType>> SetGreeksAsync(GreeksType greeksType, CancellationToken ct = default);

    /// <summary>
    /// Increase or decrease the margin of the isolated position.
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-increase-decrease-margin" /></para>
    /// </summary>
    /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="marginAddReduce">Type</param>
    /// <param name="amount">Amount</param>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="auto">Automatic loan transfer out</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXMarginAmount[]>> SetMarginAmountAsync(string symbol, PositionSide positionSide, MarginAddReduce marginAddReduce, decimal amount, string? asset = null, bool? auto = null, CancellationToken ct = default);

    /// <summary>
    /// Transfer asset. This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts. Direct transfers between sub-accounts are not allowed.
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-funds-transfer" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="amount">Amount</param>
    /// <param name="type">Transfer type</param>
    /// <param name="fromAccount">The remitting account</param>
    /// <param name="toAccount">The beneficiary account</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="fromSymbol">MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred out.</param>
    /// <param name="toSymbol">MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred in.</param>
    /// <param name="loanTransfer">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin</param>
    /// <param name="clientId">Client id</param>
    /// <param name="ignorePositionRisk">Ignore position risk</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTransferResponse>> TransferAsync(string asset, decimal amount, TransferType type, AccountType fromAccount, AccountType toAccount, string? subAccountName = null, string? fromSymbol = null, string? toSymbol = null, bool? loanTransfer = null, string? clientId = null, bool? ignorePositionRisk = null, CancellationToken ct = default);

    /// <summary>
    /// Withdraw an asset
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-withdrawal" /></para>
    /// </summary>
    /// <param name="asset">Asset, for example `ETH`</param>
    /// <param name="amount">Amount</param>
    /// <param name="destination">Withdrawal destination address</param>
    /// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'</param>
    /// <param name="fee">Transaction fee</param>
    /// <param name="network">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni. If this parameter is not filled in because it is not available, it will default to the main chain.</param>
    /// <param name="areaCode">	Area code for the phone number, e.g. 86. If toAddr is a phone number, this parameter is required.</param>
    /// <param name="clientId">Client-supplied ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXWithdrawalResponse>> WithdrawAsync(string asset, decimal amount, WithdrawalDestination destination, string toAddress, decimal fee, string? network = null, string? areaCode = null, string? clientId = null, CancellationToken ct = default);

    /// <summary>
    /// Get assets available for dust conversion
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-easy-convert-currency-list" /></para>
    /// </summary>
    /// <param name="sourceAccount">Source account type</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXDustAssets>> GetEasyConvertDustAssetsAsync(AccountType? sourceAccount = null, CancellationToken ct = default);

    /// <summary>
    /// Convert small assets in funding account to a target asset
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-easy-convert" /></para>
    /// </summary>
    /// <param name="assets">Assets to convert, for example `ETH`</param>
    /// <param name="targetAsset">Target asset</param>
    /// <param name="sourceAccount">Convert from account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXDustConvertEntry[]>> EasyConvertDustAsync(IEnumerable<string> assets, string targetAsset, AccountType? sourceAccount = null, CancellationToken ct = default);

    /// <summary>
    /// Get easy dust convert history
    /// <para><a href="https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-easy-convert-history" /></para>
    /// </summary>
    /// <param name="startTime">Filter by start time</param>
    /// <param name="endTime">Filter by end time</param>
    /// <param name="limit">Max number of results</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXDustConvertEntry[]>> GetEasyConvertDustHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    /// <summary>
    /// Set isolated margin mode for the Margin or Contracts instrument type
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-isolated-margin-trading-settings" /></para>
    /// </summary>
    /// <param name="instumentType">Instrument type, only Margin and Contracts supported</param>
    /// <param name="isolatedMarginMode">Isolated margin mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountIsolatedMarginMode>> SetIsolatedMarginModeAsync(InstrumentType instumentType, IsolatedMarginMode isolatedMarginMode, CancellationToken ct = default);

    /// <summary>
    /// Get info on a transfer
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-funds-transfer-state" /></para>
    /// </summary>
    /// <param name="transferId">Transfer id, either this or clientTransferId needs to be provided</param>
    /// <param name="clientTransferId">Client transfer id, either this or transferId needs to be provided</param>
    /// <param name="type">The type of transfer</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXTransferInfo>> GetTransferAsync(string? transferId = null, string? clientTransferId = null, TransferType? type = null, CancellationToken ct = default);

    /// <summary>
    /// Preset info for switching account mode
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-preset-account-mode-switch" /></para>
    /// </summary>
    /// <param name="mode">Account mode</param>
    /// <param name="leverage">Leverage, required when switching from Portfolio margin mode to Spot and futures mode or Multi-currency margin mode, and the user holds cross-margin positions.</param>
    /// <param name="riskOffsetType">Risk offset type, applicable when switching from Spot and futures mode or Multi-currency margin mode to Portfolio margin mode.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXPresetAccountMode>> PresetAccountModeSwitchAsync(AccountLevel mode, int? leverage = null, RiskOffsetType? riskOffsetType = null, CancellationToken ct = default);

    /// <summary>
    /// Run a pre-check for account mode switching
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-precheck-account-mode-switch" /></para>
    /// </summary>
    /// <param name="mode">Account mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountSwitchCheckResult>> PrecheckAccountModeSwitchAsync(AccountLevel mode, CancellationToken ct = default);

    /// <summary>
    /// Set the account mode
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-account-mode" /></para>
    /// </summary>
    /// <param name="mode">New account mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXAccountMode>> SetAccountModeAsync(AccountLevel mode, CancellationToken ct = default);

    /// <summary>
    /// Get details of an affiliate invitee
    /// <para><a href="https://www.okx.com/docs-v5/en/#affiliate-rest-api-get-the-invitee-39-s-detail" /></para>
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<OKXInviteeDetails>> GetAffiliateInviteeDetailsAsync(string userId, CancellationToken ct = default);

    /// <summary>
    /// Get asset valuation
    /// <para><a href="https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-account-asset-valuation" /></para>
    /// </summary>
    /// <param name="asset">The asset to denote the value in, defaults to BTC</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<WebCallResult<OKXAssetValuation>> GetAssetValuationAsync(string? asset = null, CancellationToken ct = default);

    /// <summary>
    /// Manually borrow / repay. Only applicable to Spot mode
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-manual-borrow-repay" /></para>
    /// </summary>
    /// <param name="asset">Asset name</param>
    /// <param name="BorrowRepaySide">Borrow or repay</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXBorrowRepayResult>> ManualBorrowRepay(string asset, BorrowRepaySide BorrowRepaySide, decimal quantity, CancellationToken ct = default);

    /// <summary>
    /// Set auto repay. Only applicable to Spot mode
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-auto-repay" /></para>
    /// </summary>
    /// <param name="autoRepay">Auto repay enabled or not</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXAutoRepayStatus>> SetAutoRepayAsync(bool autoRepay, CancellationToken ct = default);

    /// <summary>
    /// Get borrow/repay history
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-borrow-repay-history" /></para>
    /// </summary>
    /// <param name="asset">Filter by asset</param>
    /// <param name="type">Filter by type</param>
    /// <param name="startTime">Filter by start time</param>
    /// <param name="endTime">Filter by end time</param>
    /// <param name="limit">Max number of results</param>
    /// <param name="ct">Cancellation token</param>
    Task<WebCallResult<OKXBorrowRepayEntry[]>> GetBorrowRepayHistoryAsync(string? asset = null, BorrowRepayType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    /// <summary>
    /// Get a list of instruments that are available to the user
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-instruments" /></para>
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="symbol">Filter by symbol, for example `ETH-USDT`</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    Task<WebCallResult<Objects.Public.OKXInstrument[]>> GetSymbolsAsync(InstrumentType instrumentType, string? underlying = null, string? symbol = null, string? instrumentFamily = null, CancellationToken ct = default);

    /// <summary>
    /// Set fee charge type for spot trading
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-fee-type" /></para>
    /// </summary>
    /// <param name="feeType">Fee type</param>
    /// <param name="ct">Cancellation Token</param>
    Task<WebCallResult<OKXFeeType>> SetFeeTypeAsync(FeeType feeType, CancellationToken ct = default);

    /// <summary>
    /// Set settlement asset for USD contracts
    /// <para><a href="https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-settle-currency" /></para>
    /// </summary>
    /// <param name="settleAsset">The settlement asset</param>
    /// <param name="ct">Cancellation Token</param>
    Task<WebCallResult<OKXSettleAsset>> SetSettleAssetAsync(string settleAsset, CancellationToken ct = default);
}
