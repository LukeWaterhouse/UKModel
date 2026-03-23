namespace Energy.Domain.Models;

public record GridRegion(
    DnoRegion DnoRegion,
    GridRegionType Region,
    CarbonIntensity Intensity,
    IReadOnlyList<GenerationMixEntry> GenerationMix);
