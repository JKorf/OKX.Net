using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class MaintenanceSystemConverter : BaseConverter<OKXMaintenanceSystem>
{
    public MaintenanceSystemConverter() : this(true) { }
    public MaintenanceSystemConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXMaintenanceSystem, string>> Mapping => new List<KeyValuePair<OKXMaintenanceSystem, string>>
    {
        new KeyValuePair<OKXMaintenanceSystem, string>(OKXMaintenanceSystem.Classic, "classic"),
        new KeyValuePair<OKXMaintenanceSystem, string>(OKXMaintenanceSystem.Unified, "unified"),
    };
}