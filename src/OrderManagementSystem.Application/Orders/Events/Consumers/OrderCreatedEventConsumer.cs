using MassTransit;
using OrderManagementSystem.Application.Orders.Events.Models;
using OrderManagementSystem.Domain.Entitites;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Repositories;
using OrderManagementSystem.Domain.Services;

namespace OrderManagementSystem.Application.Orders.Events.Consumers;

public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IOrderRepository _orderRepository;

    public OrderCreatedEventConsumer(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var order = new Order
        {
            Id = context.Message.OrderId,
            ProductName = context.Message.ProductName,
            Price = context.Message.Price,
            Status = OrderStatus.Processed
        };

        await _orderRepository.UpdateOrderAsync(order);
    }
}