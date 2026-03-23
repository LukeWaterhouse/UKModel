namespace Energy.Domain.Models;

public record CarbonIntensity(int Forecast, int? Actual, CarbonIntensityIndex Index)
{
    public string Unit { get; } = "gCO2/kWh";
}
