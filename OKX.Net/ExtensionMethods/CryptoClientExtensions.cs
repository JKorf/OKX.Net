using OKX.Net.Clients;
using OKX.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the OKX REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IOKXRestClient OKX(this ICryptoRestClient baseClient) => baseClient.TryGet<IOKXRestClient>(() => new OKXRestClient());

        /// <summary>
        /// Get the OKX Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IOKXSocketClient OKX(this ICryptoSocketClient baseClient) => baseClient.TryGet<IOKXSocketClient>(() => new OKXSocketClient());
    }
}
