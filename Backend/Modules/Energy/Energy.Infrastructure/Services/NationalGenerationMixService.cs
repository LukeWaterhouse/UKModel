using Energy.Domain.Models;
using Energy.Domain.Services;
using Energy.Infrastructure.ExternalApi;
using Energy.Infrastructure.Mapping;

namespace Energy.Infrastructure.Services;

internal sealed class NationalGenerationMixService(
    CarbonIntensityApiClient client) : INationalGenerationMixService
{
    public async Task<NationalGenerationMix> GetCurrentMixAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await client.GetNationalGenerationMixAsync(cancellationToken);
        if (response?.Data == null || response.Data.Count == 0)
            return new NationalGenerationMix(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, []);

        var firstItem = response.Data[0];
        return firstItem.ToDomain();
    }
}
