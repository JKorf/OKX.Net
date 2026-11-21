namespace OKX.Net.Objects.Core;

/// <summary>The status and message for an individual operation in an OKX response.</summary>
[SerializationModel]
public abstract record OKXOperationResponse
{
    /// <summary>The result code for a successful operation.</summary>
    internal const string SuccessCode = "0";

    /// <summary>
    /// ["<c>sCode</c>"] Operation result code
    /// </summary>
    [JsonPropertyName("sCode")]
    public string Code { get; set; } = SuccessCode;

    /// <summary>
    /// ["<c>sMsg</c>"] Operation result message
    /// </summary>
    [JsonPropertyName("sMsg")]
    public string Message { get; set; } = string.Empty;
}
