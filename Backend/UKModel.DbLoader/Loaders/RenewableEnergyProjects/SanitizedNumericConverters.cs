using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace UKModel.DbLoader.Loaders.RenewableEnergyProjects;

public sealed class SanitizedNullableDecimalConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var sanitized = text?.Trim(' ', '\u00a0');
        if (string.IsNullOrEmpty(sanitized)) return null;
        return decimal.TryParse(sanitized, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result
            : null;
    }
}

public sealed class SanitizedNullableIntConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var sanitized = text?.Trim(' ', '\u00a0');
        if (string.IsNullOrEmpty(sanitized)) return null;
        return int.TryParse(sanitized, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result
            : null;
    }
}
