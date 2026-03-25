using Energy.Domain.Enums;
using Energy.Domain.Models;
using Energy.Infrastructure.ExternalApis.Models.Response;

namespace Energy.Infrastructure.ExternalApis.Mapping;

internal static class MappingExtensions
{
    // Infrastructure to Domain mappings
    public static GridRegion ToDomain(this RegionItem r)
    {
        var regionType = r.ShortName.ToGridRegionType();
        return new(
            DnoRegion: r.DnoRegion.ToDnoRegionType(),
            Region: regionType,
            Intensity: new CarbonIntensity(r.Intensity.Forecast, r.Intensity.Actual, r.Intensity.Index.ToCarbonIntensityIndexType()),
            GenerationMix: r.GenerationMix.ToDomain());
    }

    public static NationalGenerationMix ToDomain(this GenerationDataItem item, CarbonIntensity intensity) =>
        new(
            From: DateTimeOffset.Parse(item.From),
            To: DateTimeOffset.Parse(item.To),
            Intensity: intensity,
            Mix: item.GenerationMix.ToDomain());

    internal static CarbonIntensity ToDomain(this IntensityItem item) =>
        new(item.Forecast, item.Actual, item.Index.ToCarbonIntensityIndexType());


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

    private static DnoRegionType ToDnoRegionType(this string dnoRegion) => dnoRegion switch
    {
        "Scottish Hydro Electric Power Distribution" => DnoRegionType.ScottishHydroElectric,
        "SP Distribution" => DnoRegionType.SpDistribution,
        "Electricity North West" => DnoRegionType.ElectricityNorthWest,
        "NPG North East" => DnoRegionType.NpgNorthEast,
        "NPG Yorkshire" => DnoRegionType.NpgYorkshire,
        "SP Manweb" => DnoRegionType.SpManweb,
        "WPD South Wales" => DnoRegionType.WpdSouthWales,
        "WPD West Midlands" => DnoRegionType.WpdWestMidlands,
        "WPD East Midlands" => DnoRegionType.WpdEastMidlands,
        "UKPN East" => DnoRegionType.UkpnEast,
        "WPD South West" => DnoRegionType.WpdSouthWest,
        "SSE South" => DnoRegionType.SseSouth,
        "UKPN London" => DnoRegionType.UkpnLondon,
        "UKPN South East" => DnoRegionType.UkpnSouthEast,
        "England" => DnoRegionType.England,
        "Scotland" => DnoRegionType.Scotland,
        "Wales" => DnoRegionType.Wales,
        "GB" => DnoRegionType.Gb,
        _ => DnoRegionType.Gb
    };

    private static CarbonIntensityIndexType ToCarbonIntensityIndexType(this string index) => index.ToLowerInvariant() switch
    {
        "very low" => CarbonIntensityIndexType.VeryLow,
        "low" => CarbonIntensityIndexType.Low,
        "moderate" => CarbonIntensityIndexType.Moderate,
        "high" => CarbonIntensityIndexType.High,
        "very high" => CarbonIntensityIndexType.VeryHigh,
        _ => CarbonIntensityIndexType.Moderate
    };

    public static PowerPlant ToDomain(this OverpassElement element)
    {
        var tags = element.Tags ?? new Dictionary<string, string>();

        return new PowerPlant(
            Coordinates: new Coordinates(element.Center?.Lat ?? 0, element.Center?.Lon ?? 0),
            Name: tags.GetValueOrDefault("name", "Unknown"),
            Operator: tags.GetValueOrDefault("operator"),
            PlannedEndDate: tags.GetValueOrDefault("planned:end_date"),
            OutputMW: ParseOutputMW(tags.GetValueOrDefault("plant:output:electricity", "")),
            StartDate: tags.GetValueOrDefault("start_date"),
            Source: tags.GetValueOrDefault("plant:source", "").ToPlantSourceType());
    }

    private static PlantSourceType ToPlantSourceType(this string source) => source.ToLowerInvariant() switch
    {
        "nuclear" => PlantSourceType.Nuclear,
        "wind" => PlantSourceType.Wind,
        "solar" => PlantSourceType.Solar,
        "gas" => PlantSourceType.Gas,
        "coal" => PlantSourceType.Coal,
        "hydro" => PlantSourceType.Hydro,
        "oil" => PlantSourceType.Oil,
        "biomass" => PlantSourceType.Biomass,
        "waste" => PlantSourceType.Waste,
        "biogas" => PlantSourceType.Biogas,
        _ => PlantSourceType.Gas
    };

    private static int ParseOutputMW(string output)
    {
        var numericPart = output.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        return int.TryParse(numericPart, out var mw) ? mw : 0;
    }
}