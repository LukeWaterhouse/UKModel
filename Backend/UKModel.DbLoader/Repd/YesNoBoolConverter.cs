using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace UKModel.DbLoader.Repd;

public sealed class YesNoBoolConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return string.Equals(text?.Trim(), "Yes", StringComparison.OrdinalIgnoreCase);
    }
}
