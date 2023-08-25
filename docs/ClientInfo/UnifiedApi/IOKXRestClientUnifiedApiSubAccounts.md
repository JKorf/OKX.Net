---
title: IOKXRestClientUnifiedApiSubAccounts
has_children: false
parent: IOKXRestClientUnifiedApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`OKXRestClient > UnifiedApi > SubAccounts`  
*Unified API subaccount endpoints*
  

***

## GetSubAccountBillsAsync  

[https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-history-of-sub-account-transfer](https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-history-of-sub-account-transfer)  
<p>

*applies to master accounts only*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.SubAccounts.GetSubAccountBillsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXSubAccountBill>>> GetSubAccountBillsAsync(string? subAccountName = default, string? asset = default, OKXSubAccountTransferType? type = default, DateTime? endTime = default, DateTime? startTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subAccountName|Sub Account Name|
|_[Optional]_ asset|Asset|
|_[Optional]_ type|0: Transfers from master account to sub-account ;1 : Transfers from sub-account to master account.|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetSubAccountFundingBalancesAsync  

[https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-funding-balance](https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-funding-balance)  
<p>

*Get sub-account funding balance*  
*Query detailed balance info of Funding Account of a sub-account via the master account (applies to master accounts only)*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.SubAccounts.GetSubAccountFundingBalancesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXSubAccountFundingBalance>>> GetSubAccountFundingBalancesAsync(string subAccountName, string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountName|Sub Account Name|
|_[Optional]_ asset|Single asset or multiple assets (no more than 20) separated with comma, e.g. BTC or BTC,ETH.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetSubAccountsAsync  

[https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-list](https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-list)  
<p>

*applies to master accounts only*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.SubAccounts.GetSubAccountsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXSubAccount>>> GetSubAccountsAsync(bool? enable = default, string? subAccountName = default, DateTime? endTime = default, DateTime? startTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ enable|Sub-account status，true: Normal ; false: Frozen|
|_[Optional]_ subAccountName|Sub Account Name|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetSubAccountTradingBalancesAsync  

[https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-trading-balance](https://www.okx.com/docs-v5/en/#sub-account-rest-api-get-sub-account-trading-balance)  
<p>

*Query detailed balance info of Trading Account of a sub-account via the master account (applies to master accounts only)*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.SubAccounts.GetSubAccountTradingBalancesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXSubAccountTradingBalance>> GetSubAccountTradingBalancesAsync(string subAccountName, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountName|Sub Account Name|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## ResetSubAccountApiKeyAsync  

[https://www.okx.com/docs-v5/en/#sub-account-rest-api-reset-the-api-key-of-a-sub-account](https://www.okx.com/docs-v5/en/#sub-account-rest-api-reset-the-api-key-of-a-sub-account)  
<p>

*applies to master accounts only*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.SubAccounts.ResetSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXSubAccountApiKey>> ResetSubAccountApiKeyAsync(string subAccountName, string apiKey, string? apiLabel = default, bool? readPermission = default, bool? tradePermission = default, string? ipAddresses = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountName|Sub Account Name|
|apiKey|APIKey note|
|_[Optional]_ apiLabel|APIKey note|
|_[Optional]_ readPermission|Read permission|
|_[Optional]_ tradePermission|Trade permission|
|_[Optional]_ ipAddresses|Link IP addresses, separate with commas if more than one. Support up to 20 IP addresses.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## TransferBetweenSubAccountsAsync  

[https://www.okx.com/docs-v5/en/#sub-account-rest-api-master-accounts-manage-the-transfers-between-sub-accounts](https://www.okx.com/docs-v5/en/#sub-account-rest-api-master-accounts-manage-the-transfers-between-sub-accounts)  
<p>

*applies to master accounts only*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.SubAccounts.TransferBetweenSubAccountsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXSubAccountTransfer>> TransferBetweenSubAccountsAsync(string asset, decimal amount, OKXAccount fromAccount, OKXAccount toAccount, string fromSubAccountName, string toSubAccountName, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|amount|Amount|
|fromAccount|6:Funding Account 18:Unified Account|
|toAccount|6:Funding Account 18:Unified Account|
|fromSubAccountName|Sub-account name of the account that transfers funds out.|
|toSubAccountName|Sub-account name of the account that transfers funds in.|
|_[Optional]_ ct|Cancellation Token|

</p>
