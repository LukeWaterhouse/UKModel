using UKModel.Api.Dtos.Energy.Enums;

namespace UKModel.Api.Dtos.Energy;

public record RegionDto(
    GridRegionTypeDto RegionType,
    string DnoRegion,
    IntensityDto Intensity,
    IReadOnlyList<GenerationMixEntryDto> GenerationMix);
