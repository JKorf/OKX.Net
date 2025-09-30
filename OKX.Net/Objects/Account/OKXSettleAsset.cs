using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Settlement asset
/// </summary>
public record OKXSettleAsset
{
    /// <summary>
    /// Settlement asset
    /// </summary>
    [JsonPropertyName("settleCcy")]
    public string SettleAsset { get; set; } = string.Empty;
}
