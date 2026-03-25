namespace Energy.Infrastructure.ExternalApis.Models.Response;

internal sealed record IntensityItem(int Forecast, int? Actual, string Index);
