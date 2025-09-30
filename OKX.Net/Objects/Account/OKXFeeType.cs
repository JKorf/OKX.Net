using OKX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKX.Net.Objects.Account;

/// <summary>
/// Fee type
/// </summary>
public record OKXFeeType
{
    /// <summary>
    /// Fee type
    /// </summary>
    [JsonPropertyName("feeType")]
    public FeeType FeeType { get; set; }
}
