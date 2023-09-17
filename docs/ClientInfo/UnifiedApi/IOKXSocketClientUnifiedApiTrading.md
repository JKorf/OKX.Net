---
title: IOKXSocketClientUnifiedApiTrading
has_children: false
parent: IOKXSocketClientUnifiedApi
grand_parent: Socket API documentation
---
*[generated documentation]*  
`OKXSocketClient > UnifiedApi > Trading`  
*Unified API*
  

***

## AmendMultipleOrdersAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-amend-multiple-orders](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-amend-multiple-orders)  
<p>

*Amend incomplete orders in batches. Maximum 20 orders can be amended per request.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.AmendMultipleOrdersAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<IEnumerable<OKXOrderAmendResponse>>> AmendMultipleOrdersAsync(IEnumerable<OKXOrderAmendRequest> ordersToCancel);  
```  

|Parameter|Description|
|---|---|
|ordersToCancel|Orders to cancel|

</p>

***

## AmendOrderAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-amend-order](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-amend-order)  
<p>

*Amend an incomplete order.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.AmendOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<OKXOrderAmendResponse>> AmendOrderAsync(string symbol, long? orderId = default, string? clientOrderId = default, string? requestId = default, decimal? newQuantity = default, decimal? newPrice = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ orderId|Amend by order id. This or clientOrderId should be provided|
|_[Optional]_ clientOrderId|Amend by client order id. This or orderId should be provided|
|_[Optional]_ requestId|Request id|
|_[Optional]_ newQuantity|New quantity|
|_[Optional]_ newPrice|New price|

</p>

***

## CancelMultipleOrdersAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-cancel-multiple-orders](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-cancel-multiple-orders)  
<p>

*Cancel incomplete orders in batches. Maximum 20 orders can be canceled per request.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.CancelMultipleOrdersAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<IEnumerable<OKXOrderCancelResponse>>> CancelMultipleOrdersAsync(IEnumerable<OKXOrderCancelRequest> ordersToCancel);  
```  

|Parameter|Description|
|---|---|
|ordersToCancel|Orders to cancel|

</p>

***

## CancelOrderAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-cancel-order](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-cancel-order)  
<p>

*Cancel an incomplete order*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<OKXOrderCancelResponse>> CancelOrderAsync(string symbol, string? orderId = default, string? clientOrderId = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ orderId|Cancel by order id. This or clientOrderId should be provided|
|_[Optional]_ clientOrderId|Cancel by client order id. This or orderId should be provided|

</p>

***

## PlaceMultipleOrdersAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-multiple-orders](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-multiple-orders)  
<p>

*Place orders in a batch. Maximum 20 orders can be placed per request*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.PlaceMultipleOrdersAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<IEnumerable<OKXOrderPlaceResponse>>> PlaceMultipleOrdersAsync(IEnumerable<OKXOrderPlaceRequest> orders);  
```  

|Parameter|Description|
|---|---|
|orders|The orders to place|

</p>

***

## PlaceOrderAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-order](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-order)  
<p>

*Place a new order*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.PlaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<OKXOrderPlaceResponse>> PlaceOrderAsync(string symbol, OKXOrderSide side, OKXOrderType type, OKXTradeMode tradeMode, decimal quantity, decimal? price = default, OKXPositionSide? positionSide = default, OKXQuickMarginType? quickMarginType = default, int? selfTradePreventionId = default, OKXSelfTradePreventionMode? selfTradePreventionMode = default, string? asset = default, OKXQuantityAsset? quantityAsset = default, string? clientOrderId = default, bool? reduceOnly = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|side|Order Side|
|type|Order Type|
|tradeMode|Trade Mode|
|quantity|Quantity|
|_[Optional]_ price|Price|
|_[Optional]_ positionSide|Position Side|
|_[Optional]_ quickMarginType|Quick margin type|
|_[Optional]_ selfTradePreventionId|Self trade prevention id|
|_[Optional]_ selfTradePreventionMode|Self trade prevention mode|
|_[Optional]_ asset|Asset|
|_[Optional]_ quantityAsset|Asset of the quantity when placing market order|
|_[Optional]_ clientOrderId|Client Order ID|
|_[Optional]_ reduceOnly|Whether to reduce position only or not, true false, the default is false.|

</p>

***

## SubscribeToAdvanceAlgoOrderUpdatesAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-ws-advance-algo-orders-channel](https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-ws-advance-algo-orders-channel)  
<p>

*Subscribe to advance algo orders (includes iceberg order and twap order) updates. Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.SubscribeToAdvanceAlgoOrderUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(OKXInstrumentType instrumentType, string? symbol, string? algoId, Action<OKXAlgoOrderUpdate> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|symbol|Symbol|
|algoId|Algo order id|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SubscribeToAlgoOrderUpdatesAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-ws-algo-orders-channel](https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-ws-algo-orders-channel)  
<p>

*Subscribe to algo orders (includes trigger order, oco order, conditional order) updates. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.SubscribeToAlgoOrderUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(OKXInstrumentType instrumentType, string? symbol, string? instrumentFamily, Action<OKXAlgoOrderUpdate> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|symbol|Symbol|
|instrumentFamily|Instrument family|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SubscribeToLiquidationWarningUpdatesAsync  

[https://www.okx.com/docs-v5/en/#trading-account-websocket-position-risk-warning](https://www.okx.com/docs-v5/en/#trading-account-websocket-position-risk-warning)  
<p>

*This push channel is only used as a risk warning, and is not recommended as a risk judgment for strategic trading. In the case that the market is volatile, there may be the possibility that the position has been liquidated at the same time that this message is pushed. The warning is sent when a position is at risk of liquidation for isolated margin positions.The warning is sent when all the positions are at risk of liquidation for cross margin positions.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.SubscribeToLiquidationWarningUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(OKXInstrumentType instrumentType, string? instrumentFamily, Action<OKXPosition> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|The instrument type|
|instrumentFamily|Optional instrument family|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SubscribeToOrderUpdatesAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-order-channel](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-order-channel)  
<p>

*Subscribe to order information updates. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.SubscribeToOrderUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(OKXInstrumentType instrumentType, string? symbol, string? instrumentFamily, Action<OKXOrderUpdate> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|symbol|Symbol|
|instrumentFamily|Instrument family|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## SubscribeToPositionUpdatesAsync  

[https://www.okx.com/docs-v5/en/#trading-account-websocket-positions-channel](https://www.okx.com/docs-v5/en/#trading-account-websocket-positions-channel)  
<p>

*Subscribe to position information updates. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.*  

```csharp  
var client = new OKXSocketClient();  
var result = await client.UnifiedApi.Trading.SubscribeToPositionUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(OKXInstrumentType instrumentType, string? symbol, string? instrumentFamily, bool regularUpdates, Action<OKXPosition> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|symbol|Instrument ID|
|instrumentFamily|Instrument family|
|regularUpdates|If true will send updates regularly even if nothing has changed. If false only send update on change|
|onData|On Data Handler|
|_[Optional]_ ct|Cancellation Token|

</p>
