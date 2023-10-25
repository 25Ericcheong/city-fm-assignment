using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityFm.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("/api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [EnableCors("CorsPolicy")]
    public string Get()
    {
        Console.WriteLine("Api-triggered");
        return "somthing";
    }
}