using Energy.Domain.Models;

namespace Energy.Domain.Services;

public interface INationalGenerationMixService
{
    Task<NationalGenerationMix> GetCurrentMixAsync(CancellationToken cancellationToken = default);
}

