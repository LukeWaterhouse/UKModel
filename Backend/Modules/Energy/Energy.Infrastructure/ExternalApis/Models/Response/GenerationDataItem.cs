namespace Energy.Infrastructure.ExternalApis.Models.Response;

internal sealed record GenerationDataItem(string From, string To, List<GenerationMixItem> GenerationMix);
