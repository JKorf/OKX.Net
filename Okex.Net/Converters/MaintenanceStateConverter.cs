using OKX.Net.Enums;

namespace OKX.Net.Converters;

internal class MaintenanceStateConverter : BaseConverter<OKXMaintenanceState>
{
    public MaintenanceStateConverter() : this(true) { }
    public MaintenanceStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OKXMaintenanceState, string>> Mapping => new List<KeyValuePair<OKXMaintenanceState, string>>
    {
        new KeyValuePair<OKXMaintenanceState, string>(OKXMaintenanceState.Scheduled, "scheduled"),
        new KeyValuePair<OKXMaintenanceState, string>(OKXMaintenanceState.Ongoing, "ongoing"),
        new KeyValuePair<OKXMaintenanceState, string>(OKXMaintenanceState.Completed, "completed"),
        new KeyValuePair<OKXMaintenanceState, string>(OKXMaintenanceState.Canceled, "canceled"),
    };
}