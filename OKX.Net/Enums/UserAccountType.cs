using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace OKX.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UserAccountType>))]
    public enum UserAccountType
    {
        /// <summary>
        /// Main account
        /// </summary>
        [Map("0")]
        Main,
        /// <summary>
        /// Standard sub account
        /// </summary>
        [Map("1")]
        StandardSubAccount,
        /// <summary>
        /// Managed trading sub account
        /// </summary>
        [Map("2")]
        ManagedSubAccount,
        /// <summary>
        /// Custody trading sub-account - Copper
        /// </summary>
        [Map("5")]
        CustodySubAccountCopper,
        /// <summary>
        /// Managed trading sub-account - Copper
        /// </summary>
        [Map("9")]
        ManagedSubAccountCopper,
        /// <summary>
        /// Custody trading sub-account - Komainu
        /// </summary>
        [Map("12")]
        CustodySubAccountKomainu
    }
}
