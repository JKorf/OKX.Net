using CryptoExchange.Net.SharedApis;
using OKX.Net.Clients;

// SharedApis are exchange-agnostic wrappers from CryptoExchange.Net.
// OKX exposes them through UnifiedApi.SharedClient.
var okxShared = new OKXRestClient().UnifiedApi.SharedClient;

var spotSymbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
var ticker = await okxShared.GetSpotTickerAsync(new GetTickerRequest(spotSymbol));
if (!ticker.Success)
{
    Console.WriteLine($"Shared ticker failed: {ticker.Error}");
    return;
}

Console.WriteLine($"OKX ETH/USDT: {ticker.Data.LastPrice}");

var orderBook = await okxShared.GetOrderBookAsync(new GetOrderBookRequest(spotSymbol));
if (!orderBook.Success)
{
    Console.WriteLine($"Shared order book failed: {orderBook.Error}");
    return;
}

Console.WriteLine($"Best bid: {orderBook.Data.Bids.FirstOrDefault()?.Price}");

// For another CryptoExchange.Net exchange, replace only the concrete client:
// var binanceShared = new BinanceRestClient().SpotApi.SharedClient;
// var bybitShared = new BybitRestClient().V5Api.SharedClient;
// The request/response pattern remains the same.
