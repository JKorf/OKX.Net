using System.Net;
using System.Text;
using CryptoExchange.Net.Interfaces;
using OKX.Net.Clients;
using OKX.Net.Interfaces.Clients;
using OKX.Net.Objects.Options;
using Moq;

namespace OKX.Net.UnitTests
{
    public class TestHelpers
    {
        public static IOKXRestClient CreateClient(Action<OKXRestOptions> options = null)
        {
            IOKXRestClient client;
            client = options != null ? new OKXRestClient(options) : new OKXRestClient();
            client.UnifiedApi.RequestFactory = Mock.Of<IRequestFactory>();
            return client;
        }

        public static void SetResponse(OKXRestClient client, string responseData, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();
            response.Setup(c => c.StatusCode).Returns(statusCode);
            response.Setup(c => c.IsSuccessStatusCode).Returns(statusCode == HttpStatusCode.OK);
            response.Setup(c => c.GetResponseStreamAsync()).Returns(Task.FromResult((Stream)responseStream));

            var request = new Mock<IRequest>();
            request.Setup(c => c.Uri).Returns(new Uri("http://www.test.com"));
            request.Setup(c => c.GetResponseAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(response.Object));
            request.Setup(c => c.GetHeaders()).Returns([]);

            var factory = Mock.Get(client.UnifiedApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<Version>(), It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);

        }
    }
}
