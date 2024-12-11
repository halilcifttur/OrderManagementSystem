using OrderManagementSystem.Domain.Entitites;

namespace OrderManagementSystem.Domain.Repositories;

public interface IOrderRepository
{
    Task CreateOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<List<Order>> GetOrdersAsync();
    Task UpdateOrderAsync(Order order);
}