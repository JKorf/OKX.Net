using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Enums;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public enum RiskOffsetType
{
    [Map("1")]
    SpotDerivativesUsdtOffset,
    [Map("2")]
    SpotDerivativesCryptoOffset,
    [Map("4")]
    SpotDerivativesUsdcOffset,
    [Map("3")]
    DerivativesOnlyMode
}
