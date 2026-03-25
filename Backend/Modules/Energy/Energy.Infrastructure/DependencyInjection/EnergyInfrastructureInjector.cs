using Energy.Domain.Services;
using Energy.Infrastructure.ExternalApi;
using Energy.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Energy.Infrastructure.DependencyInjection;

public static class EnergyInfrastructureInjector
{
    public static IServiceCollection AddEnergyInfrastructureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddHttpClient<CarbonIntensityApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.carbonintensity.org.uk/");
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddHttpClient<OverpassApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://overpass-api.de/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddScoped<IRegionalCarbonIntensityService, RegionalCarbonIntensityService>();
        services.AddScoped<INationalGenerationMixService, NationalGenerationMixService>();
        services.AddScoped<IPowerPlantService, PowerPlantService>();

        return services;
    }
}
