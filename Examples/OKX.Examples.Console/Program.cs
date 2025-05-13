using OKX.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using OKX.Net.Objects.Options;
using Microsoft.Extensions.Options;

// REST
var restClient = new OKXRestClient();
var ticker = await restClient.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT");
Console.WriteLine($"Rest client ticker price for ETHUSDT: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
// Optional, manually add logging
var logFactory = new LoggerFactory();
logFactory.AddProvider(new TraceLoggerProvider());

var socketClient = new OKXSocketClient(Options.Create(new OKXSocketOptions { }), logFactory);
var subscription = await socketClient.UnifiedApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETH-USDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.LastPrice}");
});

Console.ReadLine();
