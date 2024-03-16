# ![Icon!](https://github.com/JKorf/OKX.Net/blob/358d31f58d8ee51fc234bff1940878a8d0ce5676/Okex.Net/Icon/icon.png "OKX.Net") OKX.Net

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/OKX.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/OKX.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/OKX.Net?style=for-the-badge)

OKX.Net is strongly typed client library for accessing the [OKX REST and Websocket API](https://www.okx.com/docs-v5/en/). All data is mapped to readable models and enum values. Additional features include an implementation for maintaining a client side order book, easy integration with other exchange client libraries and more.

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

## Get the library
[![Nuget version](https://img.shields.io/nuget/v/JK.okx.net.svg?style=for-the-badge)](https://www.nuget.org/packages/JK.OKX.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/JK.okx.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/JK.OKX.Net)

	dotnet add package JK.OKX.Net

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

For information on the clients, dependency injection, response processing and more see the [documentation](https://jkorf.github.io/CryptoExchange.Net), or have a look at the examples  [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
Mexc.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_common).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|Huobi/HTX|[JKorf/Huobi.Net](https://github.com/JKorf/Huobi.Net)|[![Nuget version](https://img.shields.io/nuget/v/Huobi.net.svg?style=flat-square)](https://www.nuget.org/packages/Huobi.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|

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
I develop and maintain this package on my own for free in my spare time, any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1qz0jv0my7fc60rxeupr23e75x95qmlq6489n8gh  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
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
