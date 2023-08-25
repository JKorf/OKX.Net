---
title: IOKXRestClientUnifiedApiAccount
has_children: false
parent: IOKXRestClientUnifiedApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`OKXRestClient > UnifiedApi > Account`  
*Unified account endpoints*
  

***

## CancelWithdrawalAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-cancel-withdrawal](https://www.okx.com/docs-v5/en/#funding-account-rest-api-cancel-withdrawal)  
<p>

*Cancel withdrawal*  
*You can cancel normal withdrawal requests, but you cannot cancel withdrawal requests on Lightning.*  
*Rate Limit: 6 requests per second*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.CancelWithdrawalAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXWithdrawalId>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|withdrawalId|Withdrawal ID|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetAccountBalanceAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-balance](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-balance)  
<p>

*Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the account.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetAccountBalanceAsync();  
```  

```csharp  
Task<WebCallResult<OKXAccountBalance>> GetAccountBalanceAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetAccountConfigurationAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-account-configuration](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-account-configuration)  
<p>

*Retrieve current account configuration.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetAccountConfigurationAsync();  
```  

```csharp  
Task<WebCallResult<OKXAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetAccountLeverageAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-leverage](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-leverage)  
<p>

*Get Leverage*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetAccountLeverageAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXLeverage>>> GetAccountLeverageAsync(string symbols, OKXMarginMode marginMode, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|Single symbol or multiple symbols (no more than 20) separated with comma|
|marginMode|Margin Mode|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetAccountPositionHistoryAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-positions-history](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-positions-history)  
<p>

*Retrieve the updated position data for the last 3 months. Return in reverse chronological order using utime.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetAccountPositionHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXClosingPosition>>> GetAccountPositionHistoryAsync(OKXInstrumentType? instrumentType = default, string? symbol = default, OKXMarginMode? marginMode = default, OKXClosingPositionType? type = default, string? positionId = default, DateTime? endTime = default, DateTime? startTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ instrumentType|Instrument type|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ marginMode|Margin mode|
|_[Optional]_ type|The type of closing position. It is the latest type if there are several types for the same position.|
|_[Optional]_ positionId|Position ID|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100. The default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetAccountPositionRiskAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-account-and-position-risk](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-account-and-position-risk)  
<p>

*Get account and position risk*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetAccountPositionRiskAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXPositionRisk>>> GetAccountPositionRiskAsync(OKXInstrumentType? instrumentType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ instrumentType|Instrument Type|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetAccountPositionsAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-positions](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-positions)  
<p>

*Retrieve information on your positions. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetAccountPositionsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXPosition>>> GetAccountPositionsAsync(OKXInstrumentType? instrumentType = default, string? symbol = default, string? positionId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ instrumentType|Instrument Type|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ positionId|Position ID|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetAssetsAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-currencies](https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-currencies)  
<p>

*Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetAssetsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXAsset>>> GetAssetsAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Single asset or multiple assets (no more than 20) separated with comma|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetBillArchiveAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-bills-details-last-3-months](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-bills-details-last-3-months)  
<p>

*Retrieve the account’s bills. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with most recent first. This endpoint can retrieve data from the last 3 months.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetBillArchiveAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXAccountBill>>> GetBillArchiveAsync(OKXInstrumentType? instrumentType = default, string? asset = default, OKXMarginMode? marginMode = default, OKXContractType? contractType = default, OKXAccountBillType? billType = default, OKXAccountBillSubType? billSubType = default, DateTime? endTime = default, DateTime? startTime = default, int limit, string? fromId = default, string? toId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ instrumentType|Instrument Type|
|_[Optional]_ asset|Asset|
|_[Optional]_ marginMode|Margin Mode|
|_[Optional]_ contractType|Contract Type|
|_[Optional]_ billType|Bill Type|
|_[Optional]_ billSubType|Bill Sub Type|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ fromId|Pagination of data to return records earlier than the requested id|
|_[Optional]_ toId|Pagination of data to return records newer than the requested id|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetBillHistoryAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-bills-details-last-7-days](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-bills-details-last-7-days)  
<p>

*Retrieve the bills of the account. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with the most recent first. This endpoint can retrieve data from the last 7 days.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetBillHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXAccountBill>>> GetBillHistoryAsync(OKXInstrumentType? instrumentType = default, string? asset = default, OKXMarginMode? marginMode = default, OKXContractType? contractType = default, OKXAccountBillType? billType = default, OKXAccountBillSubType? billSubType = default, DateTime? endTime = default, DateTime? startTime = default, int limit, string? fromId = default, string? toId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ instrumentType|Instrument Type|
|_[Optional]_ asset|Asset|
|_[Optional]_ marginMode|Margin Mode|
|_[Optional]_ contractType|Contract Type|
|_[Optional]_ billType|Bill Type|
|_[Optional]_ billSubType|Bill Sub Type|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ fromId|Pagination of data to return records earlier than the requested id|
|_[Optional]_ toId|Pagination of data to return records newer than the requested id|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetDepositAddressAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-deposit-address](https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-deposit-address)  
<p>

*Retrieve the deposit addresses of currencies, including previously-used addresses.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetDepositAddressAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXDepositAddress>>> GetDepositAddressAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetDepositHistoryAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-deposit-history](https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-deposit-history)  
<p>

*Retrieve the deposit history of all assets, up to 100 recent records in a year.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetDepositHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXDepositHistory>>> GetDepositHistoryAsync(string? asset = default, string? transactionId = default, OKXDepositState? state = default, DateTime? endTime = default, DateTime? startTime = default, int limit, string? depositId = default, string? fromWithdrawalId = default, OKXDepositType? type = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset|
|_[Optional]_ transactionId|Transaction ID|
|_[Optional]_ state|State|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ depositId|Deposit ID|
|_[Optional]_ fromWithdrawalId|Internal transfer initiator's withdrawal ID|
|_[Optional]_ type|Deposit Type|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetFeeRatesAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-fee-rates](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-fee-rates)  
<p>

*Get Fee Rates*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetFeeRatesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXFeeRate>> GetFeeRatesAsync(OKXInstrumentType instrumentType, string? symbol = default, string? underlying = default, OKXFeeRateCategory? category = default, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ underlying|Underlying|
|_[Optional]_ category|Category|
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetFundingBalanceAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-balance](https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-balance)  
<p>

*Retrieve the balances of all the assets, and the amount that is available or on hold.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetFundingBalanceAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXFundingBalance>>> GetFundingBalanceAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetFundingBillDetailsAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-asset-bills-details](https://www.okx.com/docs-v5/en/#funding-account-rest-api-asset-bills-details)  
<p>

*Query the billing record, you can get the latest 1 month historical data*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetFundingBillDetailsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXFundingBill>>> GetFundingBillDetailsAsync(string? asset = default, OKXFundingBillType? type = default, DateTime? endTime = default, DateTime? startTime = default, int limit, string? clientId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset|
|_[Optional]_ type|Bill type|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ clientId|Client id|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetInterestAccruedAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-interest-accrued-data](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-interest-accrued-data)  
<p>

*Get interest-accrued*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetInterestAccruedAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXInterestAccrued>>> GetInterestAccruedAsync(string? symbol = default, string? asset = default, OKXMarginMode? marginMode = default, DateTime? endTime = default, DateTime? startTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ asset|Asset|
|_[Optional]_ marginMode|Margin Mode|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetInterestRateAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-interest-rate](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-interest-rate)  
<p>

*Get the user's current leveraged currency borrowing interest rate*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetInterestRateAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXInterestRate>>> GetInterestRateAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetLightningDepositsAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-lightning-deposits](https://www.okx.com/docs-v5/en/#funding-account-rest-api-lightning-deposits)  
<p>

*Users can create up to 10,000 different invoices within 24 hours.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetLightningDepositsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXLightningDeposit>>> GetLightningDepositsAsync(string currency, decimal amount, OKXLightningDepositAccount? account = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|currency|Currency|
|amount|deposit amount between 0.000001 - 0.1|
|_[Optional]_ account|Receiving account|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetLightningWithdrawalsAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-lightning-withdrawals](https://www.okx.com/docs-v5/en/#funding-account-rest-api-lightning-withdrawals)  
<p>

*The maximum withdrawal amount is 0.1 BTC per request, and 1 BTC in 24 hours. The minimum withdrawal amount is approximately 0.000001 BTC. Sub-account does not support withdrawal.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetLightningWithdrawalsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXLightningWithdrawal>> GetLightningWithdrawalsAsync(string asset, string invoice, string? memo = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset. Currently only BTC is supported.|
|invoice|Invoice text|
|_[Optional]_ memo|Lightning withdrawal memo|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetMaximumAmountAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-buy-sell-amount-or-open-amount](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-buy-sell-amount-or-open-amount)  
<p>

*Get maximum buy/sell amount or open amount*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetMaximumAmountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXMaximumAmount>>> GetMaximumAmountAsync(string symbol, OKXTradeMode tradeMode, string? asset = default, decimal? price = default, int? leverage = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|SymbolD|
|tradeMode|Trade Mode|
|_[Optional]_ asset|Asset|
|_[Optional]_ price|Price|
|_[Optional]_ leverage|Leverage|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetMaximumAvailableAmountAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-available-tradable-amount](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-available-tradable-amount)  
<p>

*Get Maximum Available Tradable Amount*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetMaximumAvailableAmountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXMaximumAvailableAmount>>> GetMaximumAvailableAmountAsync(string symbol, OKXTradeMode tradeMode, string? asset = default, bool? reduceOnly = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|tradeMode|Trade Mode|
|_[Optional]_ asset|Currency|
|_[Optional]_ reduceOnly|Reduce Only|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetMaximumLoanAmountAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-the-maximum-loan-of-instrument](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-the-maximum-loan-of-instrument)  
<p>

*Get the maximum loan of instrument*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetMaximumLoanAmountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXMaximumLoanAmount>>> GetMaximumLoanAmountAsync(string instrumentId, OKXMarginMode marginMode, string? marginAsset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentId|Instrument ID|
|marginMode|Margin Mode|
|_[Optional]_ marginAsset|Margin asset|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetMaximumWithdrawalsAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-withdrawals](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-maximum-withdrawals)  
<p>

*Retrieve the maximum transferable amount.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetMaximumWithdrawalsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXWithdrawalAmount>>> GetMaximumWithdrawalsAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetSavingBalancesAsync  

[https://www.okx.com/docs-v5/en/#financial-product-savings-get-saving-balance](https://www.okx.com/docs-v5/en/#financial-product-savings-get-saving-balance)  
<p>

*Get saving balance*  
*Only the assets in the funding account can be used for saving.*  
*Rate Limit: 6 requests per second*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetSavingBalancesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXSavingBalance>>> GetSavingBalancesAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset, e.g. BTC|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetWithdrawalHistoryAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-withdrawal-history](https://www.okx.com/docs-v5/en/#funding-account-rest-api-get-withdrawal-history)  
<p>

*Retrieve the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.GetWithdrawalHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXWithdrawalHistory>>> GetWithdrawalHistoryAsync(string? asset = default, string? transactionId = default, OKXWithdrawalState? state = default, DateTime? endTime = default, DateTime? startTime = default, int limit, string? withdrawalId = default, string? clientId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Asset|
|_[Optional]_ transactionId|Transaction ID|
|_[Optional]_ state|State|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ withdrawalId|Client-supplied ID|
|_[Optional]_ clientId|Client id|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SavingPurchaseRedemptionAsync  

[https://www.okx.com/docs-v5/en/#financial-product-savings-post-savings-purchase-redemption](https://www.okx.com/docs-v5/en/#financial-product-savings-post-savings-purchase-redemption)  
<p>

**  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.SavingPurchaseRedemptionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXSavingActionResponse>> SavingPurchaseRedemptionAsync(string asset, decimal amount, OKXSavingActionSide side, decimal? rate = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset||
|amount||
|side||
|_[Optional]_ rate||
|_[Optional]_ ct||

</p>

***

## SetAccountLeverageAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-leverage](https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-leverage)  
<p>

*The following are the setting leverage cases for an instrument:*  
*Set leverage for isolated MARGIN at pairs level.*  
*Set leverage for cross MARGIN in Single-currency margin at pairs level.*  
*Set leverage for cross MARGIN in Multi-currency margin at currency level.*  
*Set leverage for cross/isolated FUTURES/SWAP at underlying/contract level.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.SetAccountLeverageAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXLeverage>>> SetAccountLeverageAsync(int leverage, OKXMarginMode marginMode, string? asset = default, string? symbol = default, OKXPositionSide? positionSide = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|leverage|Leverage|
|marginMode|Margin Mode|
|_[Optional]_ asset|Asset|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ positionSide|Position Side|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SetAccountPositionModeAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-position-mode](https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-position-mode)  
<p>

*FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.SetAccountPositionModeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXAccountPositionMode>> SetAccountPositionModeAsync(OKXPositionMode positionMode, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|positionMode||
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SetGreeksAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-greeks-pa-bs](https://www.okx.com/docs-v5/en/#trading-account-rest-api-set-greeks-pa-bs)  
<p>

*Set the display type of Greeks.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.SetGreeksAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXAccountGreeksType>> SetGreeksAsync(OKXGreeksType greeksType, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|greeksType|Display type of Greeks.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SetMarginAmountAsync  

[https://www.okx.com/docs-v5/en/#trading-account-rest-api-increase-decrease-margin](https://www.okx.com/docs-v5/en/#trading-account-rest-api-increase-decrease-margin)  
<p>

*Increase or decrease the margin of the isolated position.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.SetMarginAmountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXMarginAmount>>> SetMarginAmountAsync(string symbol, OKXPositionSide positionSide, OKXMarginAddReduce marginAddReduce, decimal amount, string? asset = default, bool? auto = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|positionSide|Position Side|
|marginAddReduce|Type|
|amount|Amount|
|_[Optional]_ asset|Asset|
|_[Optional]_ auto|Automatic loan transfer out|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## TransferAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-funds-transfer](https://www.okx.com/docs-v5/en/#funding-account-rest-api-funds-transfer)  
<p>

*This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts. Direct transfers between sub-accounts are not allowed.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.TransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXTransferResponse>> TransferAsync(string asset, decimal amount, OKXTransferType type, OKXAccount fromAccount, OKXAccount toAccount, string? subAccountName = default, string? fromSymbol = default, string? toSymbol = default, bool? loanTransfer = default, string? clientId = default, bool? ignorePositionRisk = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Currency|
|amount|Amount|
|type|Transfer type|
|fromAccount|The remitting account|
|toAccount|The beneficiary account|
|_[Optional]_ subAccountName|Sub Account Name|
|_[Optional]_ fromSymbol|MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred out.|
|_[Optional]_ toSymbol|MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred in.|
|_[Optional]_ loanTransfer|Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin|
|_[Optional]_ clientId|Client id|
|_[Optional]_ ignorePositionRisk|Ignore position risk|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## WithdrawAsync  

[https://www.okx.com/docs-v5/en/#funding-account-rest-api-withdrawal](https://www.okx.com/docs-v5/en/#funding-account-rest-api-withdrawal)  
<p>

*Withdrawal of tokens.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.Account.WithdrawAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXWithdrawalResponse>> WithdrawAsync(string asset, decimal amount, OKXWithdrawalDestination destination, string toAddress, decimal fee, string? network = default, string? areaCode = default, string? clientId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|amount|Amount|
|destination|Withdrawal destination address|
|toAddress|Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'|
|fee|Transaction fee|
|_[Optional]_ network|Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni. If this parameter is not filled in because it is not available, it will default to the main chain.|
|_[Optional]_ areaCode|	Area code for the phone number, e.g. 86. If toAddr is a phone number, this parameter is required.|
|_[Optional]_ clientId|Client-supplied ID|
|_[Optional]_ ct|Cancellation Token|

</p>
