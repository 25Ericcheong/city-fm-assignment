using CityFm.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityFm.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("/api/fx-rates")]
public class FxRatesController : ControllerBase
{
    private readonly IFxRatesService _fxRatesService;

    public FxRatesController(IFxRatesService fxRatesService)
    {
        _fxRatesService = fxRatesService;
    }

    [HttpGet]
    public async Task<JsonResult> GetFxRates()
    {
        var fxRates = await _fxRatesService.GetFxRates();
        return new JsonResult(fxRates);
    }
}