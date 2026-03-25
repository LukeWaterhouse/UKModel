using Energy.Domain.Interfaces;
using Energy.Domain.Models;
using Energy.Infrastructure.ExternalApis.Clients;
using Energy.Infrastructure.ExternalApis.Mapping;
using Microsoft.Extensions.Caching.Memory;

namespace Energy.Infrastructure.ExternalApis.Services;

internal sealed class PowerPlantService(
    OverpassApiClient client,
    IMemoryCache cache) : IPowerPlantService
{
    private const string CacheKey = "power-plants";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(24);

    public async Task<IReadOnlyList<PowerPlant>> GetPlantsAsync(
        CancellationToken cancellationToken = default)
    {
        if (cache.TryGetValue(CacheKey, out IReadOnlyList<PowerPlant>? cached) && cached is not null)
            return cached;

        var response = await client.QueryPowerPlantsAsync(cancellationToken);

        if (response?.Elements is null or { Count: 0 })
            return [];

        var plants = response.Elements
            .Where(e => e.Center is not null && e.Tags is not null && e.Tags.ContainsKey("name"))
            .Select(e => e.ToDomain())
            .ToList();

        cache.Set(CacheKey, (IReadOnlyList<PowerPlant>)plants, CacheDuration);

        return plants;
    }
}
