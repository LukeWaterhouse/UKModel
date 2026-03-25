using Energy.Domain.Models;

namespace Energy.Domain.Interfaces;

public interface IPowerPlantService
{
    Task<IReadOnlyList<PowerPlant>> GetPlantsAsync(CancellationToken cancellationToken = default);
}
