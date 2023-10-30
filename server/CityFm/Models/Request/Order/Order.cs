namespace CityFm.Models.Request.Order;

public class Order
{
    public string CustomerName { get; set; }

    public string CustomerEmail { get; set; }

    public OrderLineItem[] LineItems { get; set; }
}