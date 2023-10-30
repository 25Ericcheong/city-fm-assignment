using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CityFm.Models.Request.Order;
using CityFm.Models.Static.Http;

namespace CityFm.Services;

public class OrdersService : IOrdersService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrdersService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HttpResponseMessage> SubmitOrder(Order order)
    {
        var orderJson = new StringContent(
            JsonSerializer.Serialize(order),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var httpClient = _httpClientFactory.CreateClient(ClientKeys.AllTheCloudsOrder);
        var response = await httpClient.PostAsync(ExternalUri.AllTheCloudsOrders, orderJson);

        response.EnsureSuccessStatusCode();
        return response;
    }
}