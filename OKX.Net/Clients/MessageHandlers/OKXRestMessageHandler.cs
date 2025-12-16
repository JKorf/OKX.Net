using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using CryptoExchange.Net.Objects.Errors;
using OKX.Net.Objects.Core;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OKX.Net.Clients.MessageHandlers
{
    internal class OKXRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(OKXExchange._serializerContext);

        public OKXRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override Error? CheckDeserializedResponse<T>(HttpResponseHeaders responseHeaders, T result)
        {
            if (result is not OKXRestApiResponse okxResponse)
                return null;

            if (okxResponse.ErrorCode < 50000)
                return null;

            return new ServerError(okxResponse.ErrorCode, _errorMapping.GetErrorInfo(okxResponse.ErrorCode.ToString(), okxResponse.ErrorMessage));
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            if (httpStatusCode == 401)
                return new ServerError(new ErrorInfo(ErrorType.Unauthorized, "Unauthorized"));

            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            var code = document!.RootElement.TryGetProperty("code", out var codeProp) ? codeProp.GetString() : null;
            var msg = document!.RootElement.TryGetProperty("msg", out var msgProp) ? msgProp.GetString() : null;
            if (code == null)
                return new ServerError(ErrorInfo.Unknown);

            return new ServerError(code, _errorMapping.GetErrorInfo(code, msg));
        }
    }
}
