using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Converters
{
    internal class OKXClientIdConverter : ReplaceConverter
    {
        public OKXClientIdConverter() : base($"{OKXExchange.ClientOrderIdPrefix}->") { }
    }
}
