using CryptoExchange.Net.Clients;
using OKX.Net.Objects;
using OKX.Net.Objects.Options;

namespace OKX.Net;

internal class OKXAuthenticationProvider : AuthenticationProvider<ApiCredentials>
{
    private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(OKXExchange._serializerContext));

    public OKXAuthenticationProvider(ApiCredentials credentials) : base(credentials)
    {
        if (string.IsNullOrEmpty(credentials.Pass))
            throw new ArgumentNullException(nameof(ApiCredentials.Pass), "Passphrase is required for OKX authentication");
    }

    public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
    {
        if (!request.Authenticated && !((OKXRestOptions)apiClient.ClientOptions).SignPublicRequests)
            return;

        var time = GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var queryString = request.GetQueryString(true);
        if (!string.IsNullOrEmpty(queryString))
            queryString = $"?{queryString}";
        var body = request.ParameterPosition == HttpMethodParameterPosition.InBody ? request.BodyParameters.Any() ? GetSerializedBody(_serializer, request.BodyParameters) : "{}" : string.Empty;
        var signStr = time + request.Method + request.Path + queryString + body;

        var signature = SignHMACSHA256(signStr, SignOutputType.Base64);
        request.Headers.Add("OK-ACCESS-KEY", _credentials.Key);
        request.Headers.Add("OK-ACCESS-SIGN", signature);
        request.Headers.Add("OK-ACCESS-TIMESTAMP", time);
        request.Headers.Add("OK-ACCESS-PASSPHRASE", _credentials.Pass!);

        request.SetBodyContent(body);
        request.SetQueryString(queryString);
    }

    public string SignWebsocket(string timestamp)
    {
        var signtext = timestamp + "GET" + "/users/self/verify";
        return SignHMACSHA256(signtext, SignOutputType.Base64);
    }
}
