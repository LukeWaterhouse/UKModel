using Energy.Domain.Models;
using Energy.Infrastructure.Models.Response;

namespace Energy.Infrastructure.Mapping;

internal static class MappingExtensions
{
    // Infrastructure to Domain mappings
    public static GridRegion ToDomain(this RegionItem r)
    {
        var regionType = r.ShortName.ToGridRegionType();
        return new(
            DnoRegion: r.DnoRegion,
            Region: regionType,
            Intensity: new CarbonIntensity(r.Intensity.Forecast, r.Intensity.Actual, r.Intensity.Index.ToCarbonIntensityIndex()),
            GenerationMix: r.GenerationMix.ToDomain());
    }

    public static NationalGenerationMix ToDomain(this GenerationDataItem item) =>
        new(
            From: DateTimeOffset.Parse(item.From),
            To: DateTimeOffset.Parse(item.To),
            Mix: item.GenerationMix.ToDomain());


    private static IReadOnlyList<GenerationMixEntry> ToDomain(this List<GenerationMixItem> items) =>
        items.Select(i => new GenerationMixEntry(i.Fuel.ToFuelType(), i.Perc)).ToList();

    private static FuelType ToFuelType(this string fuel) => fuel.ToLowerInvariant() switch
    {
        "gas" => FuelType.Gas,
        "coal" => FuelType.Coal,
        "biomass" => FuelType.Biomass,
        "nuclear" => FuelType.Nuclear,
        "hydro" => FuelType.Hydro,
        "imports" => FuelType.Imports,
        "wind" => FuelType.Wind,
        "solar" => FuelType.Solar,
        "storage" => FuelType.Storage,
        _ => FuelType.Other
    };

    private static GridRegionType ToGridRegionType(this string shortName) => shortName switch
    {
        "North Scotland" => GridRegionType.NorthScotland,
        "South Scotland" => GridRegionType.SouthScotland,
        "North West England" => GridRegionType.NorthWestEngland,
        "North East England" => GridRegionType.NorthEastEngland,
        "Yorkshire" => GridRegionType.Yorkshire,
        "North Wales" or "North Wales & Merseyside" => GridRegionType.NorthWales,
        "South Wales" => GridRegionType.SouthWales,
        "West Midlands" => GridRegionType.WestMidlands,
        "East Midlands" => GridRegionType.EastMidlands,
        "East England" => GridRegionType.EastEngland,
        "South West England" => GridRegionType.SouthWestEngland,
        "South England" => GridRegionType.SouthEngland,
        "London" => GridRegionType.London,
        "South East England" => GridRegionType.SouthEastEngland,
        "England" => GridRegionType.England,
        "Scotland" => GridRegionType.Scotland,
        "Wales" => GridRegionType.Wales,
        _ => GridRegionType.England
    };

    private static CarbonIntensityIndex ToCarbonIntensityIndex(this string index) => index.ToLowerInvariant() switch
    {
        "very low" => CarbonIntensityIndex.VeryLow,
        "low" => CarbonIntensityIndex.Low,
        "moderate" => CarbonIntensityIndex.Moderate,
        "high" => CarbonIntensityIndex.High,
        "very high" => CarbonIntensityIndex.VeryHigh,
        _ => CarbonIntensityIndex.Moderate
    };
}