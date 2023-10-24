# ![Icon!](https://github.com/JKorf/OKX.Net/blob/358d31f58d8ee51fc234bff1940878a8d0ce5676/Okex.Net/Icon/icon.png "OKX.Net") OKX.Net

[![.NET](https://github.com/JKorf/OKX.Net/actions/workflows/dotnet.yml/badge.svg)](https://github.com/JKorf/OKX.Net/actions/workflows/dotnet.yml) [![Nuget version](https://img.shields.io/nuget/v/JK.okx.net.svg)](https://www.nuget.org/packages/JK.OKX.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/JK.okx.Net.svg)](https://www.nuget.org/packages/JK.OKX.Net)

A .Net wrapper for the OKX API as described on [OKX](https://www.okx.com/docs-v5/en/), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/OKX.Net/issues)**

[Documentation](https://jkorf.github.io/OKX.Net/)

## Installation
`dotnet add package JK.OKX.Net`

## Support the project
I develop and maintain this package on my own for free in my spare time, any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1qz0jv0my7fc60rxeupr23e75x95qmlq6489n8gh  
**Eth**:  0x8E21C4d955975cB645589745ac0c46ECA8FAE504   

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Discord
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Release notes
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
