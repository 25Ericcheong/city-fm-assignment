using CityFm.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityFm.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Produces("application/json")]
[Route("/api/fx-rates")]
public class FxRatesController : ControllerBase
{
    private readonly IFxRatesService _fxRatesService;

    public FxRatesController(IFxRatesService fxRatesService)
    {
        _fxRatesService = fxRatesService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFxRates()
    {
        var fxRates = await _fxRatesService.GetFxRates();
        return Ok(fxRates);
    }
}