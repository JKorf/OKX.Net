namespace OKX.Net.Objects;

/// <inheritdoc />
public class OKXApiCredentials : ApiCredentials
{
    /// <summary>
    /// Passphrase
    /// </summary>
    public string PassPhrase { get; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="apiSecret"></param>
    /// <param name="apiPassPhrase"></param>
    public OKXApiCredentials(string apiKey, string apiSecret, string apiPassPhrase) : base(apiKey, apiSecret)
    {
        PassPhrase = apiPassPhrase;
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
