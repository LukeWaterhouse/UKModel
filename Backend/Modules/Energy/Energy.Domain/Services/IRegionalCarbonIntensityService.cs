using Energy.Domain.Models;

namespace Energy.Domain.Services;

public interface IRegionalCarbonIntensityService
{
    Task<IReadOnlyList<GridRegion>> GetAllRegionsAsync(CancellationToken cancellationToken = default);
}

