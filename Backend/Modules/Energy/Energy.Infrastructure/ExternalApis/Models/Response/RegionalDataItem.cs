namespace Energy.Infrastructure.ExternalApis.Models.Response;

internal sealed record RegionalDataItem(string From, string To, List<RegionItem> Regions);
