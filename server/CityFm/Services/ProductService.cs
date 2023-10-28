using CityFm.Domain;
using CityFm.Models.Config;
using CityFm.Models.Static.Http;
using Microsoft.Extensions.Options;

namespace CityFm.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AppSettings _settings;

    public ProductService(IOptions<AppSettings> options, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _settings = options.Value;
    }

    public async Task<List<Product>?> GetProducts()
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(ExternalUri.AllTheClouds);
        httpClient.DefaultRequestHeaders.Add("accept", ContentType.Json);
        httpClient.DefaultRequestHeaders.Add("api-key", _settings.AllTheClouds.ApiKey);

        return await httpClient.GetFromJsonAsync<List<Product>>(ExternalUri.AllTheCloudsProducts);
    }
}