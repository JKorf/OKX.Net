using OKX.Net.Clients;
using OKX.Net.Enums;
using OKX.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using OKX.Net.Objects.Trade;

namespace OKX.Net.UnitTests
{
    [TestFixture]
    public class SocketRequestTests
    {
        private OKXSocketClient CreateClient()
        {
            var fact = new LoggerFactory();
            fact.AddProvider(new TraceLoggerProvider());
            var client = new OKXSocketClient(Options.Create(new OKXSocketOptions
            {
                OutputOriginalData = true,
                RequestTimeout = TimeSpan.FromSeconds(5),
                ApiCredentials = new ApiCredentials("123", "123", "123")
            }), fact);
            return client;
        }

        [Test]
        public async Task ValidateExchangeApiCalls()
        {
            var tester = new SocketRequestValidator<OKXSocketClient>("Socket/UnifiedApi");

            await tester.ValidateAsync(CreateClient(), client => client.UnifiedApi.Trading.PlaceOrderAsync("ETH-USDT", OrderSide.Buy, OrderType.Limit, TradeMode.Cross, 0.01m), "PlaceOrder", nestedJsonProperty: "data", ignoreProperties: [ ], useSingleArrayItem: true);
            await tester.ValidateAsync(CreateClient(), client => client.UnifiedApi.Trading.PlaceMultipleOrdersAsync([new OKXOrderPlaceRequest()]), "PlaceMultipleOrders", nestedJsonProperty: "data", skipResponseValidation: true);
            await tester.ValidateAsync(CreateClient(), client => client.UnifiedApi.Trading.CancelOrderAsync("ETH-USDT", "123"), "CancelOrder", nestedJsonProperty: "data", ignoreProperties: [ ], useSingleArrayItem: true);
            await tester.ValidateAsync(CreateClient(), client => client.UnifiedApi.Trading.CancelMultipleOrdersAsync([new OKXOrderCancelRequest()]), "CancelMultipleOrders", nestedJsonProperty: "data", ignoreProperties: [ ]);
            await tester.ValidateAsync(CreateClient(), client => client.UnifiedApi.Trading.AmendOrderAsync("ETH-USDT", 123), "AmendOrder", nestedJsonProperty: "data", ignoreProperties: [ ], useSingleArrayItem: true);
            await tester.ValidateAsync(CreateClient(), client => client.UnifiedApi.Trading.AmendMultipleOrdersAsync([new OKXOrderAmendRequest()]), "AmendMultipleOrders", nestedJsonProperty: "data", ignoreProperties: [ ]);
        }
    }
}
