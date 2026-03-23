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
            Intensity: new IntensityDto(
                region.Intensity.Forecast,
                region.Intensity.Actual,
                region.Intensity.Index.FromDomain(),
                region.Intensity.Unit),
            GenerationMix: region.GenerationMix
                .Select(m => m.FromDomain())
                .ToList());
    }

    private static GridRegionTypeDto FromDomain(this GridRegionType domainType) => (GridRegionTypeDto)(int)domainType;

    private static DnoRegionDto FromDomain(this DnoRegion dnoRegion) => (DnoRegionDto)(int)dnoRegion;

    public static GenerationMixEntryDto FromDomain(this GenerationMixEntry entry) =>
        new(entry.Fuel.FromDomain(), entry.Percentage);

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

    private static CarbonIntensityIndexTypeDto FromDomain(this CarbonIntensityIndex intensityIndex) => intensityIndex switch
    {
        CarbonIntensityIndex.VeryLow => CarbonIntensityIndexTypeDto.VeryLow,
        CarbonIntensityIndex.Low => CarbonIntensityIndexTypeDto.Low,
        CarbonIntensityIndex.Moderate => CarbonIntensityIndexTypeDto.Moderate,
        CarbonIntensityIndex.High => CarbonIntensityIndexTypeDto.High,
        CarbonIntensityIndex.VeryHigh => CarbonIntensityIndexTypeDto.VeryHigh,
        _ => CarbonIntensityIndexTypeDto.Moderate
    };
}