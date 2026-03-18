using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using OKX.Net.Objects.Options;
using OKX.Net.Objects.Sockets.Models;
using OKX.Net.Objects.Sockets.Queries;

namespace OKX.Net;

internal class OKXAuthenticationProvider : AuthenticationProvider<OKXCredentials, OKXCredentials>
{
    private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(OKXExchange._serializerContext));

    public OKXAuthenticationProvider(OKXCredentials credentials) : base(credentials, credentials)
    {
        if (string.IsNullOrEmpty(Credential.Pass))
            throw new ArgumentNullException(nameof(Credential.Pass), "Passphrase is required for OKX authentication");
    }

    public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
    {
        if (!request.Authenticated && !((OKXRestOptions)apiClient.ClientOptions).SignPublicRequests)
            return;

        var time = GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var queryString = request.GetQueryString(true);
        if (!string.IsNullOrEmpty(queryString))
            queryString = $"?{queryString}";
        var body = request.ParameterPosition == HttpMethodParameterPosition.InBody ? request.BodyParameters?.Count > 0 ? GetSerializedBody(_serializer, request.BodyParameters) : "{}" : string.Empty;
        var signStr = time + request.Method + request.Path + queryString + body;

        var signature = SignHMACSHA256(signStr, SignOutputType.Base64);
        request.Headers ??= new Dictionary<string, string>();
        request.Headers.Add("OK-ACCESS-KEY", Credential.Key);
        request.Headers.Add("OK-ACCESS-SIGN", signature);
        request.Headers.Add("OK-ACCESS-TIMESTAMP", time);
        request.Headers.Add("OK-ACCESS-PASSPHRASE", Credential.Pass!);

        request.SetBodyContent(body);
        request.SetQueryString(queryString);
    }

    public override Query? GetAuthenticationQuery(SocketApiClient apiClient, SocketConnection connection, Dictionary<string, object?>? context = null)
    {
        var timestamp = (GetMillisecondTimestampLong(apiClient) / 1000).ToString(CultureInfo.InvariantCulture);
        var signText = timestamp + "GET" + "/users/self/verify";
        var signature = SignHMACSHA256(signText, SignOutputType.Base64);
        var request = new OKXSocketAuthRequest
        {
            Op = "login",
            Args =
            [
                new OKXSocketAuthArgs
                {
                    ApiKey = Credential.Key,
                    Passphrase = Credential.Pass!,
                    Timestamp = timestamp,
                    Sign = signature,
                }
            ]
        };
        return new OKXQuery(apiClient, request, false);
    }
}
