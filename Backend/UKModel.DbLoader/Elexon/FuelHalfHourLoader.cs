using Energy.Domain.Interfaces;
using Energy.Infrastructure.ExternalApis.Clients;
using Microsoft.Extensions.Logging;
using DomainFuelHalfHour = Energy.Domain.Models.FuelHalfHour;

namespace UKModel.DbLoader.Elexon;

public class FuelHalfHourLoader(
    ElexonApiClient elexonClient,
    IFuelHalfHourRepository repository,
    ILogger<FuelHalfHourLoader> logger)
{
    private const int MaxDaysPerRequest = 7;

    public async Task LoadLatestAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var latest = await repository.GetLatestAsync(cancellationToken);
        var from = latest.Count > 0
            ? DateOnly.FromDateTime(latest[0].StartTimeUtc)
            : today.AddDays(-1);

        await LoadAsync(from, today, cancellationToken);
    }

    public async Task LoadAsync(DateOnly from, DateOnly to, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Starting FUELHH backfill from {From} to {To}", from, to);

        var totalRows = 0;
        var chunkStart = from;

        while (chunkStart <= to)
        {
            var chunkEnd = MinDate(chunkStart.AddDays(MaxDaysPerRequest - 1), to);

            logger.LogInformation("Fetching {Start} to {End}...", chunkStart, chunkEnd);

            var response = await elexonClient.GetFuelHalfHourAsync(chunkStart, chunkEnd, cancellationToken);

            if (response?.Data is not { Count: > 0 })
            {
                logger.LogWarning("No data returned for {Start} to {End}", chunkStart, chunkEnd);
                chunkStart = chunkEnd.AddDays(1);
                continue;
            }

            var domainRows = response.Data.Select(r => new DomainFuelHalfHour(
                StartTimeUtc: r.StartTime,
                FuelType: r.FuelType,
                GenerationMw: r.Generation,
                PublishTimeUtc: r.PublishTime,
                SettlementDate: DateOnly.Parse(r.SettlementDate),
                SettlementPeriod: r.SettlementPeriod,
                SourceDataset: r.Dataset
            )).ToList();

            await repository.UpsertAsync(domainRows, cancellationToken);
            totalRows += domainRows.Count;

            logger.LogInformation("Upserted {Count} rows ({Total} total)", domainRows.Count, totalRows);

            chunkStart = chunkEnd.AddDays(1);
        }

        logger.LogInformation("Finished. Total rows upserted: {Total}", totalRows);
    }

    private static DateOnly MinDate(DateOnly a, DateOnly b) => a < b ? a : b;
}
