using CityFm.Domain;
using CityFm.Models.Static.Http;

namespace CityFm.Services;

public class FxRatesService : IFxRatesService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FxRatesService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<FxRate>?> GetFxRates()
    {
        var httpClient = _httpClientFactory.CreateClient(ClientKeys.AllTheCloudsProductFxRate);
        var response = await httpClient.GetAsync(ExternalUri.AllTheCloudsFxRates);

        if (!response.IsSuccessStatusCode) return new List<FxRate>();

        return await response.Content.ReadFromJsonAsync<List<FxRate>>();
    }
}