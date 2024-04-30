using CryptoExchange.Net.Clients;
using OKX.Net.Objects;
using OKX.Net.Objects.Options;

namespace OKX.Net;

internal class OKXAuthenticationProvider : AuthenticationProvider<OKXApiCredentials>
{
    public string ApiKey => _credentials.Key!.GetString();

    public string Passphrase => _credentials.PassPhrase.GetString();

    public OKXAuthenticationProvider(OKXApiCredentials credentials) : base(credentials)
    {
        if (credentials == null || credentials.Secret == null)
            throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");
    }

    public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            IDictionary<string, object> uriParams,
            IDictionary<string, object> bodyParams,
            Dictionary<string, string> headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat bodyFormat)
    {
        if (!(auth || ((OKXRestOptions)apiClient.ClientOptions).SignPublicRequests))
            return;

        // Set Parameters
        uri = uri.SetParameters(uriParams, arraySerialization);

        // Signature Body
        var time = GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var signtext = time + method.Method.ToUpper() + uri.PathAndQuery.Trim('?');

        if (method == HttpMethod.Post)
        {
            if (bodyParams.Count == 1 && bodyParams.Keys.First() == "<BODY>")
                signtext += JsonConvert.SerializeObject(bodyParams["<BODY>"]);
            else
                signtext += JsonConvert.SerializeObject(bodyParams);
        }

        // Compute Signature
        var signature = SignHMACSHA256(signtext, SignOutputType.Base64);

        // Headers
        headers.Add("OK-ACCESS-KEY", _credentials.Key!.GetString());
        headers.Add("OK-ACCESS-SIGN", signature);
        headers.Add("OK-ACCESS-TIMESTAMP", time);
        headers.Add("OK-ACCESS-PASSPHRASE", _credentials.PassPhrase.GetString());
    }

    public string SignWebsocket(string timestamp)
    {
        var signtext = timestamp + "GET" + "/users/self/verify";
        return SignHMACSHA256(signtext, SignOutputType.Base64);
    }
}
