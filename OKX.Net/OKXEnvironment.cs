using OKX.Net.Objects;

namespace OKX.Net
{
    /// <summary>
    /// OKX environments
    /// </summary>
    public class OKXEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Rest API base address
        /// </summary>
        public string RestAddress { get; }
        /// <summary>
        /// Socket API base address
        /// </summary>
        public string SocketAddress { get; }

        internal OKXEnvironment(string name,
            string restAddress, string socketAddress) : base(name)
        {
            RestAddress = restAddress;
            SocketAddress = socketAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static OKXEnvironment Live { get; }
            = new OKXEnvironment(TradeEnvironmentNames.Live,
                                   OKXApiAddresses.Default.UnifiedRestAddress,
                                   OKXApiAddresses.Default.UnifiedSocketAddress);

        /// <summary>
        /// Live environment
        /// </summary>
        public static OKXEnvironment Demo { get; }
            = new OKXEnvironment(TradeEnvironmentNames.Testnet,
                                   OKXApiAddresses.Demo.UnifiedRestAddress,
                                   OKXApiAddresses.Demo.UnifiedSocketAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="restAddress"></param>
        /// <param name="socketAddress"></param>
        /// <returns></returns>
        public static OKXEnvironment CreateCustom(string name, string restAddress, string socketAddress)
            => new OKXEnvironment(name, restAddress, socketAddress);
    }
}
