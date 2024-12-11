using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Domain.Entitites;

public class Order
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public OrderStatus Status { get; set; }

    public Order()
    {
        Id = Guid.NewGuid();
    }
}