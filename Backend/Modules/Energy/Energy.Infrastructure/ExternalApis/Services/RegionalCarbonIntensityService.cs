using Energy.Domain.Models;
using Energy.Domain.Services;
using Energy.Infrastructure.ExternalApi;
using Energy.Infrastructure.Mapping;

namespace Energy.Infrastructure.Services;

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
