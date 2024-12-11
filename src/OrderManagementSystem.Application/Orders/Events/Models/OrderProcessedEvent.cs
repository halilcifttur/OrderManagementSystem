using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.Orders.Events.Models;

public class OrderProcessedEvent
{
    public Guid OrderId { get; init; }
}