using FluentValidation.AspNetCore;
using FluentValidation;
using OrderManagementSystem.API.Validators.Orders;

namespace OrderManagementSystem.API.Configurations;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

        return services;
    }
}
