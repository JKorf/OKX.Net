using OKX.Net.Interfaces.Clients.UnifiedApi;
using OKX.Net.Objects;
using OKX.Net.Objects.Core;
using OKX.Net.Objects.Options;

namespace OKX.Net.Clients.UnifiedApi;

internal class OKXRestClientUnifiedApi : RestApiClient, IOKXRestClientUnifiedApi
{
    #region Internal Fields
    private static TimeSyncState _timeSyncState = new("Unified Api");
    #endregion

    public IOKXRestClientUnifiedApiAccount Account { get; private set; }
    public IOKXRestClientUnifiedApiExchangeData ExchangeData { get; private set; }
    public IOKXRestClientUnifiedApiTrading Trading { get; private set; }
    public IOKXRestClientUnifiedApiSubAccounts SubAccounts { get; private set; }

    internal OKXRestClientUnifiedApi(ILogger logger, HttpClient? httpClient, OKXRestOptions options)
            : base(logger, httpClient, options.Environment.RestAddress, options, options.UnifiedOptions)
    {
        Account = new OKXRestClientUnifiedApiAccount(this);
        ExchangeData = new OKXRestClientUnifiedApiExchangeData(this);
        Trading = new OKXRestClientUnifiedApiTrading(this);
        SubAccounts = new OKXRestClientUnifiedApiSubAccounts(this);
    }

    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OKXAuthenticationProvider((OKXApiCredentials)credentials);

    internal async Task<WebCallResult<T>> ExecuteAsync<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false, HttpMethodParameterPosition? parameterPosition = null) where T : class
    {
        var result = await SendRequestAsync<T>(uri, method, ct, parameters, signed, parameterPosition, requestWeight: weight, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
        if (!result) return result.AsError<T>(result.Error!);

        return result.As(result.Data);
    }

    internal Uri GetUri(string endpoint, string param = "")
    {
        var x = endpoint.IndexOf('<');
        var y = endpoint.IndexOf('>');
        if (x > -1 && y > -1) endpoint = endpoint.Replace(endpoint.Substring(x, y - x + 1), param);

        return new Uri($"{BaseAddress.TrimEnd('/')}/{endpoint}");
    }

    /// <inheritdoc />
    protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
        => ExchangeData.GetServerTimeAsync();

    /// <inheritdoc />
    public override TimeSyncInfo? GetTimeSyncInfo()
        => new(_logger, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, _timeSyncState);

    /// <inheritdoc />
    public override TimeSpan? GetTimeOffset()
        => _timeSyncState.TimeOffset;

    /// <inheritdoc />
    protected override void WriteParamBody(IRequest request, SortedDictionary<string, object> parameters, string contentType)
    {
        if (requestBodyFormat == RequestBodyFormat.Json)
        {
            if (parameters.Count == 1 && parameters.Keys.First() == "<BODY>")
            {
                // Write the parameters as json in the body
                var stringData = JsonConvert.SerializeObject(parameters["<BODY>"]);
                request.SetContent(stringData, contentType);
            }
            else
            {
                // Write the parameters as json in the body
                var stringData = JsonConvert.SerializeObject(parameters);
                request.SetContent(stringData, contentType);
            }
        }
        else if (requestBodyFormat == RequestBodyFormat.FormData)
        {
            // Write the parameters as form data in the body
            var stringData = parameters.ToFormData();
            request.SetContent(stringData, contentType);
        }
    }

    /// <inheritdoc />
    protected override Error ParseErrorResponse(JToken error)
    {
        if (error["code"] == null || error["msg"] == null)
            return new ServerError(error.ToString());

        return new ServerError((int)error["code"]!, (string)error["msg"]!);
    }

}
