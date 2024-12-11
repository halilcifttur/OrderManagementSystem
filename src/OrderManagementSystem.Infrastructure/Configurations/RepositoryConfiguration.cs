using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Domain.Repositories;
using OrderManagementSystem.Infrastructure.Data;

namespace OrderManagementSystem.Infrastructure.Configurations;

public static class RepositoryConfiguration
{
    public static void AddRepositoryConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}