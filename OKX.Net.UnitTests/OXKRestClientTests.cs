using Newtonsoft.Json;
using NUnit.Framework;
using System.Reflection;
using CryptoExchange.Net.Objects;
using System.Diagnostics;
using CryptoExchange.Net.Sockets;
using OKX.Net.Clients;
using OKX.Net.Objects.Core;
using OKX.Net.Interfaces.Clients.UnifiedApi;
using CryptoExchange.Net.Objects.Sockets;
using NUnit.Framework.Legacy;
using CryptoExchange.Net.Clients;
using OKX.Net.Objects;

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
                ErrorMessage = "Error occured"
            };

            TestHelpers.SetResponse((OKXRestClient)client, JsonConvert.SerializeObject(resultObj));

            // act
            var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.InstrumentType.Spot);

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error!.Code == 400001);
            Assert.That(result.Error.Message == "Error occured");
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
            var resultObj = new OKXRestApiResponse<object>()
            {
                ErrorCode = 400001,
                Data = default!,
                ErrorMessage = "Error occured"
            };

            TestHelpers.SetResponse((OKXRestClient)client, JsonConvert.SerializeObject(resultObj), System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.InstrumentType.Spot);

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error!.Code == 400001);
            Assert.That(result.Error.Message == "Error occured");
        }


        [Test]
        public void CheckSignatureExample()
        {
            var authProvider = new OKXAuthenticationProvider(
                new OKXApiCredentials("XXX", "22582BD0CFF14C41EDBF1AB98506286D", "PHRASE")
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
    }
}
