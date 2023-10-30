using System.Text.RegularExpressions;
using CityFm.Domain;
using CityFm.Exceptions;
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
    private const int MaximumProductQuantity = 2147483647;
    private const int MinimumProductQuantity = 0;
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

        if (products is null || products.Count == 0)
            throw new ExternalApiException("Product list is empty. Please retry.");

        var customerData = order.Customer;
        var ordersData = order.Orders;

        if (customerData is null) throw new ArgumentException("Customer data is missing");

        if (ordersData is null || ordersData.Length == 0) throw new ArgumentException("Order items are missing");

        ValidateCustomerData(customerData);
        ValidateProductOrdersData(ordersData, products);

        return new HttpResponseMessage();
    }

    public void ValidateProductOrdersData(OrderItemDTO[] ordersRequest, List<Product> products)
    {
        if (ordersRequest.Length == 0) throw new ArgumentException("There should be at least 1 order");

        var isQuantityBoundaryValid =
            ordersRequest.All(o => o.Quantity > MinimumProductQuantity && o.Quantity <= MaximumProductQuantity);

        if (!isQuantityBoundaryValid)
            throw new ArgumentException("The number of quantities of product is not within valid boundary quantity");

        var productIds = products.Select(p => p.ProductId);
        var doesProductExist = ordersRequest.All(o => productIds.Contains(o.ProductId));
        if (!doesProductExist) throw new ArgumentException("An invalid product ID has been provided.");

        foreach (var productOrder in ordersRequest)
        {
            var productInformation = products.First(p => p.ProductId == productOrder.ProductId);
            var isProductOrderCountOverMax = productOrder.Quantity > productInformation.MaximumQuantity;
            if (isProductOrderCountOverMax)
                throw new ArgumentException("Order quantity is over the available maximum limit of product available");
        }
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