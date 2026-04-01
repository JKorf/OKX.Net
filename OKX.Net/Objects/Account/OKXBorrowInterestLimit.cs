namespace OKX.Net.Objects.Account;

/// <summary>
/// Borrow interest and limit
/// </summary>
[SerializationModel]
public record OKXBorrowInterestLimit
{
    /// <summary>
    /// ["<c>debt</c>"] Debt
    /// </summary>
    [JsonPropertyName("debt")]
    public decimal Debt { get; set; }

    /// <summary>
    /// ["<c>interest</c>"] Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// ["<c>nextDiscountTime</c>"] Next discount time
    /// </summary>
    [JsonPropertyName("nextDiscountTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? NextDiscountTime { get; set; }

    /// <summary>
    /// ["<c>nextInterestTime</c>"] Next interest time
    /// </summary>
    [JsonPropertyName("nextInterestTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? NextInterestTime { get; set; }

    /// <summary>
    /// ["<c>records</c>"] Records
    /// </summary>
    [JsonPropertyName("records")]
    public OKXBorrowInterestLimitRecord[] Records { get; set; } = Array.Empty<OKXBorrowInterestLimitRecord>();
}

/// <summary>
/// Borrow interest limit record
/// </summary>
[SerializationModel]
public record OKXBorrowInterestLimitRecord
{
    /// <summary>
    /// ["<c>ccy</c>"] Currency
    /// </summary>
    [JsonPropertyName("ccy")]
    public string Asset { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>interest</c>"] Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// ["<c>availLoan</c>"] Available loan
    /// </summary>
    [JsonPropertyName("availLoan")]
    public decimal AvailableLoan { get; set; }

    /// <summary>
    /// ["<c>rate</c>"] Rate
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// ["<c>usedLoan</c>"] Used loan
    /// </summary>
    [JsonPropertyName("usedLoan")]
    public decimal UsedLoan { get; set; }

    /// <summary>
    /// ["<c>posLoan</c>"] Position loan
    /// </summary>
    [JsonPropertyName("posLoan")]
    public decimal PositionLoan { get; set; }
}