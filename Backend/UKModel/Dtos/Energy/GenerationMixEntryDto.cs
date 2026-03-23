using UKModel.Api.Dtos.Energy.Enums;

namespace UKModel.Api.Dtos.Energy;

public record GenerationMixEntryDto(FuelTypeDto FuelType, decimal Percentage);
