namespace OKX.Net.Objects;

/// <inheritdoc />
public class OKXApiCredentials : ApiCredentials
{
    /// <summary>
    /// Passphrase
    /// </summary>
    public string PassPhrase { get; set; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="key">The API key</param>
    /// <param name="secret">The API secret</param>
    /// <param name="passPhrase">The API passphrase</param>
    public OKXApiCredentials(string key, string secret, string passPhrase) : base(key, secret)
    {
        PassPhrase = passPhrase;
    }

    /// <summary>
    /// Copy
    /// </summary>
    /// <returns></returns>
    public override ApiCredentials Copy()
    {
        return new OKXApiCredentials(Key, Secret, PassPhrase);
    }
}
