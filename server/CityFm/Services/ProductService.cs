using CityFm.Domain;
using CityFm.Models.Config;
using CityFm.Models.Static.Http;
using Microsoft.Extensions.Options;

namespace CityFm.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient, IOptions<AppSettings> options)
    {
        _httpClient = httpClient;
        var settings = options.Value;

        _httpClient.BaseAddress = new Uri(ExternalUri.AllTheClouds);
        _httpClient.DefaultRequestHeaders.Add("accept", ContentType.Json);
        _httpClient.DefaultRequestHeaders.Add("api-key", settings.AllTheClouds.ApiKey);
    }

    public async Task<List<Product>?> GetProducts()
    {
        return await _httpClient.GetFromJsonAsync<List<Product>>(ExternalUri.AllTheCloudsProducts);
    }
}