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
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public OKXEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the OKX environment by name
        /// </summary>
        public static OKXEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             TradeEnvironmentNames.Testnet => Demo,
             "Europe" => Europe,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name, Demo.Name, Europe.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static OKXEnvironment Live { get; }
            = new OKXEnvironment(TradeEnvironmentNames.Live,
                                   OKXApiAddresses.Default.UnifiedRestAddress,
                                   OKXApiAddresses.Default.UnifiedSocketAddress);

        /// <summary>
        /// Live environment for Europe customers
        /// </summary>
        public static OKXEnvironment Europe { get; }
            = new OKXEnvironment("Europe",
                                   OKXApiAddresses.Europe.UnifiedRestAddress,
                                   OKXApiAddresses.Europe.UnifiedSocketAddress);
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
