using Microsoft.AspNetCore.Mvc;
using UKModel.DbLoader.Elexon;

namespace UKModel.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController(
    IConfiguration configuration,
    FuelHalfHourLoader fuelHalfHourLoader) : ControllerBase
{
    [HttpPost("fuelhh/backfill")]
    public async Task<IActionResult> BackfillFuelHalfHour(
        [FromBody] BackfillRequest request,
        CancellationToken cancellationToken)
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

        await fuelHalfHourLoader.LoadAsync(from, to, cancellationToken);

        return Ok(new { message = $"Backfill complete from {from:yyyy-MM-dd} to {to:yyyy-MM-dd}" });
    }
}

public record BackfillRequest(string From, string To);
