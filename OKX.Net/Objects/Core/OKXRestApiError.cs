namespace OKX.Net.Objects.Core;

/// <summary>
/// API error
/// </summary>
public class OKXRestApiError : Error
{
    /// <summary>
    /// ctor
    /// </summary>
    public OKXRestApiError(int? code, string message, Exception? exception) : base(code, message, exception)
    {
    }
}