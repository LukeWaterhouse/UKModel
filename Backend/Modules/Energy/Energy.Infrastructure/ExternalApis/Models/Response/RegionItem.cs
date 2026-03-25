namespace Energy.Infrastructure.Models.Response;

internal sealed record RegionItem(
    int RegionId,
    string DnoRegion,
    string ShortName,
    IntensityItem Intensity,
    List<GenerationMixItem> GenerationMix);
