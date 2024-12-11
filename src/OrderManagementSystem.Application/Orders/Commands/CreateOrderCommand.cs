using MediatR;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Entitites;
using OrderManagementSystem.Domain.Repositories;
using OrderManagementSystem.Domain.Services;
using OrderManagementSystem.Application.Orders.Events.Models;

namespace OrderManagementSystem.Application.Orders.Commands;

public record CreateOrderCommand(string ProductName, decimal Price) : IRequest<Guid>;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IEventBusService eventBus) : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IEventBusService _eventBus = eventBus;

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            ProductName = request.ProductName,
            Price = request.Price,
            Status = OrderStatus.Pending
        };

        await _orderRepository.CreateOrderAsync(order);

        await _eventBus.PublishAsync(new OrderCreatedEvent
        {
            OrderId = order.Id,
            ProductName = order.ProductName,
            Price = order.Price,
            Status = order.Status

        }, cancellationToken);

        return order.Id;
    }
}