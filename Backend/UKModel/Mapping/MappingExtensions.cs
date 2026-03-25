using Energy.Domain.Enums;
using Energy.Domain.Models;
using UKModel.Api.Dtos.Energy;
using UKModel.Api.Dtos.Energy.Enums;

namespace UKModel.Api.Mapping;

public static class MappingExtensions
{
    public static RegionDto FromDomain(this GridRegion region)
    {
        return new RegionDto(
            RegionType: region.Region.FromDomain(),
            DnoRegion: region.DnoRegion.FromDomain(),
            Intensity: region.Intensity.FromDomain(),
            GenerationMix: region.GenerationMix
                .Select(m => m.FromDomain())
                .ToList());
    }

    private static GridRegionTypeDto FromDomain(this GridRegionType domainType) => (GridRegionTypeDto)(int)domainType;

    private static DnoRegionDto FromDomain(this DnoRegionType dnoRegion) => (DnoRegionDto)(int)dnoRegion;

    public static GenerationMixEntryDto FromDomain(this GenerationMixEntry entry) =>
        new(entry.Fuel.FromDomain(), entry.Percentage);

    public static IntensityDto FromDomain(this CarbonIntensity intensity) =>
        new(intensity.Forecast, intensity.Actual, intensity.Index.FromDomain(), intensity.Unit);

    private static FuelTypeDto FromDomain(this FuelType fuelType) => fuelType switch
    {
        FuelType.Gas => FuelTypeDto.Gas,
        FuelType.Coal => FuelTypeDto.Coal,
        FuelType.Biomass => FuelTypeDto.Biomass,
        FuelType.Nuclear => FuelTypeDto.Nuclear,
        FuelType.Hydro => FuelTypeDto.Hydro,
        FuelType.Imports => FuelTypeDto.Imports,
        FuelType.Wind => FuelTypeDto.Wind,
        FuelType.Solar => FuelTypeDto.Solar,
        FuelType.Storage => FuelTypeDto.Storage,
        _ => FuelTypeDto.Other
    };

    private static CarbonIntensityIndexTypeDto FromDomain(this CarbonIntensityIndexType intensityIndex) => intensityIndex switch
    {
        CarbonIntensityIndexType.VeryLow => CarbonIntensityIndexTypeDto.VeryLow,
        CarbonIntensityIndexType.Low => CarbonIntensityIndexTypeDto.Low,
        CarbonIntensityIndexType.Moderate => CarbonIntensityIndexTypeDto.Moderate,
        CarbonIntensityIndexType.High => CarbonIntensityIndexTypeDto.High,
        CarbonIntensityIndexType.VeryHigh => CarbonIntensityIndexTypeDto.VeryHigh,
        _ => CarbonIntensityIndexTypeDto.Moderate
    };

    public static PowerPlantDto FromDomain(this PowerPlant plant) =>
        new(new CoordinatesDto(plant.Coordinates.Latitude, plant.Coordinates.Longitude),
            plant.Name, plant.Operator, plant.PlannedEndDate, plant.OutputMW, plant.StartDate, (PlantSourceDto)(int)plant.Source);

    public static RenewableEnergyProjectDto FromDomain(this RenewableEnergyProject project) =>
        new(new CoordinatesDto(project.Latitude ?? 0, project.Longitude ?? 0),
            (TechnologyTypeDto)(int)(project.TechnologyType ?? TechnologyType.Unknown),
            project.SiteName,
            project.Operator,
            project.InstalledCapacityMWe,
            project.DevelopmentStatus,
            project.Region,
            project.Country);
}