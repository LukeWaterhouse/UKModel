using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Energy.Domain.Models;
using Energy.Domain.Services;

namespace UKModel.DbLoader.Loaders.RenewableEnergyProjects;

public class RepdCsvLoader(IRenewableEnergyProjectRepository repository)
{
    private const int BatchSize = 1000;

    public async Task LoadAsync(string csvPath, CancellationToken cancellationToken = default)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            TrimOptions = TrimOptions.Trim
        };

        using var reader = new StreamReader(csvPath, Encoding.Latin1);
        using var csv = new CsvReader(reader, config);
        csv.Context.TypeConverterCache.AddConverter<decimal?>(new SanitizedNullableDecimalConverter());
        csv.Context.TypeConverterCache.AddConverter<int?>(new SanitizedNullableIntConverter());
        csv.Context.RegisterClassMap<RepdCsvMap>();

        var batch = new List<RenewableEnergyProject>(BatchSize);
        var totalCount = 0;

        await foreach (var record in csv.GetRecordsAsync<RenewableEnergyProject>(cancellationToken))
        {
            batch.Add(record);

            if (batch.Count >= BatchSize)
            {
                await repository.UpsertAsync(batch, cancellationToken);
                totalCount += batch.Count;
                Console.WriteLine($"Upserted {totalCount} records...");
                batch.Clear();
            }
        }

        if (batch.Count > 0)
        {
            await repository.UpsertAsync(batch, cancellationToken);
            totalCount += batch.Count;
        }

        Console.WriteLine($"Finished. Total records upserted: {totalCount}");
    }
}
