namespace Energy.Infrastructure.Models.Response;

internal record OverpassElement(string Type, long Id, OverpassCenter? Center, Dictionary<string, string>? Tags);
