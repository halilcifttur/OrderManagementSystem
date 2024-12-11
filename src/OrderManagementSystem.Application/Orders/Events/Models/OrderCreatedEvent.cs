using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.Orders.Events.Models;

public class OrderCreatedEvent
{
    public Guid OrderId { get; init; }
    public string ProductName { get; init; }
    public decimal Price { get; set; }
    public OrderStatus Status { get; init; }
}