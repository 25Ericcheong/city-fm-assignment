using CityFm.Domain;

namespace CityFm.Services;

public interface IProductsService
{
    public Task<List<Product>?> GetProducts();
}