using NUnit.Framework;
using OKX.Net.Clients;
using OKX.Net.Objects.Core;
using NUnit.Framework.Legacy;
using CryptoExchange.Net.Clients;
using OKX.Net.Objects;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoExchange.Net.Objects;
using OKX.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;

namespace OKX.Net.UnitTests
{
    [TestFixture]
    public class OXKRestClientTests
    {
        [TestCase()]
        public async Task ReceivingError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            var resultObj = new OKXRestApiResponse<object>()
            {
                ErrorCode = 400001,
                Data = default!,
                ErrorMessage = "Error occurred"
            };

            TestHelpers.SetResponse((OKXRestClient)client, JsonSerializer.Serialize(resultObj));

            // act
            var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.InstrumentType.Spot);

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error!.ErrorCode == "400001");
            Assert.That(result.Error.Message == "Error occurred");
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithNoJson_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetResponse((OKXRestClient)client, "", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.InstrumentType.Spot);

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithJsonError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetResponse((OKXRestClient)client, "{ \"code\": \"400001\", \"msg\": \"Error occurred\" }", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.InstrumentType.Spot);

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error!.ErrorCode == "400001");
            Assert.That(result.Error.Message == "Error occurred");
        }


        [Test]
        public void CheckSignatureExample()
        {
            var authProvider = new OKXAuthenticationProvider(
                new ApiCredentials("XXX", "22582BD0CFF14C41EDBF1AB98506286D", "PHRASE")
                );
            var client = (RestApiClient)new OKXRestClient().UnifiedApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v5/account/balance",
                (uriParams, bodyParams, headers) =>
                {
                    return headers["OK-ACCESS-SIGN"].ToString();
                },
                "SQ8OzSqaLcC5tF3MMKwonxGUXwGfGPkM60flrI/UJjo=",
                new Dictionary<string, object>
                {
                    { "instId", "BTC-USDT" },
                    { "lever", "5" },
                    { "mgnMode", "isolated" }
                },
                time: new DateTime(2020, 12, 08, 09, 08, 57, 715, DateTimeKind.Utc));
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<OKXRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<OKXSocketClient>();
        }
        

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://www.okx.com")]
        [TestCase(TradeEnvironmentNames.Testnet, "https://www.okx.com")]
        [TestCase("", "https://www.okx.com")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "OKX:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddOKX(configuration.GetSection("OKX"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IOKXRestClient>();

            var address = client.UnifiedApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "OKX", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddOKX(configuration.GetSection("OKX"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IOKXRestClient>();

            var address = client.UnifiedApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://www.okx.com"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "OKX:Environment:Name", "test" },
                    { "OKX:Rest:Environment:Name", "live" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddOKX(configuration.GetSection("OKX"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IOKXRestClient>();

            var address = client.UnifiedApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://www.okx.com"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "ApiCredentials:Pass", "222" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Socket:ApiCredentials:Pass", "111" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddOKX(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<IOKXRestClient>();
            var socketClient = provider.GetRequiredService<IOKXSocketClient>();

            Assert.That(((BaseApiClient)restClient.UnifiedApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.UnifiedApi).OutputOriginalData, Is.False);
            Assert.That(((BaseApiClient)restClient.UnifiedApi).AuthenticationProvider.ApiKey, Is.EqualTo("123"));
            Assert.That(((BaseApiClient)socketClient.UnifiedApi).AuthenticationProvider.ApiKey, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.UnifiedApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.UnifiedApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.UnifiedApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.UnifiedApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }
    }
}
