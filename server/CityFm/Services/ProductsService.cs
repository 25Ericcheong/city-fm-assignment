using CityFm.Domain;
using CityFm.Models.Static.Http;

namespace CityFm.Services;

public class ProductsService : IProductsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly double _vendorMultiplier = 1.2;

    public ProductsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Product>?> GetProducts()
    {
        var httpClient = _httpClientFactory.CreateClient(ClientKeys.AllTheCloudsProductFxRate);
        var products = await httpClient.GetFromJsonAsync<List<Product>>(ExternalUri.AllTheCloudsProducts);

        return products?.Select(product =>
        {
            product.UnitPrice *= _vendorMultiplier;

            return product;
        }).ToList();
    }
}