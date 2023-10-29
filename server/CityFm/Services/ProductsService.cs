using System.Text.Json;
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
        var response = await httpClient.GetAsync(ExternalUri.AllTheCloudsProducts);

        if (!response.IsSuccessStatusCode) return new List<Product>();

        await using var content = await response.Content.ReadAsStreamAsync();
        var products = await JsonSerializer.DeserializeAsync<List<Product>>(content);

        return products?.Select(product =>
        {
            product.UnitPrice *= _vendorMultiplier;

            return product;
        }).ToList();
    }
}