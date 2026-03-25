using Energy.Domain.Interfaces;
using Energy.Domain.Models;
using Energy.Infrastructure.AzureSqlRepository.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Energy.Infrastructure.AzureSqlRepository;

public class RenewableEnergyProjectRepository(EnergyDbContext context) : IRenewableEnergyProjectRepository
{
    public async Task UpsertAsync(IEnumerable<RenewableEnergyProject> projects, CancellationToken cancellationToken = default)
    {
        var projectList = projects.ToList();
        var refIds = projectList.Select(p => p.RefId).ToList();

        var existingEntities = await context.RenewableEnergyProjects
            .Where(p => refIds.Contains(p.RefId))
            .ToDictionaryAsync(p => p.RefId, cancellationToken);

        foreach (var project in projectList)
        {
            var dbEntity = project.FromDomain();

            if (existingEntities.TryGetValue(project.RefId, out var existing))
            {
                dbEntity.Id = existing.Id;
                context.Entry(existing).CurrentValues.SetValues(dbEntity);
            }
            else
            {
                context.RenewableEnergyProjects.Add(dbEntity);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<RenewableEnergyProject?> GetByRefIdAsync(int refId, CancellationToken cancellationToken = default)
    {
        var entity = await context.RenewableEnergyProjects
            .FirstOrDefaultAsync(p => p.RefId == refId, cancellationToken);

        return entity?.ToDomain();
    }

    public async Task<IReadOnlyList<RenewableEnergyProject>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await context.RenewableEnergyProjects
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return entities.Select(e => e.ToDomain()).ToList();
    }
}
