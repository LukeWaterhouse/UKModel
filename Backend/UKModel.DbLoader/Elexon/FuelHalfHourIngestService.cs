using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UKModel.DbLoader.Elexon;

public sealed class FuelHalfHourIngestService(
    IServiceScopeFactory scopeFactory,
    ILogger<FuelHalfHourIngestService> logger) : BackgroundService
{
    private static readonly TimeSpan InitialDelay = TimeSpan.FromSeconds(10);
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(30);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(InitialDelay, cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var loader = scope.ServiceProvider.GetRequiredService<FuelHalfHourLoader>();
                await loader.LoadLatestAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "FUELHH ingestion failed");
            }

            await Task.Delay(Interval, cancellationToken);
        }
    }
}
