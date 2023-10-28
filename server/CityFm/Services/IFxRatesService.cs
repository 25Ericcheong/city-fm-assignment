using CityFm.Domain;

namespace CityFm.Services;

public interface IFxRatesService
{
    public Task<List<FxRate>?> GetFxRates();
}