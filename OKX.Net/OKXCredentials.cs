using CryptoExchange.Net.Authentication;

namespace OKX.Net
{
    /// <summary>
    /// OKX credentials
    /// </summary>
    public class OKXCredentials : ApiCredentials
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        /// <param name="passphrase">Passphrase</param>
        public OKXCredentials(string apiKey, string secret, string passphrase) : this(new HMACCredential(apiKey, secret, passphrase)) { }
       
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public OKXCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new OKXCredentials(Hmac!);
    }
}
