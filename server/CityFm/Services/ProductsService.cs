using CityFm.Domain;
using CityFm.Models.Static.Http;

namespace CityFm.Services;

public class ProductsService : IProductsService
{
    private const double VendorMultiplier = 1.2;
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Product>?> GetProducts()
    {
        var httpClient = _httpClientFactory.CreateClient(ClientKeys.AllTheCloudsProductFxRate);
        var response = await httpClient.GetAsync(ExternalUri.AllTheCloudsProducts);

        if (!response.IsSuccessStatusCode) return new List<Product>();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        return products?.Select(product =>
        {
            product.UnitPrice *= VendorMultiplier;

            return product;
        }).ToList();
    }
}