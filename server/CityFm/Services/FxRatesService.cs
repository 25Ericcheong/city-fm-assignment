using CityFm.Domain;
using CityFm.Models.Static.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityFm.Services;

public class FxRatesService : IFxRatesService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FxRatesService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<List<FxRate>?> GetFxRates()
    {
        var httpClient = _httpClientFactory.CreateClient(ClientKeys.AllTheCloudsProductFxRate);
        return await httpClient.GetFromJsonAsync<List<FxRate>>(ExternalUri.AllTheCloudsFxRates);
    }
}