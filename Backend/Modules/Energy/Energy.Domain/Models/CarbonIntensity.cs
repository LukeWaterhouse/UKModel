using Energy.Domain.Enums;

namespace Energy.Domain.Models;

public record CarbonIntensity(int Forecast, int? Actual, CarbonIntensityIndexType Index)
{
    public string Unit { get; } = "gCO2/kWh";
}
