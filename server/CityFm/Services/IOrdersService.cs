using CityFm.Models.Request.Order;

namespace CityFm.Services;

public interface IOrdersService
{
    public Task<HttpResponseMessage> SubmitOrder(Order order);
}