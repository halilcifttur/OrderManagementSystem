using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderManagementSystem.Domain.Services;
using OrderManagementSystem.Infrastructure.Services;
using OrderManagementSystem.Infrastructure.Settings;

namespace OrderManagementSystem.Infrastructure.Configurations;

public static class ServiceConfiguration
{
    public static void AddServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitMqSettings>>().Value);
        services.AddTransient<IEventBusService, EventBusService>();
    }
}