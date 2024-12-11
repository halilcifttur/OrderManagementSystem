using Hangfire;
using Hangfire.MemoryStorage;

namespace OrderManagementSystem.Worker.Configurations;

public static class HangfireConfiguration
{
    public static IServiceCollection AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
        {
            config.UseMemoryStorage();
        });

        services.AddHangfireServer();

        return services;
    }
}