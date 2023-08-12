using Newtonsoft.Json;
using NUnit.Framework;
using System.Reflection;
using CryptoExchange.Net.Objects;
using System.Diagnostics;
using CryptoExchange.Net.Sockets;
using OKX.Net.Clients;
using OKX.Net.Objects.Core;
using OKX.Net.Interfaces.Clients.UnifiedApi;

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
            var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.OKXInstrumentType.Spot);

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error!.Code == 400001);
            Assert.IsTrue(result.Error.Message == "Error occured");
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithNoJson_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetResponse((OKXRestClient)client, "", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.OKXInstrumentType.Spot);

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
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
            var result = await client.UnifiedApi.ExchangeData.GetTickersAsync(Enums.OKXInstrumentType.Spot);

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error!.Code == 400001);
            Assert.IsTrue(result.Error.Message == "Error occured");
        }

        [Test]
        public void CheckRestInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(OKXRestClient));
            var ignore = new string[] { "IOKXClientUnifiedApi" };
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IOKXClientUnifiedApi") && !ignore.Contains(t.Name));

            foreach (var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod, $"{method.Name} not found in interface {clientInterface.Name}");
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }

        [Test]
        public void CheckSocketInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(IOKXSocketClientUnifiedApi));
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IOKXSocketClientUnifiedApi"));

            foreach (var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task<CallResult<UpdateSubscription>>))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod);
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }
    }
}
