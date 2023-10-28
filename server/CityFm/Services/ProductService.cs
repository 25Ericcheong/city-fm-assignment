using CityFm.Domain;
using CityFm.Models.Config;
using CityFm.Models.Static;
using CityFm.Models.Static.Http;
using Microsoft.Extensions.Options;

namespace CityFm.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly double VendorMultiple = 1.2;

    public ProductService(IOptions<AppSettings> options, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Product>?> GetProducts()
    {
        var httpClient = _httpClientFactory.CreateClient(ClientKeys.AllTheCloudsProduct);
        var products = await httpClient.GetFromJsonAsync<List<Product>>(ExternalUri.AllTheCloudsProducts);

        return products?.Select(product =>
        {
            product.UnitPrice *= VendorMultiple;

            return product;
        }).ToList();
    }
}