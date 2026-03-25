namespace Energy.Domain.Models;

public record FuelHalfHour(
    DateTime StartTimeUtc,
    string FuelType,
    int GenerationMw,
    DateTime PublishTimeUtc,
    DateOnly SettlementDate,
    int SettlementPeriod,
    string SourceDataset);
