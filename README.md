# ![Icon!](https://github.com/JKorf/OKX.Net/blob/358d31f58d8ee51fc234bff1940878a8d0ce5676/Okex.Net/Icon/icon.png "OKX.Net") OKX.Net

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/OKX.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/OKX.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/OKX.Net?style=for-the-badge)

OKX.Net is strongly typed client library for accessing the [OKX REST and Websocket API](https://www.okx.com/docs-v5/en/).
## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=for-the-badge)](https://www.nuget.org/packages/JK.OKX.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/JK.OKX.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/JK.OKX.Net)

	dotnet add package JK.OKX.Net
	
### GitHub packages
OKX.Net is available on [GitHub packages](https://github.com/JKorf/OKX.Net/pkgs/nuget/JK.OKX.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/OKX.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/OKX.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/OKX.Net/releases).

## How to use
*REST Endpoints*  

```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new OKXRestClient();
var tickerResult = await restClient.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT");
var lastPrice = tickerResult.Data.LastPrice;
```
	
*Websocket streams*  

```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new OKXSocketClient();
var tickerSubscriptionResult = socketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETH-USDT", (update) =>
{
	var lastPrice = update.Data.LastPrice;
});
```

**Note that for European clients the environment should be updated to Europe like so:**
```csharp
// With DI
services.AddOKX(options =>
{
    options.Environment = OKXEnvironment.Europe;
});

// Or when constructing client
var client = new OKXRestClient(options => 
{
    options.Environment = OKXEnvironment.Europe;
});
```


For information on the clients, dependency injection, response processing and more see the [OKX.Net documentation](https://jkorf.github.io/OKX.Net), [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net), or have a look at the examples [here](https://github.com/JKorf/OKX.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
OKX.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.HTX.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Supported functionality

### Unified API
|API|Supported|Location|
|--|--:|--|
|Trading Account Rest|✓|`restClient.UnifiedApi.Account`|
|Trading Account Websocket|✓|`socketClient.UnifiedApi.Account`|
|Order Book Trade Rest|✓|`restClient.UnifiedApi.Trading`|
|Order Book Trade Websocket|✓|`socketClient.UnifiedApi.Trading`|
|Order Book Algo Trading Rest|✓|`restClient.UnifiedApi.Trading`|
|Order Book Algo Trading Websocket|✓|`socketClient.UnifiedApi.Trading`|
|Order Book Grid Trading Rest|x||
|Order Book Grid Trading Websocket|x||
|Order Book Signal Trading Rest|x||
|Order Book Recurring Buy Rest|x||
|Order Book Recurring Buy Websocket|x||
|Order Book Copy Trading Rest|x||
|Order Book Copy Trading Websocket|x||
|Order Book Market Data Rest|✓|`restClient.UnifiedApi.ExchangeData`|
|Order Book Market Data Websocket|✓|`socketClient.UnifiedApi.ExchangeData`|
|Block Trading|X||
|Spread Trading|X||
|Public Data Rest|✓|`restClient.UnifiedApi.ExchangeData`|
|Public Data Websocket|✓|`socketClient.UnifiedApi.ExchangeData`|
|Trading Statistics|✓|`restClient.UnifiedApi.ExchangeData`|
|Funding Account Rest|✓|`restClient.UnifiedApi.Account`|
|Funding Account Websocket|✓|`socketClient.UnifiedApi.Account`|
|Sub-Account|✓|`restClient.UnifiedApi.SubAccounts`|
|Financial Product|X||
|Affiliate|X||
|Status Rest|✓|`restClient.UnifiedApi.ExchangeData`|
|Status Websocket|✓|`socketClient.UnifiedApi.ExchangeData`|

### Broker API
|API|Supported|Location|
|--|--:|--|
|Trading Account Rest|X||

## Support the project
Any support is greatly appreciated.

### Referal
If you do not yet have an account please consider using this referal link to sign up:  
[Link](https://okx.com/join/48046699)

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 2.13.1 - 20 Dec 2024
    * Added Europe environment

* Version 2.13.0 - 13 Dec 2024
    * Added restClient.UnifiedApi.Account.PresetAccountModeSwitchAsync endpoint
    * Added restClient.UnifiedApi.Account.PrecheckAccountModeSwitchAsync endpoint

* Version 2.12.1 - 08 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.4 to fix deserialization error in .net framework

* Version 2.12.0 - 03 Dec 2024
    * Added AllowAppendingClientOrderId option
    * Added Cash value to MarginMode enum
    * Updated client order id logic
    * Removed BrokerId option
    * Fix for orderbook creation via OKXOrderBookFactory

* Version 2.11.0 - 28 Nov 2024
    * Updated CryptoExchange.Net to version 8.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.4.0
    * Added GetFeesAsync Shared REST client implementations
    * Updated OKXOptions to LibraryOptions implementation
    * Updated test and analyzer package versions

* Version 2.10.0 - 25 Nov 2024
    * Added Chase Algo order support
    * Added AccountType property to restClient.UnifiedApi.Account.GetAccountConfigurationAsync response model
    * Updated restClient.UnifiedApi.Account.GetAssetsAsync response model
    * Updated restClient.UnifiedApi.Account.GetMaximumLoanAmountAsync parameters

* Version 2.9.0 - 19 Nov 2024
    * Updated CryptoExchange.Net to version 8.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.3.0
    * Added support for loading client settings from IConfiguration
    * Added DI registration method for configuring Rest and Socket options at the same time
    * Added DisplayName and ImageUrl properties to OKXExchange class
    * Updated client constructors to accept IOptions from DI
    * Removed redundant OKXSocketClient constructor
    * Fixed deserialization issue in okxRestClient.UnifiedApi.ExchangeData.GetDiscountInfoAsync

* Version 2.8.0 - 06 Nov 2024
    * Updated CryptoExchange.Net to version 8.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.2.0
    * Added AuctionEndTime property to restClient.UnifiedApi.ExchangeData.GetSymbolsAsync and socketClient.UnifiedApi.ExchangeData.SubscribeToSymbolUpdatesAsync models

* Version 2.7.0 - 28 Oct 2024
    * Updated CryptoExchange.Net to version 8.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.1.0
    * Moved FormatSymbol to OKXExchange class
    * Added support Side setting on SharedTrade model
    * Added OKXTrackerFactory for creating trackers
    * Added overload to Create method on OKXOrderBookFactory support SharedSymbol parameter
    * Added support for different order book levels in OKXSymbolOrderBook

* Version 2.6.0 - 21 Oct 2024
    * Added restClient.UnifiedApi.Account.ManualBorrowRepayAsync, SetAutoRepayAsync and GetBorrowRepayHistoryAsync endpoints
    * Added EasyConvertDustAsync, GetEasyConvertDustAssetsAsync and GetEasyConvertDustHistoryAsync endpoints
    * Added BurningFeeRate property to restClient.UnifiedApi.Account.GetAssetsAsync response model
    * Updated AccountBillSubType and AccountSubType Enum values
    * Refactored restClient.UnifiedApi.Trading.PlaceOrderAsync take profit / stop loss parameters to support the full functionality offered by the API
    * Fixed restClient.UnifiedApi.Trading.CancelMultipleOrdersAsync order canceled event processing
    * Removed restClient.UnifiedApi.Account.ConvertDustAsync deprecated endpoint

* Version 2.5.1 - 14 Oct 2024
    * Updated CryptoExchange.Net to version 8.0.3, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.3
    * Fixed TypeLoadException during initialization

* Version 2.5.0 - 08 Oct 2024
    * Added ExchangeData.GetAnnouncementsAsync and GetAnnouncementTypesAsync endpoints
    * Added asset parameter to Account.GetLeverageAsync endpoint
    * Added IsTradeBorrowMode property to Algo order response model
    * Updated OKXAccountConfiguration response model
    * Updated OKXDiscountInfo response model

* Version 2.4.0 - 27 Sep 2024
    * Updated CryptoExchange.Net to version 8.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.0
    * Added Shared client interfaces implementation for Unified Rest and Socket clients
    * Updated Sourcelink package version
    * Fixed UnifiedApi.ExchangeData.GetOpenInterestsAsync request for Swap instruments
    * Marked ISpotClient references as deprecated

* Version 2.3.1 - 11 Sep 2024
    * Added Spot fields to Balance response models
    * Added OpenInterestUsd field to ExchangeData.GetOpenInterestAsync response model
    * Added RuleType parameter and response field to Account.GetFeeRatesAsync
    * Added Attachment field to Account.GetDepositAddressAsync response model

* Version 2.3.0 - 07 Aug 2024
    * Updated CryptoExchange.Net to version 7.11.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.11.0
    * Updated XML code comments
    * Added UnifiedApi.Trading.CheckOrderAsync endpoint
    * Added PositionSide property to UnifiedApi.Account.GetPositionHistoryAsync response model
    * Updated property nullability for OKXInterestAccrued.MarginMode and OKXAlgoOrder.PositionSide properties

* Version 2.2.0 - 27 Jul 2024
    * Updated CryptoExchange.Net to version 7.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.10.0
    * Added RuleType property on UnifiedApi.ExchangeData.GetSymbolsAsync response model
    * Fixed marginMode serialization in multiple endpoints

* Version 2.1.0 - 16 Jul 2024
    * Updated CryptoExchange.Net to version 7.9.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.9.0

* Version 2.0.1 - 10 Jul 2024
    * Fixed error during parsing of error response
    * Fixed exception during CancelOrderAsync error response
    * Updated internal classes to internal access modifier

* Version 2.0.0 - 02 Jul 2024
    * Updated CryptoExchange.Net to V7.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.8.0
    * Added client side rate limiting
    * Added Trading.CancelAllAfterAsync endpoint
    * Updated json serializer from Newtonsoft.Json to System.Text.Json
    * Updated request sending to new CryptoExchange.Net implementation
    * Updated all enum conversions to use new EnumConverter
    * Updated websocket kline subscriptions models to IEnumerable
    * Updated AccountBillSubType enum values
    * Updated AccountBillType enum values
    * Updated FundingBillType enum values
    * Updated InstrumentAlias enum values
    * Updated various response models
    * Updated response checking from every endpoint to central method
    * Renamed all enums, OKX prefix removed. For example OKXOrderSide is now OrderSide
    * Renamed OrderType.MarketOrder to OrderType.Market
    * Renamed OrderType.LimitOrder to OrderType.Limit
    * Renamed Candlestick references to Kline
    * Renamed OKXPeriod to KlineInterval
    * Renamed Account.GetAccountPositionsAsync to GetPositionsAsync
    * Renamed Account.GetAccountPositionHistoryAsync to GetPositionHistoryAsync
    * Renamed Account.GetAccountPositionRiskAsync to GetPositionRiskAsync
    * Renamed Account.SetAccountPositionModeAsync to SetPositionModeAsync
    * Renamed Account.GetAccountLeverageAsync to GetLeverageAsync
    * Renamed Account.SetAccountLeverageAsync to SetLeverageAsync
    * Renamed Account.GetLightningWithdrawalsAsync to GetLightningWithdrawalAsync
    * Renamed ExchangeData.GetRubik* to GetTradeStats*
    * Cleanup unnused types

* Version 1.11.1 - 25 Jun 2024
    * Updated CryptoExchange.Net to 7.7.2, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.7.2
    * Fixed deserialization issue in OkxTicker
    * Fixed deserialization issue in SetLeverage

* Version 1.11.0 - 23 Jun 2024
    * Updated CryptoExchange.Net to version 7.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.7.0
    * Added CancellationToken optional parameter to websocket requests
    * Added dedicated connection configuration; a websocket connection can now be established before making the first request by calling `okxSocketClient.UnifiedApi.PrepareConnectionsAsync();`

* Version 1.10.1 - 17 Jun 2024
    * Fixed deserialization issue in market sell websocket order updates

* Version 1.10.0 - 11 Jun 2024
    * Updated CryptoExchange.Net to v7.6.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.9.0 - 02 Jun 2024
    * Added UnifiedApi.Account.GetAssetValuationAsync endpoint
    * Renamed BestAskSize to BestAskQuantity in OKXTicker model
    * Fixed OKXSocketOptions not using OKXApiCredentials

* Version 1.8.4 - 07 May 2024
    * Updated CryptoExchange.Net to v7.5.2, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.8.3 - 01 May 2024
    * Updated CryptoExchange.Net to v7.5.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.8.2 - 28 Apr 2024
    * Added OKXExchange static info class
    * Added OKXOrderBookFactory book creation method
    * Fixed OKXOrderBookFactory injection issue
    * Updated CryptoExchange.Net to v7.4.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.8.1 - 23 Apr 2024
    * Fixed error check on UnifiedApi.ExchangeData.GetOrderBookAsync
    * Updated CryptoExchange.Net to 7.3.3, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.8.0 - 18 Apr 2024
    * Updated CryptoExchange.Net to 7.3.1, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.7.2 - 28 Mar 2024
    * Added support for multiple symbol tickers subscriptions in single call
    * Fixed quantity being required in UnifiedApi.Trading.PlaceAlgoOrderAsync

* Version 1.7.1 - 24 Mar 2024
	* Updated CryptoExchange.Net to 7.2.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.7.0 - 16 Mar 2024
    * Updated CryptoExchange.Net to 7.1.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
	
* Version 1.6.2 - 13 Mar 2024
    * Added UnifiedApi.Account..GetAffiliateInviteeDetailsAsync endpoint
    * Fixed websocket AlgoOrder update subscriptions url
    * Fixed Symbol property not set on websocket kline updates

* Version 1.6.1 - 26 Feb 2024
    * Update OKXPosition model
    * Fixed PlaceMultipleOrdersAsync and AmendMultipleOrdersAsync quantity and price parameter serialization
    * Fixed deserialization OKXFundingRate model

* Version 1.6.0 - 25 Feb 2024
    * Updated the position model to include stop order info
    * Updated CryptoExchange.Net and implemented reworked websocket message handling. For release notes for the CryptoExchange.Net base library see: https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes
    * Fixed websocket message handling with empty data array
    * Added UnifiedApi.Account.SetAccountModeAsync
    * Fixed issue in DI registration causing http client to not be correctly injected
    * Added DataEvent wrapper to socket client subscription callbacks
    * Updated subscriptions to return lists where multiple updates are pushed in a single websocket message
    * Updated socket client PlaceOrderAsync endpoint to correctly respect client order id
    * Removed redundant OkxRestClient constructor overload
    * Updated some namespaces

* Version 1.5.0 - 04 Jan 2024
    * Added UnifiedApi.Trading.GetAlgoOrderAsync endpoint
    * Added UnifiedApi.Trading.AmendAlgoOrderAsync endpoint
    * Added UnifiedApi.Account.SetIsolatedMarginModeAsync endpoint
    * Added UnifiedApi.Account.GetTransferAsync endpoint
    * Updated savings endpoints to new url

* Version 1.4.5 - 26 Dec 2023
    * Added Account.DusConvertAsync endpoint

* Version 1.4.4 - 03 Dec 2023
    * Updated CryptoExchange.Net

* Version 1.4.3 - 28 Nov 2023
    * Updated BrokerId logic so clientOrderId isn't influenced
    * Fixed order update deserialization issue

* Version 1.4.2 - 24 Oct 2023
    * Updated CryptoExchange.Net

* Version 1.4.1 - 09 Oct 2023
    * Updated CryptoExchange.Net version
    * Fix incorrect OKXSocketClient injection
    * Added missing ISocketClient interface on IOKXSocketClient

* Version 1.4.0 - 30 Sep 2023
    * Added support for settings custom broker id
    * Added missing fields for klines/candles
    * Fix for Symbol deserialization
    * Fix for Ticker deserialization
    * Fixed algoId defined as long instead of string

* Version 1.3.2 - 20 Sep 2023
    * Added ISpotClient/CommonSpotClient implementation
    * Added missing ConfigureAwait(false) statements

* Version 1.3.1 - 17 Sep 2023
    * Added demo environment support

* Version 1.3.0 - 17 Sep 2023
    * Fixed start/end time filtering
    * Mapped BalanceAndPositionUpdates stream to new model
    * Added missing properties to position model

* Version 1.2.0 - 26 Aug 2023
    * Split socket client into different topics
    * Added websocket order management support
    * Added LiquidationWarning update stream
    * Added Withdrawal update stream
    * Added Deposit update stream

* Version 1.1.1 - 25 Aug 2023
    * Updated CryptoExchange.Net

* Version 1.1.0 - 13 Aug 2023
    * Updated parameters for endpoints to the latest version of the documentation

* Version 1.0.0 - 12 Aug 2023
    * Updated models to latest documentation
    * Added unit tests

* Version 0.0.2 - 06 Aug 2023
    * Fixed Unified socket API stream addresses
    * Renamed Unified socket API underlying parameters to intrumentFamily

* Version 0.0.1 - 04 Aug 2023
    * Initial version
