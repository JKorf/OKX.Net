using OKX.Net;
using OKX.Net.Clients;
using OKX.Net.Enums;

var socketClient = new OKXSocketClient();

var tickerSubscription = await socketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync(
    "ETH-USDT",
    update =>
    {
        Console.WriteLine($"Ticker {update.Data.Symbol}: {update.Data.LastPrice}");
    });

if (!tickerSubscription.Success)
{
    Console.WriteLine($"Ticker subscription failed: {tickerSubscription.Error}");
    return;
}

var klineSubscription = await socketClient.UnifiedApi.ExchangeData.SubscribeToKlineUpdatesAsync(
    "ETH-USDT",
    KlineInterval.OneMinute,
    update =>
    {
        Console.WriteLine($"Kline close: {update.Data.ClosePrice}");
    });

if (!klineSubscription.Success)
{
    Console.WriteLine($"Kline subscription failed: {klineSubscription.Error}");
    await socketClient.UnsubscribeAsync(tickerSubscription.Data);
    return;
}

// Private streams require the OKX key, secret, and passphrase.
var privateSocketClient = new OKXSocketClient(options =>
{
    options.ApiCredentials = new OKXCredentials("API_KEY", "API_SECRET", "PASSPHRASE");
});

var orderSubscription = await privateSocketClient.UnifiedApi.Trading.SubscribeToOrderUpdatesAsync(
    InstrumentType.Spot,
    symbol: "ETH-USDT",
    instrumentFamily: null,
    onData: update =>
    {
        Console.WriteLine($"Order {update.Data.OrderId}: {update.Data.OrderState}");
    });

if (!orderSubscription.Success)
{
    Console.WriteLine($"Order subscription failed: {orderSubscription.Error}");
}

await Task.Delay(TimeSpan.FromSeconds(5));

// Keep UpdateSubscription objects and unsubscribe during shutdown.
await socketClient.UnsubscribeAsync(tickerSubscription.Data);
await socketClient.UnsubscribeAsync(klineSubscription.Data);

if (orderSubscription.Success)
    await privateSocketClient.UnsubscribeAsync(orderSubscription.Data);
