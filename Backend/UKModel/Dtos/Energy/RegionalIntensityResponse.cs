namespace UKModel.Api.Dtos.Energy;

public record RegionalIntensityResponse(
    DateTimeOffset RetrievedAt,
    IReadOnlyList<RegionDto> Regions);
