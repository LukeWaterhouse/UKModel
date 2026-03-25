namespace Energy.Infrastructure.Models.Response;

internal sealed record RegionalDataItem(string From, string To, List<RegionItem> Regions);
