using Energy.Domain.Models;
using Energy.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using UKModel.Api.Dtos.Energy;
using UKModel.Api.Mapping;

namespace UKModel.Api.Controllers;

[ApiController]
[Route("api/v1/energy")]
[Tags("Energy")]
public class EnergyController(
    IRegionalCarbonIntensityService regionalService,
    INationalGenerationMixService nationalService)
    : ControllerBase
{
    [HttpGet("regions")]
    public async Task<ActionResult<RegionalIntensityResponse>> GetAllRegions(CancellationToken cancellationToken)
    {
        var regions = await regionalService.GetAllRegionsAsync(cancellationToken);

        var response = new RegionalIntensityResponse(
            RetrievedAt: DateTimeOffset.UtcNow,
            Regions: regions.Select(r => r.FromDomain()).ToList());

        return Ok(response);
    }

    [HttpGet("generation")]
    public async Task<ActionResult<NationalGenerationMixResponse>> GetNationalGenerationMix(CancellationToken cancellationToken)
    {
        var mix = await nationalService.GetCurrentMixAsync(cancellationToken);

        var response = new NationalGenerationMixResponse(
            From: mix.From,
            To: mix.To,
            Mix: mix.Mix.Select(m => m.FromDomain()).ToList());

        return Ok(response);
    }
}