namespace UKModel.Api.Dtos.Energy;

public record NationalGenerationMixResponse(
    DateTimeOffset From,
    DateTimeOffset To,
    IntensityDto Intensity,
    IReadOnlyList<GenerationMixEntryDto> Mix);
