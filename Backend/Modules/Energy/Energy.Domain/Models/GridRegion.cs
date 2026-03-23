namespace Energy.Domain.Models;

public record GridRegion(
    string DnoRegion, // Distribution Network Operators (companies that operate the electricity distribution network in the UK)
    GridRegionType Region,
    CarbonIntensity Intensity,
    IReadOnlyList<GenerationMixEntry> GenerationMix);
