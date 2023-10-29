namespace CityFm.Models.Request.Order;

public class OrderDTO
{
    public CustomerDTO Customer { get; set; }

    public OrderItemDTO[] Orders { get; set; }
}