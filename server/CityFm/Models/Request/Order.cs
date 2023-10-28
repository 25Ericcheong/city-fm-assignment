namespace CityFm.Models.Request;

public class Order
{
    public Customer Customer { get; set; }

    public OrderItem[] Orders { get; set; }
}