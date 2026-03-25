using System.Net.Http.Json;
using System.Text.Json;
using Energy.Infrastructure.Models.Response;

namespace Energy.Infrastructure.ExternalApi;

internal sealed class CarbonIntensityApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<CarbonIntensityRegionalResponse?> GetRegionalIntensityAsync(
        CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<CarbonIntensityRegionalResponse>(
            "regional", JsonOptions, cancellationToken);
    }

    public async Task<CarbonIntensityGenerationResponse?> GetNationalGenerationMixAsync(
        CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<CarbonIntensityGenerationResponse>(
            "generation", JsonOptions, cancellationToken);
    }

    public async Task<CarbonIntensityNationalResponse?> GetNationalIntensityAsync(
        CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<CarbonIntensityNationalResponse>(
            "intensity/date", JsonOptions, cancellationToken);
    }
}
