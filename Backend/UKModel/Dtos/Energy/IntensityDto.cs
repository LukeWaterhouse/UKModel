using UKModel.Api.Dtos.Energy.Enums;

namespace UKModel.Api.Dtos.Energy;

// Forecast is the predicted carbon intensity, Actual is the real measured value after the time period
public record IntensityDto(int Forecast, int? Actual, CarbonIntensityIndexTypeDto Index, string Unit);
