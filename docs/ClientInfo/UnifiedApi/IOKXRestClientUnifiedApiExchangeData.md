---
title: IOKXRestClientUnifiedApiExchangeData
has_children: false
parent: IOKXRestClientUnifiedApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`OKXRestClient > UnifiedApi > ExchangeData`  
*Unified exchange data endpoints*
  

***

## Get24HourVolumeAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-24h-total-volume](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-24h-total-volume)  
<p>

*The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.Get24HourVolumeAsync();  
```  

```csharp  
Task<WebCallResult<OKX24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetBlockTickerAsync  

[https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-ticker](https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-ticker)  
<p>

*Get block ticker*  
*Retrieve the latest block trading volume in the last 24 hours.*  
*Rate Limit: 20 requests per 2 seconds*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetBlockTickerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXBlockTicker>> GetBlockTickerAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetBlockTickersAsync  

[https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-tickers](https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-tickers)  
<p>

*Get block tickers*  
*Retrieve the latest block trading volume in the last 24 hours.*  
*Rate Limit: 20 requests per 2 seconds*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetBlockTickersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXBlockTicker>>> GetBlockTickersAsync(OKXInstrumentType instrumentType, string? underlying = default, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Type of instrument|
|_[Optional]_ underlying|Underlying|
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetBlockTradesAsync  

[https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-trades](https://www.okx.com/docs-v5/en/#block-trading-rest-api-get-block-trades)  
<p>

*Get block trades*  
*Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.*  
*Rate Limit: 20 requests per 2 seconds*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetBlockTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXTrade>>> GetBlockTradesAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetDeliveryExerciseHistoryAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-delivery-exercise-history](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-delivery-exercise-history)  
<p>

*Retrieve the estimated delivery price, which will only have a return value one hour before the delivery/exercise.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetDeliveryExerciseHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXDeliveryExerciseHistory>>> GetDeliveryExerciseHistoryAsync(OKXInstrumentType instrumentType, string? underlying = default, DateTime? startTime = default, DateTime? endTime = default, int limit, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|_[Optional]_ underlying|Underlying|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetDiscountInfoAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-discount-rate-and-interest-free-quota](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-discount-rate-and-interest-free-quota)  
<p>

*Retrieve discount rate level and interest-free quota.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetDiscountInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXDiscountInfo>>> GetDiscountInfoAsync(int? discountLevel = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ discountLevel|Discount level|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetEstimatedPriceAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-estimated-delivery-exercise-price](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-estimated-delivery-exercise-price)  
<p>

*Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetEstimatedPriceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXEstimatedPrice>> GetEstimatedPriceAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetFundingRateHistoryAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate-history](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate-history)  
<p>

*Retrieve funding rate history. This endpoint can retrieve data from the last 3 months.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetFundingRateHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, DateTime? startTime = default, DateTime? endTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetFundingRatesAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-funding-rate)  
<p>

*Retrieve funding rate.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetFundingRatesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXFundingRate>>> GetFundingRatesAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetIndexComponentsAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-components](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-components)  
<p>

*Get the index component information data on the market*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetIndexComponentsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|index||
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetIndexKlinesAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-candlesticks](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-candlesticks)  
<p>

*Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetIndexKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetIndexKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = default, DateTime? endTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|period|Bar size, the default is 1m|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetIndexTickersAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-tickers](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-index-tickers)  
<p>

*Retrieve index tickers.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetIndexTickersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXIndexTicker>>> GetIndexTickersAsync(string? quoteAsset = default, string? symbol = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ quoteAsset|Quote asset. Currently there is only an index with USD/USDT/BTC as the quote asset.|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetInsuranceFundAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-insurance-fund](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-insurance-fund)  
<p>

*Get insurance fund*  
*Get insurance fund balance information*  
*Rate Limit: 10 requests per 2 seconds*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetInsuranceFundAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXInsuranceFund>> GetInsuranceFundAsync(OKXInstrumentType instrumentType, OKXInsuranceType type, string? underlying = default, string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int limit, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType||
|type||
|_[Optional]_ underlying||
|_[Optional]_ asset||
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|limit||
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetInterestRatesAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota)  
<p>

*Get margin interest rate*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetInterestRatesAsync();  
```  

```csharp  
Task<WebCallResult<OKXInterestRate>> GetInterestRatesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetKlineHistoryAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks-history](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks-history)  
<p>

*Retrieve history candlestick charts from recent years.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetKlineHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetKlineHistoryAsync(string symbol, OKXPeriod period, DateTime? startTime = default, DateTime? endTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|period|Bar size, the default is 1m|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetKlinesAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-candlesticks)  
<p>

*Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = default, DateTime? endTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|period|Bar size, the default is 1m|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|limit|Number of results per request. The maximum is 300; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetLimitPriceAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-limit-price](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-limit-price)  
<p>

*Retrieve the highest buy limit and lowest sell limit of the instrument.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetLimitPriceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXLimitPrice>> GetLimitPriceAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetMarkPriceKlinesAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price-candlesticks](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price-candlesticks)  
<p>

*Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetMarkPriceKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXCandlestick>>> GetMarkPriceKlinesAsync(string symbol, OKXPeriod period, DateTime? startTime = default, DateTime? endTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|period|Bar size, the default is 1m|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetMarkPricesAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-mark-price)  
<p>

*Retrieve mark price.*  
*We set the mark price based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetMarkPricesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXMarkPrice>>> GetMarkPricesAsync(OKXInstrumentType instrumentType, string? underlying = default, string? symbol = default, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|_[Optional]_ underlying|Underlying|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetOpenInterestsAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-open-interest](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-open-interest)  
<p>

*Retrieve the total open interest for contracts on OKX.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetOpenInterestsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXOpenInterest>>> GetOpenInterestsAsync(OKXInstrumentType instrumentType, string? underlying = default, string? symbol = default, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|_[Optional]_ underlying|Underlying|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetOptionMarketDataAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-option-market-data](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-option-market-data)  
<p>

*Retrieve option market data.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetOptionMarketDataAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXOptionSummary>>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = default, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|underlying|Underlying|
|_[Optional]_ expiryDate|Contract expiry date|
|_[Optional]_ instrumentFamily|Instrument type|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetOracleAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-oracle](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-oracle)  
<p>

*Get the crypto price of signing using Open Oracle smart contract.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetOracleAsync();  
```  

```csharp  
Task<WebCallResult<OKXOracle>> GetOracleAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetOrderBookAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-order-book](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-order-book)  
<p>

*Retrieve a symbol order book.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXOrderBook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|depth|Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetPositionTiersAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-position-tiers](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-position-tiers)  
<p>

*Position information，Maximum leverage depends on your borrowings and margin ratio.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetPositionTiersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXPositionTier>>> GetPositionTiersAsync(OKXInstrumentType instrumentType, OKXMarginMode marginMode, string underlying, string? symbol = default, string? tier = default, string? asset = default, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|marginMode|Margin Mode|
|underlying|Underlying|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ tier|Tiers|
|_[Optional]_ asset|Margin currency|
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRecentTradesAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades)  
<p>

*Retrieve the recent transactions of an instrument.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRecentTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXTrade>>> GetRecentTradesAsync(string symbol, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikContractSummaryAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-contracts-open-interest-and-volume](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-contracts-open-interest-and-volume)  
<p>

*Open interest is the sum of all long and short futures and perpetual swap positions.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikContractSummaryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXInterestVolume>>> GetRubikContractSummaryAsync(string asset, OKXPeriod period, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|period|period, the default is 5m, e.g. [5m/1H/1D]|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikInterestVolumeExpiryAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-expiry](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-expiry)  
<p>

*This shows the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikInterestVolumeExpiryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXInterestVolumeExpiry>>> GetRubikInterestVolumeExpiryAsync(string asset, OKXPeriod period, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|period|period, the default is 8H. e.g. [8H/1D]|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikInterestVolumeStrikeAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-strike](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-open-interest-and-volume-strike)  
<p>

*This shows what option strikes are the most popular for each expiration.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikInterestVolumeStrikeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXInterestVolumeStrike>>> GetRubikInterestVolumeStrikeAsync(string asset, string expiryTime, OKXPeriod period, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|expiryTime|expiry time (Format: YYYYMMdd, for example: "20210623")|
|period|period, the default is 8H. e.g. [8H/1D]|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikLongShortRatioAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-long-short-ratio](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-long-short-ratio)  
<p>

*This is the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikLongShortRatioAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXRatio>>> GetRubikLongShortRatioAsync(string asset, OKXPeriod period, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|period|period, the default is 5m, e.g. [5m/1H/1D]|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikMarginLendingRatioAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-margin-lending-ratio](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-margin-lending-ratio)  
<p>

*This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikMarginLendingRatioAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXRatio>>> GetRubikMarginLendingRatioAsync(string asset, OKXPeriod period, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Currency|
|period|period, the default is 5m, e.g. [5m/1H/1D]|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikOptionsSummaryAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-options-open-interest-and-volume](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-options-open-interest-and-volume)  
<p>

*This shows the sum of all open positions and how much total trading volume has taken place.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikOptionsSummaryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXInterestVolume>>> GetRubikOptionsSummaryAsync(string asset, OKXPeriod period, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|period|period, the default is 8H. e.g. [8H/1D]|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikPutCallRatioAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-put-call-ratio](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-put-call-ratio)  
<p>

*This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikPutCallRatioAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXPutCallRatio>>> GetRubikPutCallRatioAsync(string asset, OKXPeriod period, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|period|period, the default is 8H. e.g. [8H/1D]|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikSupportCoinAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-support-coin](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-support-coin)  
<p>

*Get the currency supported by the transaction big data interface*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikSupportCoinAsync();  
```  

```csharp  
Task<WebCallResult<OKXSupportCoins>> GetRubikSupportCoinAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikTakerFlowAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-flow](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-flow)  
<p>

*This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikTakerFlowAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXTakerFlow>> GetRubikTakerFlowAsync(string asset, OKXPeriod period, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|period|period, the default is 8H. e.g. [8H/1D]|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetRubikTakerVolumeAsync  

[https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-volume](https://www.okx.com/docs-v5/en/#trading-statistics-rest-api-get-taker-volume)  
<p>

*This is the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetRubikTakerVolumeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXTakerVolume>>> GetRubikTakerVolumeAsync(string asset, OKXInstrumentType instrumentType, OKXPeriod period, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Currency|
|instrumentType|Instrument Type|
|period|period, the default is 5m, e.g. [5m/1H/1D]|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetServerTimeAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-system-time](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-system-time)  
<p>

*Retrieve API server time.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetServerTimeAsync();  
```  

```csharp  
Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetSymbolsAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-instruments](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-instruments)  
<p>

*Retrieve a list of instruments with open contracts.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetSymbolsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXInstrument>>> GetSymbolsAsync(OKXInstrumentType instrumentType, string? underlying = default, string? symbol = default, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|_[Optional]_ underlying|Underlying|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetTickerAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-ticker](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-ticker)  
<p>

*Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetTickerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetTickersAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-tickers](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-tickers)  
<p>

*Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXTicker>>> GetTickersAsync(OKXInstrumentType instrumentType, string? underlying = default, string? instrumentFamily = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|_[Optional]_ underlying|Underlying|
|_[Optional]_ instrumentFamily|Instrument family|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetTradeHistoryAsync  

[https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades-history](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-trades-history)  
<p>

*Get trades history*  
*Retrieve the recent transactions of an instrument from the last 3 months with pagination.*  
*Rate Limit: 10 requests per 2 seconds*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXTrade>>> GetTradeHistoryAsync(string symbol, OKXTradeHistoryPaginationType type, DateTime? startTime = default, DateTime? endTime = default, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol, e.g. BTC-USDT|
|type|Pagination Type|
|_[Optional]_ startTime|Pagination of data to return records earlier than the requested ts|
|_[Optional]_ endTime|Pagination of data to return records newer than the requested ts|
|limit|Number of results per request. The maximum is 100; the default is 100.|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetUnderlyingAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-underlying](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-underlying)  
<p>

*Get Underlying*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetUnderlyingAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<string>>> GetUnderlyingAsync(OKXInstrumentType instrumentType, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|instrumentType|Instrument Type|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## GetVIPInterestRatesAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-interest-rate-and-loan-quota)  
<p>

*Get interest rate and loan quota for VIP loans*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.GetVIPInterestRatesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<OKXVipInterestRate>>> GetVIPInterestRatesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation Token|

</p>

***

## UnitConvertAsync  

[https://www.okx.com/docs-v5/en/#public-data-rest-api-unit-convert](https://www.okx.com/docs-v5/en/#public-data-rest-api-unit-convert)  
<p>

*Convert units*  

```csharp  
var client = new OKXRestClient();  
var result = await client.UnifiedApi.ExchangeData.UnitConvertAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<OKXUnitConvert>> UnitConvertAsync(OKXConvertType type, string symbol, decimal quantity, OKXConvertUnit? unit = default, decimal? price = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|type|Convert type|
|symbol|Symbol|
|quantity|Quantity to buy or sell|
|_[Optional]_ unit|The unit of currency|
|_[Optional]_ price|Order price|
|_[Optional]_ ct|Cancellation Token|

</p>
