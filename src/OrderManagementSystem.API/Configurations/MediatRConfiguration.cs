using MediatR;
using System.Reflection;

namespace OrderManagementSystem.API.Configurations;

public static class MediatRConfiguration
{
    public static void AddMediatRConfiguration(this IServiceCollection services)
    {
        var assemblies = new[] { Assembly.GetAssembly(typeof(Application.Orders.Commands.CreateOrderCommand)) }
                .Where(a => a != null)
                .Cast<Assembly>()
                .ToArray();

        services.AddScoped<IMediator, Mediator>();
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}
