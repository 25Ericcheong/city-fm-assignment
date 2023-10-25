using Microsoft.AspNetCore.Mvc;

namespace CityFm.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public void Get()
    {
        Console.WriteLine("Api-triggered");
    }
}