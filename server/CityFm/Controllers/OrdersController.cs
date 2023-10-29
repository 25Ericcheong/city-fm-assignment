using System.Text.RegularExpressions;
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
        if (order is null) throw new ArgumentNullException(null, "Entire order request data is missing");

        var products = await _productsService.GetProducts();

        var customerData = order.Customer;
        var ordersData = order.Orders;

        if (customerData is null) throw new ArgumentException("Customer data is missing");

        if (ordersData is null || ordersData.Length == 0) throw new ArgumentException("Order items are missing");

        ValidateCustomerData(customerData);


        return new HttpResponseMessage();
    }

    private void ValidateCustomerData(CustomerDTO customer)
    {
        if (customer is null) throw new ArgumentException("Customer data is missing");

        if (string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.Email))
            throw new ArgumentException("Customer name and email data is required");

        var regex = new Regex(
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        if (!regex.IsMatch(customer.Email)) throw new ArgumentException("Customer email is in invalid format");
    }
}