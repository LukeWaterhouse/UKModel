using System.Net.Http.Json;
using System.Text.Json;
using Energy.Infrastructure.ExternalApis.Models;
using Energy.Infrastructure.ExternalApis.Models.Response;

namespace Energy.Infrastructure.ExternalApis.Clients;

public sealed class ElexonApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<ElexonFuelHalfHourResponse?> GetFuelHalfHourAsync(
        DateOnly from, DateOnly to, CancellationToken cancellationToken = default)
    {
        var url = $"datasets/FUELHH?format=json&settlementDateFrom={from:yyyy-MM-dd}&settlementDateTo={to:yyyy-MM-dd}";
        return await httpClient.GetFromJsonAsync<ElexonFuelHalfHourResponse>(url, JsonOptions, cancellationToken);
    }
}
