using Energy.Domain.Models;

namespace Energy.Domain.Services;

public interface IPowerPlantService
{
    Task<IReadOnlyList<PowerPlant>> GetPlantsAsync(CancellationToken cancellationToken = default);
}
