using Energy.Domain.Enums;

namespace Energy.Domain.Models;

public record GridRegion(
    DnoRegionType DnoRegion,
    GridRegionType Region,
    CarbonIntensity Intensity,
    IReadOnlyList<GenerationMixEntry> GenerationMix);
