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
    INationalGenerationMixService nationalService,
    IPowerPlantService powerPlantService)
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

    [HttpGet("national")]
    public async Task<ActionResult<NationalGenerationMixResponse>> GetNationalGenerationMix(CancellationToken cancellationToken)
    {
        var mix = await nationalService.GetCurrentMixAsync(cancellationToken);

        var response = new NationalGenerationMixResponse(
            From: mix.From,
            To: mix.To,
            Intensity: mix.Intensity.FromDomain(),
            Mix: mix.Mix.Select(m => m.FromDomain()).ToList());

        return Ok(response);
    }

    [HttpGet("power-plants")]
    public async Task<ActionResult<PowerPlantsResponse>> GetPowerPlants(CancellationToken cancellationToken)
    {
        var plants = await powerPlantService.GetPlantsAsync(cancellationToken);

        var response = new PowerPlantsResponse(
            Plants: plants.Select(p => p.FromDomain()).ToList());

        return Ok(response);
    }
}