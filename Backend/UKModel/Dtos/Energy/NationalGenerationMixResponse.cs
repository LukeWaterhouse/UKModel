namespace UKModel.Api.Dtos.Energy;

public record NationalGenerationMixResponse(
    DateTimeOffset From,
    DateTimeOffset To,
    IReadOnlyList<GenerationMixEntryDto> Mix);
