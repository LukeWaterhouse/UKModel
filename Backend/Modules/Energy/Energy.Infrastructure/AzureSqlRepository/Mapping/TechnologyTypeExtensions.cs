using Energy.Domain.Enums;

namespace Energy.Infrastructure.AzureSqlRepository.Mapping;

public static class TechnologyTypeExtensions
{
    private static readonly Dictionary<string, TechnologyType> DisplayNameToEnum = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Advanced Conversion Technologies"] = TechnologyType.AdvancedConversionTechnologies,
        ["Anaerobic Digestion"] = TechnologyType.AnaerobicDigestion,
        ["Battery"] = TechnologyType.Battery,
        ["Biomass (co-firing)"] = TechnologyType.BiomassCofiring,
        ["Biomass (dedicated)"] = TechnologyType.BiomassDedicated,
        ["Compressed Air Energy Storage"] = TechnologyType.CompressedAirEnergyStorage,
        ["EfW Incineration"] = TechnologyType.EfwIncineration,
        ["Flywheels"] = TechnologyType.Flywheels,
        ["Fuel Cell (Hydrogen)"] = TechnologyType.FuelCellHydrogen,
        ["Geothermal"] = TechnologyType.Geothermal,
        ["Hot Dry Rocks (HDR)"] = TechnologyType.HotDryRocks,
        ["Hydrogen"] = TechnologyType.Hydrogen,
        ["Landfill Gas"] = TechnologyType.LandfillGas,
        ["Large Hydro"] = TechnologyType.LargeHydro,
        ["Liquid Air Energy Storage"] = TechnologyType.LiquidAirEnergyStorage,
        ["Pumped Storage Hydroelectricity"] = TechnologyType.PumpedStorageHydroelectricity,
        ["Sewage Sludge Digestion"] = TechnologyType.SewageSludgeDigestion,
        ["Shoreline Wave"] = TechnologyType.ShorelineWave,
        ["Small Hydro"] = TechnologyType.SmallHydro,
        ["Solar Photovoltaics"] = TechnologyType.SolarPhotovoltaics,
        ["Tidal Lagoon"] = TechnologyType.TidalLagoon,
        ["Tidal Stream"] = TechnologyType.TidalStream,
        ["Unknown"] = TechnologyType.Unknown,
        ["Wind Offshore"] = TechnologyType.WindOffshore,
        ["Wind Onshore"] = TechnologyType.WindOnshore,
    };

    private static readonly Dictionary<TechnologyType, string> EnumToDisplayName =
        DisplayNameToEnum.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

    public static TechnologyType? ParseTechnologyType(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return DisplayNameToEnum.TryGetValue(value.Trim(), out var result)
            ? result
            : throw new ArgumentException($"Unknown technology type: '{value}'");
    }

    public static string? ToDisplayString(this TechnologyType? value)
    {
        return value.HasValue && EnumToDisplayName.TryGetValue(value.Value, out var name) ? name : null;
    }
}
