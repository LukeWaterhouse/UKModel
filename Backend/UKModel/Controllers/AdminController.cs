using Microsoft.AspNetCore.Mvc;
using UKModel.DbLoader.Elexon;

namespace UKModel.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController(
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory,
    ILogger<AdminController> logger) : ControllerBase
{
    [HttpPost("fuelhh/backfill")]
    public IActionResult BackfillFuelHalfHour([FromBody] BackfillRequest request)
    {
        var expectedKey = configuration["AdminApiKey"];
        if (string.IsNullOrEmpty(expectedKey))
            return StatusCode(503, "Admin API key not configured");

        if (!Request.Headers.TryGetValue("X-Admin-Key", out var providedKey) ||
            !string.Equals(expectedKey, providedKey, StringComparison.Ordinal))
            return Unauthorized();

        if (!DateOnly.TryParse(request.From, out var from) || !DateOnly.TryParse(request.To, out var to))
            return BadRequest("Invalid date format. Use yyyy-MM-dd.");

        if (from > to)
            return BadRequest("'from' must be before 'to'.");

        _ = Task.Run(async () =>
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var loader = scope.ServiceProvider.GetRequiredService<FuelHalfHourLoader>();
                logger.LogInformation("Background backfill started: {From} to {To}", from, to);
                await loader.LoadAsync(from, to, CancellationToken.None);
                logger.LogInformation("Background backfill completed: {From} to {To}", from, to);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Background backfill failed: {From} to {To}", from, to);
            }
        });

        return Accepted(new { message = $"Backfill queued from {from:yyyy-MM-dd} to {to:yyyy-MM-dd}. Check logs for progress." });
    }
}

public record BackfillRequest(string From, string To);
