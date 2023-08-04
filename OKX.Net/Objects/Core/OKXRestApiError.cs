namespace OKX.Net.Objects.Core;

/// <summary>
/// API error
/// </summary>
public class OKXRestApiError : Error
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    public OKXRestApiError(int? code, string message, object? data) : base(code, message, data)
    {
    }
}