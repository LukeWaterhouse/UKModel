namespace Energy.Domain.Models;

public record GridRegion(
    string DnoRegion, // Distribution Network Operators (companies that operate the electricity distribution network in the UK)
    GridRegionType Region,
    Coordinate Coordinate,
    CarbonIntensity Intensity,
    IReadOnlyList<GenerationMixEntry> GenerationMix);
