namespace CityFm.Models.Request.Order;

public class Order
{
    public Customer Customer { get; set; }

    public OrderItem[] Orders { get; set; }
}