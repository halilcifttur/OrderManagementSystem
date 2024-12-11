using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entitites;
using OrderManagementSystem.Domain.Repositories;

namespace OrderManagementSystem.Infrastructure.Data;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}