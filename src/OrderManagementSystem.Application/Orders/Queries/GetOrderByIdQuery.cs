using MediatR;
using OrderManagementSystem.Domain.Entitites;
using OrderManagementSystem.Domain.Exceptions;
using OrderManagementSystem.Domain.Repositories;

namespace OrderManagementSystem.Application.Orders.Queries;

public record GetOrderByIdQuery(Guid Id) : IRequest<Order>;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, Order>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.Id);
        return order ?? throw new EntityNotFoundException();
    }
}