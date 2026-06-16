using CryptoExchange.Net.Objects;
using OKX.Net.Clients;
using OKX.Net.Enums;

var client = new OKXRestClient();

var ticker = await client.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT");
if (!ticker.Success)
{
    LogError("ticker", ticker);
    return;
}

Console.WriteLine($"Last price: {ticker.Data.LastPrice}");

var klines = await ExecuteWithTransientRetryAsync(() =>
    client.UnifiedApi.ExchangeData.GetKlinesAsync("ETH-USDT", KlineInterval.OneMinute));

if (!klines.Success)
{
    LogError("klines", klines);
    return;
}

Console.WriteLine($"Received {klines.Data.Length} candles");

static void LogError<T>(string operation, HttpResult<T> result)
{
    Console.WriteLine($"{operation} failed");
    Console.WriteLine($"Code: {result.Error?.Code}");
    Console.WriteLine($"Message: {result.Error?.Message}");
}

static async Task<HttpResult<T>> ExecuteWithTransientRetryAsync<T>(
    Func<Task<HttpResult<T>>> action)
{
    var first = await action();
    if (first.Success || first.Error?.IsTransient != true)
        return first;

    await Task.Delay(TimeSpan.FromSeconds(1));
    return await action();
}
