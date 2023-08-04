using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class ApiPermissionConverter : BaseConverter<OKXApiPermission>
{
    public ApiPermissionConverter() : this(true) { }
    public ApiPermissionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXApiPermission, string>> Mapping => new List<KeyValuePair<OKXApiPermission, string>>
    {
        new KeyValuePair<OKXApiPermission, string>(OKXApiPermission.ReadOnly, "read_only"),
        new KeyValuePair<OKXApiPermission, string>(OKXApiPermission.Trade, "trade"),
    };
}