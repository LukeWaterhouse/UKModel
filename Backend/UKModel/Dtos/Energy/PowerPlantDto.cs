using UKModel.Api.Dtos.Energy.Enums;

namespace UKModel.Api.Dtos.Energy;

public record PowerPlantDto(
    CoordinatesDto Coordinates,
    string Name,
    string? Operator,
    string? PlannedEndDate,
    int OutputMW,
    string? StartDate,
    PlantSourceDto Source);
