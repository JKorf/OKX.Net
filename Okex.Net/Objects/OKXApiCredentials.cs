namespace OKX.Net.Objects;

/// <inheritdoc />
public class OKXApiCredentials : ApiCredentials
{
    /// <summary>
    /// Passphrase
    /// </summary>
    public SecureString PassPhrase { get; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="apiSecret"></param>
    /// <param name="apiPassPhrase"></param>
    public OKXApiCredentials(string apiKey, string apiSecret, string apiPassPhrase) : base(apiKey, apiSecret)
    {
        PassPhrase = apiPassPhrase.ToSecureString();
    }

    /// <summary>
    /// Copy
    /// </summary>
    /// <returns></returns>
    public override ApiCredentials Copy()
    {
        return new OKXApiCredentials(Key!.GetString(), Secret!.GetString(), PassPhrase!.GetString());
    }
}
