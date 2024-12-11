using Hangfire;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Repositories;

namespace OrderManagementSystem.Worker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        RecurringJob.AddOrUpdate("update-processed-orders", () => UpdateProcessedOrders(), "*/1 * * * *");
        await Task.CompletedTask;
    }

    public async Task UpdateProcessedOrders()
    {
        using var scope = _serviceProvider.CreateScope();
        var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

        var orders = await orderRepository.GetOrdersAsync();
        var processedOrders = orders.Where(o => o.Status == OrderStatus.Processed).ToList();

        foreach (var order in processedOrders)
        {
            order.Status = OrderStatus.Completed;
            await orderRepository.UpdateOrderAsync(order);
        }
    }
}