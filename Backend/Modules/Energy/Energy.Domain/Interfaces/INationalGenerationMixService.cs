using Energy.Domain.Models;

namespace Energy.Domain.Interfaces;

public interface INationalGenerationMixService
{
    Task<NationalGenerationMix> GetCurrentMixAsync(CancellationToken cancellationToken = default);
}
