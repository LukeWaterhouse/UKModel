using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Energy.Infrastructure.AzureSqlRepository.Mapping;

namespace UKModel.DbLoader.Loaders.RenewableEnergyProjects;

public sealed class TechnologyTypeConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return TechnologyTypeExtensions.ParseTechnologyType(text);
    }
}
