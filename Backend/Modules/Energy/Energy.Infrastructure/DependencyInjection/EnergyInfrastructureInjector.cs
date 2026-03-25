using System.Net;
using Energy.Domain.Interfaces;
using Energy.Infrastructure.AzureSqlRepository;
using Energy.Infrastructure.ExternalApis.Clients;
using Energy.Infrastructure.ExternalApis.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Extensions.Http;

namespace Energy.Infrastructure.DependencyInjection;

public static class EnergyInfrastructureInjector
{
    public static IServiceCollection AddEnergyInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
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

        services.AddDbContext<EnergyDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("EnergyDb"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));

        services.AddScoped<IRenewableEnergyProjectRepository, RenewableEnergyProjectRepository>();
        services.AddScoped<IFuelHalfHourRepository, FuelHalfHourRepository>();

        services.AddHttpClient<ElexonApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://data.elexon.co.uk/bmrs/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        })
        .AddPolicyHandler(HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(r => r.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));

        return services;
    }
}
