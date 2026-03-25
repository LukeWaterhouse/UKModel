using Energy.Domain.Interfaces;
using Energy.Domain.Models;
using Energy.Infrastructure.AzureSqlRepository.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Energy.Infrastructure.AzureSqlRepository;

public class FuelHalfHourRepository(EnergyDbContext context) : IFuelHalfHourRepository
{
    public async Task UpsertAsync(IEnumerable<FuelHalfHour> rows, CancellationToken cancellationToken = default)
    {
        var rowList = rows.ToList();
        var startTimes = rowList.Select(r => r.StartTimeUtc).Distinct().ToList();

        var existing = await context.FuelHalfHours
            .Where(r => startTimes.Contains(r.StartTimeUtc))
            .ToDictionaryAsync(r => (r.StartTimeUtc, r.FuelType), cancellationToken);

        foreach (var row in rowList)
        {
            var db = row.FromDomain();
            var key = (db.StartTimeUtc, db.FuelType);

            if (existing.TryGetValue(key, out var entity))
            {
                db.Id = entity.Id;
                context.Entry(entity).CurrentValues.SetValues(db);
            }
            else
            {
                context.FuelHalfHours.Add(db);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<FuelHalfHour>> GetByDateRangeAsync(
        DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return await context.FuelHalfHours
            .Where(r => r.StartTimeUtc >= from && r.StartTimeUtc < to)
            .OrderBy(r => r.StartTimeUtc)
            .Select(r => r.ToDomain())
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<FuelHalfHour>> GetLatestAsync(CancellationToken cancellationToken = default)
    {
        var latestTime = await context.FuelHalfHours
            .MaxAsync(r => (DateTime?)r.StartTimeUtc, cancellationToken);

        if (latestTime is null)
            return [];

        return await context.FuelHalfHours
            .Where(r => r.StartTimeUtc == latestTime.Value)
            .Select(r => r.ToDomain())
            .ToListAsync(cancellationToken);
    }
}
