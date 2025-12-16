namespace OKX.Net.Objects.Account;

/// <summary>
/// Auto repay status
/// </summary>
[SerializationModel]
public record OKXAutoRepayStatus
{
    /// <summary>
    /// Auto repay enabled or not
    /// </summary>
    [JsonPropertyName("autoRepay")]
    public bool AutoRepay { get; set; }
}

