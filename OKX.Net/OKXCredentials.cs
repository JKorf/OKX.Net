using CryptoExchange.Net.Authentication;

namespace OKX.Net
{
    /// <summary>
    /// OKX API credentials
    /// </summary>
    public class OKXCredentials : HMACPassCredential
    {
        /// <summary>
        /// Create new credentials
        /// </summary>
        public OKXCredentials() { }

        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public OKXCredentials(string key, string secret, string pass) : base(key, secret, pass)
        {
        }

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="credential">HMAC credentials</param>
        public OKXCredentials(HMACPassCredential credential) : base(credential.Key, credential.Secret, credential.Pass)
        {
        }

        /// <summary>
        /// Specify the HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public OKXCredentials WithHMAC(string key, string secret, string pass)
        {
            if (!string.IsNullOrEmpty(Key)) throw new InvalidOperationException("Credentials already set");

            Key = key;
            Secret = secret;
            Pass = pass;
            return this;
        }
    }
}
