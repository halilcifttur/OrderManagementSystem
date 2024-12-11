using FluentValidation;
using OrderManagementSystem.Application.Orders.Commands;

namespace OrderManagementSystem.API.Validators.Orders;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name cannot be empty")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}
