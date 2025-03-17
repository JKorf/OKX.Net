using CryptoExchange.Net.Clients;
using OKX.Net.Objects;
using OKX.Net.Objects.Options;

namespace OKX.Net;

internal class OKXAuthenticationProvider : AuthenticationProvider<ApiCredentials>
{
    private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(OKXExchange.SerializerContext));

    public OKXAuthenticationProvider(ApiCredentials credentials) : base(credentials)
    {
        if (string.IsNullOrEmpty(credentials.Pass))
            throw new ArgumentNullException(nameof(ApiCredentials.Pass), "Passphrase is required for OKX authentication");
    }

    public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
    {
        if (!(auth || ((OKXRestOptions)apiClient.ClientOptions).SignPublicRequests))
            return;

        // Set Parameters
        if (uriParameters != null)
            uri = uri.SetParameters(uriParameters, arraySerialization);

        // Signature Body
        var time = GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var signtext = time + method.Method.ToUpper() + uri.PathAndQuery.Trim('?');

        if (method == HttpMethod.Post)
        {
            if (bodyParameters?.Any() == true)
                signtext += GetSerializedBody(_serializer, bodyParameters);
            else
                signtext += "{}";
        }

        // Compute Signature
        var signature = SignHMACSHA256(signtext, SignOutputType.Base64);

        // Headers
        headers ??= new Dictionary<string, string>();
        headers.Add("OK-ACCESS-KEY", _credentials.Key);
        headers.Add("OK-ACCESS-SIGN", signature);
        headers.Add("OK-ACCESS-TIMESTAMP", time);
        headers.Add("OK-ACCESS-PASSPHRASE", _credentials.Pass);
    }

    public string SignWebsocket(string timestamp)
    {
        var signtext = timestamp + "GET" + "/users/self/verify";
        return SignHMACSHA256(signtext, SignOutputType.Base64);
    }
}
