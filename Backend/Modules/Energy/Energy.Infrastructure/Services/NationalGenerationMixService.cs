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
        var generationTask = client.GetNationalGenerationMixAsync(cancellationToken);
        var intensityTask = client.GetNationalIntensityAsync(cancellationToken);

        await Task.WhenAll(generationTask, intensityTask);

        var generationResponse = await generationTask;
        var intensityResponse = await intensityTask;

        var intItem = intensityResponse?.Data?.FirstOrDefault();
        var intensity = intItem != null
            ? intItem.Intensity.ToDomain()
            : new CarbonIntensity(0, null, CarbonIntensityIndex.Moderate);

        var genItem = generationResponse?.Data;
        if (genItem == null)
            return new NationalGenerationMix(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, intensity, []);

        return genItem.ToDomain(intensity);
    }
}
