using OKX.Net;
using OKX.Net.Clients;
using OKX.Net.Enums;

// OKX.Net exposes the OKX v5 API through a single UnifiedApi root.
// Public market data does not require API credentials.
var publicClient = new OKXRestClient();

var ticker = await publicClient.UnifiedApi.ExchangeData.GetTickerAsync("ETH-USDT");
if (!ticker.Success)
{
    Console.WriteLine($"Ticker request failed: {ticker.Error}");
    return;
}

Console.WriteLine($"ETH-USDT last price: {ticker.Data.LastPrice}");

// Private account and trading endpoints require key, secret, and passphrase.
// Reuse clients in production, or register them through services.AddOKX(...).
var tradingClient = new OKXRestClient(options =>
{
    options.ApiCredentials = new OKXCredentials("API_KEY", "API_SECRET", "PASSPHRASE");
});

var balance = await tradingClient.UnifiedApi.Account.GetAccountBalanceAsync("USDT");
if (!balance.Success)
{
    Console.WriteLine($"Balance request failed: {balance.Error}");
    return;
}

foreach (var asset in balance.Data.Details)
    Console.WriteLine($"{asset.Asset}: available {asset.AvailableBalance}, total equity {asset.Equity}");

// Spot limit order. For OKX the same PlaceOrderAsync method is used for Spot,
// Margin, Futures, Swap, and Options. The symbol and tradeMode decide the shape.
var order = await tradingClient.UnifiedApi.Trading.PlaceOrderAsync(
    symbol: "ETH-USDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.01m,
    price: 2000m,
    tradeMode: TradeMode.Cash);

if (!order.Success)
{
    Console.WriteLine($"Order placement failed: {order.Error}");
    return;
}

Console.WriteLine($"Placed order id: {order.Data.OrderId}");

var orderDetails = await tradingClient.UnifiedApi.Trading.GetOrderDetailsAsync(
    symbol: "ETH-USDT",
    orderId: order.Data.OrderId);

if (!orderDetails.Success)
{
    Console.WriteLine($"Order lookup failed: {orderDetails.Error}");
    return;
}

Console.WriteLine($"Order status: {orderDetails.Data.OrderState}");
