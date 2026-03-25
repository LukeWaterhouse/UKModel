using System.Net.Http.Json;
using System.Text.Json;
using Energy.Infrastructure.ExternalApis.Models.Response;

namespace Energy.Infrastructure.ExternalApis.Clients;

internal sealed class OverpassApiClient(HttpClient httpClient)
{
    private const string PowerPlantsQuery = """
        [out:json][timeout:120];
        area["ISO3166-1"="GB"]["boundary"="administrative"]->.uk;
        (
          nwr(area.uk)["power"="plant"]["plant:source"~"^(nuclear|wind|solar|gas|hydro|biomass|waste|coal|oil)$"];
        );
        out center tags;
        """;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<OverpassResponse?> QueryPowerPlantsAsync(
        CancellationToken cancellationToken = default)
    {
        var content = new FormUrlEncodedContent([new KeyValuePair<string, string>("data", PowerPlantsQuery)]);

        var response = await httpClient.PostAsync("interpreter", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<OverpassResponse>(JsonOptions, cancellationToken);
    }
}
