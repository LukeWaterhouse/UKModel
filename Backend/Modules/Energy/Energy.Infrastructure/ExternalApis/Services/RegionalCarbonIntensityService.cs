using Energy.Domain.Interfaces;
using Energy.Domain.Models;
using Energy.Infrastructure.ExternalApis.Clients;
using Energy.Infrastructure.ExternalApis.Mapping;

namespace Energy.Infrastructure.ExternalApis.Services;

internal sealed class RegionalCarbonIntensityService(
    CarbonIntensityApiClient client) : IRegionalCarbonIntensityService
{
    public async Task<IReadOnlyList<GridRegion>> GetAllRegionsAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await client.GetRegionalIntensityAsync(cancellationToken);
        if (response?.Data == null || response.Data.Count == 0)
            return [];

        var firstItem = response.Data[0];
        return firstItem.Regions.Select(r => r.ToDomain()).ToList();
    }
}
