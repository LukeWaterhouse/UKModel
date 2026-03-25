using UKModel.Api.Dtos.Energy.Enums;

namespace UKModel.Api.Dtos.Energy;

public record RenewableEnergyProjectDto(
    CoordinatesDto Coordinates,
    TechnologyTypeDto TechnologyType,
    string? SiteName,
    string? Operator,
    decimal? InstalledCapacityMWe,
    string? DevelopmentStatus,
    string? Region,
    string? Country);
