using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Energy.Domain.Interfaces;
using Energy.Domain.Models;
using Microsoft.Extensions.Logging;
using UKModel.DbLoader.Shared;

namespace UKModel.DbLoader.Repd;

public class RepdCsvLoader(
    IRenewableEnergyProjectRepository repository,
    ILogger<RepdCsvLoader> logger)
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
            var coords = BngToWgs84Converter.Convert(record.XCoordinate, record.YCoordinate);
            var enriched = record with { Latitude = coords?.Latitude, Longitude = coords?.Longitude };
            batch.Add(enriched);

            if (batch.Count >= BatchSize)
            {
                await repository.UpsertAsync(batch, cancellationToken);
                totalCount += batch.Count;
                logger.LogInformation("Upserted {Count} records...", totalCount);
                batch.Clear();
            }
        }

        if (batch.Count > 0)
        {
            await repository.UpsertAsync(batch, cancellationToken);
            totalCount += batch.Count;
        }

        logger.LogInformation("Finished. Total records upserted: {Count}", totalCount);
    }
}
