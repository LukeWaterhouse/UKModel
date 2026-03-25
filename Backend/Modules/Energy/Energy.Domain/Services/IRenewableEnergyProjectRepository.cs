using Energy.Domain.Models;

namespace Energy.Domain.Services;

public interface IRenewableEnergyProjectRepository
{
    Task UpsertAsync(IEnumerable<RenewableEnergyProject> projects, CancellationToken cancellationToken = default);
    Task<RenewableEnergyProject?> GetByRefIdAsync(int refId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RenewableEnergyProject>> GetAllAsync(CancellationToken cancellationToken = default);
}
