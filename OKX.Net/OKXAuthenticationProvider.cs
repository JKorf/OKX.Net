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

    public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
    {
        uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
        bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
        headers = new Dictionary<string, string>();

        if (!(auth || ((OKXRestOptions)apiClient.ClientOptions).SignPublicRequests))
            return;

        // Set Parameters
        uri = uri.SetParameters(uriParameters, arraySerialization);

        // Signature Body
        var time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var signtext = time + method.Method.ToUpper() + uri.PathAndQuery.Trim('?');

        if (method == HttpMethod.Post)
        {
            if (bodyParameters.Count == 1 && bodyParameters.Keys.First() == "<BODY>")
                signtext += JsonConvert.SerializeObject(bodyParameters["<BODY>"]);
            else
                signtext += JsonConvert.SerializeObject(bodyParameters);
        }

        // Compute Signature
        var signature = SignHMACSHA256(signtext, SignOutputType.Base64);

        // Headers
        headers.Add("OK-ACCESS-KEY", _credentials.Key!.GetString());
        headers.Add("OK-ACCESS-SIGN", signature);
        headers.Add("OK-ACCESS-TIMESTAMP", time);
        headers.Add("OK-ACCESS-PASSPHRASE", _credentials.PassPhrase.GetString());

        // Demo Trading Flag
        //if (baseClient.Options.DemoTradingService)
        //    headers.Add("x-simulated-trading", "1");
    }

    public string SignWebsocket(string timestamp)
    {
        var signtext = timestamp + "GET" + "/users/self/verify";
        return SignHMACSHA256(signtext, SignOutputType.Base64);
    }
}
