using System.Net.Mime;
using CityFm.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityFm.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Produces(MediaTypeNames.Application.Json)]
[Route("/api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productsService.GetProducts();
        return Ok(products);
    }
}