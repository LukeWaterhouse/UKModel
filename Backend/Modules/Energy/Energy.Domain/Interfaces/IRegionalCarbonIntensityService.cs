using Energy.Domain.Models;

namespace Energy.Domain.Interfaces;

public interface IRegionalCarbonIntensityService
{
    Task<IReadOnlyList<GridRegion>> GetAllRegionsAsync(CancellationToken cancellationToken = default);
}
