namespace Energy.Domain.Models;

public record NationalGenerationMix(
    DateTimeOffset From,
    DateTimeOffset To,
    CarbonIntensity Intensity,
    IReadOnlyList<GenerationMixEntry> Mix);
