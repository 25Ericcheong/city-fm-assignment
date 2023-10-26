using CityFm.Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityFm.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("/api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        Console.WriteLine("Api-triggered");

        var test = new Product
        {
            ProductId = "1",
            Name = "eric",
            Description = "test test",
            UnitPrice = 11.11,
            MaximumQuantity = null
        };

        return new JsonResult(test);
    }
}