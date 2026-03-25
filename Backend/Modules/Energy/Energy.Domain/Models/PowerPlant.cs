using Energy.Domain.Enums;

namespace Energy.Domain.Models;

public record PowerPlant(
    Coordinates Coordinates,
    string Name,
    string? Operator,
    string? PlannedEndDate,
    int OutputMW,
    string? StartDate,
    PlantSourceType Source);
