namespace Energy.Infrastructure.ExternalApis.Models.Response;

public record ElexonFuelHalfHourResponse(List<ElexonFuelHalfHourRow> Data);

public record ElexonFuelHalfHourRow(
    string Dataset,
    DateTime PublishTime,
    DateTime StartTime,
    string SettlementDate,
    int SettlementPeriod,
    string FuelType,
    int Generation);
