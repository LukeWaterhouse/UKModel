namespace Energy.Infrastructure.ExternalApis.Models.Response;

internal record OverpassElement(string Type, long Id, OverpassCenter? Center, Dictionary<string, string>? Tags);
