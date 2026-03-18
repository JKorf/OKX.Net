using CryptoExchange.Net.Authentication;

namespace OKX.Net
{
    /// <summary>
    /// OKX API credentials
    /// </summary>
    public class OKXCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public OKXCredentials(string key, string secret, string pass) : base(key, secret, pass)
        {
        }
    }
}
