using MassTransit;
using OrderManagementSystem.Application.Orders.Events.Consumers;
using OrderManagementSystem.Infrastructure.Settings;

namespace OrderManagementSystem.API.Configurations;

public static class MassTransitConfiguration
{
    public static void AddMassTransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumer<OrderCreatedEventConsumer>();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                RabbitMqSettings settings = context.GetRequiredService<RabbitMqSettings>();

                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
    }
}
