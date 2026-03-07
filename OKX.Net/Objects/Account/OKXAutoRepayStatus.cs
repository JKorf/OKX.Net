namespace OKX.Net.Objects.Account;

/// <summary>
/// Auto repay status
/// </summary>
[SerializationModel]
public record OKXAutoRepayStatus
{
    /// <summary>
    /// ["<c>autoRepay</c>"] Auto repay enabled or not
    /// </summary>
    [JsonPropertyName("autoRepay")]
    public bool AutoRepay { get; set; }
}

