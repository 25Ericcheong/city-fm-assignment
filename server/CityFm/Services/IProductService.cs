using CityFm.Domain;

namespace CityFm.Services;

public interface IProductService
{
    public Task<List<Product>?> GetProducts();
}