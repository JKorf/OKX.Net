---
title: IOKXSocketClientUnifiedApiAccount
has_children: false
parent: IOKXSocketClientUnifiedApi
grand_parent: Socket API documentation
---
*[generated documentation]*  
`OKXSocketClient > UnifiedApi > Account`  
*Unified API*
  

***

## SubscribeToAccountUpdatesAsync  

[https://www.okx.com/docs-v5/en/#trading-account-websocket-account-channel](https://www.okx.com/docs-v5/en/#trading-account-websocket-account-channel)  
<p>

*Subscribe to account information updates. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Account.SubscribeToAccountUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string? asset, bool regularUpdates, Action<OKXAccountBalance> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Only receive updates for this asset|
|regularUpdates|If true will send updates regularly even if nothing has changed. If false only send update on change|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SubscribeToBalanceAndPositionUpdatesAsync  

[https://www.okx.com/docs-v5/en/#trading-account-websocket-balance-and-position-channel](https://www.okx.com/docs-v5/en/#trading-account-websocket-balance-and-position-channel)  
<p>

*Subscribe to account balance and position information updates. Data will be pushed when triggered by events such as filled order, funding transfer.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Account.SubscribeToBalanceAndPositionUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(Action<OKXPositionRisk> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SubscribeToDepositUpdatesAsync  

[https://www.okx.com/docs-v5/en/#funding-account-websocket-deposit-info-channel](https://www.okx.com/docs-v5/en/#funding-account-websocket-deposit-info-channel)  
<p>

*Subscribe to deposit updates*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Account.SubscribeToDepositUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToDepositUpdatesAsync(Action<OKXDepositHistory> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SubscribeToWithdrawalUpdatesAsync  

[https://www.okx.com/docs-v5/en/#funding-account-websocket-withdrawal-info-channel](https://www.okx.com/docs-v5/en/#funding-account-websocket-withdrawal-info-channel)  
<p>

*Subscribe to withdrawal updates*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Account.SubscribeToWithdrawalUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToWithdrawalUpdatesAsync(Action<OKXWithdrawalHistory> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>
