namespace Energy.Domain.Models;

public record NationalGenerationMix(
    DateTimeOffset From,
    DateTimeOffset To,
    IReadOnlyList<GenerationMixEntry> Mix);
