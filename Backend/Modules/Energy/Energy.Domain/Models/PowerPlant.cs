namespace Energy.Domain.Models;

public record PowerPlant(
    Coordinates Coordinates,
    string Name,
    string? Operator,
    string? PlannedEndDate,
    int OutputMW,
    string? StartDate,
    PlantSource Source);
