namespace CityFm.Services;

public class OrdersService : IOrdersService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrdersService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
}