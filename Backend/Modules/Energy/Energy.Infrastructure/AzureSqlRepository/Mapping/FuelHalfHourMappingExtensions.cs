using Energy.Domain.Models;
using Energy.Infrastructure.AzureSqlRepository.Models;

namespace Energy.Infrastructure.AzureSqlRepository.Mapping;

public static class FuelHalfHourMappingExtensions
{
    public static FuelHalfHourDb FromDomain(this FuelHalfHour domain) => new()
    {
        StartTimeUtc = domain.StartTimeUtc,
        FuelType = domain.FuelType,
        GenerationMw = domain.GenerationMw,
        PublishTimeUtc = domain.PublishTimeUtc,
        SettlementDate = domain.SettlementDate,
        SettlementPeriod = domain.SettlementPeriod,
        SourceDataset = domain.SourceDataset,
        CreatedAtUtc = DateTime.UtcNow
    };

    public static FuelHalfHour ToDomain(this FuelHalfHourDb db) => new(
        StartTimeUtc: db.StartTimeUtc,
        FuelType: db.FuelType,
        GenerationMw: db.GenerationMw,
        PublishTimeUtc: db.PublishTimeUtc,
        SettlementDate: db.SettlementDate,
        SettlementPeriod: db.SettlementPeriod,
        SourceDataset: db.SourceDataset);
}
