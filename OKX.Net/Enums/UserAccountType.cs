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
        /// ["<c>0</c>"] Main account
        /// </summary>
        [Map("0")]
        Main,
        /// <summary>
        /// ["<c>1</c>"] Standard sub account
        /// </summary>
        [Map("1")]
        StandardSubAccount,
        /// <summary>
        /// ["<c>2</c>"] Managed trading sub account
        /// </summary>
        [Map("2")]
        ManagedSubAccount,
        /// <summary>
        /// ["<c>5</c>"] Custody trading sub-account - Copper
        /// </summary>
        [Map("5")]
        CustodySubAccountCopper,
        /// <summary>
        /// ["<c>9</c>"] Managed trading sub-account - Copper
        /// </summary>
        [Map("9")]
        ManagedSubAccountCopper,
        /// <summary>
        /// ["<c>12</c>"] Custody trading sub-account - Komainu
        /// </summary>
        [Map("12")]
        CustodySubAccountKomainu
    }
}
