using Energy.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UKModel.DbLoader.Elexon;
using UKModel.DbLoader.Repd;

var host = Host.CreateDefaultBuilder(args)
    .UseContentRoot(AppContext.BaseDirectory)
    .ConfigureServices((context, services) =>
    {
        services.AddEnergyInfrastructureServices(context.Configuration);
        services.AddTransient<RepdCsvLoader>();
        services.AddTransient<FuelHalfHourLoader>();
    })
    .Build();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== UKModel Database Loader ===");
    Console.WriteLine();
    Console.WriteLine("  1. Load REPD Renewable Energy Projects");
    Console.WriteLine("  2. Backfill Elexon FUELHH");
    Console.WriteLine("  0. Exit");
    Console.WriteLine();
    Console.Write("Select an option: ");

    var input = Console.ReadLine()?.Trim();

    switch (input)
    {
        case "1":
            var csvPath = Path.Combine(AppContext.BaseDirectory, "Assets", "REPD_Renewables_locations_Q4_2025.csv");
            using (var scope = host.Services.CreateScope())
            {
                var loader = scope.ServiceProvider.GetRequiredService<RepdCsvLoader>();
                await loader.LoadAsync(csvPath);
            }
            break;

        case "2":
            Console.Write("Start date (yyyy-MM-dd): ");
            var fromInput = Console.ReadLine()?.Trim();
            Console.Write("End date (yyyy-MM-dd): ");
            var toInput = Console.ReadLine()?.Trim();

            if (!DateOnly.TryParse(fromInput, out var fromDate) || !DateOnly.TryParse(toInput, out var toDate))
            {
                Console.WriteLine("Invalid date format.");
                break;
            }

            using (var scope = host.Services.CreateScope())
            {
                var fuelLoader = scope.ServiceProvider.GetRequiredService<FuelHalfHourLoader>();
                await fuelLoader.LoadAsync(fromDate, toDate);
            }
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid option, please try again.");
            break;
    }
}