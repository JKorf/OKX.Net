using OKX.Net.Enums;

namespace OKX.Net.Objects.CopyTrading;

/// <summary>
/// Copy trading account details
/// </summary>
[SerializationModel]
public class OKXCopyTradingAccountDetails
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonPropertyName("instType")]
    public InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Role type
    /// </summary>
    [JsonPropertyName("roleType")]
    public CopyTradingRole RoleType { get; set; }

    /// <summary>
    /// Profit sharing ratio.
    /// Only applicable to lead trader, or it will be "". 0.1 represents 10%
    /// </summary>
    [JsonPropertyName("profitSharingRatio")]
    public decimal? ProfitSharingRatio { get; set; }

    /// <summary>
    /// Maximum number of copy traders
    /// </summary>
    [JsonPropertyName("maxCopyTraderNum")]
    public int? MaximumCopyTraderNumber { get; set; }

    /// <summary>
    /// Current number of copy traders
    /// </summary>
    [JsonPropertyName("copyTraderNum")]
    public int? NumberOfCopyTraders { get; set; }
}
