using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Infrastructure.Settings;

namespace OrderManagementSystem.Infrastructure.Configurations;

public static class SettingsConfiguration
{
    public static void AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));
    }
}