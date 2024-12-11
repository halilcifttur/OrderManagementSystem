using MediatR;
using OrderManagementSystem.Domain.Entitites;
using OrderManagementSystem.Domain.Exceptions;
using OrderManagementSystem.Domain.Repositories;

namespace OrderManagementSystem.Application.Orders.Queries;

public record GetOrdersQuery() : IRequest<List<Order>>;

public class GetOrdersQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrdersQuery, List<Order>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<List<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersAsync();
        if (orders == null || orders.Count <= 0)
        {
            throw new EntityNotFoundException();
        }

        return orders;
    }
}