using CityFm.Models.Request.Order;
using CityFm.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityFm.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("/api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IProductsService _productsService;

    public OrdersController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpPost]
    public async Task<HttpResponseMessage> CreateOrders([FromBody] OrderDTO order)
    {
        var products = await _productsService.GetProducts();
        return new HttpResponseMessage();
    }
}